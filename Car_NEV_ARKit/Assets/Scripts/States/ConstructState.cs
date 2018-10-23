using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConstructState : FSMState
{
    public ConstructState(IStateControl control)
    {
        this.stateID = StateID.Construct;
        this.control = control;

        constru_Ctrl = control as ConstructControl;  //转换control类型
        constru_Ctrl.ExploreHandler = ClearSelected;   //注册委托 监听Explore方法 清除Selected
    }
    ~ConstructState()
    {
        constru_Ctrl.ExploreHandler -= ClearSelected;  //注销委托
    }
    //Construct Layer
    private LayerMask HitLayer = 1 << 9;
    private GameObject tempSelected;
    private ConstructControl constru_Ctrl;

    private Material red;
    private Material[] tempMats;

    public override void DoUpdate()
    {
        if( Input.GetMouseButtonDown(0) )
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            //碰到Construct层
            if( Physics.Raycast(ray, out hit, 100, HitLayer) )
            {
                ClearSelected();    //清除上一个

                tempSelected = hit.collider.gameObject;

                MeshRenderer mh = hit.collider.GetComponent<MeshRenderer>();
                tempMats = mh.materials;   //将原本的材质暂存起来

                //材质只能替换用数组去替换
                Material[] mat_arr = new Material[ mh.sharedMaterials.Length ];
                for( int i = 0; i < mat_arr.Length; i++ )
                {
                    mat_arr[ i ] = red;
                }
                mh.sharedMaterials = mat_arr;

                //打开UI
                constru_Ctrl.OpenUI(hit.collider.name);
            }
        }
    }

    public override void DoBeforeEntering()
    {
        base.DoBeforeEntering();


        //加载材质
        if( red == null )
            red = Resources.Load("Material/Red") as Material;
    }

    public override void DoBeforeLeaving()
    {
        base.DoBeforeLeaving();

        ClearSelected();
    }

    //清空上一个颜色UI
    public void ClearSelected()
    {
        if( tempSelected != null )
        {
            //变色
            MeshRenderer mh = tempSelected.GetComponent<MeshRenderer>();

            mh.sharedMaterials = tempMats;   //将之前暂存的材质换回来

            //关闭UI
            constru_Ctrl.CloseUI(tempSelected.name);
        }
    }

}
