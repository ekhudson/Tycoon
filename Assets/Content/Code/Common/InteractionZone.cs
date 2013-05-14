using UnityEngine;
using System.Collections;

public class InteractionZone : TriggerVolume
{
    public UseableObject[] ActivateOnUse01;
    public UseableObject[] ActivateOnUse02;
    public UseableObject[] ActivateOnUse03;

    protected override void Start()
    {
        EventManager.Instance.AddHandler<UserInputKeyEvent>(InputHandler);
        base.Start();
    }

    public void Update()
    {
        if(ObjectList.Contains(TycoonPlayer.Instance.gameObject.collider) &&
            TycoonPlayer.Instance.GetState != TycoonPlayer.PlayerStates.WORKING &&
            TycoonPlayer.Instance.GetState != TycoonPlayer.PlayerStates.CLIMBING)
        {
            foreach(UseableObject obj in ActivateOnUse01)
            {
                if (obj.PreuseMessage != null)
                {
                    TycoonPlayer.Instance.AddMessage(obj.PreuseMessage);
                }
            }

            foreach(UseableObject obj in ActivateOnUse02)
            {
               if (obj.PreuseMessage != null)
                {
                    TycoonPlayer.Instance.AddMessage(obj.PreuseMessage);
                }
            }
            foreach(UseableObject obj in ActivateOnUse03)
            {
               if (obj.PreuseMessage != null)
                {
                    TycoonPlayer.Instance.AddMessage(obj.PreuseMessage);
                }
            }
        }
    }

    public virtual void Activate(UseableObject[] objectsToActivate, TycoonEntity activator)
    {
        foreach(UseableObject obj in objectsToActivate)
        {
            //obj.Activate(activator);
            activator.UseObject(obj);
        }
    }

    public virtual void InputHandler(object sender, UserInputKeyEvent evt)
    {
        if (!collider.bounds.Intersects(TycoonPlayer.Instance.collider.bounds))
        {
            return;
        }

        if(evt.KeyBind == TycoonUserInput.Instance.UseKey01 && (evt.Type == UserInputKeyEvent.TYPE.KEYDOWN))
        {
            Activate(ActivateOnUse01, TycoonPlayer.Instance.GetEntity);
        }

        if(evt.KeyBind == TycoonUserInput.Instance.UseKey02 && (evt.Type == UserInputKeyEvent.TYPE.KEYDOWN))
        {
            Activate(ActivateOnUse02, TycoonPlayer.Instance.GetEntity);
        }
    }
}
