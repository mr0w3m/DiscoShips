using UnityEngine;
using System.Collections;

public class DetectStartScript : MonoBehaviour {

    public GameManager gameManager;
    private bool theSwitch;

    void Awake()
    {
        theSwitch = false;
    }

    void Update()
    {
        if (Input.GetAxis("BlackStartButton") > 0.01 || Input.GetAxis("WhiteStartButton") > 0.01 || Input.GetAxis("Player3StartButton") > 0.01 || Input.GetAxis("Player4StartButton") > 0.01)
        {
            if (!theSwitch && gameManager.selectedMap > 0)
            {
                theSwitch = true;
                gameManager.SelectScreenStart();
            }
        }
	}
}
