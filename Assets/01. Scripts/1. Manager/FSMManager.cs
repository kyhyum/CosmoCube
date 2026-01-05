using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FSMManager
{
    public InitGameState InitGameState;
    public InGameState InGameState;
    public InitScreenState initScreenState;

    FSMInterface currentState;

    public FSMManager()
    {
        InitGameState = new InitGameState();
        InGameState = new InGameState();
        initScreenState = new InitScreenState();
    }

    public void ChangeState(FSMInterface nextState)
    {
        if (currentState != null)
            currentState.ExitState();
        currentState = nextState;
        nextState.EnterState();
    }
}

public class InitGameState : FSMInterface
{
    public void EnterState()
    {
        DoState();
    }

    public void DoState()
    {
        DataManager.Instance.GetData();
        NewGameManager.Instance.step++;
        NewGameManager.Instance.SetStep();
    }

    public void ExitState()
    {
    }
}
public class InitScreenState : FSMInterface
{
    public void EnterState()
    {
        DoState();
    }

    public void DoState()
    {
        UIManager.Instance.Push(UIName.Initlayer);
    }

    public void ExitState()
    {
    }
}

public class InGameState : FSMInterface
{
    public void EnterState()
    {
        DoState();
    }

    public void DoState()
    {
    }

    public void ExitState()
    {
    }
}