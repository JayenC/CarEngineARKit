using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IStateControl
{
    void InitState();  //状态初始化工作
    void ResetState();    //把状态还原
}
