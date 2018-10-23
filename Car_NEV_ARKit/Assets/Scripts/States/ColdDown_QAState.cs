using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColdDown_QAState : FSMState
{
    public ColdDown_QAState(IStateControl control)
    {
        this.stateID = StateID.Colddown_QA;
        this.control = control;
    }



    public override void DoUpdate()
    {

    }
}
