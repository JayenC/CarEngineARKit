using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColddownControl : MonoBehaviour, IStateControl
{
    public Animator anim;
    public GameObject UIroot;

    private void Start()
    {
        anim = GetComponentInChildren<Animator>();
    }

    //根据名字打开UI
    public void OpenUI(string name)
    {
        UIroot.transform.Find(name).gameObject.SetActive(true);
    }
    //关闭UI
    public void CloseUI(string name)
    {
        UIroot.transform.Find(name).gameObject.SetActive(false);
    }

    //答题键进入答题界面
    public void QAbtn_Click()
    {
        FSMManager.Instance.FSM.PerformTransition(Transition.ColddownToQA);
    }

    public void HighTemp_Click()
    {
        FSMManager.Instance.FSM.PerformTransition(Transition.ColddownToHigh);
    }
    public void LowTemp_Click()
    {
        FSMManager.Instance.FSM.PerformTransition(Transition.ColddownToLow);
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
