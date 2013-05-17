using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class TycoonPlayer : Singleton<TycoonPlayer>
{
    public float MoveSpeed = 1.0f;
    public float ClimbSpeed = 1.0f;
    public TycoonPlayerData PlayerData;

    protected Vector3 mTarget = Vector3.zero;
    protected TycoonEntity mController;

    private Collider mClimbingVolume;

    private List<string> mMessages = new List<string>();

    //Message Box
    private Vector3 mScreenPosition = Vector3.zero;
    private const float kMessageBoxWidth = 96f;
    private const float kMessageBoxHeight = 48f;
    private const float kMessageBoxHoverHeight = 32f;

    public enum PlayerStates
    {
        IDLE,
        MOVING,
        CLIMBING,
        WORKING,
    }

    protected PlayerStates mPlayerState = PlayerStates.IDLE;

    public PlayerStates GetState
    {
        get
        {
            return mPlayerState;
        }
    }

    public TycoonEntity GetEntity
    {
        get
        {
            return mController;
        }
    }

    public void AddMessage(string message)
    {
        if (!mMessages.Contains(message))
        {
            mMessages.Add(message);
        }
    }

    protected void Start()
    {
        EventManager.Instance.AddHandler<UserInputKeyEvent>(InputHandler);
        mController = GetComponent<TycoonEntity>();

        PlayerData.BankAmount = PlayerData.StartingBankAmount;

        if (PlayerData.PlayerInventory == null)
        {
            PlayerData.PlayerInventory = new TycoonInventory();
        }
    }

    //TODO: Move all this stuff to the TycoonEntity
    private void OnGUI()
    {
        mScreenPosition = Camera.main.WorldToScreenPoint(collider.bounds.center + new Vector3(0,collider.bounds.extents.y, 0));
        mScreenPosition.y = (Screen.height - mScreenPosition.y) - (kMessageBoxHoverHeight + kMessageBoxHeight);
        mScreenPosition.x +=  -(kMessageBoxWidth * 0.5f);
        
        Rect rect = new Rect(mScreenPosition.x, mScreenPosition.y, kMessageBoxWidth,kMessageBoxHeight);
        GUILayout.BeginArea(rect);

        if(mController.State == TycoonEntity.TycoonEntityStates.USING)
        {
            GUILayout.Label( mController.CurrentUsingObject.UsingMessage, GUI.skin.button);

            Rect progBarRect = GUILayoutUtility.GetLastRect();

            progBarRect.width = progBarRect.width * ( (Time.realtimeSinceStartup - mController.UseStartTime) / mController.CurrentUsingObject.UseTime);

            GUI.color = Color.yellow;

            GUI.Box(progBarRect, string.Empty, GUI.skin.button);

            GUI.color = Color.white;
        }
        else
        {
            foreach(string message in mMessages)
            {
                GUILayout.Label(message, GUI.skin.button);
            }
        }

        GUILayout.EndArea();

        if (Event.current.type == EventType.repaint)
        {
            mMessages.Clear();
        }
    }

    protected void Update()
    {
        if (mController == null)
        {
            return;
        }

        Vector3 norm = mTarget.normalized;
        mController.Move( ((new Vector3(norm.x, 0, norm.z) * MoveSpeed) + new Vector3(0, mTarget.y, 0)) * Time.deltaTime);
        mTarget = Vector3.zero;


        switch(mController.State)
        {
            case TycoonEntity.TycoonEntityStates.IDLE:

            break;
    
            case TycoonEntity.TycoonEntityStates.MOVING:
    
            break;
    
            case TycoonEntity.TycoonEntityStates.CLIMBING:

                if (mClimbingVolume == null || !mClimbingVolume.bounds.Contains(collider.bounds.center - new Vector3(0,collider.bounds.extents.y, 0)))
                {
                    StopClimbing();
                }
    
            break;

            case TycoonEntity.TycoonEntityStates.USING:

            break;
        }
    }

    public void MoveEntity(Vector3 direction)
    {
        mTarget += direction;
    }

    public void InputHandler(object sender, UserInputKeyEvent evt)
    {
        if (mController.State == TycoonEntity.TycoonEntityStates.IDLE || mController.State == TycoonEntity.TycoonEntityStates.MOVING)
        {

            if(evt.KeyBind == TycoonUserInput.Instance.MoveLeft && (evt.Type == UserInputKeyEvent.TYPE.KEYDOWN || evt.Type == UserInputKeyEvent.TYPE.KEYHELD))
            {
                mTarget += (new Vector3(-1 , 0 , 0));
            }
    
            if(evt.KeyBind == TycoonUserInput.Instance.MoveRight && (evt.Type == UserInputKeyEvent.TYPE.KEYDOWN || evt.Type == UserInputKeyEvent.TYPE.KEYHELD))
            {
                mTarget += (new Vector3(1, 0, 0));
            }
        }
        else if (mController.State == TycoonEntity.TycoonEntityStates.CLIMBING)
        {
            if(evt.KeyBind == TycoonUserInput.Instance.UseKey01 && (evt.Type == UserInputKeyEvent.TYPE.KEYDOWN || evt.Type == UserInputKeyEvent.TYPE.KEYHELD))
            {
                mTarget = (Vector3.up * ClimbSpeed);
            }

            if(evt.KeyBind == TycoonUserInput.Instance.UseKey02 && (evt.Type == UserInputKeyEvent.TYPE.KEYDOWN || evt.Type == UserInputKeyEvent.TYPE.KEYHELD))
            {
                mTarget = (Vector3.down * ClimbSpeed);
            }
        }
    }

    public void StartClimbing(Collider climbingVolume)
    {
        mClimbingVolume = climbingVolume;
        rigidbody.useGravity = false;
        rigidbody.velocity = Vector3.zero;
        mController.SetState(TycoonEntity.TycoonEntityStates.CLIMBING);
    }

    private void StopClimbing()
    {
        mController.SetState(TycoonEntity.TycoonEntityStates.IDLE);
        Vector3 pos = transform.position;
        Ray ray = new Ray(transform.position, Vector3.forward);
        RaycastHit hit = new RaycastHit();

        if (Physics.Raycast(ray, out hit, 10f))
        {
            pos.z = hit.collider.bounds.center.z - 1;
        }
        else
        {
            pos.z = 0; //otherwise assume we're somewhere above ground
        }

        transform.position = pos;
    }

    public void SetState(PlayerStates state)
    {
        if (state == mPlayerState)
        {
            return;
        }

        switch(state)
        {
            case PlayerStates.IDLE:

                rigidbody.useGravity = true;

            break;

            case PlayerStates.MOVING:

                rigidbody.useGravity = true;

            break;

            case PlayerStates.CLIMBING:



            break;

            case PlayerStates.WORKING:

            break;
        }

        mPlayerState = state;
    }

}
