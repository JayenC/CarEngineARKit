using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//转换条件
public enum Transition
{
    NullTransition = 0,
    GoBack,
    ScanOver,
    MenuToSensor,
    MenuToCold,
    MenuToConstruct,
    SensorToQA,
    ConstruToQA,
    ColddownToQA,
    ColddownToHigh,
    ColddownToLow
}

//状态ID
public enum StateID
{
    NullStateID = 0,
    StartScan,
    Menu,
    ColdDown,
    Construct,
    Sensor,
    Sensor_QA,
    Construct_QA,
    Colddown_QA,
    HighTemp,
    LowTemp
}

public abstract class FSMState
{
    protected StateID stateID;
    public StateID ID { get { return stateID; } }

    public FSMSystem fsm;  //属于哪个状态机

    public Dictionary<Transition, StateID> map = new Dictionary<Transition, StateID>();

    public IStateControl control;

    //添加转换
    public void AddTransition(Transition trans, StateID id)
    {
        if( trans == Transition.NullTransition || id == StateID.NullStateID )
        {
            Debug.LogError("Can't add Null transition or stateID");
            return;
        }
        if( map.ContainsKey(trans) )
        {
            Debug.LogError("The transition you want to add is already exist in map!");
            return;
        }

        map.Add(trans, id);
    }
    //删除转换
    public void RemoveTransition(Transition trans)
    {
        if( !map.ContainsKey(trans) )
        {
            Debug.LogError("The transition u wnat to remove is not exist!");
            return;
        }

        map.Remove(trans);
    }

    //根据传递过来的转化条件，返回对应状态
    public StateID GetOutputState(Transition trans)
    {
        if( map.ContainsKey(trans) )
        {
            return map[ trans ];
        }
        return StateID.NullStateID;
    }


    public virtual void DoBeforeEntering()//进入状态机 执行一次
    {
        if( control != null )
            control.InitState();
    }
    public virtual void DoBeforeLeaving()//离开状态机 执行一次
    {
        if( control != null )
            control.ResetState();
    }
    public abstract void DoUpdate();//当状态机处于当前状态，一直调用

}
