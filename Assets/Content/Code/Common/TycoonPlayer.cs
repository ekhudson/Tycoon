using UnityEngine;
using System.Collections;

public class TycoonPlayer : Singleton<TycoonPlayer>
{
    public float MoveSpeed = 1.0f;
    public TycoonPlayerData PlayerData;

    protected Vector3 mTarget = Vector3.zero;
    protected CharacterEntity mController;

    protected void Start()
    {
        EventManager.Instance.AddHandler<UserInputKeyEvent>(InputHandler);
        mController = GetComponent<CharacterEntity>();

        PlayerData.BankAmount = PlayerData.StartingBankAmount;
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
    }

    public void MoveEntity(Vector3 direction)
    {
        mTarget += direction;
    }

    public void InputHandler(object sender, UserInputKeyEvent evt)
    {
        if(evt.KeyBind == UserInput.Instance.MoveLeft && (evt.Type == UserInputKeyEvent.TYPE.KEYDOWN || evt.Type == UserInputKeyEvent.TYPE.KEYHELD))
        {
            mTarget += (new Vector3(-1 , 0 , 0));
        }

        if(evt.KeyBind == UserInput.Instance.MoveRight && (evt.Type == UserInputKeyEvent.TYPE.KEYDOWN || evt.Type == UserInputKeyEvent.TYPE.KEYHELD))
        {
            mTarget += (new Vector3(1, 0, 0));
        }
    }

}
