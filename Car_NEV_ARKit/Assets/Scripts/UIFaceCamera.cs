using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIFaceCamera : MonoBehaviour
{
    private Transform cameraPos;
    Vector3 tempPos;

    //跟踪位置------------
    public GameObject followTarget;
    public float offset_Y = 0.3f;
    public float offset_X = 0;
    public float offset_Z = 0;
    public Transform findRoot;

    private void Start()
    {
        cameraPos = Camera.main.transform;

        //在root下查找名字一样的物体，并跟随
        followTarget = GetTransform(findRoot, this.gameObject.name).gameObject;
        if( followTarget == null )
            Debug.LogError("No " + this.gameObject.name + " find ");
    }

    //查找所有子物体
    Transform GetTransform(Transform trans, string name)
    {
        Transform forreturn = null;

        foreach( Transform t in trans.GetComponentsInChildren<Transform>() )
        {
            if( t.name == name )
            {
                forreturn = t;
                return t;

            }

        }
        return forreturn;
    }

    void Update()
    {
        tempPos = cameraPos.position;

        tempPos.y = transform.position.y;

        //Y轴指向
        transform.LookAt(tempPos);


        //跟随xz
        float targetY = followTarget.transform.position.y + offset_Y + ( findRoot.transform.localScale.x - 1 ) * 0.1f * ( offset_Y / Mathf.Abs(offset_Y) );
        Vector3 temp = followTarget.transform.position + (followTarget.transform.right * offset_X + followTarget.transform.forward * offset_Z) * followTarget.transform.localScale.x;

        Vector3 pos = new Vector3(temp.x, targetY, temp.z);
        this.transform.position = pos;
    }
}
