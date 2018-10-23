using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuState : FSMState
{
    public MenuState(IStateControl control)
    {
        this.stateID = StateID.Menu;
        this.control = control;
    }



    public override void DoUpdate()
    {
    }
}
