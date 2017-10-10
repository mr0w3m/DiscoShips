using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class TeleBeacon : MonoBehaviour
{
    //Health
    private int beaconHealth;
    //Timer
    private float timer;
    private float otherTimer;
    //TimeLeft rapidly decreasing circle.
    private Image circleImg;

    private float fill;
    private float convertedFill;

    //decrease Rate
    public float decreaseRate;
    public float smoothing;

    void Awake()
    {
        circleImg = GetComponentInChildren<Image>();
    }

    void Start()
    {
        fill = 100.0f;
    }

    void Update()
    {
        otherTimer += Time.deltaTime;
        TimerFunk();
        MonitorFill();
        TimedDestroy();
    }

    void MonitorFill()
    {
        convertedFill = fill * 0.01f;
        circleImg.fillAmount = Mathf.Lerp(circleImg.fillAmount, convertedFill, smoothing * Time.deltaTime);
    }

    void TimerFunk()
    {
        timer += Time.deltaTime;

        if (timer > 1)
        {
            //Debug.Log(timer + "fill: " + fill);
            timer = 0.0f;
            fill -= decreaseRate;
        }
    }

    void TimedDestroy()
    {
        if (circleImg.fillAmount < 0.01f)
        {
            Destroy(this.gameObject);
        }
    }
    
    /*
    I might add this later. But for right now we'll keep it simple and make the beacon just disolve over time.
    //DamageHealth
    public void damageBeacon(int amt)
    {
        beaconHealth -= amt;
    }
    */
}
