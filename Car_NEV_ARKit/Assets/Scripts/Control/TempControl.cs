using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TempControl : MonoBehaviour, IStateControl
{
    public void InitState()
    {
        this.gameObject.SetActive(true);
    }

    public void ResetState()
    {
        this.gameObject.SetActive(false);
    }
}