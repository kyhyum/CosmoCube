using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IPlayerState<T>
{
    void StateEnter(T sender);
    void StateUpdate(T sender);
    void StateExit(T sender);
}