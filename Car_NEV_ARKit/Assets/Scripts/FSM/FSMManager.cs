using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FSMManager : MonoBehaviour
{
    private static FSMSystem fsm;
    public FSMSystem FSM { get { return fsm; } }

    //单例
    private static FSMManager fSMManager;
    private FSMManager() { }
    public static FSMManager Instance
    {
        get
        {
            return fSMManager;
        }
    }

    //公共成员-----------------------------------(Controls)
    public ScanControl scan_ctrl;
    public MenuControl menu_ctrl;
    public ConstructControl construct_ctrl;
    public ColddownControl colddown_ctrl;
    public SensorControl sensor_ctrl;
    public Sensor_QAControl SensorQA_ctrl;
    public Sensor_QAControl ConstructQA_ctrl;
    public Sensor_QAControl ColddownQA_ctrl;
    public TempControl HighTemp_ctrl;
    public TempControl LowTemp_ctrl;
    //-----------------------------------------



    private void Awake()
    {
        fSMManager = this; //单例

        if( fsm == null )
            fsm = new FSMSystem();  //初始化状态机

        //新建状态(传入control)
        MenuState menuState = new MenuState(menu_ctrl);
        menuState.AddTransition(Transition.MenuToConstruct, StateID.Construct);
        menuState.AddTransition(Transition.MenuToCold, StateID.ColdDown);
        menuState.AddTransition(Transition.MenuToSensor, StateID.Sensor);
        menuState.AddTransition(Transition.GoBack, StateID.StartScan);


        ScanState scanState = new ScanState(scan_ctrl);
        scanState.AddTransition(Transition.ScanOver, StateID.Menu);

        ConstructState constructState = new ConstructState(construct_ctrl);
        constructState.AddTransition(Transition.GoBack, StateID.Menu);
        constructState.AddTransition(Transition.ConstruToQA, StateID.Construct_QA);

        Construct_QAState construct_QAState = new Construct_QAState(ConstructQA_ctrl);
        construct_QAState.AddTransition(Transition.GoBack, StateID.Construct);


        ColdDownState coldDownState = new ColdDownState(colddown_ctrl);
        coldDownState.AddTransition(Transition.GoBack, StateID.Menu);
        coldDownState.AddTransition(Transition.ColddownToQA, StateID.Colddown_QA);
        coldDownState.AddTransition(Transition.ColddownToHigh, StateID.HighTemp);
        coldDownState.AddTransition(Transition.ColddownToLow, StateID.LowTemp);

        ColdDown_QAState colddown_QAState = new ColdDown_QAState(ColddownQA_ctrl);
        colddown_QAState.AddTransition(Transition.GoBack, StateID.ColdDown);

        HighTempState highTempState = new HighTempState(HighTemp_ctrl);
        highTempState.AddTransition(Transition.GoBack, StateID.ColdDown);

        LowTempState lowTempState = new LowTempState(LowTemp_ctrl);
        lowTempState.AddTransition(Transition.GoBack, StateID.ColdDown);


        SensorState sensorState = new SensorState(sensor_ctrl);
        sensorState.AddTransition(Transition.GoBack, StateID.Menu);
        sensorState.AddTransition(Transition.SensorToQA, StateID.Sensor_QA);

        Sensor_QAState sensor_QAState = new Sensor_QAState(SensorQA_ctrl);
        sensor_QAState.AddTransition(Transition.GoBack, StateID.Sensor);

        //添加状态到状态机
        fsm.AddState(menuState);
        fsm.AddState(scanState);
        fsm.AddState(constructState);
        fsm.AddState(coldDownState);
        fsm.AddState(colddown_QAState);
        fsm.AddState(sensorState);
        fsm.AddState(sensor_QAState);
        fsm.AddState(construct_QAState);
        fsm.AddState(highTempState);
        fsm.AddState(lowTempState);
    }

    private void Start()
    {
        GameObject root = menu_ctrl.transform.parent.gameObject;
        for( int i = 0; i < root.transform.childCount; i++ )
        {
            root.transform.GetChild(i).gameObject.SetActive(false);
        }

        fsm.Start(StateID.StartScan);  //开启状态机
    }

    private void Update()
    {
 //      print(fsm.CurrentState);
        fsm.CurrentState.DoUpdate();
    }


    //全局返回
    public void GoBack()
    {
        fsm.PerformTransition(Transition.GoBack);
    }
}
