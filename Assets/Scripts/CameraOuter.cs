using UnityEngine;
using System.Collections;

public class CameraOuter : MonoBehaviour {

    private CameraBehavior cameraScript;

    void Start()
    {
        cameraScript = GetComponentInParent<CameraBehavior>();
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player1(Black)" || other.tag == "Player2(White)")
        {
            cameraScript.cameraState = 0;
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.tag == "Player1(Black)" || other.tag == "Player2(White)")
        {
            cameraScript.cameraState = 1;
        }
    }
}
