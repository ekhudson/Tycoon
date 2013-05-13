using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class TycoonHUD : MonoBehaviour
{
    public TycoonItem Shovel;
    private const float kTopBarHeight = 32f;

    private int shovelAmount = 0;

    public List<GameObject> StructureOptions = new List<GameObject>();

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

        GUILayout.Space(8);
        DrawBuildDebug();
        DrawInventoryDebug();

        GUILayout.FlexibleSpace();
    }

    private void DrawBuildDebug()
    {
        if (TycoonBuildManager.Instance.CurrentBuildOption != null)
        {
            if (GUILayout.Button("Clear"))
            {
                TycoonBuildManager.Instance.SetCurrentBuildOption(null);
            }

        }

        foreach(GameObject option in TycoonBuildManager.Instance.StructureOptions)
        {
            if (GUILayout.Button(option.name))
            {
                TycoonBuildManager.Instance.SetCurrentBuildOption(option);
            }
        }
    }

    private void DrawInventoryDebug()
    {
        if (TycoonPlayer.Instance.PlayerData.PlayerInventory.GetInventory == null)
        {
            return;
        }

        shovelAmount = System.Int32.Parse(GUILayout.TextField(shovelAmount.ToString()));

        if (GUILayout.Button("Add Shovels"))
        {
           shovelAmount = TycoonPlayer.Instance.PlayerData.PlayerInventory.InsertItem(Shovel, shovelAmount);
        }

        foreach(KeyValuePair<TycoonItem, List<TycoonInventory.TycoonInventorySlot>> item in TycoonPlayer.Instance.PlayerData.PlayerInventory.GetInventory)
        {
            foreach(TycoonInventory.TycoonInventorySlot slot in item.Value)
            {
                GUILayout.Box(slot.Item.name  + " " + slot.Quantity.ToString());
            }
        }
    }
}
