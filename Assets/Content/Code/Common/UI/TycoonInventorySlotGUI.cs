using UnityEngine;
using System.Collections;

public class TycoonInventorySlotGUI
{
    private const int kSlotSize = 64;
    private static GUIStyle mLabelStyle;

    public static TycoonInventory.TycoonInventorySlot CurrentActiveSlot = null;

    public static void DrawSlotLayout(TycoonInventory.TycoonInventorySlot slot)
    {
        if (mLabelStyle == null)
        {
            mLabelStyle = new GUIStyle(GUI.skin.label);
        }

        GUILayout.BeginVertical(GUILayout.Width(kSlotSize));

        Rect rect = GUILayoutUtility.GetRect(kSlotSize, kSlotSize, new GUILayoutOption[]{GUILayout.Width(kSlotSize), GUILayout.Height(kSlotSize)});

        if (slot == null)
        {
            GUI.Box(rect, string.Empty, TycoonHUD.Instance.TycoonHUDSkin.customStyles[1]);
            GUILayout.EndVertical();
            return;
        }

        if (CurrentActiveSlot == slot)
        {
            GUI.color = GrendelColor.CustomAlpha(Color.white, 0.65f);
        }

        if(GUI.Toggle(rect, CurrentActiveSlot == slot, slot.Item.Icon, TycoonHUD.Instance.TycoonHUDSkin.customStyles[0]))
        {
            CurrentActiveSlot = slot;
        }

        GUI.color = Color.white;

        if (slot.Quantity > 1)
        {
            mLabelStyle.alignment = TextAnchor.UpperRight;
            GUI.Label(rect, slot.Quantity.ToString(), mLabelStyle);
        }

        mLabelStyle.alignment = TextAnchor.LowerCenter;
        GUI.Label(rect, slot.Item.Name, mLabelStyle);
        //GUILayout.Label(item.name);

        GUILayout.EndVertical();
    }

}
