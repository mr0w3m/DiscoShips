using UnityEngine;
using System.Collections;

public class Shake : MonoBehaviour {

    private float shakeTimer;
    private float shakeAmount;

    private Vector2 shakePos;
    private Vector2 startPos;

    void Start ()
    {
        startPos = transform.position;
	}
	
    void Update()
    {
        if (shakeTimer >= 0)
        {
            shakePos = Random.insideUnitCircle * shakeAmount;
            transform.position = new Vector2(transform.position.x + shakePos.x, transform.position.y + shakePos.y);
            shakeTimer -= Time.deltaTime;
        }
    }

    public void ShakeThis(float shakePower, float shakeTime)
    {
        shakeAmount = shakePower;
        shakeTimer = shakeTime;
    }
}
