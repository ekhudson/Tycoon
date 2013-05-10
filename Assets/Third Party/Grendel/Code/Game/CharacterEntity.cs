using UnityEngine;
using System.Collections;

//A rigidbody based character controller for Grendel Entities
public class CharacterEntity : Entity
{
    public float SkinWidth = 0.01f;
    public float StepOffset = 0.35f;
    public float MinMoveAmount = 0.1f;
    private Vector3 mCurrentMove = Vector3.zero;
    private bool mIsGrounded = false;

    public bool IsGrounded
    {
        get
        {
            return mIsGrounded;
        }
    }

    protected override void Awake()
    {
        base.Awake();
        mRigidbody.isKinematic = false;
    }

    // Use this for initialization
    protected override void Start ()
    {
        base.Start();
    }
    
    // Update is called once per frame
    private void FixedUpdate ()
    {
        if (!mIsGrounded)
        {
            CheckIfGrounded();
        }

        mTransform.Translate(mCurrentMove, Space.World);

        mCurrentMove *= mRigidbody.drag;
    }

    public void Move(Vector3 amount)
    {
        mCurrentMove += amount;
    }

    public void CheckIfGrounded()
    {
        Ray ray = new Ray(mTransform.position, -mTransform.up);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, (mCollider.bounds.size.y * 0.5f) + SkinWidth))
        {
            mIsGrounded = true;
        }
    }

    public void OnCollisionExit(Collision collisionInfo)
    {
        mIsGrounded = false;
    }

    private void OnDrawGizmosSelected()
    {
        if(!Application.isPlaying)
        {
            return;
        }

        Debug.DrawLine(mTransform.position - new Vector3(0, mCollider.bounds.size.y * 0.5f, 0), mTransform.position - new Vector3(0, (mCollider.bounds.size.y * 0.5f) + SkinWidth, 0), Color.yellow);
    }
}
