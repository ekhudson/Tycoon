using UnityEngine;
using System.Collections;

public class TycoonDiggingZone : UseableObject
{
    public Transform[] ObjectsToMove;
    public Transform[] ObjectsToScale;

    public override bool Activate(TycoonEntity activator)
    {
        base.Activate(activator);

        float yAmt = transform.collider.bounds.extents.y * 2;

        foreach(Transform obj in ObjectsToMove)
        {
            obj.position -= new Vector3(0, yAmt, 0);
        }

        foreach(Transform obj in ObjectsToScale)
        {
            float currentYScale = obj.localScale.y;

            obj.localScale += new Vector3(0, yAmt, 0);
            obj.transform.position += new Vector3(0, yAmt * 0.5f, 0);

            if (obj.renderer != null)
            {
                obj.renderer.material.mainTextureScale += new Vector2(0, 0.25f);
                //obj.renderer.material.mainTextureOffset += new Vector2(0, (obj.localScale.y / currentYScale) - 1);
            }
        }

        return true;
    }
}
