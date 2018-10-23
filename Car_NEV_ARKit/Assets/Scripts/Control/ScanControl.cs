using HedgehogTeam.EasyTouch;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.iOS;

public class ScanControl : MonoBehaviour, IStateControl
{
    public Transform E6;
    public Transform positionRoot;    //所有state的位置
    public GameObject scanCanvas;   //UI
    public UpdateGameObjectPos scanUI;

    public GameObject pointCloud;

    //调整E6位置、旋转
    public void Adjust_Update()
    {
        if( !scanCanvas.activeInHierarchy )
        {
            scanUI.gameObject.SetActive(false);
            scanCanvas.SetActive(true); 
        }
    }

    ////移动
    //public void MoveBtn_Click()
    //{
    //    E6.GetComponent<QuickSwipe>().enabled = false;
    //    E6.GetComponent<QuickDrag>().enabled = true;
    //}
    ////旋转
    //public void RotateBtn_Click()
    //{
    //    E6.GetComponent<QuickDrag>().enabled = false;
    //    E6.GetComponent<QuickSwipe>().enabled = true;
    //}

    //锁定

    public void LockBtn_Click()
    {
        //让UI位置变到放置模型位置
        positionRoot.position = E6.position;
        positionRoot.rotation = E6.rotation;
        FSMManager.Instance.FSM.PerformTransition(Transition.ScanOver);
    }



    public void InitState()
    {
        this.gameObject.SetActive(true);

        //初始化打开E6
        E6.GetComponent<BoxCollider>().enabled = true;
        E6.GetComponent<QuickDrag>().enabled = true;
        E6.GetComponent<QuickSwipe>().enabled = true;
        //默认关闭UI按钮
        scanCanvas.SetActive(false);
        //打开扫描UI
        scanUI.gameObject.SetActive(true);
        scanUI.OnStart();

        pointCloud.SetActive(true);
    }

    public void ResetState()
    {
        this.gameObject.SetActive(false);

        E6.GetComponent<BoxCollider>().enabled = false;
        E6.GetComponent<QuickDrag>().enabled = false;
        E6.GetComponent<QuickSwipe>() .enabled = false;

        pointCloud.SetActive(false);
    }
}