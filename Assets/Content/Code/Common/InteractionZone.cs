using UnityEngine;
using System.Collections;

public class InteractionZone : TriggerVolume
{
    public string MessageOnEnter = string.Empty;
    public bool ActivateOnUse01 = true;
    public bool ActivateOnUse02 = true;
    public bool ActivateOnUse03 = true;
    public GameObject[] ActivateOnUse;

    private Vector3 mScreenPosition;

    private const float kMessageBoxWidth = 96f;
    private const float kMessageBoxHeight = 32f;
    private const float kMessageBoxHoverHeight = 32f;

    protected override void Start()
    {
        EventManager.Instance.AddHandler<UserInputKeyEvent>(InputHandler);
        base.Start();
    }

    public void OnGUI()
    {
        if(ObjectList.Contains(TycoonPlayer.Instance.gameObject.collider))
        {
            mScreenPosition = Camera.main.WorldToScreenPoint(collider.bounds.center + new Vector3(0,collider.bounds.extents.y, 0));
            mScreenPosition.y = (Screen.height - mScreenPosition.y) - kMessageBoxHoverHeight;
            mScreenPosition.x +=  -(kMessageBoxWidth * 0.5f);

            GUI.Label(new Rect(mScreenPosition.x, mScreenPosition.y, kMessageBoxWidth,kMessageBoxHeight), MessageOnEnter, GUI.skin.box);
        }
    }

    public virtual void Activate()
    {
        foreach(GameObject obj in ActivateOnUse)
        {
            obj.SendMessage("Activate");
        }
    }

    public virtual void InputHandler(object sender, UserInputKeyEvent evt)
    {
        if (!collider.bounds.Intersects(TycoonPlayer.Instance.collider.bounds))
        {
            return;
        }

        if(evt.KeyBind == TycoonUserInput.Instance.UseKey01 && (evt.Type == UserInputKeyEvent.TYPE.KEYDOWN) && ActivateOnUse01)
        {
            Activate();
        }

        if(evt.KeyBind == TycoonUserInput.Instance.UseKey02 && (evt.Type == UserInputKeyEvent.TYPE.KEYDOWN) && ActivateOnUse02)
        {
            Activate();
        }
    }
}
