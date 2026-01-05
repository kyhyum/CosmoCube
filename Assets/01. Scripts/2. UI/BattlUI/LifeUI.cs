using System.Collections.Generic;
using UnityEngine;

public class LifeUI : MonoBehaviour
{
    public List<CanvasGroup> lifeCG;

    public void InitLife(int life)
    {
        for (int i = 0; i < life; i++)
        {
            lifeCG[i].alpha = 1;
        }
    }

    public void RecoveryLife(int currentLife)
    {
        if (currentLife < lifeCG.Count)
        {
            lifeCG[currentLife].alpha = 1;
        }
    }

    public void ReduceLife(int currentLife)
    {
        lifeCG[currentLife].alpha = 0;
    }
}   