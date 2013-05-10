using UnityEngine;
using UnityEditor;

using System.Collections;

[CustomEditor(typeof(TycoonItem))]
public class TycoonItemEditor : GrendelEditor<TycoonItem>
{
     [MenuItem("Assets/Create/Create Item %#i")]
     public static void CreateAsset ()
     {
         ScriptableObjectUtility.CreateAsset<TycoonItem> ();
     }

}
