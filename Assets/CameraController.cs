using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class CameraController : MonoBehaviour
{
    [SerializeField] Camera cam;
    [SerializeField] Vector3 startPos;

    [Range(min:1.0f, max: 5.0f)]
    [SerializeField] private float camera_moveSpeed;

    [Range(min: 1.0f, max: 5.0f)]
    [SerializeField] private float camera_zoomSpeed;
    [SerializeField] private float zoomLimit_min;
    [SerializeField] private float zoomLimit_max;
    private void Start()
    {
        cam = GetComponent<Camera>();
        startPos = new Vector3(0,0,-10);
        zoomLimit_min = 5.0f;
        zoomLimit_min = 7.0f;
    }

    private void Update()
    {
        CameraZoom(); // 카메라 줌 인/아웃
        CameraMove(); // 카메라 움직임
    }

    void CameraMove()
    {
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");

        transform.localPosition += new Vector3(horizontal, vertical, 0) * Time.deltaTime * camera_moveSpeed;

        CameraLimit();
        /*
        ## TODO
         1. 카메라 움직임 부드럽게 
         2. 카메라 움직임 범위를 스테이지 타일맵 크기에 맞추기.
         */
    }
    void CameraZoom()
    {
        float zoomAmount = Input.GetAxis("Mouse ScrollWheel") * camera_zoomSpeed;
        /*
        ## TODO
         1. 카메라 줌 인/아웃 기능 만들기
         2. 카메라 size에 제한 zoomLimit_min,max
         */
    }
    void CameraLimit()
    {

    }
}
