using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Playables;

public class PlayerController : MonoBehaviour
{
    public Dictionary<PlayerState, IPlayerState<PlayerController>> dicState = new Dictionary<PlayerState, IPlayerState<PlayerController>>();
    public PlayerStateMachine<PlayerController> sm;
    private void Awake()
    {
        IPlayerState<PlayerController> idle = new PlayerIdle();
        IPlayerState<PlayerController> up = new PlayerUp();
        IPlayerState<PlayerController> down = new PlayerDown();
        IPlayerState<PlayerController> right = new PlayerRight();
        IPlayerState<PlayerController> left = new PlayerLeft();
        IPlayerState<PlayerController> destroy = new PlayerDestroy();
        IPlayerState<PlayerController> meta = new PlayerMeta();

        dicState.Add(PlayerState.Idle, idle);
        dicState.Add(PlayerState.Up, up);
        dicState.Add(PlayerState.Down, down);
        dicState.Add(PlayerState.Right, right);
        dicState.Add(PlayerState.Left, left);
        dicState.Add(PlayerState.Destroy, destroy);
        dicState.Add(PlayerState.Meta, meta);

        sm = new PlayerStateMachine<PlayerController>(this, dicState[PlayerState.Idle]);
    }
}