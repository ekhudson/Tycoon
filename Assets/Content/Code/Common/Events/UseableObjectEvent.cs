using UnityEngine;
using System.Collections;

public class UseableObjectEvent : EventBase
{
    public readonly TycoonEntity Activator;
    public readonly UseableObject.UseableObjectStates ObjectState;
 
    public UseableObjectEvent(TycoonEntity activator, UseableObject.UseableObjectStates objectState, object sender) : base (Vector3.zero, sender)
    {
        Activator = activator;
        ObjectState = objectState;
    }

    public UseableObjectEvent() : base (Vector3.zero, null)
    {
    
    }
}
