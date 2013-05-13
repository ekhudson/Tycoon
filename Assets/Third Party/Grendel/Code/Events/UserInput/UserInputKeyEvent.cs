using UnityEngine;
using System.Collections;

public class UserInputKeyEvent : EventBase
{
    public enum TYPE
    {
        KEYDOWN,
        KEYHELD,
        KEYUP,        
    }
    
    public readonly TYPE Type;
    public readonly GrendelKeyBinding KeyBind;
    
    public UserInputKeyEvent(UserInputKeyEvent.TYPE inputType, GrendelKeyBinding bind, Vector3 location, object sender) : base(location, sender)
    {
        Type = inputType;
        KeyBind = bind;
    }
    
    public UserInputKeyEvent() : base (Vector3.zero, null)
    {        
        
    }
}