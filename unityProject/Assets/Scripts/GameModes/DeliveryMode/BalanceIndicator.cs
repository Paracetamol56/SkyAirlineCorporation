using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BalanceIndicator : MonoBehaviour
{
    [SerializeField]
    private Text bankText;

    private float bankBalance = 0f;

    public static BalanceIndicator instance;

    private void Awake()
    {
        if(instance != null)
        {
            Debug.LogError("BalanceIndicator multiple instance");
            return;
        }
        instance = this;
    }

    public void Start()
    {
        Debug.Log(bankBalance.ToString());
        bankText.text = "Bank account balance : " + bankBalance.ToString() + "$";
    }

    public void AddMoney(float value)
    {
        bankBalance += value;
        bankText.text = "Bank account balance : " + bankBalance.ToString() + "$";
    }
}
