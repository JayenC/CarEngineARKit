using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuControl : MonoBehaviour, IStateControl
{
    public GameObject menuCanvas;

    public void ConstructClick()
    {
        FSMManager.Instance.FSM.PerformTransition(Transition.MenuToConstruct);
    }
    public void SensorClick()
    {
        FSMManager.Instance.FSM.PerformTransition(Transition.MenuToSensor);
    }
    public void ColddownClick()
    {
        FSMManager.Instance.FSM.PerformTransition(Transition.MenuToCold);
    }

    public void InitState()
    {
        this.gameObject.SetActive(true);
    }

    public void ResetState()
    {
        this.gameObject.SetActive(false);
    }
}
