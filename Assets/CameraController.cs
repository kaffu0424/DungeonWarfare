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
        CameraZoom(); // ī�޶� �� ��/�ƿ�
        CameraMove(); // ī�޶� ������
    }

    void CameraMove()
    {
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");

        transform.localPosition += new Vector3(horizontal, vertical, 0) * Time.deltaTime * camera_moveSpeed;

        CameraLimit();
        /*
        ## TODO
         1. ī�޶� ������ �ε巴�� 
         2. ī�޶� ������ ������ �������� Ÿ�ϸ� ũ�⿡ ���߱�.
         */
    }
    void CameraZoom()
    {
        float zoomAmount = Input.GetAxis("Mouse ScrollWheel") * camera_zoomSpeed;
        /*
        ## TODO
         1. ī�޶� �� ��/�ƿ� ��� �����
         2. ī�޶� size�� ���� zoomLimit_min,max
         */
    }
    void CameraLimit()
    {

    }
}
