using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

public class SensorControl : MonoBehaviour, IStateControl
{
    public VideoPlayer vp;
    public GameObject QA_btn;


    //private void OnEnable()
    //{
    //    vp.loopPointReached += VideoEnd;  //视频结束
    //}
    //private void OnDisable()
    //{
    //    vp.loopPointReached -= VideoEnd;
    //}

    ////视频结束打开答题按钮
    //public void VideoEnd(VideoPlayer vp)
    //{
    //    QA_btn.SetActive(true);  
    //}

    ////视频点击
    //public void VideoClick()
    //{

    //    if( !vp.isPlaying )
    //    {
    //        vp.Play();
    //    }
    //    else
    //    {
    //        vp.Pause();
    //    }
    //}

    //点击答题键进入答题
    public void QAbtn_Click()
    {
        FSMManager.Instance.FSM.PerformTransition(Transition.SensorToQA);
    }

    public void InitState()
    {
        this.gameObject.SetActive(true);

        string url = Application.streamingAssetsPath + "/DianJiVideo2.mp4";
        vp.url = url;

        vp.Play();
    }

    public void ResetState()
    {
        this.gameObject.SetActive(false);

        vp.Stop();
    }
}