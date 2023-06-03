using System.Collections;
using UnityEngine;

// NOTE:    Make sure this script is attached to the top most parent object
//          otherwise unity goes all freaky even if i transfrom the position 
//          to world coordinates 

public class EnemyLockOn : MonoBehaviour{
    #region INSPECTOR VARIABLES
    [Range(0f, 360f)]
    [SerializeField] float lockOnAngle;
    [Tooltip("Values less than 0 means the max lock distance is infinite")]
    [Range(-1f, 100f)]
    [SerializeField] float maxLockOnDistance;
    [Tooltip("The \"Max LockOn Distance\" will not be used, instead every enemy visible can be locked onto. " +
             "Note that even if the enemy is in range and its not visible, then it will be detected. " + 
             "(Requires a slightly higher computational power)")]
    [SerializeField] bool setMaxToVisible;
    [Tooltip("Zooms out the camer to fit both the player and the locked enemy on the screen")]

    [Space]
    [Header("REQUIRED COMPONENTS")]
    [SerializeField] Rigidbody rigidbody;

    [Space]
    [Header("DEBUGGING")]
    [SerializeField] bool debug;
    [SerializeField] bool drawLockOnAngle;
    #endregion

    #region INPUT VRIABLES
    bool lockOnIsHeld;
    #endregion

    #region STATIC VARIABLES
    static GameObject lockedEnemy;
    #endregion

    #region PRIVATE VARIABLES
    ArrayList enemiesInLockOnRange = new ArrayList();
    #endregion

    #region GETTERS & SETTERS
    public static GameObject LOCKED_ENEMY{
        get{
            return lockedEnemy;
        }
    }
    #endregion

    #region EXECUTION
    void Start(){
        InputManager.PlayerInput.GroundedMovement.Lock.performed += _ => lockOnIsHeld = true;
        InputManager.PlayerInput.GroundedMovement.Lock.canceled += _ => lockOnIsHeld = false;
    }

    void Update(){
        if(!lockOnIsHeld){
            RemoveAllEnemiesInRange();
            lockedEnemy = null;
            return;
        }

        enemiesInLockOnRange = GetEnemiesInRange();
        if(lockedEnemy == null){
            lockedEnemy = GetClosestEnemyInRange();
        }
        else{
            if(!EnemyIsWithinLockOnAngle(lockedEnemy, GetAngleWithPlayerDirection(lockedEnemy)))
                lockedEnemy = GetClosestEnemyInRange();
            else if(LockOnNextInputReceived())
                lockedEnemy = GetNextEnemyInRange(lockedEnemy);
            else if(LockOnPreviousInputReceived())
                lockedEnemy = GetPreviousEnemyInRange(lockedEnemy);
        }

        
    }

    void OnDrawGizmos(){
        if(!debug)
            return;

        DrawLockOnAngle();
    }
    #endregion

    #region GETTING ENEMIES IN RANGE
    ArrayList GetEnemiesInRange(){
        ArrayList enemiesInRange = new ArrayList();

        foreach(GameObject enemy in EnemyTracker.EnemiesInTheScene){
            if(setMaxToVisible){
                if(!EnemyIsVisible(enemy))
                    continue;
            }
            else if(!EnemyIsWithinMaxDistance(enemy))
                continue;

            float angle = GetAngleWithPlayerDirection(enemy);
            if(EnemyIsWithinLockOnAngle(enemy, angle))
                enemiesInRange.Add(enemy);
        }

        return enemiesInRange;
    }

    float GetAngleWithPlayerDirection(GameObject enemy){
        Vector2 enemyDirection = GetEnemyDirection(enemy);
        Vector2 playerMovementForward = new Vector2(rigidbody.velocity.normalized.x, rigidbody.velocity.normalized.z);
        return Vector2.Angle(playerMovementForward, enemyDirection);
    }

    bool EnemyIsWithinMaxDistance(GameObject enemy){
        if(maxLockOnDistance < 0) // Distacne is infinite
            return true;

        float distanceToPlayer = Vector3.Distance(enemy.transform.position, transform.TransformPoint(transform.position));
        if(distanceToPlayer <= maxLockOnDistance)
            return true;
        
        return false;
    }

    bool EnemyIsWithinLockOnAngle(GameObject enemy, float angle){
        // Note:    We divide the lockOnAngle by 2 because this angle 
        //          is facing the forward of the player
        if(angle < lockOnAngle/2f)
            return true;
        return false;
    }

