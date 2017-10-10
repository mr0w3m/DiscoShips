using UnityEngine;
using UnityEngine.UI;
using Rewired;
using System.Collections;

public class MapSelectManager : MonoBehaviour {

    private Player rWPlayer1;
    private Player rWPlayer2;
    private Player rWPlayer3;
    private Player rWPlayer4;

    public GameManager gameManagerScript;

    //When selected, go to this configuration
    public Vector3 standardMapSelected;
    public Vector3 staticMapSelected;
    public Vector3 fireMapSelected;
    public Vector3 poisonMapSelected;
    public Vector3 iceMapSelected;
    //StandardMap
    public Vector3 standardMapNotSelectedLeft;
    //StaticMap
    public Vector3 staticMapNotSelectedRight;
    public Vector3 staticMapNotSelectedLeft;
    //FireMap
    public Vector3 fireMapNotSelectedRight;
    public Vector3 fireMapNotSelectedLeft;
    //PoisonMap
    public Vector3 poisonMapNotSelectedRight;
    public Vector3 poisonMapNotSelectedLeft;
    //IceMap
    public Vector3 iceMapNotSelectedRight;
    //Selected/NotSelected Size
    public Vector3 mapSelectedSize;
    public Vector3 mapNotSelectedSize;

    //The Map Boxes
    public GameObject standardMapBox;
    public GameObject staticMapBox;
    public GameObject fireMapBox;
    public GameObject poisonMapBox;
    public GameObject iceMapBox;

    public GameObject standardMapVertical;
    public GameObject staticMapVertical;
    public GameObject fireMapVertical;
    public GameObject poisonMapVertical;
    public GameObject iceMapVertical;

    public SpriteRenderer standardMapRend;
    public SpriteRenderer staticMapRend;
    public SpriteRenderer fireMapRend;
    public SpriteRenderer poisonMapRend;
    public SpriteRenderer iceMapRend;

    public GameObject standardMapObj;
    public GameObject staticMapObj;
    public GameObject fireMapObj;
    public GameObject poisonMapObj;
    public GameObject iceMapObj;

    public float smoothing;


    public bool mapSelectScreenActive;

    private int currentMapSelected = 3;

    private bool switchMap;

    private float selectMoveCooldownTime = 1.5f;
    private float selectMoveCooldown;

    void Awake()
    {
        //Rewired
        rWPlayer1 = ReInput.players.GetPlayer(0);
        rWPlayer2 = ReInput.players.GetPlayer(1);
        rWPlayer3 = ReInput.players.GetPlayer(2);
        rWPlayer4 = ReInput.players.GetPlayer(3);

        gameManagerScript.mapSelectManager = this;
    }

    void Update()
    {
        if (mapSelectScreenActive)
        {
            ManageInput();
            ManageSelectionBoxes();
            ManageSelectedMap();
            ManageStickInput();
        }
    }

    void ManageSelectedMap()
    {
        if (currentMapSelected == 3)
        {
            standardMapRend.enabled = false;
            standardMapObj.SetActive(true);
        }
        else
        {
            standardMapRend.enabled = true;
            standardMapObj.SetActive(false);
        }

        if (currentMapSelected == 4)
        {
            staticMapRend.enabled = false;
            staticMapObj.SetActive(true);
        }
        else
        {
            staticMapRend.enabled = true;
            staticMapObj.SetActive(false);
        }

        if (currentMapSelected == 5)
        {
            fireMapRend.enabled = false;
            fireMapObj.SetActive(true);
        }
        else
        {
            fireMapRend.enabled = true;
            fireMapObj.SetActive(false);
        }

        if (currentMapSelected == 6)
        {
            poisonMapRend.enabled = false;
            poisonMapObj.SetActive(true);
        }
        else
        {
            poisonMapRend.enabled = true;
            poisonMapObj.SetActive(false);
        }

        if (currentMapSelected == 7)
        {
            iceMapRend.enabled = false;
            iceMapObj.SetActive(true);
        }
        else
        {
            iceMapRend.enabled = true;
            iceMapObj.SetActive(false);
        }
    }

