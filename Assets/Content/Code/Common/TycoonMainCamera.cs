using UnityEngine;
using System.Collections;

public class TycoonMainCamera : Singleton<TycoonMainCamera>
{
    public float MinFollowSpeed = 1.0f;
    public float MaxFollowSpeed = 10.0f;
    public Vector3 MaxFollowDistance = new Vector3(0, 15, -50);
    public Vector3 MinFollowDistance = new Vector3(0, 2, -50);
    public float DistanceToMouse = 0.5f;

    private Transform mTransform;
    private Vector3 mCurrentDistance = Vector3.zero;

    public float MaxDepth = -128f;
    public float MaxOrtho = 25;
    public float MinOrtho = 1;

    private Vector3 mCurrentFollowDistance;
    private float mCurrentFollowSpeed;

    private void Start()
    {
        mTransform = transform;
        mCurrentFollowDistance = MaxFollowDistance;
        mCurrentFollowSpeed = MinFollowSpeed;
    }

    private void FixedUpdate()
    {
        Vector3 mouseDist = Vector3.zero;

        mouseDist = Camera.main.ScreenToWorldPoint(Input.mousePosition + new Vector3(0,0, mTransform.position.z)) - mTransform.position;

        Vector3 mouseWeightOffset = Vector3.zero;

        mouseWeightOffset = (mouseDist * DistanceToMouse);

        mTransform.position = Vector3.Lerp(mTransform.position, TycoonPlayer.Instance.transform.position + mCurrentFollowDistance + mouseWeightOffset, Time.deltaTime * mCurrentFollowSpeed);

        if (TycoonPlayer.Instance.transform.position.y < 0)
        {
            camera.orthographicSize = Mathf.Lerp(MaxOrtho, MinOrtho, Mathf.Clamp(TycoonPlayer.Instance.transform.position.y / MaxDepth, 0f, 1f));
            mCurrentFollowDistance = Vector3.Lerp(MaxFollowDistance, MinFollowDistance, Mathf.Clamp(TycoonPlayer.Instance.transform.position.y / MaxDepth, 0f, 1f));
            mCurrentFollowSpeed = Mathf.Lerp(MinFollowSpeed, MaxFollowSpeed, Mathf.Clamp(TycoonPlayer.Instance.transform.position.y / MaxDepth, 0f, 1f));
        }
        else
        {
            camera.orthographicSize = MaxOrtho;
            mCurrentFollowDistance = MaxFollowDistance;
            mCurrentFollowSpeed = MinFollowSpeed;
        }
    }

}
