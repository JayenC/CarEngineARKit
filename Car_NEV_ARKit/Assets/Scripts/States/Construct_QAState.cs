using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Construct_QAState : FSMState
{
    public Construct_QAState(IStateControl control)
    {
        this.stateID = StateID.Construct_QA;
        this.control = control;
    }

    

    public override void DoUpdate()
    {
      
    }
}
