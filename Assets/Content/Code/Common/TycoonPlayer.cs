using UnityEngine;
using System.Collections;

public class TycoonPlayer : Singleton<TycoonPlayer>
{
    public float MoveSpeed = 1.0f;
    public float ClimbSpeed = 1.0f;
    public TycoonPlayerData PlayerData;

    protected Vector3 mTarget = Vector3.zero;
    protected CharacterEntity mController;

    private Collider mClimbingVolume;

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

    protected void Start()
    {
        EventManager.Instance.AddHandler<UserInputKeyEvent>(InputHandler);
        mController = GetComponent<CharacterEntity>();

        PlayerData.BankAmount = PlayerData.StartingBankAmount;

        if (PlayerData.PlayerInventory == null)
        {
            PlayerData.PlayerInventory = new TycoonInventory();
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


        switch(mPlayerState)
        {
            case PlayerStates.IDLE:
    


            break;
    
            case PlayerStates.MOVING:
    

    
            break;
    
            case PlayerStates.CLIMBING:

                if (mClimbingVolume == null || !mClimbingVolume.bounds.Contains(collider.bounds.center - new Vector3(0,collider.bounds.extents.y, 0)))
                {
                    StopClimbing();
                }
    
            break;

            case PlayerStates.WORKING:

            break;
        }
    }

    public void MoveEntity(Vector3 direction)
    {
        mTarget += direction;
    }

    public void InputHandler(object sender, UserInputKeyEvent evt)
    {
        if (mPlayerState == PlayerStates.IDLE || mPlayerState == PlayerStates.MOVING)
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
        else if (mPlayerState == PlayerStates.CLIMBING)
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
        SetState(PlayerStates.CLIMBING);
    }

    private void StopClimbing()
    {
        SetState(PlayerStates.IDLE);
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
