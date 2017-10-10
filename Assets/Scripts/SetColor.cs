using UnityEngine;
using System.Collections;

public class SetColor : MonoBehaviour {

    public SpriteRenderer myImage;
    
    public ColorMinerals mineralColorScript;
    public Color[] possibleColors;

    private Color thisColor;

	void Awake ()
    {
        myImage = GetComponentInChildren<SpriteRenderer>();
        thisColor = possibleColors[0];
        myImage.color = thisColor;
	}

    void Start()
    {
        mineralColorScript.SetMineralColor(thisColor);
    }
}
