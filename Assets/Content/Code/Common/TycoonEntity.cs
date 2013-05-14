using UnityEngine;
using System.Collections;

public class TycoonEntity : CharacterEntity
{
    public enum TycoonEntityStates
    {
        IDLE,
        MOVING,
        CLIMBING,
        USING,
    }

    private TycoonEntityStates mState = TycoonEntityStates.IDLE;
    private UseableObject mCurrentUsingObject = null;
    private float mUseStartTime = 0f;

    public TycoonEntityStates State
    {
        get
        {
            return mState;
        }
    }

    public float UseStartTime
    {
        get
        {
            return mUseStartTime;
        }
    }

    public UseableObject CurrentUsingObject
    {
        get
        {
            return mCurrentUsingObject;
        }
    }

    public void SetState(TycoonEntityStates state)
    {
        if (mState == state)
        {
            return;
        }

        switch(state)
        {
            case TycoonEntityStates.IDLE:

            break;

            case TycoonEntityStates.MOVING:

            break;

            case TycoonEntityStates.CLIMBING:

            break;

            case TycoonEntityStates.USING:

            break;
        }

        mState = state;
    }

    public void UseObject(UseableObject objectToUse)
    {
        if (mState == TycoonEntityStates.CLIMBING || mState == TycoonEntityStates.USING)
        {
            return;
        }

        if (objectToUse.Activate(this))
        {
            mCurrentUsingObject = objectToUse;
            mUseStartTime = Time.realtimeSinceStartup;
            EventManager.Instance.AddHandler<UseableObjectEvent>(UseableObjectEventHandler);
            SetState(TycoonEntityStates.USING);
        }
    }

    public void UseableObjectEventHandler(object sender, UseableObjectEvent evt)
    {
        if (evt.Sender == mCurrentUsingObject)
        {
            mCurrentUsingObject = null;
            SetState(TycoonEntityStates.IDLE);
            EventManager.Instance.RemoveHandler<UseableObjectEvent>(UseableObjectEventHandler);
        }
    }
}
