using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface FSMInterface
{
    void EnterState();
    void DoState();
    void ExitState();
}