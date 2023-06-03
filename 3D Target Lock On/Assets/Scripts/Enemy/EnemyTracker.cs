using System.Collections;
using UnityEngine;

// This class keeps track of all enemies inside the scene
// without having to search for them.
// It is attached to each enemy object.
// 
// NOTE:    Its important that you delete an enemy from the
//          array after its death

public class EnemyTracker : MonoBehaviour{
    [Tooltip("Usually this object that contains the model and especially logic scripts")]
    [SerializeField] GameObject actualEnemyObject;
    [HideInInspector] public static ArrayList EnemiesInTheScene = new ArrayList();

    private void Start() {
        if(actualEnemyObject == null){
            throw new System.Exception("The <color=red>Actual Enemy Object</color> field, in the "
                +gameObject.name+" enemy object, is not set");
        }
        EnemiesInTheScene.Add(actualEnemyObject);
    }
}