using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConstructControl : MonoBehaviour, IStateControl
{
    private Animator anim;
    public GameObject UIroot;

    public delegate void ExploreDelegate();
    public ExploreDelegate ExploreHandler;


    private void Start()
    {
        anim = GetComponentInChildren<Animator>();
    }

    //拆解&聚合
    public void ExploreBtn_Click()
    {
        anim.SetTrigger("Explore");
        if( ExploreHandler != null )
            ExploreHandler();
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
        FSMManager.Instance.FSM.PerformTransition(Transition.ConstruToQA);
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
