using UnityEngine;
using System.Collections;

public class LerpToNone : MonoBehaviour {

    public float lerpSpeed;
    private SpriteRenderer mySprite;

	void Awake ()
    {
        mySprite = GetComponent<SpriteRenderer>();   
	}
    
	void Update ()
    {
        mySprite.color = Color.Lerp(mySprite.color, Color.clear, lerpSpeed * Time.deltaTime);
	}
}
