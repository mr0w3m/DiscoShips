using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class SpeedUI : MonoBehaviour {

    public Image speedBar;
    
    void Awake()
    {
        speedBar = GetComponent<Image>();
    }
}
