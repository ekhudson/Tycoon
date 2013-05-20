using UnityEngine;
using System.Collections;

public class MouseInteractableObject : UseableObject
{
    public bool HighlightOnMouseOver = true;
    public Material HighlightMaterial;

    private Material mOriginalMaterial;


    public void OnMouseEnter()
    {
        if (mOriginalMaterial != renderer.sharedMaterial)
        {
            mOriginalMaterial = renderer.sharedMaterial;
        }

        renderer.material = HighlightMaterial;

    }

    public void OnMouseExit()
    {
        renderer.material = mOriginalMaterial;
    }

    public void OnMouseDown()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Activate(TycoonPlayer.Instance.GetEntity);
        }
    }

    public override void OnUseComplete()
    {
        TycoonPlayer.Instance.PlayerData.BankAmount += 0.25f;
        Destroy(gameObject);
    }

}
