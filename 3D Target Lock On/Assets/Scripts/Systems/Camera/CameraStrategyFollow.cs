using UnityEngine;
using UnityEngine.InputSystem;

public class CameraStrategyFollow : MonoBehaviour, CameraStrategy{

    [SerializeField] Vector2 pitchMinMax = new Vector2(-45f, 85f);
    [Range(0f, 1f)]
    [SerializeField] float mouseSensitivity = 0.3f;
    [Tooltip("Camera offset from the player position")]
    [SerializeField] float offsetDistance;
    [Space]
    [Tooltip("How much time it takes for the camera to be set on the player position after the camera behavior is switched")]
    [Range(0f, 5f)]
    [SerializeField] float transitionTime = 0.35f;
    [Tooltip("How fast the camera will reset its rotation after the camera behavior is switched")]
    [Range(0f, 10f)]
    [SerializeField] float rotationResetSpeed = 5f;

    [Space]
    [SerializeField] Transform rotationTarget;

    float yaw;
    float pitch;    
    float distanceToPlayer;
    bool snappedToPlayer;
    Vector3 velocityCache;

    public void Execute(){
        if(!snappedToPlayer){
            SnapToPlayer();
            return;
        }

        RotateAroundPlayer();
    }

    void SnapToPlayer(){
        Vector3 position = GetPlayerPosition();

        if(Vector3.Distance(transform.position, position) > offsetDistance){
            transform.position = Vector3.SmoothDamp(transform.position, position, ref velocityCache, transitionTime);
            return;
        }
        
        snappedToPlayer = true;
        distanceToPlayer = Vector3.Distance(transform.position, position);
        
        Vector3 GetPlayerPosition(){
            if(Player.OBJECT_INSTANCE == null){
                Debug.Log($"<color=yellow>Camera Controller:</color> <color=red>NO PLAYER FOUND IN THE SCENE.</color>");
                return Vector3.zero;
            }
            return Player.OBJECT_INSTANCE.transform.position;
        }
    }

    void RotateAroundPlayer(){
        yaw += Mouse.current.delta.x.ReadValue() * mouseSensitivity;
        pitch -= Mouse.current.delta.y.ReadValue() * mouseSensitivity;
        pitch = Mathf.Clamp(pitch, pitchMinMax.x, pitchMinMax.y);

        // idk why the x and y are reversed
        Vector3 targetRotation = new Vector3(pitch, yaw);
        transform.eulerAngles = targetRotation;
        transform.position = rotationTarget.position - transform.forward * offsetDistance;
    }
}