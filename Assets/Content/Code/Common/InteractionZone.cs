using UnityEngine;
using System.Collections;

public class InteractionZone : TriggerVolume
{
    public string MessageOnEnter = string.Empty;
    //public bool ActivateOnUse01 = true;
    //public bool ActivateOnUse02 = true;
    //public bool ActivateOnUse03 = true;
    public UsableObject[] ActivateOnUse01;
    public UsableObject[] ActivateOnUse02;
    public UsableObject[] ActivateOnUse03;

    protected override void Start()
    {
        EventManager.Instance.AddHandler<UserInputKeyEvent>(InputHandler);
        base.Start();
    }

    public void OnGUI()
    {
        if(ObjectList.Contains(TycoonPlayer.Instance.gameObject.collider))
        {
//            mScreenPosition = Camera.main.WorldToScreenPoint(collider.bounds.center + new Vector3(0,collider.bounds.extents.y, 0));
//            mScreenPosition.y = (Screen.height - mScreenPosition.y) - kMessageBoxHoverHeight;
//            mScreenPosition.x +=  -(kMessageBoxWidth * 0.5f);
//
//            Rect rect = new Rect(mScreenPosition.x, mScreenPosition.y, kMessageBoxWidth,kMessageBoxHeight);
//            GUILayout.BeginArea(rect);
//
//            GUILayout.Label(MessageOnEnter, GUI.skin.button);
//
//            GUILayout.EndArea();

            foreach(UsableObject obj in ActivateOnUse01)
            {
                if (obj.PreuseMessage != null)
                {
                    TycoonPlayer.Instance.AddMessage(obj.PreuseMessage);
                }
            }

            foreach(UsableObject obj in ActivateOnUse02)
            {
               if (obj.PreuseMessage != null)
                {
                    TycoonPlayer.Instance.AddMessage(obj.PreuseMessage);
                }
            }
            foreach(UsableObject obj in ActivateOnUse03)
            {
               if (obj.PreuseMessage != null)
                {
                    TycoonPlayer.Instance.AddMessage(obj.PreuseMessage);
                }
            }
        }
    }

    public virtual void Activate(UsableObject[] objectsToActivate)
    {
        foreach(UsableObject obj in objectsToActivate)
        {
            obj.Activate();
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
            Activate(ActivateOnUse01);
        }

        if(evt.KeyBind == TycoonUserInput.Instance.UseKey02 && (evt.Type == UserInputKeyEvent.TYPE.KEYDOWN))
        {
            Activate(ActivateOnUse02);
        }
    }
}