    bool EnemyIsVisible(GameObject enemy){
        // Refer to: https://docs.unity3d.com/ScriptReference/GeometryUtility.TestPlanesAABB.html
        Plane[] planes = GeometryUtility.CalculateFrustumPlanes(Camera.main);
        Collider enemyCollider = enemy.GetComponentInChildren<Collider>();

        return GeometryUtility.TestPlanesAABB(planes, enemyCollider.bounds);
    }

    Vector2 GetEnemyDirection(GameObject enemy){
        // We're only gonna consider the horizontal plane 
        Vector2 enemyPos = new Vector2(enemy.transform.position.x, enemy.transform.position.z);
        Vector2 playerPos = new Vector2(transform.position.x, transform.position.z);
        Vector2 direction = enemyPos - playerPos;
        direction.Normalize();
        return direction;
    }
    #endregion

    #region SELECTING LOCKED ENEMY
    GameObject GetClosestEnemyInRange(){
        GameObject closestEnemy = null;
        float leastDistance = -1f;

        foreach(GameObject enemy in enemiesInLockOnRange){
            if(closestEnemy == null){
                closestEnemy = enemy;
                leastDistance = Vector3.Distance(transform.position, enemy.transform.position);
                continue;
            }

            if(enemy == closestEnemy)
                continue;

            float distance = Vector3.Distance(transform.position, enemy.transform.position);

            if(distance < leastDistance){
                closestEnemy = enemy;
                leastDistance = distance;
            }
        }

        return closestEnemy;
    }

    GameObject GetNextEnemyInRange(GameObject current){
        if(enemiesInLockOnRange.Count == 0)
            return null;
        if(enemiesInLockOnRange.Count == 1)
            return current;
        if(current == null)
            return GetClosestEnemyInRange();
        
        int currentIndex = enemiesInLockOnRange.IndexOf(current);
        return (GameObject)enemiesInLockOnRange[(currentIndex+1)%enemiesInLockOnRange.Count];
    }
    
    GameObject GetPreviousEnemyInRange(GameObject current){
        if(enemiesInLockOnRange.Count == 0)
            return null;
        if(enemiesInLockOnRange.Count == 1)
            return current;
        if(current == null)
            return GetClosestEnemyInRange();
        
        int currentIndex = enemiesInLockOnRange.IndexOf(current);
        int nextIndex = DecreaseIndex(currentIndex, enemiesInLockOnRange.Count);
        return (GameObject)enemiesInLockOnRange[nextIndex];
    }

    int DecreaseIndex(int current, int length){
        int decreased = current-1;
        decreased %= length;
        // For some reason the modulo of a negative number doesnt return a positive number
        // I.E. :
        // -1 % 4 equals -1 and not 3
        if(decreased < 0)
            decreased += length;
        return decreased;
    }
    #endregion

    #region REMOVING LOCKED ENEMIES
    void RemoveAllEnemiesInRange(){
        if(enemiesInLockOnRange.Count != 0)
            enemiesInLockOnRange.Clear();
    }
    #endregion

    #region INPUT
    bool LockOnNextInputReceived(){
        if(InputManager.PlayerLockSwitch > 0f) 
            return true;
        return false;
    }

    bool LockOnPreviousInputReceived(){
        if(InputManager.PlayerLockSwitch < 0f) 
            return true;
        return false;
    }
    #endregion

    #region DEBUGGING
    void DrawLockOnAngle(){
        if(!drawLockOnAngle)
            return;

        Vector3 startPoint = transform.position;
        float armLength = maxLockOnDistance < 0 ? 100f : maxLockOnDistance;

        Vector3 dir = rigidbody.velocity.normalized;

        Vector3 arm1EndPoint = startPoint + Quaternion.AngleAxis(lockOnAngle / 2, transform.up) * dir * armLength;
        Vector3 arm2EndPoint = startPoint + Quaternion.AngleAxis(-lockOnAngle / 2, transform.up) * dir * armLength;

        Debug.DrawRay(startPoint, arm1EndPoint - startPoint, Color.red);
        Debug.DrawRay(startPoint, arm2EndPoint - startPoint, Color.red);
        Debug.DrawRay(transform.position, dir * armLength, Color.green);
    }
    #endregion
}