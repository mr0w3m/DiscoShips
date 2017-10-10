using UnityEngine;
using System.Collections;

public class ProtoPlayerMovement : MonoBehaviour {

    public int teamCount;

    public float speed;
    public float boostSpeed;
    public Rigidbody2D playerRigidbody;
    public float angleDir;
    public int burstCoolDownTime;

    public GameManager gameManagerScript;

    //JetPack
    public bool jetPack;
    public int jetPackSpeed;

    private int burstCoolDown;
    
    public float minX;
    public float maxX;
    public float minY;
    public float maxY;

    public bool classSelect;
    
    void Awake()
    {
        
    }

	void FixedUpdate()
    {
        Movement();
        Turning();
    }

    void Update()
    {
        
    }

    void Movement()
    {
        float moveHorizontal;
        float moveVertical;
        if (teamCount == 1) //player1
        {
            moveHorizontal = Input.GetAxisRaw("BlackHorizontal");
            moveVertical = Input.GetAxisRaw("BlackVertical");
        }
        else if (teamCount == 2) //player2
        {
            moveHorizontal = Input.GetAxisRaw("WhiteHorizontal");
            moveVertical = Input.GetAxisRaw("WhiteVertical");
        }
        else if (teamCount == 3) //player3
        {
            moveHorizontal = Input.GetAxisRaw("Player3Horizontal");
            moveVertical = Input.GetAxisRaw("Player3Vertical");
        }
        else //(teamCount == 4) //player4
        {
            moveHorizontal = Input.GetAxisRaw("Player4Horizontal");
            moveVertical = Input.GetAxisRaw("Player4Horizontal");
        }
        Vector2 movement = new Vector2(moveHorizontal, moveVertical);
        //NormalMovement

        //If jetPack is not activated
        if (jetPack != true)
        {
            playerRigidbody.AddForce(movement * speed);
        }
        else //jetPack is activated
        {
            playerRigidbody.AddForce(movement * jetPackSpeed);
        }
        
        if (classSelect == true)
        {
            playerRigidbody.position = new Vector2(Mathf.Clamp(playerRigidbody.position.x, minX, maxX), Mathf.Clamp(playerRigidbody.position.y, minY, maxY));
        }

        
        if (burstCoolDown < 1)
        {
            if (teamCount == 1)
            {
                if (Input.GetAxis("BlackBoost") > .1f)
                {
                    playerRigidbody.AddForce(movement * boostSpeed);
                    burstCoolDown = burstCoolDownTime;
                }
            }
            else if (teamCount == 2)
            {
                if (Input.GetAxis("WhiteBoost") > .1f)
                {
                    playerRigidbody.AddForce(movement * boostSpeed);
                    burstCoolDown = burstCoolDownTime;
                }
            }
            else if (teamCount == 3)
            {
                if (Input.GetAxis("Player3Boost") > .1f)
                {
                    playerRigidbody.AddForce(movement * boostSpeed);
                    burstCoolDown = burstCoolDownTime;
                }
            }
            else // if (teamCount == 4)
            {
                if (Input.GetAxis("Player4Boost") > .1f)
                {
                    playerRigidbody.AddForce(movement * boostSpeed);
                    burstCoolDown = burstCoolDownTime;
                }
            }
        }
        burstCoolDown -= 1;
    }

    void Turning()
    {
        float XLook;
        float YLook;
        if (teamCount == 1)
        {
            XLook = Input.GetAxisRaw("BlackXLook");
            YLook = Input.GetAxisRaw("BlackYLook");
        }
        else if (teamCount == 2)
        {
            XLook = Input.GetAxisRaw("WhiteXLook");
            YLook = Input.GetAxisRaw("WhiteYLook");
        }
        else if (teamCount == 3)
        {
            XLook = Input.GetAxisRaw("Player3XLook");
            YLook = Input.GetAxisRaw("Player3YLook");
        }
        else //if (teamCount == 4)
        {
            XLook = Input.GetAxisRaw("Player4XLook");
            YLook = Input.GetAxisRaw("Player4YLook");
        }

        if (XLook < .001f && XLook >-.001f && YLook < .001f && YLook > -.001f)
        {
            return;
        }
        else
        {
            Vector2 lookPos = new Vector2(XLook, YLook);
            //We have to convert this X and Y into a degrees
            //First we have to find the ArcTan of both then multiply to get the degrees.
            angleDir = Mathf.Atan2(XLook, YLook) * Mathf.Rad2Deg;
            angleDir += 180;
            //Debug.Log("AngleDir: " + angleDir);

            transform.eulerAngles = new Vector3(transform.eulerAngles.x, transform.eulerAngles.y, angleDir);
        }
    }
    
    public float myAngle()
    {
        //Debug.Log("Called.");
        return angleDir;
    }
    
    void OnDestroy()
    {
        if (classSelect != true)
        {
            gameManagerScript.killCount += 1;
        }
    }
}
