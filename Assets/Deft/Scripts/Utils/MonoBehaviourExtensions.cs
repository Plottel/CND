using UnityEngine;

namespace Deft
{
    public static class MonoBehaviourExtensions
    {
        public static T Find<T>(this MonoBehaviour b, string name) where T : Object
            => TransformExtensions.Find<T>(b.transform, name);
    }
}
