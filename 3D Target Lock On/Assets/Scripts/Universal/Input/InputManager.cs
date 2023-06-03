using UnityEngine;

public class InputManager : MonoBehaviour{
    static PlayerControlsMaps controlsMaps;

    public static PlayerControlsMaps PlayerInput{
        get{
            return controlsMaps;
        }
    }

    public static Vector2 PlayerMove{
        get{
            return controlsMaps.GroundedMovement.Move.ReadValue<Vector2>();
        }
    }

    void Awake() {
        controlsMaps = new PlayerControlsMaps();    
    }

    void OnEnable() {
        controlsMaps.Enable();
    }

    void OnDisable() {
        controlsMaps.Disable();    
    }
}
