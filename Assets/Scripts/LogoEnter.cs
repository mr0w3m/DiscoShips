using UnityEngine;
using System.Collections;

public class LogoEnter : MonoBehaviour {

    public int type;

	void Start ()
    {
	
	}
	
	void Update ()
    {
        if (transform.localScale.x < 1.5f)
        {
            transform.localScale = Vector3.Lerp(transform.localScale, new Vector3(1.6f, 1.6f, 1.6f), 0.75f * Time.deltaTime);
        }
    }
}
