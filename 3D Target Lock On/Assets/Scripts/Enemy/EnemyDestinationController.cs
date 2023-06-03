using UnityEngine;
using UnityEngine.AI;

public class EnemyDestinationController : MonoBehaviour {
    [Tooltip("How close can the enemy get to the border")]
    [SerializeField] float borderOffset;

    [Space]
    [SerializeField] EnemyGuardArea guardArea;
    [SerializeField] NavMeshAgent navMeshAgent;
    [SerializeField] LayerMask obstaclesLayer;
    [SerializeField] BoxCollider collider;

    bool destinationReached;
    private void Update() {
        destinationReached = transform.position.NearPointHorizontal(navMeshAgent.destination);

        if(destinationReached){
            SetNextGuardPoint();
            destinationReached = false;
        }
    }

    void SetNextGuardPoint(){
        Vector3 point;
        bool validPoint;
        do{
            point = GetPointWithinBounds();
            validPoint = ValidatePoint();
        }while(!validPoint);
        

        navMeshAgent.destination = point;

        Vector3 GetPointWithinBounds(){
            return new Vector3(
            Random.Range(guardArea.transform.position.x - (guardArea.radius - borderOffset),  
                guardArea.transform.position.x + (guardArea.radius - borderOffset)), 
            transform.position.y,
            Random.Range(guardArea.transform.position.z - (guardArea.radius - borderOffset),  
                guardArea.transform.position.z + (guardArea.radius - borderOffset)));
        }

        /// <summary>
        /// Checks if the point is placed inside any obstacle
        /// </summary>
        /// <returns>true if the point is valid, false if inside a collider</returns>
        bool ValidatePoint(){
            Collider[] colliderDetected = Physics.OverlapBox(point, collider.size/2f, Quaternion.identity, obstaclesLayer);
            if(colliderDetected.Length > 0)
                return false;
            return true;
        }
    }

}
