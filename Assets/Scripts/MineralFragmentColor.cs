using UnityEngine;
using System.Collections;

public class MineralFragmentColor : MonoBehaviour {

    private SpriteRenderer myImage;
    public Color[] possibleColors;
    
    void Start()
    {
        myImage = GetComponentInChildren<SpriteRenderer>();
        Color thisColor;
        thisColor = possibleColors[Random.Range(0, 4)];
        myImage.color = thisColor;
    }
}
