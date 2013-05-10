using UnityEngine;
using System.Collections;

public class TycoonHUD : MonoBehaviour
{
    private const float kTopBarHeight = 32f;


    private void OnGUI()
    {
        GUILayout.BeginHorizontal(GUI.skin.box, new GUILayoutOption[]{ GUILayout.Height(kTopBarHeight), GUILayout.ExpandWidth(true) });

            DrawTopBar();

        GUILayout.EndHorizontal();
    }

    private void DrawTopBar()
    {
        GUILayout.Label(string.Format("Bank: {0}", TycoonPlayer.Instance.PlayerData.BankAmount.ToString("c2")));
        GUILayout.Label(string.Format("Time: {0}", EnvironmentManager.Instance.CurrentTime.ToString()));

        GUILayout.FlexibleSpace();
    }
}
