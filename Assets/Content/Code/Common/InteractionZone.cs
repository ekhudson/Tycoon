using UnityEngine;
using System.Collections;

public class InteractionZone : TriggerVolume
{
    public string MessageOnEnter = string.Empty;

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

    public void InputHandler(object sender, UserInputKeyEvent evt)
    {
        if (!collider.bounds.Contains(TycoonPlayer.Instance.transform.position))
        {
            return;
        }

        if(evt.KeyBind == UserInput.Instance.MoveUp && (evt.Type == UserInputKeyEvent.TYPE.KEYDOWN || evt.Type == UserInputKeyEvent.TYPE.KEYHELD))
        {

        }

        if(evt.KeyBind == UserInput.Instance.MoveDown && (evt.Type == UserInputKeyEvent.TYPE.KEYDOWN || evt.Type == UserInputKeyEvent.TYPE.KEYHELD))
        {

        }
    }
}
