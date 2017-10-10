using UnityEngine;
using System.Collections;

public class SlideFollow : MonoBehaviour {

    //Keeps track of player position
    //and responds.

    //So first thing is knowing the player's position. possibly a connection to the gamemanager?

    public GameManager gameManagerScript;
    public int playerNumAssociated;
    public float smoothing;

    public SpriteRenderer lightsSprite;
    public SpriteRenderer leftArrow;
    public SpriteRenderer rightArrow;

    public SpriteRenderer weapDpadSprite;
    public SpriteRenderer abilDpadSprite;
    public SpriteRenderer teamDpadSprite;

    public SpriteRenderer weapText;
    public SpriteRenderer abilText;
    public SpriteRenderer teamText;

    public SpriteRenderer weapBox;
    public SpriteRenderer abilBox;
    public SpriteRenderer startText;

    //We're going to have to get tricky for color block fades new Color(teamColorBlock.color.a, teamColorBlock.color.a, teamColorBlock.color.a, 0.3);

    //Things to Color
    public SpriteRenderer playerText;
    public SpriteRenderer teamColorBlock;
    private Color colorBlockColor;
    public SpriteRenderer startBox;
    private Color startBoxColor;
    public SpriteRenderer backPanel;
    public SpriteRenderer backGradient;

    private Vector2 weaponPos = new Vector2(0, 0f);
    private Vector2 abilityPos = new Vector2(0, -12.9f);
    private Vector2 teamPos = new Vector2(0, -21.7f);

    public Color fadeColor = new Color(1f, 1f, 1f, 0.3f);
    public Color grayColor;

    public Animator doorAnim;
    public GameObject pressA;

    public GameObject deadPlayer;
    private GameObject deadPlayerCopy;
    public GameObject deadPlayerSpawnPos;
    

    public void PlayerEntered()
    {
        doorAnim.SetTrigger("playerEnter");
        deadPlayerCopy = Instantiate(deadPlayer, deadPlayerSpawnPos.transform.position, Quaternion.identity)as GameObject;
        pressA.SetActive(false);
    }

    public void PlayerExited()
    {
        //Not in there yetVV
        doorAnim.SetTrigger("playerExit");
        Destroy(deadPlayerCopy);
        pressA.SetActive(true);
    }

