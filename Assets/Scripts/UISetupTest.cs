using UnityEngine;
using System.Collections;

public class UISetupTest : MonoBehaviour {

    public GameObject P1UI;
    private GameObject copy;
    private RectTransform rectTransform;

    // Use this for initialization
    void Start()
    {
        copy = Instantiate(P1UI, transform.position, Quaternion.identity) as GameObject;
        copy.transform.SetParent(this.transform);
        rectTransform = copy.GetComponent<RectTransform>();
        
    }

    void Update()
    {
        if (Input.GetKey(KeyCode.X))
        {
            SetPosition();
        }
    }

    void SetPosition()
    {
        Debug.Log(rectTransform.position);
        rectTransform.position = new Vector2(120, 328);
    }
}
