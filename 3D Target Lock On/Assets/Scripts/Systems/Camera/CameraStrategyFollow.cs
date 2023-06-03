using UnityEngine;

public class CameraStrategyFollow : MonoBehaviour, CameraStrategy{
    [Tooltip("Camera offset from the player position")]
    [SerializeField] Vector3 offset;
    [Space]
    [Tooltip("How much time it takes for the camera to be set on the player position after the camera behavior is switched")]
    [Range(0f, 5f)]
    [SerializeField] float transitionTime = 0.35f;
    [Tooltip("How fast the camera will reset its rotation after the camera behavior is switched")]
    [Range(0f, 10f)]
    [SerializeField] float rotationResetSpeed = 5f;
    
    Quaternion rotation;
    Vector3 velocityCache;


    void Start(){
        rotation = transform.rotation;
    }

    public void Execute(){

        SetCameraToPosition(GetPlayerPosition() + offset);
        ResetCameraRotation();
    }

    void SetCameraToPosition(Vector3 position){
        if(Vector3.Distance(transform.position, position) > 0.1f){
            transform.position = Vector3.SmoothDamp(transform.position, position, ref velocityCache, transitionTime);
            return;
        }
    }

    void ResetCameraRotation(){
        if(transform.rotation != rotation)
            transform.rotation = Quaternion.Slerp(transform.rotation, rotation, rotationResetSpeed * Time.deltaTime);
    }

    Vector3 GetPlayerPosition(){
        if(Player.OBJECT_INSTANCE == null){
            Debug.Log($"<color=yellow>Camera Controller:</color> <color=red>NO PLAYER FOUND IN THE SCENE.</color>");
            return Vector3.zero;
        }
        return Player.OBJECT_INSTANCE.transform.position;
    }
}