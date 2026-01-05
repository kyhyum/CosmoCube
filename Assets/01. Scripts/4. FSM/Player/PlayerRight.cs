using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerRight : MonoBehaviour, IPlayerState<PlayerController>
{
    public void StateEnter(PlayerController sender)
    {
        InGameManager.Instance.TryPerformMove(PlayerState.Right);
    }

    public void StateExit(PlayerController sender)
    {

    }

    public void StateUpdate(PlayerController sender)
    {

    }
}