using UnityEngine;

public static class Extensions  {
    public static bool NearPointHorizontal(this Vector3 currentPosition, Vector3 destination){
        return Mathf.Abs(destination.x - currentPosition.x) < 1f; 
    }
}