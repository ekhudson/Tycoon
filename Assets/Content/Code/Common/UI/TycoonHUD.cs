using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class TycoonHUD : MonoBehaviour
{
    public TycoonItem Shovel;
    private const float kTopBarHeight = 32f;
    private const int kInventoryColumnAmount = 5;

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

        int columnCount = 0;
        int slotCount = 0;

        GUILayout.BeginVertical();

        GUILayout.BeginHorizontal();

        foreach(KeyValuePair<TycoonItem, List<TycoonInventory.TycoonInventorySlot>> item in TycoonPlayer.Instance.PlayerData.PlayerInventory.GetInventory)
        {
            foreach(TycoonInventory.TycoonInventorySlot slot in item.Value)
            {
                //GUILayout.Box(new GUIContent(string.Format("{0} x {1}", slot.Item.Name, slot.Quantity.ToString()), slot.Item.Icon));
                TycoonInventorySlotGUI.DrawSlotLayout(slot);
                columnCount++;
                slotCount++;

                if (columnCount % kInventoryColumnAmount == 0)
                {
                    GUILayout.EndHorizontal();
                    GUILayout.BeginHorizontal();
                }
            }
        }

        for(int i = 0; i < (TycoonPlayer.Instance.PlayerData.PlayerInventory.InventorySlots - slotCount); i++)
        {
            TycoonInventorySlotGUI.DrawSlotLayout(null);
            columnCount++;

            if (columnCount % kInventoryColumnAmount == 0)
            {
                GUILayout.EndHorizontal();
                GUILayout.BeginHorizontal();
            }
        }

        GUILayout.FlexibleSpace();

        GUILayout.EndHorizontal();

        GUILayout.EndVertical();

        Rect inventoryRect = GUILayoutUtility.GetLastRect();

        if (TycoonInventorySlotGUI.CurrentActiveSlot != null)
        {
            GUIStyle style = new GUIStyle(GUI.skin.label);

            GUILayout.BeginVertical(GUI.skin.box, new GUILayoutOption[]{GUILayout.Width(256), GUILayout.Height(256)});

            GUILayout.BeginHorizontal();
            GUILayout.FlexibleSpace();
            GUILayout.Box(TycoonInventorySlotGUI.CurrentActiveSlot.Item.Icon, new GUILayoutOption[]{GUILayout.Width(128), GUILayout.Height(128)});
            GUILayout.FlexibleSpace();
            GUILayout.EndHorizontal();

            style.alignment = TextAnchor.MiddleCenter;
            GUILayout.Label(TycoonInventorySlotGUI.CurrentActiveSlot.Item.Name, style);

            style.alignment = TextAnchor.MiddleLeft;
            GUILayout.Label(TycoonInventorySlotGUI.CurrentActiveSlot.Item.Description, style);

            GUILayout.EndVertical();
        }

        if (Input.GetMouseButtonDown(0) && !inventoryRect.Contains(Input.mousePosition))
        {
            TycoonInventorySlotGUI.CurrentActiveSlot = null;
        }
    }
}
