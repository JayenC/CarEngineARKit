using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HighTempState : FSMState
{
    public HighTempState(IStateControl control)
    {
        this.stateID = StateID.HighTemp;
        this.control = control;
    }
    
    public override void DoUpdate()
    {
    }
}
