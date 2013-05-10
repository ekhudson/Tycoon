using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class TycoonInventory : MonoBehaviour
{
    public class TycoonInventorySlot
    {
        public TycoonItem Item;
        public int Quantity;
    }

    private int mInventorySlots = 10; //how many slots this inventory has
    private Dictionary<TycoonItem, List<TycoonInventorySlot>> mItemDictionary = new Dictionary<TycoonItem, List<TycoonInventorySlot>>();

    public Dictionary<TycoonItem, List<TycoonInventorySlot>> GetInventory
    {
        get
        {
            return mItemDictionary;
        }
    }

    public int GetNumberOfSlots
    {
        get
        {
            return mInventorySlots;
        }
    }

    public int InsertItem(TycoonItem item, int quantity)
    {

    }

    public bool CheckForSpace(TycoonItem item, int quantity)
    {
        if (mItemDictionary.ContainsKey(item))
        {
            int stacks = (mItemDictionary[item] + quantity) / item.StackSize;

        }
    }
}
