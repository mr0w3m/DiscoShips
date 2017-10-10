using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ColorMinerals : MonoBehaviour {

    private SpriteRenderer mySprite;
    
	void Awake()
    {
        mySprite = GetComponent<SpriteRenderer>();
	}
	
    public void SetMineralColor(Color color)
    {
        mySprite.color = color;
    }
}
