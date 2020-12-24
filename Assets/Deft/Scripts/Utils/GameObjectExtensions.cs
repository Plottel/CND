using UnityEngine;

public static class GameObjectExtensions
{
    public static bool GetComponent<T>(this GameObject go, out T component) where T : MonoBehaviour
    {
        component = go.GetComponent<T>();
        return component != null;
    }
}
