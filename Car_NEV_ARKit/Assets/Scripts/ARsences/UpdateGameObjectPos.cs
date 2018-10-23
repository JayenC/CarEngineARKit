using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using UnityEngine.XR.iOS;

public class UpdateGameObjectPos : MonoBehaviour
{
    
    public GameObject mScanPlane;

    public GameObject mScanUI;

    public Button mAckButton;

    UnityARGeneratePlane mUnityARGeneratePlane;

    Transform mMainCamera;

    Ray mRay;

    RaycastHit mRaycastHit;

    int mLayerIndex;

    private bool isScan;

    private Vector3 mCurrentPos;


    public GameObject model;

    float offset;

    Touch mTouch1;

    // Use this for initialization
    void Awake()
    {
        mMainCamera = Camera.main.transform;
        mUnityARGeneratePlane = GetComponent<UnityARGeneratePlane>();
        mLayerIndex = LayerMask.NameToLayer("ARKitPlane");
        mAckButton.onClick.AddListener(OnScanButtonClick);
       // mHitCube = HitCubeParend.transform;
    }

    // Update is called once per frame
    void Update()
    {
        if (isScan)
        {
            mRay = new Ray(mMainCamera.position, mMainCamera.forward);

            if (Physics.Raycast(mRay, out mRaycastHit, 100))
            {
                if (mRaycastHit.transform.gameObject.layer == mLayerIndex)
                {
                    mCurrentPos = mRaycastHit.point;
                    SetPlane();
                }
                else
                {
                    SetUI();
                }

            }
            else
            {
                SetUI();
            }
        }
    }

    private void SetUI()
    {
        mScanUI.SetActive(true);
        mScanPlane.SetActive(false);
        mAckButton.gameObject.SetActive(false);
    }

    private void SetPlane()
    {
        mScanUI.SetActive(false);
        mScanPlane.SetActive(true);
        mAckButton.gameObject.SetActive(true);
        mScanPlane.transform.localPosition = mCurrentPos;
    }

    public void OnStart()//start to scan
    {
        SetUI();
        isScan = true;
        mUnityARGeneratePlane.enabled = true;
        model.SetActive(false);
    }

    public void OnScanButtonClick()//scan over
    {

        isScan = false;
        mScanUI.SetActive(false);
        mScanPlane.SetActive(false);
        mAckButton.gameObject.SetActive(false);
        mUnityARGeneratePlane.enabled = false;
        OnScanOver();
    }

    public void OnScanOver()
    {
        model.SetActive(true);

        model.transform.localPosition = mCurrentPos;

        //model.transform.rotation = Quaternion.Slerp(Quaternion.LookRotation(new Vector3(mMainCamera.position.x - transform.position.x, 0, mMainCamera.position.z - transform.position.z)), transform.rotation, Time.deltaTime * 0.1f);

    }
}
