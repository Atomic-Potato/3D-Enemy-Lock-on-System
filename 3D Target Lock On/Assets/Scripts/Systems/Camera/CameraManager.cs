using UnityEngine;

public class CameraManager : MonoBehaviour{
    #region STRATEGIES
    [Header("Strategies")]
    [SerializeField] CameraStrategyFollow cameraFollow;
    [SerializeField] CameraStrategyLockOn cameraLockOn;
    #endregion

    CameraStrategy strategy;

    #region EXECUTION
    private void Start() {
        strategy = cameraFollow;

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    private void LateUpdate() {
        if(strategy != null)
            strategy.Execute();
    }

    private void Update(){

        if(EnemyLockOn.LOCKED_ENEMY != null){
            strategy = cameraLockOn;
            return;
        }
        
        strategy = cameraFollow;
    }
    #endregion
}
