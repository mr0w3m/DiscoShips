using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using Rewired;

public class StartButton : MonoBehaviour {

    public int testScene;

    private Player rWPlayer1;
    private Player rWPlayer2;
    private Player rWPlayer3;
    private Player rWPlayer4;

    void OnMouseDown()
    {
        Application.LoadLevel(2);
        Debug.Log("ButtonPressed");
    }

    void Awake()
    {
        //Rewired
        rWPlayer1 = ReInput.players.GetPlayer(0);
        rWPlayer2 = ReInput.players.GetPlayer(1);
        rWPlayer3 = ReInput.players.GetPlayer(2);
        rWPlayer4 = ReInput.players.GetPlayer(3);
    }

    void Update()
    {
        /*
        if (rWPlayer1.GetButtonDown("AButton") || rWPlayer2.GetButtonDown("AButton") || rWPlayer3.GetButtonDown("AButton") || rWPlayer4.GetButtonDown("AButton"))
        {
            Application.LoadLevel(2);
        }
        */

        if (Input.GetKeyDown(KeyCode.M))
        {
            Application.LoadLevel(testScene);
        }
    }
}
