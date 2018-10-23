using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LowTempState : FSMState
{
    public LowTempState(IStateControl control)
    {
        this.stateID = StateID.LowTemp;
        this.control = control;
    }
    
    public override void DoUpdate()
    {
    }
}
