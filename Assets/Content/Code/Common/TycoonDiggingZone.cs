using UnityEngine;
using System.Collections;

public class TycoonDiggingZone : UseableObject
{
    public Transform[] ObjectsToMove;
    public Transform[] ObjectsToScale;

    public GameObject[] RockPrefabs;
    public int RubbleMinAmount = 6;
    public int RubbleMaxAmount = 9;
    public float RubbleMinSize = 0.15f;
    public float RubbleMaxSize = 0.45f;

    public override bool Activate(TycoonEntity activator)
    {
        base.Activate(activator);

        return true;
    }

    public override void OnUseComplete()
    {
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
            }
        }

        int rubbleAmount = Random.Range(RubbleMinAmount, RubbleMaxAmount);


        for(int i = 0; i <= rubbleAmount; i++)
        {
            float scale = Random.Range(RubbleMinSize, RubbleMaxSize);
            Vector3 position = new Vector3(Random.Range( (renderer.bounds.center.x - renderer.bounds.extents.x) + scale, (renderer.bounds.center.x + renderer.bounds.extents.x) - scale),
                                           Random.Range(renderer.bounds.center.y + scale, renderer.bounds.center.y + renderer.bounds.extents.y),
                                           renderer.bounds.center.z);

            GameObject rubble = GameObject.Instantiate(RockPrefabs[Random.Range(0, RockPrefabs.Length)], position, Quaternion.identity) as GameObject;
            rubble.transform.localScale = new Vector3(scale,scale,scale);
        }

    }
}
