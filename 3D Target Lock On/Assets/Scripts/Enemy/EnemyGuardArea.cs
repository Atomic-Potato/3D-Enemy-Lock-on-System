using UnityEngine;

public class EnemyGuardArea : MonoBehaviour{
    [SerializeField] float areaRadius;
    public float radius{
        get{
            return areaRadius;
        }
    }

    private void OnDrawGizmos() {

        Debug.DrawLine(transform.position, transform.position + new Vector3(areaRadius, 0f, 0f), Color.green);
        Debug.DrawLine(transform.position, transform.position - new Vector3(areaRadius, 0f, 0f), Color.red);
    }
}
