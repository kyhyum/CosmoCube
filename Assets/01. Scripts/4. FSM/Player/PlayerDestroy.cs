using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDestroy : MonoBehaviour, IPlayerState<PlayerController>
{
    public void StateEnter(PlayerController sender)
    {
        // sender는 PlayerController. 같은 GameObject에 Player 컴포넌트가 붙어있다고 가정
        var player = sender.GetComponent<Player>();
        if (player != null)
        {
            TileMatcherManager.Instance.TryMatchAroundPlayer(player.nowPos);
        }

        // 작업 끝나면 상태 해제(또는 Idle 상태로 전환)
        sender.sm.SetState(null);
    }

    public void StateExit(PlayerController sender) { }

    public void StateUpdate(PlayerController sender) { }
}