using UnityEngine;
using Lean.Touch;
using System.Collections;
using UnityEngine.EventSystems;
using System.Collections.Generic;

public class CameraCtrl : MonoBehaviour
{
    public static CameraCtrl Instance { get; private set; }

    private const float rOffset = 0.5f;

    private const float speed = 6f;
    private const float minY = 0.8f;
    private const float maxY = 2.4f;

    private const float _angle = 45f;

    private Camera _mainCam;
    private Transform _mainTran;

	bool _isOnPinch = false;

    private void Awake()
    {
        Instance = this;

        _mainCam = Camera.main;



        _mainTran = _mainCam.transform;

        
    }

    private void LateUpdate()
    {
        _isOnPinch = false;
        var fingers = LeanSelectable.GetFingers(true, false);
        if (fingers.Count == 1)
        {
            Vector2 offset = LeanGesture.GetScaledDelta();
            _CameraMove(offset);
        }
        else if (fingers.Count == 2)
        {
            _isOnPinch = true;
            float pinchRatio = LeanGesture.GetPinchRatio(fingers);
            //_AdjustFov(1 - pinchRatio);
            _CameraZoom(1 - pinchRatio);
            //Vector2 offset = LeanGesture.GetScaledDelta();
            //_CameraZoom(offset.y * 30 / Screen.height);
        }
        if (Input.GetAxis("Mouse ScrollWheel") != 0)
        {
            _isOnPinch = true;
            float dis = Input.GetAxis("Mouse ScrollWheel");
            _CameraZoom(dis);
        }
        //_MouseCheck();
        //_TouchCheck();
    }

    #region 手势事件
    private float fov_min = 60f;
    private float fov_max = 100f;
    private void _AdjustFov(float dis)
    {
        float fov = _mainCam.fieldOfView + dis * 5f;
        _mainCam.fieldOfView = Mathf.Clamp(fov, fov_min, fov_max);
    }

    private float minX = float.MinValue;
    private float maxX = float.MaxValue;
    private float minZ = float.MinValue;
    private float maxZ = float.MaxValue;
    private void _DoubleTapEvent()
    {
        //Debug.Log("双击:");
        //_mainTran.position = startPosition;
        //_mainTran.eulerAngles = startEulerAngles;
        //_mainCam.fieldOfView = fov_min;
    }

    private void _CameraMove(Vector2 offset)
    {
        Vector3 calEua = _mainTran.eulerAngles + new Vector3(-offset.y / Screen.height, offset.x / Screen.width, 0.0f) * 50f * speed;
        //Quaternion q2 = Quaternion.Euler(calEua);
        if (calEua.x > 180) calEua.x -= 360;
        calEua.x = Mathf.Clamp(calEua.x, -_angle, _angle);
        _mainTran.eulerAngles = calEua;
    }

    private void _CameraZoom(float dis)
    {
        Vector3 forward = _mainTran.forward;
        //forward.y = 0;
        Vector3 newPos = _mainTran.position + forward.normalized * dis * speed * 0.5f;

        //newPos.x = Mathf.Clamp(newPos.x, minX, maxX);
        //newPos.z = Mathf.Clamp(newPos.z, minZ, maxZ);
        //newPos.y = Mathf.Clamp(newPos.y, minY, maxY);        //if (newPos.x >= minX && newPos.x <= maxX && newPos.z >= minZ && newPos.z <= maxZ/* && newPos.y >= minY && newPos.y <= maxY */)
        {
            _mainTran.position = newPos;
        }
    }
    #endregion
}

[System.Serializable]
public class SpaceConfig
{
    public long id = 0;
    public Vector3 pos = Vector3.zero;
    public Vector3 eua = Vector3.zero;
    public Vector3 center = Vector3.zero;
    public float r_width = 16f;
    public float r_height = 9f;
    public float angle = 0;
    public float ol_height = 5.5f;
}