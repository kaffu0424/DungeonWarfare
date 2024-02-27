using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

[System.Serializable]
public struct CameraLimit
{
    public float x_min;
    public float x_max;
    public float y_min;
    public float y_max;
    public Vector3 cameraPosition;
}

public class CameraController : MonoBehaviour
{
    // 1. 휠 사용 -> 화면 확대 축소
    // 2. 방향키 -> 카메라 움직임
    // 3. 카메라 확대 최소 4 최대 7 or 8
    // 4. 타일맵 밖으로 카메라 X

    private Camera playerCamera;
    private Rigidbody2D cameraRigidbody;

    [SerializeField] private float cameraZoomLimit_min;
    [SerializeField] private float cameraZoomLimit_max;
    [SerializeField] private float cameraZoomSpeed;
    [Range(min:0, max:1000)]
    [SerializeField] private float cameraMoveSpeed;

    [SerializeField] List<CameraLimit> cameraLimits = new List<CameraLimit>();

    [SerializeField] private CameraLimit currentLimit;

    public GameObject test_top;
    public GameObject test_right;
    public GameObject test_left;
    public GameObject test_bottom;
    
    private void Start()
    {
        playerCamera = GetComponent<Camera>();
        cameraRigidbody = GetComponent<Rigidbody2D>();
        cameraZoomLimit_min = 4;
        cameraZoomLimit_max = 8;
        cameraZoomSpeed = 3.0f;
    }

    private void Update()
    {
        CameraZoom();
        CameraMove();

        Vector2 right = Camera.main.ScreenToWorldPoint(new Vector2(Screen.width, Screen.height * 0.5f));
        Vector2 left = Camera.main.ScreenToWorldPoint(new Vector2(0, Screen.height * 0.5f));
        Vector2 top = Camera.main.ScreenToWorldPoint(new Vector2(Screen.width*0.5f, Screen.height));
        Vector2 bottom = Camera.main.ScreenToWorldPoint(new Vector2(Screen.width * 0.5f, 0));

        test_top.transform.position = top;
        test_right.transform.position = right;
        test_left.transform.position = left;
        test_bottom.transform.position = bottom;

        //TODO:카메라범위제한
        // top,right,left,bottom position 받아오기 완료
        // currentLimit의 값을 벗어나지않도록 구현하기.
    }
    void CameraMove()
    {
        float hori = Input.GetAxisRaw("Horizontal");
        float vert = Input.GetAxisRaw("Vertical");

        cameraRigidbody.velocity = new Vector2(hori, vert) * Time.deltaTime * cameraMoveSpeed;
    }

    void CameraZoom()
    {
        float _scroll = Input.GetAxis("Mouse ScrollWheel") * cameraZoomSpeed;

        playerCamera.orthographicSize += -_scroll;

        if (playerCamera.orthographicSize >= cameraZoomLimit_max)
            playerCamera.orthographicSize = cameraZoomLimit_max;

        if (playerCamera.orthographicSize <= cameraZoomLimit_min)
            playerCamera.orthographicSize = cameraZoomLimit_min;
    }

    public void SetCameraLimit(int _idx)
    {
        currentLimit = cameraLimits[_idx];
        this.transform.position = currentLimit.cameraPosition;
    }

}

