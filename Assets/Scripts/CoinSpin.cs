using UnityEngine;
using System.Collections;

public class CoinSpin : MonoBehaviour {
    
    private Vector3 vector = new Vector3(0.0f, 380.0f, 0.0f);
    private bool done;

    void Update()
    {
        if (transform.localRotation.y < 360f)
        {
            transform.eulerAngles = Vector3.Lerp(transform.eulerAngles, vector, 0.75f * Time.deltaTime);

        }
    }
}