    void ManageStickInput()
    {
        if (selectMoveCooldown > 0)
        {
            selectMoveCooldown -= Time.deltaTime;
        }
        else
        {
            selectMoveCooldown = 0;
        }

        //Move cooldown is ready to accept input.
        if (selectMoveCooldown < 1)
        {
            //MoveRight
            if (rWPlayer1.GetAxis("MoveHorizontal") > 0.4f || rWPlayer2.GetAxis("MoveHorizontal") > 0.4f || rWPlayer3.GetAxis("MoveHorizontal") > 0.4f || rWPlayer4.GetAxis("MoveHorizontal") > 0.4f)
            {
                //Move cursor right
                //Debug.Log("MovedRight");
                selectMoveCooldown = selectMoveCooldownTime;
                
                currentMapSelected++;
                if (currentMapSelected > 7)
                { currentMapSelected = 3; }
                gameManagerScript.selectedMap = currentMapSelected;
            }

            //MoveLeft
            if (rWPlayer1.GetAxis("MoveHorizontal") < -0.4f || rWPlayer2.GetAxis("MoveHorizontal") < -0.4f || rWPlayer3.GetAxis("MoveHorizontal") < -0.4f || rWPlayer4.GetAxis("MoveHorizontal") < -0.4f)
            {
                //Move cursor left
                //Debug.Log("MovedLeft");
                selectMoveCooldown = selectMoveCooldownTime;
                
                currentMapSelected--;
                if (currentMapSelected < 3)
                { currentMapSelected = 7; }
                gameManagerScript.selectedMap = currentMapSelected;
            }
        }
    }

    void ManageInput()
    {
        if (rWPlayer1.GetButtonDown("DpadRight") && switchMap == false || rWPlayer2.GetButtonDown("DpadRight") && switchMap == false || rWPlayer3.GetButtonDown("DpadRight") && switchMap == false || rWPlayer4.GetButtonDown("DpadRight") && switchMap == false)
        {
            switchMap = true;
            currentMapSelected++;
            if (currentMapSelected > 7)
            { currentMapSelected = 3; }
            gameManagerScript.selectedMap = currentMapSelected;
        }
        else if (rWPlayer1.GetButtonDown("DpadLeft") && switchMap == false || rWPlayer2.GetButtonDown("DpadLeft") && switchMap == false || rWPlayer3.GetButtonDown("DpadLeft") && switchMap == false || rWPlayer4.GetButtonDown("DpadLeft") && switchMap == false)
        {
            switchMap = true;
            currentMapSelected--;
            if (currentMapSelected < 3)
            { currentMapSelected = 7; }
            gameManagerScript.selectedMap = currentMapSelected;
        }
        else if (switchMap == true)
        {
            switchMap = false;
        }
    }

