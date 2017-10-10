using UnityEngine;
using System.Collections;

public class SlowSpin : MonoBehaviour {

    public float smoothing;

    public bool map;

    private bool startRotation;

	// Use this for initialization
	void Start ()
    {
        switch (map)
        {
            case (true):
                smoothing = RandomNumberFunc();
                startRotation = false;
                break;
            case (false):
                startRotation = true;
                break;
        }
	}

    int RandomNumberFunc()
    {
        int i = Random.Range(-7, 7);
        while (i == 0)
        {
            Debug.Log(i);
            i = Random.Range(-7, 7);
        }
        return i;
    }

    public void StartMapRotation()
    {
        startRotation = true;
    }
	
	// Update is called once per frame
	void FixedUpdate ()
    {
        switch(startRotation)
        {
            case (true):
                transform.Rotate(0, 0, smoothing * Time.fixedDeltaTime);
                break;
            case (false):
                break;
        }
    }
}
