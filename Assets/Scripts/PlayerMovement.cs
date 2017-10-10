using UnityEngine;
using System.Collections;
using System.Collections;
using Rewired;

public class PlayerMovement : MonoBehaviour {

    private Player playerInput;

    public int numPlayer;
    public int teamCount;
    private string inputHorizontal;
    private string inputVertical;
    private string inputBoost;

    public float speed;
    private float speedSet = 25.0f;
    private float speedIncrease = 35.0f;
    public float boostSpeed;
    private float boostSpeedSet = 600.0f;
    private float boostSpeedIncrease = 620.0f;
    public float angleDir;
    public int burstCoolDownTime;
    public float newBurstCoolDownTime;
    public GameManager gameManagerScript;

    public Rigidbody2D playerRigidbody;
    private PlayerHealth healthScript;
    private PlayerAbility abilityScript;
    private PlayerShoot shootScript;
    private PlayerRotation rotationScript;
    

    private int burstCoolDown;
    private float newBurstCoolDown;
    //public GameObject burstParticles;
    public ParticleSystem burstParticleSys;
    //public GameObject moveParticles;
    public ParticleSystem moveParticleSys;

    public float minX;
    public float maxX;
    public float minY;
    public float maxY;

    public float matchMinX;
    public float matchMaxX;
    public float matchMinY;
    public float matchMaxY;

    public bool classSelect;

    public WaitForSeconds boostDamageTime = new WaitForSeconds(.75f);
    public CircleCollider2D boostCollider;

    //Environment Effects
    public bool frozen;
    public SpriteRenderer frozenImage;
    public GameObject freezeBreakParticles;

    //GameStart
    private bool starting; //while true the player will not have control.

    //Audio
    public AudioClip boostSound;


    void Awake()
    {
        //Rewired
        int realNum = numPlayer - 1;
        playerInput = ReInput.players.GetPlayer(realNum);

        healthScript = GetComponent<PlayerHealth>();
        abilityScript = GetComponent<PlayerAbility>();
        shootScript = GetComponent<PlayerShoot>();
        rotationScript = GetComponentInChildren<PlayerRotation>();
        //burstParticleSys = burstParticles.GetComponent<ParticleSystem>();
        //moveParticleSys = moveParticles.GetComponent<ParticleSystem>();
        frozen = false;
    }
    /*
    void SetUpInputs()
    {
        if (numPlayer == 1)
        {
            inputHorizontal = "BlackHorizontal";
            inputVertical = "BlackVertical";
            inputBoost = "BlackBoost";
        }
        else if (numPlayer == 2)
        {
            inputHorizontal = "WhiteHorizontal";
            inputVertical = "WhiteVertical";
            inputBoost = "WhiteBoost";
        }
        else if (numPlayer == 3)
        {
            inputHorizontal = "Player3Horizontal";
            inputVertical = "Player3Vertical";
            inputBoost = "Player3Boost";
        }
        else if (numPlayer == 4)
        {
            inputHorizontal = "Player4Horizontal";
            inputVertical = "Player4Vertical";
            inputBoost = "Player4Boost";
        }
        else
        {
            //Debug.Log("numPlayerNotSet");
        }
    }
    */

    void Start()
    {
        burstParticleSys.startColor = abilityScript.teamColors[healthScript.teamCount];
        moveParticleSys.startColor = abilityScript.teamColors[healthScript.teamCount];
        playerRigidbody = GetComponent<Rigidbody2D>();
        SpeedBuffEffect(false);
    }

    void Update()
    {
        if (newBurstCoolDown > 0)
        {
            newBurstCoolDown -= Time.deltaTime;
        }
        else
        {
            newBurstCoolDown = 0;
        }

        switch (starting)
        {
            case (true):
                shootScript.disabled = true;
                abilityScript.disabled = true;
                break;
            case (false):
                shootScript.disabled = false;
                abilityScript.disabled = false;
                break;
        }
    }

    public void StartingGame()
    {
        starting = true;
    }

    public void GameGo()
    {
        starting = false;
    }

    void FixedUpdate()
    {
        if (!frozen && !starting)
        {
            Movement();
        }
        else
        {
            //Can't move
        }
    }

    public void FreezePlayer(float freezeTime)
    {
        if (frozen)
        {
            //Debug.Log("Already Frozen");
            return;
        }
        else
        {
            StartCoroutine(Freeze(freezeTime));
        }
    }
    

    IEnumerator Freeze(float frozenTime)
    {
        //This script's frozen boolean
        frozenImage.enabled = true;
        frozen = true;
        abilityScript.frozen = true;
        shootScript.frozen = true;
        rotationScript.frozen = true;

        yield return new WaitForSeconds(frozenTime);

        frozenImage.enabled = false;
        frozen = false;
        abilityScript.frozen = false;
        shootScript.frozen = false;
        rotationScript.frozen = false;
        //Instantiate(freezeBreakParticles, transform.position, Quaternion.identity);
    }

    public void SpeedBuffEffect(bool active)
    {
        if (active)
        {
            //Increase the base speed by 15
            speed = speedIncrease;
            //Increase the base boostSpeed by 30
            boostSpeed = boostSpeedIncrease;
        }
        else
        {
            //Debug.Log("Deactive");
            //If no speedBuff, reset values.
            boostSpeed = boostSpeedSet;
            speed = speedSet;
        }
    }

    void Movement()
    {
        float moveHorizontal;
        float moveVertical;

        moveHorizontal = playerInput.GetAxisRaw("MoveHorizontal");
        moveVertical = playerInput.GetAxisRaw("MoveVertical");


        Vector2 movement = new Vector2(moveHorizontal, moveVertical);
        //NormalMovement

        playerRigidbody.AddForce(movement * speed);
        
        if (classSelect == true)
        {
            playerRigidbody.position = new Vector2(Mathf.Clamp(playerRigidbody.position.x, minX, maxX), Mathf.Clamp(playerRigidbody.position.y, minY, maxY));
        }
        else
        {
            playerRigidbody.position = new Vector2(Mathf.Clamp(playerRigidbody.position.x, matchMinX, matchMaxX), Mathf.Clamp(playerRigidbody.position.y, matchMinY, matchMaxY));
        }


        if (newBurstCoolDown == 0)
        {
            if (playerInput.GetButton("LeftBumper"))
            {
                boostCollider.enabled = true;
                if (true)
                {
                    StartCoroutine(BoostDamage());
                    healthScript.audioSource.clip = boostSound;
                    healthScript.audioSource.Play();
                    healthScript.boosted = true;
                    playerRigidbody.AddForce(movement * boostSpeed);
                    newBurstCoolDown = newBurstCoolDownTime;
                    burstParticleSys.Play();
                }
            }
        }
    }

    IEnumerator BoostDamage()
    {
        //Debug.Log("... Called");
        
        yield return boostDamageTime;
        boostCollider.enabled = false;
        //Debug.Log("... Finished");
    }
}
