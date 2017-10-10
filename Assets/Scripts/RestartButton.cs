using UnityEngine;
using System.Collections;
using Rewired;

public class RestartButton : MonoBehaviour {

    public GameManager gameManagerScript;

    public Vector3 selectedSize;
    public Vector3 notSelectedSize;

    public Vector3 menuALoc;
    public Vector3 selectALoc;
    public Vector3 rematchALoc;

    public float selectMoveCooldownTime;
    private float selectMoveCooldown;

    //The button currently selected, corresponding to the actual scene it represents.
    private int selectedButton; // 0-MainMenu 1-SelectScreen 2-Rematch
    private int selectedScene;

    public GameObject menuButton;
    public GameObject selectScreenButton;
    public GameObject rematchButton;

    public GameObject menuFxGO;
    public GameObject selectScreenFxGO;
    public GameObject rematchFxGO;

    private Player rWPlayer1;
    private Player rWPlayer2;
    private Player rWPlayer3;
    private Player rWPlayer4;

    
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
        if (selectMoveCooldown > 0)
        {
            //If we have cooldown left, subtract from it
            selectMoveCooldown--;
        }

        //Move cooldown is ready to accept input.
        if (selectMoveCooldown < 1)
        {
            //MoveRight
            if (rWPlayer1.GetAxis("MoveVertical") > 0.4f || rWPlayer2.GetAxis("MoveVertical") > 0.4f || rWPlayer3.GetAxis("MoveVertical") > 0.4f || rWPlayer4.GetAxis("MoveVertical") > 0.4f)
            {
                //Move cursor right
                //Debug.Log("MovedRight");
                selectMoveCooldown = selectMoveCooldownTime;
                Select(-1);
            }

            //MoveLeft
            if (rWPlayer1.GetAxis("MoveVertical") < -0.4f || rWPlayer2.GetAxis("MoveVertical") < -0.4f || rWPlayer3.GetAxis("MoveVertical") < -0.4f || rWPlayer4.GetAxis("MoveVertical") < -0.4f)
            {
                //Move cursor left
                //Debug.Log("MovedLeft");
                selectMoveCooldown = selectMoveCooldownTime;
                Select(+1);
            }
        }
        DetectInput();
        DetectButton();
    }

    void DetectButton()
    {
        switch(selectedButton)
        {
            case (0):
                //MainMenuSelected
                selectedScene = 0;

                menuFxGO.SetActive(true);
                selectScreenFxGO.SetActive(false);
                rematchFxGO.SetActive(false);

                menuButton.transform.localScale = selectedSize;
                selectScreenButton.transform.localScale = notSelectedSize;
                rematchButton.transform.localScale = notSelectedSize;
                break;
            case (1):
                //SelectScreenSelected
                selectedScene = 1;

                menuFxGO.SetActive(false);
                selectScreenFxGO.SetActive(true);
                rematchFxGO.SetActive(false);

                menuButton.transform.localScale = notSelectedSize;
                selectScreenButton.transform.localScale = selectedSize;
                rematchButton.transform.localScale = notSelectedSize;
                break;
            case (2):
                //RematchMapSelected
                selectedScene = gameManagerScript.selectedMap;

                menuFxGO.SetActive(false);
                selectScreenFxGO.SetActive(false);
                rematchFxGO.SetActive(true);


                menuButton.transform.localScale = notSelectedSize;
                selectScreenButton.transform.localScale = notSelectedSize;
                rematchButton.transform.localScale = selectedSize;
                break;
        }
    }

    void DetectInput()
    {
        if (rWPlayer1.GetButton("AButton") || rWPlayer1.GetButton("StartButton"))
        {
            gameManagerScript.RestartClicked(selectedScene);
        }
        else if (rWPlayer2.GetButton("AButton") || rWPlayer2.GetButton("StartButton"))
        {
            gameManagerScript.RestartClicked(selectedScene);
        }
        else if (rWPlayer3.GetButton("AButton") || rWPlayer3.GetButton("StartButton"))
        {
            gameManagerScript.RestartClicked(selectedScene);
        }
        else if (rWPlayer4.GetButton("AButton") || rWPlayer4.GetButton("StartButton"))
        {
            gameManagerScript.RestartClicked(selectedScene);
        }
    }

    void Select(int input)
    {
        //If selected
        switch(input)
        {
            case (1):
                selectedButton += 1;
                break;
            case (-1):
                selectedButton -= 1;
                break;
        }
        if (selectedButton > 2)
        {
            selectedButton = 0;
        }
        else if (selectedButton < 0)
        {
            selectedButton = 2;
        }
        //Debug.Log(selectedButton);
    }
}
