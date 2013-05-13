using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEditor;

using GrendelEditor.UI;

[CustomEditor(typeof(TycoonUserInput), true)]
public class UserInputEditor : GrendelEditor<TycoonUserInput>
{
    private const float kLabelWidth = 64f;
    private const float kButtonWidth = 128f;
    private const float kButtonHeight = 32f;

    private void OnEnable()
    {
        Target.GatherKeyBindings(Target.GetType());
    }

    public override void OnInspectorGUI()
	{
        Undo.SetSnapshotTarget(Target, "User Input Change");

        DrawDefaultInspector();

        bool changed = false;

        foreach(GrendelKeyBinding binding in Target.KeyBindings)
        {
            Undo.CreateSnapshot();
            changed = false;

            GUILayout.BeginVertical(GUI.skin.box);

                string name = binding.BindingName;
                name = name.Replace(" ", string.Empty);

                EditorGUI.indentLevel++;

                GUILayout.Label(name, EditorStyles.boldLabel); //TODO: Replace with something that reflects the variables actual name

                binding.BindingName = EditorGUILayout.TextField("Binding Name: ", binding.BindingName);

                GUILayout.FlexibleSpace();

                GUILayout.BeginHorizontal();

                    GUILayout.FlexibleSpace();

                    changed = CustomEditorGUI.KeyBindButtonLayout(kButtonWidth, kButtonHeight, binding, false);

                    GUILayout.FlexibleSpace();

                GUILayout.EndHorizontal();

                GUILayout.FlexibleSpace();
    
                    GUILayout.BeginHorizontal();

                     EditorGUI.BeginChangeCheck();

                        if (binding.MouseButton == GrendelKeyBinding.MouseButtons.None)
                        {
                            GUI.color = Color.grey;
                        }

                        EditorGUILayout.PrefixLabel("Mouse:");
                        binding.MouseButton = (GrendelKeyBinding.MouseButtons)EditorGUILayout.EnumPopup(binding.MouseButton);

                         GUI.color = Color.white;

                        GUILayout.FlexibleSpace();

                        if (binding.AltMouseButton == GrendelKeyBinding.MouseButtons.None)
                        {
                            GUI.color = Color.grey;
                        }

                        EditorGUILayout.PrefixLabel("Alt Mouse:");
                        binding.AltMouseButton = (GrendelKeyBinding.MouseButtons)EditorGUILayout.EnumPopup( binding.AltMouseButton);

                         GUI.color = Color.white;

                    if(EditorGUI.EndChangeCheck())
                    {
                        changed = true;
                    }
    
                    GUILayout.EndHorizontal();

            EditorGUI.indentLevel--;

            GUILayout.EndVertical();

            if (changed)
            {
                Undo.RegisterSnapshot();
                EditorUtility.SetDirty(Target);
            }

        }
    }
}

