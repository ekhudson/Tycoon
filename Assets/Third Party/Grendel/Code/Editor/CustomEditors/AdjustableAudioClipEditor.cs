using UnityEngine;
using UnityEditor;

using System.Collections;

[CustomEditor(typeof(AdjustableAudioClip))]
public class AdjustableAudioClipEditor : GrendelEditor<AdjustableAudioClip>
{
    public const float kPlayButtonWidth = 192f;
    public const float kAudioSpectrumMinHeight = 64f;
    public const float kAudioSpectrumMaxHeight = 128f;

    public static void StaticOnInspectorGUI(AdjustableAudioClip target)
    {
        GUILayout.BeginHorizontal(EditorStyles.textField);

        GUILayout.BeginVertical(GUI.skin.box, GUILayout.Width(kPlayButtonWidth));

            EditorGUILayout.Space();

            if (GrendelAudioOptions.PreviewAudioSource != null &&
                GrendelAudioOptions.PreviewAudioSource.isPlaying &&
                GrendelAudioOptions.PreviewAudioSource.clip == target.Clip)
            {
                GUI.color = Color.green;
            }

            if(GUILayout.Button(string.Format("{0} {1}",target.Clip.name, GUI.color == Color.green ? "\u25A0" : "\u25BA"), GUILayout.Width(kPlayButtonWidth)))
            {
                GrendelAudioOptions.PlayAudioClipPreview(target);
            }

            Rect buttonRect = GUILayoutUtility.GetLastRect();

            GUI.color = Color.white;

        EditorGUILayout.Space();

        target.AttributesExpanded = GUILayout.Toggle(target.AttributesExpanded, target.AttributesExpanded ? "Hide Clip Attributes" : "Show Clip Attributes", EditorStyles.toolbarButton, GUILayout.ExpandWidth(false));

        GUILayout.BeginVertical(GUILayout.Width(kPlayButtonWidth));

        if (target.AttributesExpanded)
        {
            EditorGUILayout.Space();

            target.RandomPitch = GUILayout.Toggle(target.RandomPitch, "Random Pitch", GUILayout.ExpandWidth(false));

            if (target.RandomPitch)
            {
                target.PitchMin = Mathf.Clamp(EditorGUILayout.FloatField("Pitch Min", target.PitchMin, GUILayout.ExpandWidth(false)), 0.0f, Mathf.Infinity);
                target.PitchMax = Mathf.Clamp(EditorGUILayout.FloatField("Pitch Max", target.PitchMax, GUILayout.ExpandWidth(false)), 0.0f, Mathf.Infinity);
            }
            else
            {
                target.Pitch = Mathf.Clamp(EditorGUILayout.FloatField("Pitch", target.Pitch, GUILayout.ExpandWidth(false)), 0.0f, Mathf.Infinity);
            }
        }

        GUILayout.EndVertical();

        EditorGUILayout.Space();

        GUILayout.EndVertical();

        GUILayout.BeginVertical(GUI.skin.box);

        GUILayout.Box( string.Empty, EditorStyles.textField, new GUILayoutOption[]{
                       GUILayout.Height(target.AttributesExpanded ? kAudioSpectrumMaxHeight : kAudioSpectrumMinHeight),
                       GUILayout.ExpandWidth(true)});

        Rect boxRect = GUILayoutUtility.GetLastRect();

        GUILayout.EndVertical();

        GUI.DrawTexture(boxRect, AssetPreview.GetAssetPreview(target.Clip), ScaleMode.StretchToFill);

        if (GrendelAudioOptions.PreviewAudioSource != null &&
                GrendelAudioOptions.PreviewAudioSource.isPlaying &&
                GrendelAudioOptions.PreviewAudioSource.clip == target.Clip)
        {
            boxRect.width = boxRect.width * (GrendelAudioOptions.PreviewAudioSource.time / target.Clip.length);

            GUI.color = GrendelColor.CustomAlpha(Color.green, 0.25f);
            GUI.Box(boxRect, string.Empty);
            GUI.color = Color.white;
        }

        GUILayout.EndHorizontal();


    }

}
