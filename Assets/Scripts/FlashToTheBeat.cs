using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class FlashToTheBeat : MonoBehaviour {

    public int lightType;
    private bool flashOn;
    private SpriteRenderer flashImage;
    public Color[] flashColor;
    public float flashSpeed;
    private WaitForSeconds waitTime;

    public LightCommand commandScript;

    void Awake()
    {
        flashImage = GetComponent<SpriteRenderer>();
        waitTime = new WaitForSeconds(60.0f / 100.0f);
    }

	void Start ()
    {
        StartCoroutine(Flash());
	}
	
	void FixedUpdate ()
    {
        if (flashOn == true)
        {
            if (lightType == 2 || lightType == 3)
            {
                flashImage.color = commandScript.selectedColor;
            }
            else
            {
                //flashImage.color = flashColor;
                int randomInt;
                randomInt = Random.Range(0, 4);
                flashImage.color = flashColor[randomInt];
                //Or random color from an array of usable colors.
            }
        }
        else
        {
            //flashImage.color = Color.Lerp(flashImage.color, Color.clear, flashSpeed * Time.fixedDeltaTime);
        }
        flashOn = false;

	}

    IEnumerator Flash()
    {
        while (true)
        {
            if (lightType == 0 || lightType == 3)
            {
                int num;
                num = Random.Range(0, 3);
                if (num == 0 || num == 1)
                {
                    flashOn = true;
                }
                else
                {
                    flashOn = false;
                }
            }
            else if(lightType == 1 || lightType == 2)
            {
                flashOn = true;
            }
            
            yield return waitTime;
        }
    }
}
