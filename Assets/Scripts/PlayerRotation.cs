using UnityEngine;
using System.Collections;
using Rewired;

public class PlayerRotation : MonoBehaviour {

    private Player playerInput;

    public int numPlayer;
    public bool sightUp;
    public bool frozen;

    private float angleDir;
    private string XLookInput;
    private string YLookInput;

    void Awake()
    {
        int realNum = numPlayer - 1;
        playerInput = ReInput.players.GetPlayer(realNum);
    }

    void Start ()
    {
        //SetUpInputs();
        sightUp = false;
	}

    /*
    void SetUpInputs()
    {
        if (numPlayer == 1)
        {
            XLookInput = "BlackXLook";
            YLookInput = "BlackYLook";
        }
        else if (numPlayer == 2)
        {
            XLookInput = "WhiteXLook";
            YLookInput = "WhiteYLook";
        }
        else if (numPlayer == 3)
        {
            XLookInput = "Player3XLook";
            YLookInput = "Player3YLook";
        }
        else if (numPlayer == 4)
        {
            XLookInput = "Player4XLook";
            YLookInput = "Player4YLook";
        }
        else
        {
            //Debug.Log("numPlayerNotSet");
        }
    }
    */

    void FixedUpdate ()
    {
        if (!frozen)
        {
            Turning();
        }
        else
        {
            
        }
	}

    void Turning()
    {
        float XLook;
        float YLook;
        XLook = playerInput.GetAxisRaw("AimX");
        YLook = playerInput.GetAxisRaw("AimY");


        //Are we pushing the stick all the way?
        /*
        if (XLook > 0.75f || XLook < -0.75f || YLook > 0.75f || YLook < -0.75f)
        {
            sightUp = true;
        }
        else
        {
            sightUp = false;
        }
        */

        if (XLook < 0.3f && XLook > -0.3f && YLook < 0.3f && YLook > -0.3f)
        {
            return;
        }
        else
        {
            //Vector2 lookPos = new Vector2(XLook, YLook);
            //We have to convert this X and Y into a degrees
            //First we have to find the ArcTan of both then multiply to get the degrees.
            angleDir = Mathf.Atan2(XLook, YLook) * Mathf.Rad2Deg;
            angleDir += 180;

            transform.eulerAngles = new Vector3(transform.eulerAngles.x, transform.eulerAngles.y, angleDir);
        }
    }

    public float MyAngle()
    {
        return angleDir;
    }
}
