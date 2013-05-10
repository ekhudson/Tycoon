using UnityEngine;
using UnityEditor;

using System.Collections;
using System.Collections.Generic;

using GrendelEditor.UI;

[CustomEditor(typeof(GrendelFolder))]
public class GrendelFolderEditor : GrendelEditor<GrendelFolder>
{
    private const float kLabelWidth = 256f;
    private const float kToggleWidth = 16f;
    private const float kIconWidth = 16f;

    [MenuItem("GameObject/Create Folder %#f", false, 0)]
    [MenuItem("GameObject/Create Other/Folder %#f", false)]
    public static void CreateFolder()
    {
        GameObject newFolder = new GameObject();
        newFolder.name = "New Folder";
        newFolder.AddComponent<GrendelFolder>();
        newFolder.transform.position = Vector3.zero;
        newFolder.transform.rotation = Quaternion.identity;
        newFolder.transform.localScale = new Vector3(1,1,1);

        if (Selection.activeTransform != null && Selection.activeTransform.parent != null)
        {
            newFolder.transform.parent = Selection.activeTransform.parent;
        }
    }

    [InitializeOnLoad]
    public class Initialize
    {
        static Initialize()
        {
           EditorApplication.hierarchyWindowItemOnGUI += HierarchyWindowItemOnGUI;
        }
    }

    public static void HierarchyWindowItemOnGUI(int instanceID, Rect selectionRect)
    {
        GrendelFolder folder = (EditorUtility.InstanceIDToObject(instanceID) as GameObject).GetComponent<GrendelFolder>();

        if (folder != null)
        {
            Color color = folder.FolderColor;

            if (GUIUtility.keyboardControl != 0 && Selection.activeObject == folder.gameObject)
            {
               color.a = 0.5f;
               EditorApplication.RepaintHierarchyWindow();

               GUI.color = color;

               GUI.DrawTexture(new Rect(selectionRect.x, selectionRect.y, kLabelWidth, selectionRect.height), folder.DefaultGradient, ScaleMode.StretchToFill, true);

               GUI.color = Color.white;
                return;
            }

            GUI.color = color;

            GUI.DrawTexture(new Rect(selectionRect.x, selectionRect.y, kLabelWidth, selectionRect.height), folder.DefaultGradient, ScaleMode.StretchToFill, true);

            if (folder.transform.parent == null || folder.transform.parent.gameObject.activeInHierarchy)
            {
                EditorGUI.BeginChangeCheck();

                folder.IsVisible = EditorGUI.Toggle(new Rect(selectionRect.x, selectionRect.y, kToggleWidth, selectionRect.height), folder.IsVisible);
    
                if (EditorGUI.EndChangeCheck())
                {
                    ToggleVisibilityOfChildren(folder);
                }
            }
            else
            {
                EditorGUI.LabelField(new Rect(selectionRect.x, selectionRect.y, kToggleWidth, selectionRect.height), "--");
            }

            if (folder.DefaultTexture != null)
            {
                GUI.DrawTexture(new Rect(selectionRect.x + kToggleWidth, selectionRect.y, kIconWidth, selectionRect.height), folder.DefaultTexture);
            }

            GUI.Label(new Rect(selectionRect.x + kIconWidth + kToggleWidth, selectionRect.y, selectionRect.width, selectionRect.height), folder.name);

            if (!folder.IsVisible)
            {
                color = Color.grey;
                color.a = 0.65f;
                GUI.color = color;
                GUI.DrawTexture(new Rect(selectionRect.x, selectionRect.y, kLabelWidth, selectionRect.height), folder.DefaultGradient, ScaleMode.StretchToFill, true);
            }

            GUI.color = Color.white;
        }
    }

    public override void OnInspectorGUI()
    {
        GUILayout.BeginHorizontal();
        GUILayout.Label("Folder Color: ");
        EditorGUI.BeginChangeCheck();
        Target.FolderColor = CustomEditorGUI.ColorGridLayout(8,1, 16, 2, GrendelFolder.FolderColors, Target.FolderColor);
        if (EditorGUI.EndChangeCheck())
        {
            EditorUtility.SetDirty(Target);
            EditorApplication.RepaintHierarchyWindow();
        }
        GUILayout.FlexibleSpace();
        GUILayout.EndHorizontal();
    }

    private static void ToggleVisibilityOfChildren(GrendelFolder folder)
    {
        foreach(Transform child in folder.transform)
        {
            //child.gameObject.SetActive(folder.IsVisible);
            //child.gameObject.hideFlags = HideFlags.HideInHierarchy; //folder.IsVisible;

            if (child.GetComponent<GrendelFolder>() != null)
            {
                child.GetComponent<GrendelFolder>().IsVisible = folder.IsVisible;
                ToggleVisibilityOfChildren(child.GetComponent<GrendelFolder>());
            }
        }
    }

}
