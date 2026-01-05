using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLeft : MonoBehaviour, IPlayerState<PlayerController>
{
    public void StateEnter(PlayerController sender)
    {
        InGameManager.Instance.TryPerformMove(PlayerState.Left);
    }

    public void StateExit(PlayerController sender)
    {

    }

    public void StateUpdate(PlayerController sender)
    {

    }
}