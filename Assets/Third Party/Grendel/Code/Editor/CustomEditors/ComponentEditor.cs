using UnityEngine;
using UnityEditor;

using System.Collections;

[CustomEditor(typeof(Component), true)]
public class ComponentEditor : GrendelEditor<Component>
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
    }
}
