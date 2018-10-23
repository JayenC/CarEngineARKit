using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class Question : MonoBehaviour
{
    public enum QuestionType
    {
        Single,
        Multiple
    }

    public GameObject[] answers;
    public QuestionType qType;

    public bool[] rightAnswer = new bool[ 4 ];  //正确答案
    private bool[] selectAnswer = new bool[ 4 ];  //选择的答案

    private bool isLock = false;
    [HideInInspector]
    public bool isRight = false;  //是否答对

    public Sprite[] sps;

    private void Start()
    {
        answers = new GameObject[ 4 ];
        Button[] btns = transform.GetComponentsInChildren<Button>();
        //得到4个答案
        for( int i = 0; i < 4; i++ )
        {
            btns[ i ].onClick.AddListener(Choose);
            answers[ i ] = btns[ i ].gameObject;
            answers[ i ].GetComponentInChildren<Text>().color = Color.white;
        }

        sps = new Sprite[ 4 ];

        //得到4张图片
        sps[ 0 ] = Resources.Load<Sprite>("default");
        sps[ 1 ] = Resources.Load<Sprite>("choose");
        sps[ 2 ] = Resources.Load<Sprite>("red");
        sps[ 3 ] = Resources.Load<Sprite>("green");

        //默认刷新一下
        HighLightChoose(selectAnswer);
    }

    //选择某一选项
    public void Choose()
    {
        if( isLock )
            return;

        int index = GetChooseIndex();

        if( qType == QuestionType.Single )
        {
            //单选题
            selectAnswer = new bool[ 4 ]; //清空
            selectAnswer[ index ] = true; //让选择的变为true
        }
        else
        {
            //多选题
            selectAnswer[ index ] = !selectAnswer[ index ];  //点一下取反
        }
        HighLightChoose(selectAnswer);
    }
    //选项的数字下标
    public int GetChooseIndex()
    {
        switch( EventSystem.current.currentSelectedGameObject.name )
        {
            case "A":
                return 0;
            case "B":
                return 1;
            case "C":
                return 2;
            case "D":
                return 3;
            default:
                Debug.LogError("Choose name is Not ABCD!");
                break;
        }
        return -1;
    }
    //高亮
    public void HighLightChoose(bool[] selects)
    {
        for( int i = 0; i < answers.Length; i++ )
        {
            if( selects[ i ] )
            {
                //选择
                answers[ i ].GetComponent<Image>().sprite = sps[ 1 ];
            }
            else
            {
                //未选择，默认色
                answers[ i ].GetComponent<Image>().sprite = sps[ 0 ];
            }
        }
    }



    //确认提交
    public void CommitAnswer()
    {
        if( isLock )
            return;

        isRight = IsAnswerRight();
        
        isLock = true; //提交完，锁住
    }

    //判断答案正误
    public bool IsAnswerRight()
    {
        bool isRight = true;

        for( int i = 0; i < rightAnswer.Length; i++ )
        {
            bool equel = ( rightAnswer[ i ] == selectAnswer[ i ] );
            isRight = isRight & equel;
        }

        ChangeColor();

        return isRight;
    }
    //根据对错变颜色
    public void ChangeColor()
    {
        for( int i = 0; i < rightAnswer.Length; i++ )
        {
            if( selectAnswer[ i ] )
                answers[ i ].GetComponent<Image>().sprite = sps[ 2 ];//用户选的变红
            //answers[ i ].GetComponent<Image>().color = Color.red;  
            if( rightAnswer[ i ] )
                answers[ i ].GetComponent<Image>().sprite = sps[ 3 ];//正确答案变绿
            //answers[ i ].GetComponent<Image>().color = Color.green; 
        }
    }
    //判断是否选择了
    public bool IsSelected()
    {
        bool isSelect = false;

        for( int i = 0; i < selectAnswer.Length; i++ )
        {
            isSelect = isSelect | selectAnswer[ i ];
        }
        if( isSelect == false )
            Debug.Log("请选择答案");   //判断是否选择了

        return isSelect;
    }

    public void ResetQuestion()
    {
        isLock = false;

        //按钮变回白色
        for( int i = 0; i < answers.Length; i++ )
        {
            //未选择，默认色
            answers[ i ].GetComponent<Image>().sprite = sps[ 0 ];
        }

        selectAnswer = new bool[ 4 ];
    }
}
