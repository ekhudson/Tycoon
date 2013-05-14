using UnityEngine;
using System.Collections;

public class UseableObject : MonoBehaviour
{
    public string PreuseMessage = string.Empty;
    public string UsingMessage = string.Empty;
    public float UseTime = 0;

    private TycoonEntity mCurrentActivator;

    public enum UseableObjectStates
    {
        ENABLED,
        INUSE,
        DISABLED,
    }

    private UseableObjectStates mState = UseableObjectStates.ENABLED;

    public UseableObjectStates State
    {
        get
        {
            return mState;
        }
    }

    public virtual bool Activate(TycoonEntity activator)
    {
        if (mState == UseableObjectStates.DISABLED || mState == UseableObjectStates.INUSE)
        {
            return false;
        }

        mCurrentActivator = activator;

        EventManager.Instance.Post(new UseableObjectEvent(activator, mState, this));

        StartCoroutine("Use");

        return true;
    }

    IEnumerator Use()
    {
        mState = UseableObjectStates.INUSE;
        yield return new WaitForSeconds(UseTime);
        mState = UseableObjectStates.ENABLED;
        EventManager.Instance.Post(new UseableObjectEvent(mCurrentActivator, mState, this));
    }

}
