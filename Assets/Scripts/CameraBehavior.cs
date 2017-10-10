using UnityEngine;
using System.Collections;

public class CameraBehavior : MonoBehaviour {

    public int cameraState;
    public float cameraSmooth;

    public BoxCollider2D innerCollider;
    public BoxCollider2D outerCollider;

    private Camera thisCamera;
    
    void Start()
    {
        thisCamera = GetComponent<Camera>();
    }

    void FixedUpdate()
    {
        if(cameraState == 1)
        {
            thisCamera.orthographicSize = Mathf.Lerp(thisCamera.orthographicSize, thisCamera.orthographicSize + 0.5f, cameraSmooth * Time.deltaTime);
            //thisCamera.transform.localScale = Vector3.Lerp(thisCamera.transform.localScale, thisCamera.transform.localScale + (2 * thisCamera.orthographicSize), cameraSmooth * Time.deltaTime);

            float innerSizeX;
            float innerSizeY;
            float outerSizeX;
            float outerSizeY;

            innerSizeX = Mathf.Lerp(innerCollider.size.x, innerCollider.size.x + 0.5f, cameraSmooth * Time.deltaTime);
            innerSizeY = Mathf.Lerp(innerCollider.size.y, innerCollider.size.y + 0.5f, cameraSmooth * Time.deltaTime);
            innerCollider.size = new Vector2(innerSizeX, innerSizeY);
            outerSizeX = Mathf.Lerp(outerCollider.size.x, outerCollider.size.x + 0.5f, cameraSmooth * Time.deltaTime);
            outerSizeY = Mathf.Lerp(outerCollider.size.y, outerCollider.size.y + 0.5f, cameraSmooth * Time.deltaTime);
            outerCollider.size = new Vector2(outerSizeX, outerSizeY);
        }
        else if(cameraState == 2)
        {
            thisCamera.orthographicSize = Mathf.Lerp(thisCamera.orthographicSize, thisCamera.orthographicSize - 0.5f, cameraSmooth * Time.deltaTime);

            //thisCamera.transform.localScale = Vector3.Lerp(thisCamera.transform.localScale, thisCamera.transform.localScale - (2 * thisCamera.orthographicSize), cameraSmooth * Time.deltaTime);
            float innerSizeX;
            float innerSizeY;
            float outerSizeX;
            float outerSizeY;

            innerSizeX = Mathf.Lerp(innerCollider.size.x, innerCollider.size.x - 0.5f, cameraSmooth * Time.deltaTime);
            innerSizeY = Mathf.Lerp(innerCollider.size.y, innerCollider.size.y - 0.5f, cameraSmooth * Time.deltaTime);
            innerCollider.size = new Vector2(innerSizeX, innerSizeY);
            outerSizeX = Mathf.Lerp(outerCollider.size.x, outerCollider.size.x - 0.5f, cameraSmooth * Time.deltaTime);
            outerSizeY = Mathf.Lerp(outerCollider.size.y, outerCollider.size.y - 0.5f, cameraSmooth * Time.deltaTime);
            outerCollider.size = new Vector2(outerSizeX, outerSizeY);
        }
        else
        {
            
        }
    }
}
