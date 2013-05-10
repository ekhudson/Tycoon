using UnityEngine;
using System.Collections;

[System.Serializable]
public class TycoonPlayerData
{

    public float StartingBankAmount = 5.0f;

    //BANK AMOUNT
    private float kDefaultBankAmount = 5.0f;

    public float BankAmount
    {
        get
        {
            return PlayerPrefs.GetFloat("BankAmount", kDefaultBankAmount);
        }
        set
        {
            PlayerPrefs.SetFloat("BankAmount", value);
        }
    }

}
