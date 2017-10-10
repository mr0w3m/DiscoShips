using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class HealthUI : MonoBehaviour {

    public Image healthBar;
    
    void Awake()
    {
        healthBar = GetComponent<Image>();
    }
}
