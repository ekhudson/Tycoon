using UnityEngine;
using UnityEditor;

using System.Collections;
using System.Collections.Generic;
using System.IO;

[CustomEditor(typeof(GrendelProjectData))]
public class GrendelProjectDataEditor : GrendelEditor<GrendelProjectData>
{
    private const float kDeleteButtonWidth = 48f;

    public static GrendelProjectData CreateProjectDataAsset()
    {
        GrendelProjectData asset = (GrendelProjectData)ScriptableObject.CreateInstance(typeof(GrendelProjectData));  //scriptable object
        if (!Directory.Exists(Path.Combine(Application.dataPath,"Resources\\Grendel\\")))
        {
            Directory.CreateDirectory(Path.Combine(Application.dataPath,"Resources\\Grendel\\"));
        }

        AssetDatabase.CreateAsset(asset, string.Format("Assets/Resources/Grendel/{0}.asset", "Grendel Project Data"));
        AssetDatabase.SaveAssets();
        EditorUtility.FocusProjectWindow();
        Selection.activeObject = asset;

        return asset;
    }

    public override void OnInspectorGUI()
    {
        SerializedObject serializedObject = new SerializedObject(Target);

        base.OnInspectorGUI();

        EditorGUI.BeginChangeCheck();

        GrendelAudioOptionsEditor.StaticOnInspectorGUI(Target.AudioOptions);

        if(EditorGUI.EndChangeCheck())
        {
            EditorUtility.SetDirty(Target);
            serializedObject.ApplyModifiedProperties();
        }
    }

