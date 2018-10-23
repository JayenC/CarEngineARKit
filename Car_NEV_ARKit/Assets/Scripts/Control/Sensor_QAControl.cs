using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sensor_QAControl : MonoBehaviour, IStateControl
{
    public Question[] QAs;
    public int currentQIndex = 0;

    public GameObject CommitBtn;
    public GameObject nextBtn;
    public GameObject restartBtn;

    //public AudioSource right;
    //public AudioSource fail;

    public GameObject rightUI;
    public GameObject failUI;


    //下一题按键
    public void NextBtn_Click()
    {
        QAs[ currentQIndex ].gameObject.SetActive(false);
        currentQIndex++;

        QAs[ currentQIndex ].gameObject.SetActive(true);

        nextBtn.SetActive(false);
        CommitBtn.SetActive(true);  //开启确认按钮
    }

    //确认按键
    public void CommitBtn_Click()
    {
        //判断没选答案不能确认
        if( QAs[ currentQIndex ].IsSelected() == false )
            return;

        QAs[ currentQIndex ].CommitAnswer();

        //停止题目语音
        QAs[ currentQIndex ].GetComponent<AudioSource>().Stop();
        CommitBtn.SetActive(false);  //关闭确认按钮
        PlayCommitAudio();  //播放题目正确与否音效

        //最后一题
        if( currentQIndex >= QAs.Length - 1 )
        {
            StartCoroutine(EndQA());
            return;
        }

        nextBtn.SetActive(true);  //显示下一题按钮
    }

    //全部题目答完
    IEnumerator EndQA()
    {
        //必须全对才算对
        bool allRight = true;
        for( int i = 0; i < QAs.Length; i++ )
        {
            allRight = allRight & QAs[ i ].isRight;
        }

        yield return new WaitForSeconds(3);

        if( allRight )
        {
            //全对音效
            AudioClip ac = Resources.Load<AudioClip>("Audio/AllRight");
            AudioSource.PlayClipAtPoint(ac, Vector3.zero);
            rightUI.SetActive(true);
            failUI.SetActive(false);
        }
        else
        {
            //没全对音效
            AudioClip ac = Resources.Load<AudioClip>("Audio/Wrong");
            AudioSource.PlayClipAtPoint(ac, Vector3.zero);
            rightUI.SetActive(false);
            failUI.SetActive(true);
        }

        //显示重新开始按钮
        restartBtn.SetActive(true);
        //关闭题目
        QAs[ currentQIndex ].gameObject.SetActive(false);
    }

    //提交音效
    public void PlayCommitAudio()
    {
        //判断正误
        if( QAs[ currentQIndex ].isRight )
        {
            AudioClip ac = Resources.Load<AudioClip>("Audio/Right");
            AudioSource.PlayClipAtPoint(ac, Vector3.zero);
        }
        else
        {
            AudioClip ac = Resources.Load<AudioClip>("Audio/Fail1");
            AudioSource.PlayClipAtPoint(ac, Vector3.zero);
        }
    }


    //重新开始
    public void RestartQA()
    {
        InitState();
    }

    public void InitState()
    {
        this.gameObject.SetActive(true);

        //初始化只开第一个问题
        for( int i = 0; i < QAs.Length; i++ )
        {
            QAs[ i ].gameObject.SetActive(false);
        }
        QAs[ 0 ].gameObject.SetActive(true);
        currentQIndex = 0;  //当前问题指向0

        for( int i = 0; i < QAs.Length; i++ )
        {
            QAs[ i ].ResetQuestion();
        }

        CommitBtn.SetActive(true);
        nextBtn.SetActive(false); //隐藏下一题按钮
        restartBtn.SetActive(false);
    }

    public void ResetState()
    {
        this.gameObject.SetActive(false);
    }
}
