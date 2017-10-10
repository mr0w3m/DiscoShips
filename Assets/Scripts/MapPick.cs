using UnityEngine;
using System.Collections;

public class MapPick : MonoBehaviour {

    public int sceneID; //Directly corresponds to Scene Number in build settings, (2 - Plain)(3 - Ice)
    public GameManager gameManager;
    public SpriteRenderer selectedLight;
    public SlowSpin spinScript;

    void Awake()
    {
        spinScript = GetComponent<SlowSpin>();
    }

	void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player1(Black)" || other.tag == "Player2(White)" || other.tag == "Player3" || other.tag == "Player4")
        {
            //Set the current map to this one.
            gameManager.SelectMap(sceneID);
        }
    }

    void Update()
    {
        if (gameManager.selectedMap == sceneID)
        {
            selectedLight.enabled = true;
            spinScript.enabled = true;
        }
        else
        {
            selectedLight.enabled = false;
            spinScript.enabled = false;
        }
    }
}