    public void DrawAudioOptions()
    {
//        mToggleAudioOptions = GUILayout.Toggle(mToggleAudioOptions, "Audio Options", EditorStyles.toolbarButton);
//
//        if (!mToggleAudioOptions)
//        {
//           return;
//        }
//
//        EditorGUI.indentLevel++;
//
//        GUILayout.BeginVertical();
//
//            GUILayout.Label("Audio Channels: ");
//
//            EditorGUI.indentLevel++;
//
//            for (int i = 0; i < Target.AudioOptions.AudioChannels.Count; i++)
//            {
//                GUILayout.BeginHorizontal();
//
//                Target.AudioOptions.AudioChannels[i].ChannelName = EditorGUILayout.TextField("Channel Name", Target.AudioOptions.AudioChannels[i].ChannelName);
//
//                if (GUILayout.Button(new GUIContent("X", "Delete Channel"), GUILayout.Width(kDeleteButtonWidth)))
//                {
//                    if (!EditorUtility.DisplayDialog("Delete Audio Channel", string.Format("Are you sure you want to delete the {0} Audio Channel?", Target.AudioOptions.AudioChannels[i].ChannelName), "Delete", "Cancel"))
//                    {
//                        break;
//                    }
//
//                    Target.AudioOptions.AudioChannels.RemoveAt(i);
//                    GUILayout.EndHorizontal();
//                    break;
//                }
//
//                GUILayout.EndHorizontal();
//            }
//
//            GUILayout.BeginHorizontal();
//
//            GUILayout.FlexibleSpace();
//
//            if (GUILayout.Button("Add New Channel", GUILayout.ExpandWidth(false)))
//            {
//                Target.AudioOptions.AudioChannels.Add(new GrendelAudioChannel());
//            }
//
//            GUILayout.EndHorizontal();
//
//        GUILayout.EndVertical();
//
//        EditorGUI.indentLevel--;
//
//        EditorGUILayout.Space();
//
//        GUILayout.Label("Audio Banks: ");
//
//        EditorGUI.indentLevel++;
//
//        for (int j = 0; j < Target.AudioOptions.AudioBanks.Count; j++)
//        {
//            EditorGUILayout.Space();
//
//            EditorGUI.indentLevel--;
//
//            GUILayout.BeginVertical(EditorStyles.textField);
//
//            EditorGUILayout.Space();
//
//            GUILayout.BeginHorizontal();
//
//            Target.AudioOptions.AudioBanks[j].BankName = EditorGUILayout.TextField("Bank Name", Target.AudioOptions.AudioBanks[j].BankName);
//
//            if (GUILayout.Button(new GUIContent("X", "Delete Audio Bank"), GUILayout.Width(kDeleteButtonWidth)))
//            {
//                Target.AudioOptions.AudioBanks.RemoveAt(j);
//                GUILayout.EndHorizontal();
//                break;
//            }
//
//            GUILayout.EndHorizontal();
//
//            EditorGUI.indentLevel++;
//
//            GUILayout.BeginHorizontal();
//
//            EditorGUILayout.LabelField("Bank Assets Location", string.Empty, EditorStyles.wordWrappedMiniLabel);
//
//            if (GUILayout.Button(Target.AudioOptions.AudioBanks[j].LocationOfBankAssets))
//            {
//                Selection.activeObject = AssetDatabase.LoadAssetAtPath( FileUtil.GetProjectRelativePath(Target.AudioOptions.AudioBanks[j].LocationOfBankAssets), typeof( Object ));
//            }
//
//            GUILayout.FlexibleSpace();
//
//            if (GUILayout.Button(new GUIContent("...", "Select Bank Assets Location"), GUILayout.Width(kDeleteButtonWidth)))
//            {
//                if (!string.IsNullOrEmpty(Target.AudioOptions.AudioBanks[j].LocationOfBankAssets))
//                {
//                    if(!EditorUtility.DisplayDialog("Replace Audio Bank", "This Audio Bank already has an established location and list of assets. Changing this location will replace the current bank and erase all current audio setting for this bank. Are you sure you want to continue?", "Continue", "Cancel"))
//                    {
//                        return;
//                    }
//                }
//
//                Target.AudioOptions.AudioBanks[j].LocationOfBankAssets = EditorUtility.OpenFolderPanel("Choose Audio Bank Assets Location", Target.AudioOptions.AudioBanks[j].LocationOfBankAssets, string.Empty);
//                UpdateAudioBank(Target.AudioOptions.AudioBanks[j]);
//            }
//
//            GUILayout.EndHorizontal();
//
//            Target.AudioOptions.AudioBanks[j].BankOpenInEditor = EditorGUILayout.Foldout(Target.AudioOptions.AudioBanks[j].BankOpenInEditor, "Audio Bank");
//
//            if (Target.AudioOptions.AudioBanks[j].BankOpenInEditor)
//            {
//                for(int k = 0; k < Target.AudioOptions.AudioBanks[j].AudioClips.Count; k++)
//                {
//                    if (Target.AudioOptions.AudioBanks[j].AudioClips[k].Clip == null)
//                    {
//                        //continue;
//                    }
//
//                    GUILayout.BeginHorizontal();
//
//                        EditorGUILayout.LabelField(string.Format("Clip # {0}", k.ToString()), GUILayout.Width(96));
//
//                        EditorGUILayout.Space();
//
//                        AdjustableAudioClipEditor.StaticOnInspectorGUI(Target.AudioOptions.AudioBanks[j].AudioClips[k]);
//
//                    GUILayout.EndHorizontal();
//                }
//            }
//
//            EditorGUILayout.Space();
//            EditorGUILayout.Space();
//
//            GUILayout.EndVertical();
//
//            EditorGUILayout.Space();
//        }
//
//        EditorGUILayout.Space();
//
//        GUILayout.BeginHorizontal();
//
//        GUILayout.FlexibleSpace();
//
//        if (GUILayout.Button("Add New Audio Bank"))
//        {
//            Target.AudioOptions.AudioBanks.Add(new GrendelAudioBank());
//        }
//
//        GUILayout.EndHorizontal();
//
//        EditorGUI.indentLevel--;
//        EditorGUI.indentLevel--;
//
//        if (GrendelAudioOptions.PreviewAudioSource != null && GrendelAudioOptions.PreviewAudioSource.isPlaying)
//        {
//            Repaint();
//        }
    }

//    public void UpdateAudioBank(GrendelAudioBank bank)
//    {
//        bank.AudioClips.Clear();
//
//        FindFiles.FileSystemEnumerator fileSystemEnumerator = new FindFiles.FileSystemEnumerator(bank.LocationOfBankAssets, GrendelAudioOptions.AcceptedAudioFileTypes, true);
//
//        foreach(FileInfo info in fileSystemEnumerator.Matches())
//        {
//            AdjustableAudioClip adjustableClip = new AdjustableAudioClip();
//
//            Object obj;
//
//            try
//            {
//                Debug.Log("Looking at: " + info.FullName + " Project Path: " + FileUtil.GetProjectRelativePath( info.ToString() ));
//                obj = AssetDatabase.LoadAssetAtPath( FileUtil.GetProjectRelativePath( info.ToString() ) , typeof(AudioClip));
//            }
//            catch
//            {
//                Debug.LogError(string.Format("No clip found at location: {0}", info.FullName));
//                continue;
//            }
//
//            if (obj != null && obj.GetType() == typeof(AudioClip))
//            {
//                adjustableClip.Clip = obj as AudioClip;
//                bank.AudioClips.Add(adjustableClip);
//            }
//            else
//            {
//                continue;
//            }
//
//        }
//    }
}
