using UnityEngine;
using System.Collections;

public class TycoonLadderVolume : UseableObject
{
    public override bool Activate(TycoonEntity activator)
    {
        //base.Activate(activator);

        Vector3 pos = TycoonPlayer.Instance.transform.position;
        pos.z = collider.bounds.center.z;

        //if the player is below the ladder, let's give him a boost up
        if ( (TycoonPlayer.Instance.collider.bounds.center.y - TycoonPlayer.Instance.collider.bounds.extents.y) < collider.bounds.center.y - collider.bounds.extents.y)
        {
            pos.y = (collider.bounds.center.y - collider.bounds.extents.y) + (TycoonPlayer.Instance.collider.bounds.extents.y) + 0.25f;
        }

        TycoonPlayer.Instance.transform.position = pos;
        TycoonPlayer.Instance.StartClimbing(gameObject.collider);

        return false;
    }
}
