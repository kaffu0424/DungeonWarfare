using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

[System.Serializable]
public struct CameraLimit
{
    public float left;
    public float right;
    public float bottom;
    public float top;
    public Vector3 cameraPosition;
}

public class CameraController : MonoBehaviour
{
    #region var
    private Vector2 right;
    private Vector2 left;
    private Vector2 top;
    private Vector2 bottom;
    private float hori;
    private float vert;
    private float scroll;
    #endregion

    private Camera playerCamera;
    private Rigidbody2D cameraRigidbody;

    [SerializeField] private float cameraZoomLimit_min;
    [SerializeField] private float cameraZoomLimit_max;
    [SerializeField] private float cameraZoomSpeed;
    [Range(min:0, max:1000)]
    [SerializeField] private float cameraMoveSpeed;

    [SerializeField] List<CameraLimit> cameraLimits = new List<CameraLimit>();

    [SerializeField] private CameraLimit currentLimit;


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

    }
    void CameraMove()
    {
        hori = Input.GetAxisRaw("Horizontal");
        vert = Input.GetAxisRaw("Vertical");

        cameraRigidbody.velocity = new Vector2(hori, vert) * Time.deltaTime * cameraMoveSpeed;
        CameraLimit();
    }

    void CameraLimit()
    {
        right = Camera.main.ScreenToWorldPoint(new Vector2(Screen.width, Screen.height * 0.5f));
        left = Camera.main.ScreenToWorldPoint(new Vector2(0, Screen.height * 0.5f));
        top = Camera.main.ScreenToWorldPoint(new Vector2(Screen.width * 0.5f, Screen.height));
        bottom = Camera.main.ScreenToWorldPoint(new Vector2(Screen.width * 0.5f, 0));

        if(right.x > currentLimit.right)
            transform.position = new Vector3(transform.position.x - 0.1f, transform.position.y, -10);

        if (left.x < currentLimit.left)
            transform.position = new Vector3(transform.position.x + 0.1f, transform.position.y, -10);

        if (top.y > currentLimit.top)
            transform.position = new Vector3(transform.position.x, transform.position.y - 0.1f, -10);

        if (bottom.y < currentLimit.bottom)
            transform.position = new Vector3(transform.position.x, transform.position.y + 0.1f, -10);
    }

    void CameraZoom()
    {
        scroll = Input.GetAxis("Mouse ScrollWheel") * cameraZoomSpeed;

        playerCamera.orthographicSize += -scroll;

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