    void Update()
    {
        //Monitor New Player Enter


        switch(playerNumAssociated)
        {
            case (1):
                if (gameManagerScript.player1Active)
                {
                    if (gameManagerScript.player1Copy != null)
                    {
                        MonitorPlayerPosition(gameManagerScript.player1Copy.transform.position, 1);
                    }
                }
                else
                {
                    //Display press A to enter
                    //BlastDoors are closed


                    //To be neutral
                    //background
                    backGradient.color = grayColor;
                    //startbackground
                    startBox.color = grayColor;
                    //PlayerName
                    playerText.color = grayColor;
                    //Back panel
                    backPanel.color = grayColor;
                    

                    //LerpToClear
                    lightsSprite.color = Color.Lerp(lightsSprite.color, Color.clear, smoothing * Time.deltaTime);
                    leftArrow.color = Color.Lerp(leftArrow.color, Color.clear, smoothing * Time.deltaTime);
                    rightArrow.color = Color.Lerp(rightArrow.color, Color.clear, smoothing * Time.deltaTime);

                    weapDpadSprite.color = Color.Lerp(weapDpadSprite.color, Color.clear, smoothing * Time.deltaTime);
                    abilDpadSprite.color = Color.Lerp(abilDpadSprite.color, Color.clear, smoothing * Time.deltaTime);
                    teamDpadSprite.color = Color.Lerp(teamDpadSprite.color, Color.clear, smoothing * Time.deltaTime);

                    weapText.color = Color.Lerp(weapText.color, Color.clear, smoothing * Time.deltaTime);
                    abilText.color = Color.Lerp(abilText.color, Color.clear, smoothing * Time.deltaTime);
                    teamText.color = Color.Lerp(teamText.color, Color.clear, smoothing * Time.deltaTime);

                    teamColorBlock.color = Color.Lerp(teamColorBlock.color, Color.clear, smoothing * Time.deltaTime);

                    startText.color = Color.Lerp(startText.color, fadeColor, smoothing * Time.deltaTime);
                }
                break;
            case (2):
                if (gameManagerScript.player2Active)
                {
                    if (gameManagerScript.player2Copy != null)
                    {
                        MonitorPlayerPosition(gameManagerScript.player2Copy.transform.position, 2);
                    }
                }
                else
                {
                    //all sprites = clear
                    //Display press A to enter
                    //BlastDoors are closed

                    //To be neutral
                    //background
                    backGradient.color = grayColor;
                    //startbackground
                    startBox.color = grayColor;
                    //PlayerName
                    playerText.color = grayColor;
                    //Back panel
                    backPanel.color = grayColor;


                    //LerpToClear
                    lightsSprite.color = Color.Lerp(lightsSprite.color, Color.clear, smoothing * Time.deltaTime);
                    leftArrow.color = Color.Lerp(leftArrow.color, Color.clear, smoothing * Time.deltaTime);
                    rightArrow.color = Color.Lerp(rightArrow.color, Color.clear, smoothing * Time.deltaTime);

                    weapDpadSprite.color = Color.Lerp(weapDpadSprite.color, Color.clear, smoothing * Time.deltaTime);
                    abilDpadSprite.color = Color.Lerp(abilDpadSprite.color, Color.clear, smoothing * Time.deltaTime);
                    teamDpadSprite.color = Color.Lerp(teamDpadSprite.color, Color.clear, smoothing * Time.deltaTime);

                    weapText.color = Color.Lerp(weapText.color, Color.clear, smoothing * Time.deltaTime);
                    abilText.color = Color.Lerp(abilText.color, Color.clear, smoothing * Time.deltaTime);
                    teamText.color = Color.Lerp(teamText.color, Color.clear, smoothing * Time.deltaTime);

                    teamColorBlock.color = Color.Lerp(teamColorBlock.color, Color.clear, smoothing * Time.deltaTime);

                    startText.color = Color.Lerp(startText.color, fadeColor, smoothing * Time.deltaTime);
                }
                break;
            case (3):
                if (gameManagerScript.player3Active)
                {
                    if (gameManagerScript.player3Copy != null)
                    {
                        MonitorPlayerPosition(gameManagerScript.player3Copy.transform.position, 3);
                    }
                }
                else
                {
                    //all sprites = clear
                    //Display press A to enter
                    //BlastDoors are closed


                    //To be neutral
                    //background
                    backGradient.color = grayColor;
                    //startbackground
                    startBox.color = grayColor;
                    //PlayerName
                    playerText.color = grayColor;
                    //Back panel
                    backPanel.color = grayColor;


                    //LerpToClear
                    lightsSprite.color = Color.Lerp(lightsSprite.color, Color.clear, smoothing * Time.deltaTime);
                    leftArrow.color = Color.Lerp(leftArrow.color, Color.clear, smoothing * Time.deltaTime);
                    rightArrow.color = Color.Lerp(rightArrow.color, Color.clear, smoothing * Time.deltaTime);

                    weapDpadSprite.color = Color.Lerp(weapDpadSprite.color, Color.clear, smoothing * Time.deltaTime);
                    abilDpadSprite.color = Color.Lerp(abilDpadSprite.color, Color.clear, smoothing * Time.deltaTime);
                    teamDpadSprite.color = Color.Lerp(teamDpadSprite.color, Color.clear, smoothing * Time.deltaTime);

                    weapText.color = Color.Lerp(weapText.color, Color.clear, smoothing * Time.deltaTime);
                    abilText.color = Color.Lerp(abilText.color, Color.clear, smoothing * Time.deltaTime);
                    teamText.color = Color.Lerp(teamText.color, Color.clear, smoothing * Time.deltaTime);

                    teamColorBlock.color = Color.Lerp(teamColorBlock.color, Color.clear, smoothing * Time.deltaTime);

                    startText.color = Color.Lerp(startText.color, fadeColor, smoothing * Time.deltaTime);
                }
                break;
            case (4):
                if (gameManagerScript.player4Active)
                {
                    if (gameManagerScript.player4Copy != null)
                    {
                        MonitorPlayerPosition(gameManagerScript.player4Copy.transform.position, 4);
                    }
                }
                else
                {
                    //all sprites = clear
                    //Display press A to enter
                    //BlastDoors are closed

                    //To be neutral
                    //background
                    backGradient.color = grayColor;
                    //startbackground
                    startBox.color = grayColor;
                    //PlayerName
                    playerText.color = grayColor;
                    //Back panel
                    backPanel.color = grayColor;


                    //LerpToClear
                    lightsSprite.color = Color.Lerp(lightsSprite.color, Color.clear, smoothing * Time.deltaTime);
                    leftArrow.color = Color.Lerp(leftArrow.color, Color.clear, smoothing * Time.deltaTime);
                    rightArrow.color = Color.Lerp(rightArrow.color, Color.clear, smoothing * Time.deltaTime);

                    weapDpadSprite.color = Color.Lerp(weapDpadSprite.color, Color.clear, smoothing * Time.deltaTime);
                    abilDpadSprite.color = Color.Lerp(abilDpadSprite.color, Color.clear, smoothing * Time.deltaTime);
                    teamDpadSprite.color = Color.Lerp(teamDpadSprite.color, Color.clear, smoothing * Time.deltaTime);

                    weapText.color = Color.Lerp(weapText.color, Color.clear, smoothing * Time.deltaTime);
                    abilText.color = Color.Lerp(abilText.color, Color.clear, smoothing * Time.deltaTime);
                    teamText.color = Color.Lerp(teamText.color, Color.clear, smoothing * Time.deltaTime);

                    teamColorBlock.color = Color.Lerp(teamColorBlock.color, Color.clear, smoothing * Time.deltaTime);

                    startText.color = Color.Lerp(startText.color, fadeColor, smoothing * Time.deltaTime);
                }
                break;
        }
    }
    

