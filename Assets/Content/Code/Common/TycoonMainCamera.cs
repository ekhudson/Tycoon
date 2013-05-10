using UnityEngine;
using System.Collections;

public class TycoonMainCamera : Singleton<TycoonMainCamera>
{
    public float FollowSpeed = 1.0f;
    public Vector3 FollowDistance = new Vector3(0, 15, -50);
    public float DistanceToMouse = 0.5f;

    private Transform mTransform;
    private Vector3 mCurrentDistance = Vector3.zero;

    private void Start()
    {
        mTransform = transform;
    }

    private void FixedUpdate()
    {
        Vector3 mouseDist = Vector3.zero;

        mouseDist = Camera.main.ScreenToWorldPoint(Input.mousePosition + new Vector3(0,0, mTransform.position.z)) - mTransform.position;

        Vector3 mouseWeightOffset = Vector3.zero;

        mouseWeightOffset = mouseDist * DistanceToMouse;


        mTransform.position = Vector3.Lerp(mTransform.position, TycoonPlayer.Instance.transform.position + FollowDistance + mouseWeightOffset, Time.deltaTime * FollowSpeed);
    }

}
