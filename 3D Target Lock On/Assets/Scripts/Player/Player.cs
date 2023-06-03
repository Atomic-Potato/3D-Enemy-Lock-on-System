using UnityEngine;

public class Player : MonoBehaviour{
    protected static GameObject objectInstance;

    public static GameObject OBJECT_INSTANCE{
        get{
            return objectInstance;
        }
    }

    private void Awake() {
        objectInstance = gameObject;
    }
}