using UnityEngine;
using System.Collections;

public class SelectScreenCamera : MonoBehaviour {

    public Vector3 cameraSelectPlayerPos;
    public Vector3 cameraMapSelectPos;

    public float smoothing;

    private Vector3 goToPos;

    void Start()
    {
        PlayerSelect();
    }

    public void MapSelect()
    {
        goToPos = cameraMapSelectPos;
    }

    public void PlayerSelect()
    {
        goToPos = cameraSelectPlayerPos;
    }

	void Update()
    {
        transform.position = Vector3.Lerp(transform.position, goToPos, smoothing * Time.deltaTime);
    }


}
