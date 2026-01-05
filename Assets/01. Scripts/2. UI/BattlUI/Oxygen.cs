using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Oxygen : MonoBehaviour
{
    public Image oxygen;
    public float reduceVal = 1.0f;

    Coroutine coroutineHandle;


    private void Start()
    {
        coroutineHandle = StartCoroutine(AutoDecreaseOxygen());
    }

    IEnumerator AutoDecreaseOxygen()
    {
        while (true)
        {
            InGameManager.Instance.player.oxygen -= reduceVal;
            oxygen.fillAmount = InGameManager.Instance.player.oxygen / 100;
            yield return new WaitForSeconds(0.2f);
        }
    }

    public void ReduceOxygen(float oxygenVal)
    {
        InGameManager.Instance.player.oxygen -= oxygenVal;
    }

    public void PlusOxygen(float oxygenVal)
    {
        InGameManager.Instance.player.oxygen += oxygenVal;
    }
}