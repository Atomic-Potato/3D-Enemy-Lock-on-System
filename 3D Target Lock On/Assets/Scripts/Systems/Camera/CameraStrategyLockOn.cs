using UnityEngine;

[RequireComponent(typeof(Camera))]
public class CameraStrategyLockOn : MonoBehaviour, CameraStrategy{

    [Tooltip("The offset from the center point between player and locked enemy")]
    [SerializeField] Vector2 centerPointOffset;
    [Tooltip("Determines how far is the camera from the center point between player and locked enemy")]
    [Range(1f, 5f)]
    [SerializeField] float distanceToCenterModifier = 1.25f;
    [Tooltip("The limit of how close the camera can get to the center point")]
    [Range(0f, 100f)]
    [SerializeField] float minDistanceToCenter = 10f;

    [Space]
    [Tooltip("How much time the camera needs to take to move when the locked enemy is switched")]
    [Range(0f, 100f)]
    [SerializeField] float switchTime = 1f;
    [Tooltip("How fast the camera will look at a new locked enemy")]
    [Range(0f,100f)]
    [SerializeField] float lookAtSpeed = 2f;

    [Space]
    [Header("Debugging")]
    [SerializeField] GameObject centerPoint;

    GameObject oldTarget = null; 
    bool isNewTarget = false;

    Vector3 refVelocity;

    public void Execute(){
        if(Player.OBJECT_INSTANCE == null)
            return;

        if(EnemyLockOn.LOCKED_ENEMY == null){
            oldTarget = null;
            isNewTarget = true;
        }

        if(oldTarget == null || EnemyLockOn.LOCKED_ENEMY != oldTarget){
            isNewTarget = true;
        }

        SetPosition();
        Rotate();
    }

    #region POSITION
    void SetPosition(){
        float dist = GetDistanceBetweenPlayerAndTarget(); 
        
        Vector3 offset = dist/1.25f > minDistanceToCenter ? 
            new Vector3(centerPointOffset.x, centerPointOffset.y, -dist/1.25f) :
            new Vector3(centerPointOffset.x, centerPointOffset.y, -minDistanceToCenter);

        Vector3 position = GetCenterPoint() + offset;

        if(!isNewTarget){
            transform.position = position;
            return;
        }


        transform.position = Vector3.SmoothDamp(transform.position, position,ref refVelocity, switchTime);
        
        float remainingDistance = Vector3.Distance(transform.position, position);
        if(remainingDistance < 0.1f){
            isNewTarget = false;
            oldTarget = EnemyLockOn.LOCKED_ENEMY;
        }
    }

    Vector3 GetCenterPoint(){
        if(EnemyLockOn.LOCKED_ENEMY == null)
            return Player.OBJECT_INSTANCE.transform.position;
        
        Bounds bounds = new Bounds(Player.OBJECT_INSTANCE.transform.position, Vector3.zero);
        bounds.Encapsulate(EnemyLockOn.LOCKED_ENEMY.transform.position);

        // DEBUGGING
        if(centerPoint != null){
            centerPoint.transform.position = bounds.center;
            Debug.DrawLine(transform.position, centerPoint.transform.position, Color.yellow);
        }

        return bounds.center;   
    }
    #endregion

    void Rotate(){
        Vector3 center = GetCenterPoint();
        var targetRotation = Quaternion.LookRotation(center - transform.position);
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, lookAtSpeed * Time.deltaTime);
    }

    float GetDistanceBetweenPlayerAndTarget(){
        if(EnemyLockOn.LOCKED_ENEMY == null || Player.OBJECT_INSTANCE == null)
            return 0f;
            
        return Vector3.Distance(Player.OBJECT_INSTANCE.transform.position, EnemyLockOn.LOCKED_ENEMY.transform.position);
    }
}