    void ManageSelectionBoxes()
    {
        switch(currentMapSelected)
        {
            case (3):
                //Standard
                standardMapBox.transform.position = Vector3.Lerp(standardMapBox.transform.position, standardMapSelected, smoothing * Time.deltaTime);
                standardMapBox.transform.localScale = Vector3.Lerp(standardMapBox.transform.localScale, mapSelectedSize, smoothing * Time.deltaTime);
                
                //OnRight
                staticMapBox.transform.position = Vector3.Lerp(staticMapBox.transform.position, staticMapNotSelectedRight, smoothing * Time.deltaTime);
                staticMapBox.transform.localScale = Vector3.Lerp(staticMapBox.transform.localScale, mapNotSelectedSize, smoothing * Time.deltaTime);

                staticMapVertical.transform.position = Vector3.Lerp(staticMapVertical.transform.position, staticMapNotSelectedRight, smoothing * Time.deltaTime);

                fireMapBox.transform.position = Vector3.Lerp(fireMapBox.transform.position, fireMapNotSelectedRight, smoothing * Time.deltaTime);
                fireMapBox.transform.localScale = Vector3.Lerp(fireMapBox.transform.localScale, mapNotSelectedSize, smoothing * Time.deltaTime);

                fireMapVertical.transform.position = Vector3.Lerp(fireMapVertical.transform.position, fireMapNotSelectedRight, smoothing * Time.deltaTime);

                poisonMapBox.transform.position = Vector3.Lerp(poisonMapBox.transform.position, poisonMapNotSelectedRight, smoothing * Time.deltaTime);
                poisonMapBox.transform.localScale = Vector3.Lerp(poisonMapBox.transform.localScale, mapNotSelectedSize, smoothing * Time.deltaTime);

                poisonMapVertical.transform.position = Vector3.Lerp(poisonMapVertical.transform.position, poisonMapNotSelectedRight, smoothing * Time.deltaTime);

                iceMapBox.transform.position = Vector3.Lerp(iceMapBox.transform.position, iceMapNotSelectedRight, smoothing * Time.deltaTime);
                iceMapBox.transform.localScale = Vector3.Lerp(iceMapBox.transform.localScale, mapNotSelectedSize, smoothing * Time.deltaTime);

                iceMapVertical.transform.position = Vector3.Lerp(iceMapVertical.transform.position, iceMapNotSelectedRight, smoothing * Time.deltaTime);

                break;
            case (4):
                //Static
                staticMapBox.transform.position = Vector3.Lerp(staticMapBox.transform.position, staticMapSelected, smoothing * Time.deltaTime);
                staticMapBox.transform.localScale = Vector3.Lerp(staticMapBox.transform.localScale, mapSelectedSize, smoothing * Time.deltaTime);

                //OnLeft
                standardMapBox.transform.position = Vector3.Lerp(standardMapBox.transform.position, standardMapNotSelectedLeft, smoothing * Time.deltaTime);
                standardMapBox.transform.localScale = Vector3.Lerp(standardMapBox.transform.localScale, mapNotSelectedSize, smoothing * Time.deltaTime);

                standardMapVertical.transform.position = Vector3.Lerp(standardMapVertical.transform.position, standardMapNotSelectedLeft, smoothing * Time.deltaTime);

                //OnRight
                fireMapBox.transform.position = Vector3.Lerp(fireMapBox.transform.position, fireMapNotSelectedRight, smoothing * Time.deltaTime);
                fireMapBox.transform.localScale = Vector3.Lerp(fireMapBox.transform.localScale, mapNotSelectedSize, smoothing * Time.deltaTime);

                fireMapVertical.transform.position = Vector3.Lerp(fireMapVertical.transform.position, fireMapNotSelectedRight, smoothing * Time.deltaTime);

                poisonMapBox.transform.position = Vector3.Lerp(poisonMapBox.transform.position, poisonMapNotSelectedRight, smoothing * Time.deltaTime);
                poisonMapBox.transform.localScale = Vector3.Lerp(poisonMapBox.transform.localScale, mapNotSelectedSize, smoothing * Time.deltaTime);

                poisonMapVertical.transform.position = Vector3.Lerp(poisonMapVertical.transform.position, poisonMapNotSelectedRight, smoothing * Time.deltaTime);

                iceMapBox.transform.position = Vector3.Lerp(iceMapBox.transform.position, iceMapNotSelectedRight, smoothing * Time.deltaTime);
                iceMapBox.transform.localScale = Vector3.Lerp(iceMapBox.transform.localScale, mapNotSelectedSize, smoothing * Time.deltaTime);

                iceMapVertical.transform.position = Vector3.Lerp(iceMapVertical.transform.position, iceMapNotSelectedRight, smoothing * Time.deltaTime);

                break;
            case (5):
                //Fire
                fireMapBox.transform.position = Vector3.Lerp(fireMapBox.transform.position, fireMapSelected, smoothing * Time.deltaTime);
                fireMapBox.transform.localScale = Vector3.Lerp(fireMapBox.transform.localScale, mapSelectedSize, smoothing * Time.deltaTime);

                //OnLeft
                staticMapBox.transform.position = Vector3.Lerp(staticMapBox.transform.position, staticMapNotSelectedLeft, smoothing * Time.deltaTime);
                staticMapBox.transform.localScale = Vector3.Lerp(staticMapBox.transform.localScale, mapNotSelectedSize, smoothing * Time.deltaTime);

                staticMapVertical.transform.position = Vector3.Lerp(staticMapVertical.transform.position, staticMapNotSelectedLeft, smoothing * Time.deltaTime);

                standardMapBox.transform.position = Vector3.Lerp(standardMapBox.transform.position, standardMapNotSelectedLeft, smoothing * Time.deltaTime);
                standardMapBox.transform.localScale = Vector3.Lerp(standardMapBox.transform.localScale, mapNotSelectedSize, smoothing * Time.deltaTime);

                standardMapVertical.transform.position = Vector3.Lerp(standardMapVertical.transform.position, standardMapNotSelectedLeft, smoothing * Time.deltaTime);


                //OnRight
                poisonMapBox.transform.position = Vector3.Lerp(poisonMapBox.transform.position, poisonMapNotSelectedRight, smoothing * Time.deltaTime);
                poisonMapBox.transform.localScale = Vector3.Lerp(poisonMapBox.transform.localScale, mapNotSelectedSize, smoothing * Time.deltaTime);

                poisonMapVertical.transform.position = Vector3.Lerp(poisonMapVertical.transform.position, poisonMapNotSelectedRight, smoothing * Time.deltaTime);

                iceMapBox.transform.position = Vector3.Lerp(iceMapBox.transform.position, iceMapNotSelectedRight, smoothing * Time.deltaTime);
                iceMapBox.transform.localScale = Vector3.Lerp(iceMapBox.transform.localScale, mapNotSelectedSize, smoothing * Time.deltaTime);

                iceMapVertical.transform.position = Vector3.Lerp(iceMapVertical.transform.position, iceMapNotSelectedRight, smoothing * Time.deltaTime);
                break;
            case (6):
                //Poison
                poisonMapBox.transform.position = Vector3.Lerp(poisonMapBox.transform.position, poisonMapSelected, smoothing * Time.deltaTime);
                poisonMapBox.transform.localScale = Vector3.Lerp(poisonMapBox.transform.localScale, mapSelectedSize, smoothing * Time.deltaTime);


                //OnLeft
                staticMapBox.transform.position = Vector3.Lerp(staticMapBox.transform.position, staticMapNotSelectedLeft, smoothing * Time.deltaTime);
                staticMapBox.transform.localScale = Vector3.Lerp(staticMapBox.transform.localScale, mapNotSelectedSize, smoothing * Time.deltaTime);

                staticMapVertical.transform.position = Vector3.Lerp(staticMapVertical.transform.position, staticMapNotSelectedLeft, smoothing * Time.deltaTime);

                standardMapBox.transform.position = Vector3.Lerp(standardMapBox.transform.position, standardMapNotSelectedLeft, smoothing * Time.deltaTime);
                standardMapBox.transform.localScale = Vector3.Lerp(standardMapBox.transform.localScale, mapNotSelectedSize, smoothing * Time.deltaTime);

                standardMapVertical.transform.position = Vector3.Lerp(standardMapVertical.transform.position, standardMapNotSelectedLeft, smoothing * Time.deltaTime);

                fireMapBox.transform.position = Vector3.Lerp(fireMapBox.transform.position, fireMapNotSelectedLeft, smoothing * Time.deltaTime);
                fireMapBox.transform.localScale = Vector3.Lerp(fireMapBox.transform.localScale, mapNotSelectedSize, smoothing * Time.deltaTime);

                fireMapVertical.transform.position = Vector3.Lerp(fireMapVertical.transform.position, fireMapNotSelectedLeft, smoothing * Time.deltaTime);


                //OnRight
                iceMapBox.transform.position = Vector3.Lerp(iceMapBox.transform.position, iceMapNotSelectedRight, smoothing * Time.deltaTime);
                iceMapBox.transform.localScale = Vector3.Lerp(iceMapBox.transform.localScale, mapNotSelectedSize, smoothing * Time.deltaTime);
                break;
            case (7):
                //Ice
                iceMapBox.transform.position = Vector3.Lerp(iceMapBox.transform.position, iceMapSelected, smoothing * Time.deltaTime);
                iceMapBox.transform.localScale = Vector3.Lerp(iceMapBox.transform.localScale, mapSelectedSize, smoothing * Time.deltaTime);

                //OnLeft
                staticMapBox.transform.position = Vector3.Lerp(staticMapBox.transform.position, staticMapNotSelectedLeft, smoothing * Time.deltaTime);
                staticMapBox.transform.localScale = Vector3.Lerp(staticMapBox.transform.localScale, mapNotSelectedSize, smoothing * Time.deltaTime);

                staticMapVertical.transform.position = Vector3.Lerp(staticMapVertical.transform.position, staticMapNotSelectedLeft, smoothing * Time.deltaTime);

                standardMapBox.transform.position = Vector3.Lerp(standardMapBox.transform.position, standardMapNotSelectedLeft, smoothing * Time.deltaTime);
                standardMapBox.transform.localScale = Vector3.Lerp(standardMapBox.transform.localScale, mapNotSelectedSize, smoothing * Time.deltaTime);

                standardMapVertical.transform.position = Vector3.Lerp(standardMapVertical.transform.position, standardMapNotSelectedLeft, smoothing * Time.deltaTime);

                fireMapBox.transform.position = Vector3.Lerp(fireMapBox.transform.position, fireMapNotSelectedLeft, smoothing * Time.deltaTime);
                fireMapBox.transform.localScale = Vector3.Lerp(fireMapBox.transform.localScale, mapNotSelectedSize, smoothing * Time.deltaTime);

                fireMapVertical.transform.position = Vector3.Lerp(fireMapVertical.transform.position, fireMapNotSelectedLeft, smoothing * Time.deltaTime);

                poisonMapBox.transform.position = Vector3.Lerp(poisonMapBox.transform.position, poisonMapNotSelectedLeft, smoothing * Time.deltaTime);
                poisonMapBox.transform.localScale = Vector3.Lerp(poisonMapBox.transform.localScale, mapNotSelectedSize, smoothing * Time.deltaTime);

                poisonMapVertical.transform.position = Vector3.Lerp(poisonMapVertical.transform.position, poisonMapNotSelectedLeft, smoothing * Time.deltaTime);


                break;
        }
    }

}
