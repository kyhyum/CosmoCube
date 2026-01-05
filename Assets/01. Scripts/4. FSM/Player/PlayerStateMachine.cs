using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStateMachine<T>
{
    private T m_sender;

    //현재 상태를 담는 프로퍼티
    public IPlayerState<T> CurState { get; set; }


    //기본 상태를 생성시에 설정하게 생성자 선언
    public PlayerStateMachine(T sender, IPlayerState<T> state)
    {
        m_sender = sender;
        SetState(state);
    }

    public void SetState(IPlayerState<T> state)
    {
        if (CurState != null)
            CurState.StateExit(m_sender);

        CurState = state;

        if (CurState != null)
            CurState.StateEnter(m_sender);
    }

    public void DoOperateUpdate()
    {
        CurState.StateUpdate(m_sender);
    }
}