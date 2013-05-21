using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[System.Serializable]
public class TycoonInventory
{
    public class TycoonInventorySlot
    {
        public TycoonItem Item;
        public int Quantity;

        public int AddQuantity(int quantity)
        {
            int roomAvailable = Item.StackSize - Quantity;
            Quantity += (quantity <= roomAvailable ? quantity : roomAvailable);

            //return the rejected amount
            return (quantity <= roomAvailable ? 0 : quantity - roomAvailable);
        }
    }

    public int InventorySlots = 10; //how many slots this inventory has
    private Dictionary<TycoonItem, List<TycoonInventorySlot>> mItemDictionary = new Dictionary<TycoonItem, List<TycoonInventorySlot>>();

    public Dictionary<TycoonItem, List<TycoonInventorySlot>> GetInventory
    {
        get
        {
            return mItemDictionary;
        }
    }

    public int GetMaxNumberOfSlots
    {
        get
        {
            return InventorySlots;
        }
    }

    public int GetNumberOfActiveSlots
    {
        get
        {
            int num = 0;

            foreach(KeyValuePair<TycoonItem,List<TycoonInventorySlot>> item in mItemDictionary)
            {
                num += item.Value.Count;
            }

            return num;
        }
    }

    public int InsertItem(TycoonItem item, int quantity)
    {
        List<TycoonInventorySlot> slotList = new List<TycoonInventorySlot>();

        if (mItemDictionary.ContainsKey(item))
        {
           slotList = mItemDictionary[item];
        }
        else
        {
            mItemDictionary.Add(item, new List<TycoonInventorySlot>());
            slotList = mItemDictionary[item];
        }

        foreach(TycoonInventorySlot slot in slotList)
        {
            quantity = slot.AddQuantity(quantity);
        }

        while(quantity > 0 && GetNumberOfActiveSlots < GetMaxNumberOfSlots)
        {
            TycoonInventorySlot newSlot = new TycoonInventorySlot();
            slotList.Add(newSlot);
            newSlot.Item = item;
            quantity = newSlot.AddQuantity(quantity);
        }

        return quantity;
    }

    public bool CheckForSpace(TycoonItem item, int quantity)
    {
        if (mItemDictionary.ContainsKey(item))
        {
            //int stacks = (mItemDictionary[item].Quantity + quantity) / item.StackSize;
        }

        return true;
    }
}
