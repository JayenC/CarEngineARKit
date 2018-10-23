using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sensor_QAState : FSMState
{
    public Sensor_QAState(IStateControl control) 
    {
        this.stateID = StateID.Sensor_QA;
        this.control = control;
    }


    public override void DoUpdate()
    {
    }
}