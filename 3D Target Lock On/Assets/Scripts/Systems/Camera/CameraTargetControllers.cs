using UnityEngine;
using UnityEngine.InputSystem;

public class CameraTargetControllers : MonoBehaviour {
    [SerializeField] CameraStrategyFollow followStrat;

    float yaw;
    private void Update() {
        // yaw += Mouse.current.delta.x.ReadValue() * followStrat.sensitivity;

        // // idk why the x and y are reversed
        // Vector3 targetRotation = new Vector3(0f, yaw, 0f);
        // transform.eulerAngles = targetRotation;
        // transform.position = Player.OBJECT_INSTANCE.transform.position - transform.forward * followStrat.offset;
    }
}