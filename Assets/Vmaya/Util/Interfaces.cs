using UnityEngine;
using UnityEngine.Events;

namespace Vmaya.Util
{
    public class ComponentAttribute : PropertyAttribute { }
    public class HitObjectAttribute : PropertyAttribute { }
    public class SelectableAttribute : PropertyAttribute { }
    public class OnEnabled : UnityEvent<UnityEngine.Object, bool> { };
}