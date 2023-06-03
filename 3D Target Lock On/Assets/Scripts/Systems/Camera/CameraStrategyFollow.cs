using UnityEngine;
using UnityEngine.InputSystem;

public class CameraStrategyFollow : MonoBehaviour, CameraStrategy{

    [Tooltip("X: horizontal offset\nY: vertical offset\nZ: depth offset")]
    [SerializeField] Vector3 offset = new Vector3(2f, 1f, 3f);
    [Tooltip("X: min\nY: max")]
    [SerializeField] Vector2 pitchMinMax = new Vector2(-45f, 85f);
    [Range(0f, 1f)]
    [SerializeField] float mouseSensitivity = 0.3f;
    [Tooltip("How much time it takes for the camera to be set on the player position after the camera behavior is switched")]
    [Range(0f, 5f)]
    [SerializeField] float transitionTime = 0.35f;
    [Tooltip("How fast the camera will reset its rotation after the camera behavior is switched")]
    [Range(0f, 10f)]
    [SerializeField] float rotationResetSpeed = 5f;

    [Space]
    [SerializeField] Transform rotationTarget;

    float targetYaw;
    float cameraPitch;    
    float distanceToPlayer;
    bool snappedToPlayer;
    Vector3 velocityCache;

    float refYawSmoothingSpeed;
    float refPitchSmoothingSpeed;
    Vector3 refSmoothVelocity;
    
    #region EXECUTION
    private void Start() {
        rotationTarget.position = new Vector3(rotationTarget.position.x, offset.y, rotationTarget.position.z);
    }

    public void Execute(){
        if(!snappedToPlayer){
            SnapToPlayer();
            return;
        }

        RotateAroundPlayer();
    }
    #endregion

    void SnapToPlayer(){
        Vector3 position = rotationTarget.position;

        if(Vector3.Distance(transform.position, position) > offset.y){
            transform.position = Vector3.SmoothDamp(transform.position, position, ref velocityCache, transitionTime);
            return;
        }
        
        snappedToPlayer = true;
        distanceToPlayer = Vector3.Distance(transform.position, position);
    }

    void RotateAroundPlayer(){
        // NOTE: The yaw and pitch are flipped for some reason
        targetYaw += Mouse.current.delta.x.ReadValue() * mouseSensitivity;
        cameraPitch -= Mouse.current.delta.y.ReadValue() * mouseSensitivity;
        cameraPitch = Mathf.Clamp(cameraPitch, pitchMinMax.x, pitchMinMax.y);

        PositionCamera();
        ApplyYawOnTarget();
        ApplyPitchOnCamera();
        
        /// <summary>
        /// Places the camera at a 90 degree angle
        /// from the vector going from the target to the player
        /// </summary>
        void PositionCamera(){
            var dir = (rotationTarget.position - Player.OBJECT_INSTANCE.transform.position).normalized;
            Quaternion rotation = Quaternion.Euler(0f, 90f, 0f); 
            dir =  rotation * dir;
            // transform.position = rotationTarget.position + dir  - transform.forward * offset.z * 2f;
            transform.position =  Vector3.SmoothDamp(
                transform.position, 
                rotationTarget.position + dir  - transform.forward * offset.z * 2f,
                ref refSmoothVelocity,
                transitionTime) ;
        }
        
        /// <summary>
        /// Yaws the target around the player object
        /// </summary>
        void ApplyYawOnTarget(){
            Vector3 targetRotation = new Vector3(0f, targetYaw);
            rotationTarget.eulerAngles = targetRotation;
            rotationTarget.position = Player.OBJECT_INSTANCE.transform.position - rotationTarget.forward * offset.x;
        }

        /// <summary>
        /// Pitches the camera around the target object
        /// </summary>
        void ApplyPitchOnCamera(){
            transform.LookAt(rotationTarget);
            Vector3 targetRotation = new Vector3(cameraPitch, transform.eulerAngles.y, transform.eulerAngles.z);
            transform.eulerAngles =  targetRotation;
        }
    }
}