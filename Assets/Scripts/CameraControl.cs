using UnityEngine;
using System.Collections;

public class CameraControl : MonoBehaviour
{

    public Camera myCamera;
    public float smallSize;
    public float largeSize;

    private float size;
    private float difference;

    public float smoothing;

    void Awake()
    {
        size = largeSize;
        myCamera = GetComponent<Camera>();
    }

    void Update()
    {
        Zoom();
        if (Input.GetKeyDown(KeyCode.A))
        {
            size = smallSize;
        }

        if (Input.GetKeyDown(KeyCode.S))
        {
            size = largeSize;
        }
    }

    void Zoom()
    {
        difference = Mathf.Abs(size - myCamera.orthographicSize);
        if (difference < 0.05f)
        {

        }
        else
        {
            myCamera.orthographicSize = Mathf.Lerp(myCamera.orthographicSize, size, smoothing * Time.deltaTime);
        }
    }
}
