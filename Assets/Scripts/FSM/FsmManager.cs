using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FsmManager
{

    FsmBase[] allStates;

    public FsmManager(int stateCount)
    {
        allStates = new FsmBase[stateCount];
    }

    sbyte stateCount = -1;

    sbyte curStateIndex = -1;

    public void AddState(FsmBase newState)
    {
        if (stateCount > allStates.Length - 1)
        {
            return;
        }

        stateCount++;

        allStates[stateCount] = newState;

    }

    public void ChangeState(sbyte newIndex)
    {
        if (newIndex > allStates.Length - 1 || newIndex == curStateIndex)
            return;

        if (curStateIndex != -1)
        {
            allStates[curStateIndex].OnExit();
        }

        curStateIndex = newIndex;

        allStates[curStateIndex].OnEnter();
    }

    public void FsmUpdate()
    {
        if (curStateIndex != -1)
            allStates[curStateIndex].OnStay();
    }

}
