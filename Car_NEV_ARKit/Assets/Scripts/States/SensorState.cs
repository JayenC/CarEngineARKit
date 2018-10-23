using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SensorState : FSMState
{
    public SensorState (IStateControl control) 
    {
        this.stateID = StateID.Sensor;
        this.control = control;
    }


    public override void DoUpdate()
    {
    }
}