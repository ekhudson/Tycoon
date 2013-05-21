using UnityEngine;
using System.Collections;

public class TycoonCollectableItem : MouseInteractableObject
{
    public TycoonItem ItemData;

    public override void OnUseComplete()
    {
        if(TycoonPlayer.Instance.PlayerData.PlayerInventory.InsertItem(ItemData, 1) == 0)
        {
            Destroy(gameObject);
        }
    }
}