    void MonitorPlayerPosition(Vector3 playerVector, int playerNum)
    {
        if (playerVector.y > 10.3f)
        {
            //Weapon
            transform.localPosition = Vector2.Lerp(transform.localPosition, weaponPos, smoothing * Time.deltaTime);
            //LerpToWhite
            lightsSprite.color = Color.Lerp(lightsSprite.color, Color.white, smoothing * Time.deltaTime);
            leftArrow.color = Color.Lerp(leftArrow.color, Color.white, smoothing * Time.deltaTime);
            rightArrow.color = Color.Lerp(rightArrow.color, Color.white, smoothing * Time.deltaTime);
            weapDpadSprite.color = Color.Lerp(weapDpadSprite.color, Color.white, smoothing * Time.deltaTime);
            weapText.color = Color.Lerp(weapText.color, Color.white, smoothing * Time.deltaTime);
            weapBox.color = Color.Lerp(weapBox.color, Color.white, smoothing * Time.deltaTime);
            //LerpToClear
            abilDpadSprite.color = Color.Lerp(abilDpadSprite.color, Color.clear, smoothing * Time.deltaTime);
            teamDpadSprite.color = Color.Lerp(teamDpadSprite.color, Color.clear, smoothing * Time.deltaTime);

            //LerpToFade
            abilText.color = Color.Lerp(abilText.color, fadeColor, smoothing * Time.deltaTime);
            teamText.color = Color.Lerp(teamText.color, fadeColor, smoothing * Time.deltaTime);

            abilBox.color = Color.Lerp(abilBox.color, fadeColor, smoothing * Time.deltaTime);
            startText.color = Color.Lerp(startText.color, fadeColor, smoothing * Time.deltaTime);

            //Set color first
            startBoxColor = new Color(startBox.color.r, startBox.color.g, startBox.color.b, fadeColor.a);
            startBox.color = Color.Lerp(startBox.color, startBoxColor, smoothing * Time.deltaTime);
            //Set color first
            colorBlockColor = new Color(teamColorBlock.color.r, teamColorBlock.color.g, teamColorBlock.color.b, fadeColor.a);
            teamColorBlock.color = Color.Lerp(teamColorBlock.color, colorBlockColor, smoothing * Time.deltaTime);
            
            //Still have to populate the below bits with this^^ modified.
            /*
            What else fades?
            */
            switch(playerNum)
            {
                case (1):
                    gameManagerScript.p1InWeaponZone = true;
                    gameManagerScript.p1InAbilityZone = false;
                    gameManagerScript.p1InTeamZone = false;
                    gameManagerScript.p1InStartZone = false;
                    break;
                case (2):
                    gameManagerScript.p2InWeaponZone = true;
                    gameManagerScript.p2InAbilityZone = false;
                    gameManagerScript.p2InTeamZone = false;
                    gameManagerScript.p2InStartZone = false;
                    break;
                case (3):
                    gameManagerScript.p3InWeaponZone = true;
                    gameManagerScript.p3InAbilityZone = false;
                    gameManagerScript.p3InTeamZone = false;
                    gameManagerScript.p3InStartZone = false;
                    break;
                case (4):
                    gameManagerScript.p4InWeaponZone = true;
                    gameManagerScript.p4InAbilityZone = false;
                    gameManagerScript.p4InTeamZone = false;
                    gameManagerScript.p4InStartZone = false;
                    break;
            }
        }
        else if(playerVector.y < 10.3f && playerVector.y > -0.6f)
        {
            //Ability
            transform.localPosition = Vector2.Lerp(transform.localPosition, abilityPos, smoothing * Time.deltaTime);
            //LerpToWhite
            lightsSprite.color = Color.Lerp(lightsSprite.color, Color.white, smoothing * Time.deltaTime);
            leftArrow.color = Color.Lerp(leftArrow.color, Color.white, smoothing * Time.deltaTime);
            rightArrow.color = Color.Lerp(rightArrow.color, Color.white, smoothing * Time.deltaTime);
            abilDpadSprite.color = Color.Lerp(abilDpadSprite.color, Color.white, smoothing * Time.deltaTime);
            abilText.color = Color.Lerp(abilText.color, Color.white, smoothing * Time.deltaTime);
            abilBox.color = Color.Lerp(abilBox.color, Color.white, smoothing * Time.deltaTime);
            //LerpToClear
            weapDpadSprite.color = Color.Lerp(weapDpadSprite.color, Color.clear, smoothing * Time.deltaTime);
            teamDpadSprite.color = Color.Lerp(teamDpadSprite.color, Color.clear, smoothing * Time.deltaTime);

            //LerpToFade
            weapText.color = Color.Lerp(weapText.color, fadeColor, smoothing * Time.deltaTime);
            teamText.color = Color.Lerp(teamText.color, fadeColor, smoothing * Time.deltaTime);

            weapBox.color = Color.Lerp(weapBox.color, fadeColor, smoothing * Time.deltaTime);
            startText.color = Color.Lerp(startText.color, fadeColor, smoothing * Time.deltaTime);

            //Set color first
            startBoxColor = new Color(startBox.color.r, startBox.color.g, startBox.color.b, fadeColor.a);
            startBox.color = Color.Lerp(startBox.color, startBoxColor, smoothing * Time.deltaTime);
            //Set color first
            colorBlockColor = new Color(teamColorBlock.color.r, teamColorBlock.color.g, teamColorBlock.color.b, fadeColor.a);
            teamColorBlock.color = Color.Lerp(teamColorBlock.color, colorBlockColor, smoothing * Time.deltaTime);

            switch (playerNum)
            {
                case (1):
                    gameManagerScript.p1InWeaponZone = false;
                    gameManagerScript.p1InAbilityZone = true;
                    gameManagerScript.p1InTeamZone = false;
                    gameManagerScript.p1InStartZone = false;
                    break;
                case (2):
                    gameManagerScript.p2InWeaponZone = false;
                    gameManagerScript.p2InAbilityZone = true;
                    gameManagerScript.p2InTeamZone = false;
                    gameManagerScript.p2InStartZone = false;
                    break;
                case (3):
                    gameManagerScript.p3InWeaponZone = false;
                    gameManagerScript.p3InAbilityZone = true;
                    gameManagerScript.p3InTeamZone = false;
                    gameManagerScript.p3InStartZone = false;
                    break;
                case (4):
                    gameManagerScript.p4InWeaponZone = false;
                    gameManagerScript.p4InAbilityZone = true;
                    gameManagerScript.p4InTeamZone = false;
                    gameManagerScript.p4InStartZone = false;
                    break;
            }
        }
        else if(playerVector.y < -0.6f && playerVector.y > -11.4f)
        {
            //Team
            transform.localPosition = Vector2.Lerp(transform.localPosition, teamPos, smoothing * Time.deltaTime);
            //LerpToWhite
            lightsSprite.color = Color.Lerp(lightsSprite.color, Color.white, smoothing * Time.deltaTime);
            leftArrow.color = Color.Lerp(leftArrow.color, Color.white, smoothing * Time.deltaTime);
            rightArrow.color = Color.Lerp(rightArrow.color, Color.white, smoothing * Time.deltaTime);
            teamDpadSprite.color = Color.Lerp(teamDpadSprite.color, Color.white, smoothing * Time.deltaTime);
            teamText.color = Color.Lerp(teamText.color, Color.white, smoothing * Time.deltaTime);
            //Set color first
            colorBlockColor = new Color(teamColorBlock.color.r, teamColorBlock.color.g, teamColorBlock.color.b, Color.white.a);
            teamColorBlock.color = Color.Lerp(teamColorBlock.color, colorBlockColor, smoothing * Time.deltaTime);

            //LerpToClear
            weapDpadSprite.color = Color.Lerp(weapDpadSprite.color, Color.clear, smoothing * Time.deltaTime);
            abilDpadSprite.color = Color.Lerp(abilDpadSprite.color, Color.clear, smoothing * Time.deltaTime);

            //LerpToFade
            weapText.color = Color.Lerp(weapText.color, fadeColor, smoothing * Time.deltaTime);
            abilText.color = Color.Lerp(abilText.color, fadeColor, smoothing * Time.deltaTime);

            weapBox.color = Color.Lerp(weapBox.color, fadeColor, smoothing * Time.deltaTime);
            abilBox.color = Color.Lerp(abilBox.color, fadeColor, smoothing * Time.deltaTime);
            startText.color = Color.Lerp(startText.color, fadeColor, smoothing * Time.deltaTime);

            //Set color first
            startBoxColor = new Color(startBox.color.r, startBox.color.g, startBox.color.b, fadeColor.a);
            startBox.color = Color.Lerp(startBox.color, startBoxColor, smoothing * Time.deltaTime);
            switch (playerNum)
            {
                case (1):
                    gameManagerScript.p1InWeaponZone = false;
                    gameManagerScript.p1InAbilityZone = false;
                    gameManagerScript.p1InTeamZone = true;
                    gameManagerScript.p1InStartZone = false;
                    break;
                case (2):
                    gameManagerScript.p2InWeaponZone = false;
                    gameManagerScript.p2InAbilityZone = false;
                    gameManagerScript.p2InTeamZone = true;
                    gameManagerScript.p2InStartZone = false;
                    break;
                case (3):
                    gameManagerScript.p3InWeaponZone = false;
                    gameManagerScript.p3InAbilityZone = false;
                    gameManagerScript.p3InTeamZone = true;
                    gameManagerScript.p3InStartZone = false;
                    break;
                case (4):
                    gameManagerScript.p4InWeaponZone = false;
                    gameManagerScript.p4InAbilityZone = false;
                    gameManagerScript.p4InTeamZone = true;
                    gameManagerScript.p4InStartZone = false;
                    break;
            }
        }
        else //if(playerTransform < -11.4
        {
            //Start
            //LerpTowhite
            //Set color first
            startBoxColor = new Color(startBox.color.r, startBox.color.g, startBox.color.b, Color.white.a);
            startBox.color = Color.Lerp(startBox.color, startBoxColor, smoothing * Time.deltaTime);
            //then start text
            startText.color = Color.Lerp(startText.color, Color.white, smoothing * Time.deltaTime);

            //LerpToClear
            lightsSprite.color = Color.Lerp(lightsSprite.color, Color.clear, smoothing * Time.deltaTime);
            leftArrow.color = Color.Lerp(leftArrow.color, Color.clear, smoothing * Time.deltaTime);
            rightArrow.color = Color.Lerp(rightArrow.color, Color.clear, smoothing * Time.deltaTime);

            weapDpadSprite.color = Color.Lerp(weapDpadSprite.color, Color.clear, smoothing * Time.deltaTime);
            abilDpadSprite.color = Color.Lerp(abilDpadSprite.color, Color.clear, smoothing * Time.deltaTime);
            teamDpadSprite.color = Color.Lerp(teamDpadSprite.color, Color.clear, smoothing * Time.deltaTime);

            //LerpToFade
            weapText.color = Color.Lerp(weapText.color, fadeColor, smoothing * Time.deltaTime);
            abilText.color = Color.Lerp(abilText.color, fadeColor, smoothing * Time.deltaTime);
            teamText.color = Color.Lerp(teamText.color, fadeColor, smoothing * Time.deltaTime);

            weapBox.color = Color.Lerp(weapBox.color, fadeColor, smoothing * Time.deltaTime);
            abilBox.color = Color.Lerp(abilBox.color, fadeColor, smoothing * Time.deltaTime);

            //Set color first
            colorBlockColor = new Color(teamColorBlock.color.r, teamColorBlock.color.g, teamColorBlock.color.b, fadeColor.a);
            teamColorBlock.color = Color.Lerp(teamColorBlock.color, colorBlockColor, smoothing * Time.deltaTime);

            switch (playerNum)
            {
                case (1):
                    gameManagerScript.p1InWeaponZone = false;
                    gameManagerScript.p1InAbilityZone = false;
                    gameManagerScript.p1InTeamZone = false;
                    gameManagerScript.p1InStartZone = true;
                    break;
                case (2):
                    gameManagerScript.p2InWeaponZone = false;
                    gameManagerScript.p2InAbilityZone = false;
                    gameManagerScript.p2InTeamZone = false;
                    gameManagerScript.p2InStartZone = true;
                    break;
                case (3):
                    gameManagerScript.p3InWeaponZone = false;
                    gameManagerScript.p3InAbilityZone = false;
                    gameManagerScript.p3InTeamZone = false;
                    gameManagerScript.p3InStartZone = true;
                    break;
                case (4):
                    gameManagerScript.p4InWeaponZone = false;
                    gameManagerScript.p4InAbilityZone = false;
                    gameManagerScript.p4InTeamZone = false;
                    gameManagerScript.p4InStartZone = true;
                    break;
            }
        }
    }
}
