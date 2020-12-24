using UnityEngine;

namespace Deft
{
    public static class TransformExtensions
    {
        public static T Find<T>(this Transform transform, string name) where T : Object
        {
            foreach (Transform child in transform)
            {
                if (child.name == name)
                    return child.GetComponent<T>();
                else
                {
                    T found = child.Find<T>(name);
                    if (found != null)
                        return found;
                }
            }

            return null;
        }
    }
}
