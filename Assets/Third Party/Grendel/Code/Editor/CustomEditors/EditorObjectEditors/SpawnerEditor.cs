using System.Collections;

using UnityEditor;
using UnityEngine;

using GrendelEditor.UI;

[CustomEditor(typeof(Spawner))]
[CanEditMultipleObjects] 
public class SpawnerEditor : EditorObjectEditor<Spawner> 
{	
	
	protected override void OnEnable()
	{		
		base.OnEnable();	
	}
	
	public override void OnInspectorGUI()
	{
		base.OnInspectorGUI();								
	}
	
	protected override void OnSceneGUI()
	{
		base.OnSceneGUI();		
		
	}
	
}

