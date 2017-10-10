using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ShieldsUI : MonoBehaviour {

    public Image shieldsBar;

    void Awake()
    {
        shieldsBar = GetComponent<Image>();
	}
	
}
