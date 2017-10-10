using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class UIMaster : MonoBehaviour {

    //Classes
    //private HealthUI healthUI;
    private ShieldsUI shieldsUI;
    private SpeedUI speedUI;
    private NumUI numUI;

    //meters
    //public Image healthBar;
    public Image shieldsBar;
    public Image speedBar;

    //numbers
    public Text healthNum;

    //PlayerNum
    public Image playerNum;

    //Spinner
    public Image playerSpinner;

    //Meter
    public Image abilityMeter;

    public Image glow;

    void Start()
    {
        //healthUI = GetComponentInChildren<HealthUI>();
        shieldsUI = GetComponentInChildren<ShieldsUI>();
        speedUI = GetComponentInChildren<SpeedUI>();
        numUI = GetComponentInChildren<NumUI>();

        //healthBar = healthUI.healthBar;
        shieldsBar = shieldsUI.shieldsBar;
        speedBar = speedUI.speedBar;

        healthNum = numUI.healthNum;
    }	
}
