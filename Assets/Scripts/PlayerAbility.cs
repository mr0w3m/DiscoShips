using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using Rewired;

[System.Serializable]
public class DroppedJunk
{
    public GameObject junkType;
    public float weight;
}

public class PlayerAbility : MonoBehaviour
{
    private Player playerInput;

    public int numPlayer;
    public int teamCount;
    private string playerTag;
    private string yInput;
    private Rigidbody2D playerRB;

    //AbilityCoolDownMeter
    public Image abilityMeter;
    public Image uiAbilityMeter;

    //Are we dead?
    public bool isDead;

    //What ability do we have?
    public int defenseSystem;

    //Scripts
    public GameManager gameManagerScript;
    private PlayerMovement playerMoveScript;
    private PlayerHealth playerHealthScript;
    private SpriteManager playerSpriteManager;
    private PlayerRotation playerRotateScript;

    private float smoothValue = 5f;
    public float flashSpeed;
    public Color flashColor;
    

    /*
    //Defense Systems
    //_Time Shield
    public int timeShieldCoolDownTime;
    private int timeShieldCoolDown;
    //private bool invulnerable = false;
    private bool timeShieldHit = false;
    public float timeShieldDuration;
    public SpriteRenderer timeShieldGraphic;
    public SpriteRenderer timeShieldHitGraphic;
    */

    /*
    //_Grav Push
    public float gravPushAmount;
    public int gravPushCoolDownTime;
    private int gravPushCoolDown;
    public float gravPushEffRange;
    private bool gravPushWorked = false;
    public SpriteRenderer gravPushGraphic;
    private Collider2D[] pushedThings;
    */

    //_Grav Hault
    public int gravHaultCoolDownTime;
    private float gravHaultCoolDown;
    public float gravHaultEffRange;
    private int toBeEffectedLayer;
    private PlayerHealth otherPlayerHealthScript;
    private bool gravHaultWorked = false;
    public GameObject gravHaultGraphic;
    private Collider2D[] hitThings;
    private Vector2 stopVelocity = new Vector2(0, 0);
    private Rigidbody2D hitRB;
    private Animator gravHaultAnimator;
    public SpriteRenderer gravHaultSprite;
    public GameObject gravHaultIndicator;
    private GameObject indicatorInstance;
    private ParticleSystem indicatorSys;
    private WaitForSeconds gravWait = new WaitForSeconds(0.2f);

    private RemoteLauncherShot shot;
    private LineSniperShot sniperShot;
    private EnemyFollow missileScript;
    private EnemySeekerShot missile;
    private ProtoShotPrefab rapidShot;
    private PhaseBlast phaseBlastShot;

    //ShieldMines
    public int shieldMinesCoolDownTime;
    private float shieldMinesCoolDown;
    public GameObject shieldMine;
    private ParticleSystem mineTrail1;
    private ParticleSystem mineTrail2;
    private ParticleSystem mineTrail3;
    private ParticleSystem mineTrail4;

    //New Shieldmines stuff
    public GameObject mineSpinner;
    private bool minesDeployed;
    public float minesSpinSpeed;
    private GameObject mine1;
    private GameObject mine2;
    private GameObject mine3;
    private GameObject mine4;
    public Vector2 mine1Pos;
    public Vector2 mine2Pos;
    public Vector2 mine3Pos;
    public Vector2 mine4Pos;
    private ShieldMine mineScript1;
    private ShieldMine mineScript2;
    private ShieldMine mineScript3;
    private ShieldMine mineScript4;
    private Vector3 norm = new Vector3(1, 1, 1);
    //TeleBeacon
    private TeleBeacon beaconScript;
    private SpriteRenderer beaconSprite;
    private ParticleSystem beaconParticles;
    private Image beaconImage;
    public float teleBeaconCoolDownTime;
    private float teleBeaconCoolDown;
    //public int teleBeaconCoolDownDecreaseRate;
    public GameObject beacon;
    private GameObject beaconInstance;
    private bool beaconDeployed;
    private Transform beaconLoc;

    public GameObject teleFxGO1;
    public GameObject teleFxGO2;
    private GameObject teleFxGO1Copy;
    private GameObject teleFxGO2Copy;
    private ParticleSystem teleFx1;
    private ParticleSystem teleFx2;

    private float teleInputCooldownTime = 100f;
    private float teleInputCooldown;

    private bool teleBeaconBool;

    //Trip Mine
    public GameObject tripMine; //To Instantiate
    private GameObject tripMineCopy1; //To cast as
    private GameObject tripMineCopy2;
    public float tripMineCoolDownTime; //Time till next tripMine
    private float tripMineCoolDown;
    public GameObject tripMineDummy; 
    //private GameObject tripMineDummyCopy; //To enable/disable
    private TripMine tripMineScript1;
    private TripMine tripMineScript2;
    private int flip;
    
    private float finalAngle;
    private Vector2 finalPosition;
    private Transform mapTransform;

    //AutoTurret
    public float autoTurretCoolDownTime;
    private float autoTurretCoolDown;
    public GameObject autoTurret;
    private GameObject autoTurretCopy1;
    private GameObject autoTurretCopy2;
    public GameObject autoTurretDummy;
    private AutoTurret autoTurretScript1;
    private AutoTurret autoTurretScript2;
    //reuse flip from tripmine

    //AbilityPlacer
    private GameObject abilityDummyCopy;
    private SpriteRenderer dummyColor;
    private bool placeAble;
    public LineRenderer abilityPlacer;
    public GameObject abilityPlacerEnd;
    private Vector2 placerEnd;
    private Vector2 placerStart;
    public GameObject abilityPlacerStart;
    private Vector2 hitPoint;
    private RaycastHit2D placerHit;

    //DamageBugs
    public GameObject damageBug;
    private GameObject damageBugCopy1;
    private GameObject damageBugCopy2;
    public float damageBugCoolDownTime;
    private float damageBugCoolDown;
    private DamageBug damageBugScript1;
    private DamageBug damageBugScript2;
    private Rigidbody2D damageBugRb2D;
    private Vector2 straight;
    public float damageBugShotSpeed;
    private float angleDir;
    private bool sightOut;

    public GameObject dBugThrowFx;
    public ParticleSystem dBugThrowParticles;
    public SpriteRenderer dBugThrowRay;

    //Drop Junk
    public DroppedJunk[] droppableJunk;
    private GameObject junkInstance;
    public float dropJunkCoolDownTime;
    private float dropJunkCoolDown;
    public GameObject dropLocation;
    public float dropJunkWait;
    private WaitForSeconds dropWait;

    //Randomizer Vars
    private float totalWeight;
    private float randomNumber;
    private float index;

    //TractorBeam
    private float tractorBeamCoolDown;
    public float tractorBeamCoolDownTime;
    public float tractorBeamLength;
    public float tractorBeamPullSpeed;
    private WaitForSeconds beamWait;
    private bool tractorBeamActive;
    private bool tractorBeamLocked;
    private RaycastHit2D tractorBeamHit;
    private Rigidbody2D tractorBeamHitRB;
    public GameObject tractorBeamStart;
    public GameObject tractorBeamEnd;
    private float beamedDistance;
    public GameObject tractorBeamInd;
    private GameObject tractorBeamIndCopy;
    private SpriteRenderer beamIndSprite;

    public GameObject tractorBeamFx;
    public SpriteRenderer tractorBeamFxImg;
    public ParticleSystem tractorBeamParticles;

    private bool retracting;

    //WallRide
    public float wallRideCoolDownTime;
    private float wallRideCoolDown;
    public bool wallInRange;
    private RaycastHit2D wallHit;
    private Vector2 wallLocation;
    private float playerDistanceToWall;
    private Vector2 pushDirection;

    public LineRenderer wallRideLaser;

    public SpriteRenderer wallRideSprite;

    public Collider2D wallRideCollider2D;
    public Collider2D cachedCollider2D;

    private Vector2 wallNormal;
    public float maxDistFromWall;
    public float minDistFromWall;
    public float adjustmentForcePush;
    public float adjustmentForcePull;

    private float pushCoolDown;
    private float pushCoolDownTime = 0.01f;
    private float pullCoolDown;
    private float pullCoolDownTime = 0.01f;

    //WarpField
    public GameObject warpField;
    private GameObject warpFieldCopy;
    private bool warpFieldFlip = false;

    public float warpFieldCoolDownTime;
    private float warpFieldCoolDown;
    
    private WarpField warpFieldScript;

    /*
    //JetPack
    public float jetPackCoolDownTime;
    private float jetPackCoolDown = 100f;
    public float jetPackCost;
    public float jetPackIncreaseRate;
    public float jetPackDecreaseRate;
    private bool jetPackActivated;
    */

    //Color
    public Color[] teamColors;
    
    //Timer
    private float timeTracker;

    //Audio
    private AudioSource audioSource;

    //Environment Effects
    public bool frozen;

    //GameStart
    public bool disabled;
    

    void Awake()
    {
        //Rewired
        int realNum = numPlayer - 1;
        playerInput = ReInput.players.GetPlayer(realNum);

        NumPlayerToString();
        frozen = false;
        playerMoveScript = GetComponent<PlayerMovement>();
        toBeEffectedLayer = LayerMask.GetMask("ThingsToBeEffected");
        audioSource = GetComponent<AudioSource>();
        playerHealthScript = GetComponent<PlayerHealth>();
        playerRotateScript = GetComponentInChildren<PlayerRotation>();
        playerSpriteManager = GetComponent<SpriteManager>();
        gravHaultAnimator = gravHaultGraphic.GetComponent<Animator>();
        gravHaultSprite = gravHaultGraphic.GetComponent<SpriteRenderer>();
    }

    void NumPlayerToString()
    {
        if (numPlayer == 1)
        {
            playerTag = "Player1(Black)";
            //yInput = "Player1LTrigger";
        }
        else if (numPlayer == 2)
        {
            playerTag = "Player2(White)";
            //yInput = "Player2LTrigger";
        }
        else if (numPlayer == 3)
        {
            playerTag = "Player3";
            //yInput = "Player3LTrigger";
        }
        else if (numPlayer == 4)
        {
            playerTag = "Player4";
            //yInput = "Player4LTrigger";
        }
        else
        {
            //Debug.Log("numPlayerNotSet");
        }
    }

    void Start()
    {
        numPlayer = playerMoveScript.numPlayer; //set numPlayer
        playerRB = playerMoveScript.playerRigidbody;
        beaconDeployed = false;
        teleBeaconBool = true;
        gravHaultSprite.color = teamColors[playerHealthScript.teamCount];

        dropWait = new WaitForSeconds(dropJunkWait);
        beamWait = new WaitForSeconds(tractorBeamLength);
        //teleBeaconCoolDown = teleBeaconCoolDownTime;
        SetUpDropJunk();
    }

    void Update()
    {
        switch(disabled)
        {
            case (true):
                //no ability use
                break;
            case (false):
                AbilityManager();
                break;
        }
    }

    //Ability_Manager
    void AbilityManager()
    {
        if (defenseSystem == 0)
        {
            //TripMine
            if (!frozen)
            {
                TripMineAbility();
            }
            else
            {

            }
            DisplayCoolDown(tripMineCoolDown, tripMineCoolDownTime);
        }
        else if (defenseSystem == 1)
        {
            if (!frozen)
            {
                GravHaultAbility();
            }
            else
            {
                //Debug.Log("You are frozen");
            }
            DisplayCoolDown(gravHaultCoolDown, gravHaultCoolDownTime);
        }
        else if (defenseSystem == 2)
        {
            if (!frozen)
            {
                ShieldMinesAbility();
            }
            else
            {
                //Debug.Log("You are frozen");
            }
            DisplayCoolDown(shieldMinesCoolDown, shieldMinesCoolDownTime);
        }
        else if (defenseSystem == 3)
        {
            if (!frozen)
            {
                TeleBeaconAbility();
            }
            else
            {
                //Debug.Log("You are frozen");
            }
            DisplayCoolDown(teleBeaconCoolDown, teleBeaconCoolDownTime);            
        }
        else if (defenseSystem == 4)
        {
            if (!frozen)
            {
                AutoTurretAbility();
            }
            else
            {
                //you frozen
            }
            DisplayCoolDown(autoTurretCoolDown, autoTurretCoolDownTime);
        }
        else if (defenseSystem == 5)
        {
            if (!frozen)
            {
                DamageBugsAbility();
            }
            else
            {
                //you frozen
            }
            DisplayCoolDown(damageBugCoolDown, damageBugCoolDownTime);
        }
        else if (defenseSystem == 6)
        {
            if (!frozen)
            {
                DropJunkAbility();
            }
            DisplayCoolDown(dropJunkCoolDown, dropJunkCoolDownTime);
        }
        else if (defenseSystem == 7)
        {
            if (!frozen)
            {
                TractorBeamAbility();
            }
            DisplayCoolDown(tractorBeamCoolDown, tractorBeamCoolDownTime);
        }
        else if (defenseSystem == 8)
        {
            if (!frozen)
            {
                /*
                wallRideCollider2D.enabled = true;
                WallRideAbility();
                */

                WarpFieldAbility();
            }
            DisplayCoolDown(warpFieldCoolDown, warpFieldCoolDownTime);
        }
    }

    void DisplayCoolDown(float abilityCoolDown, float abilityCoolDownTime)
    {
        if (uiAbilityMeter != null)
        {
            //We can lerp this value later.
            float fillNum; //num for final float fill amount
            fillNum = abilityCoolDown / abilityCoolDownTime; //This is the percentage of where the cooldown is at
            fillNum = Mathf.Abs(fillNum - 1);
            uiAbilityMeter.fillAmount = fillNum; //Set the fill amount of the shield bar to the new value
            abilityMeter.fillAmount = fillNum;//Set the ability meter on the player
            if (abilityCoolDownTime < 0)
            {
                abilityCoolDownTime = 0;
            }
        }
    }
    void DisplayCoolDown(int abilityCoolDown, int abilityCoolDownTime)
    {
        if (uiAbilityMeter != null)
        {
            //We can lerp this value later.
            float fillNum; //num for final float fill amount
            fillNum = (float)abilityCoolDown / abilityCoolDownTime; //This is the percentage of where the cooldown is at
            uiAbilityMeter.fillAmount = fillNum; //Set the fill amount of the shield bar to the new value
            abilityMeter.fillAmount = fillNum;//Set the ability meter on the player
            if (abilityCoolDownTime < 0)
            {
                abilityCoolDownTime = 0;
            }
        }
    }
    /*
    void TESTDisplayCoolDown(int abilityCoolDown, int abilityCoolDownTime)
    {
        float coolDownFractionA = abilityCoolDownTime * 1.0f / 5.0f; //1/5 of cooldowntime
        float coolDownFractionB = abilityCoolDownTime * 2.0f / 5.0f; //2/5 of cooldowntime
        float coolDownFractionC = abilityCoolDownTime * 3.0f / 5.0f; //3/5 of cooldowntime
        float coolDownFractionD = abilityCoolDownTime * 4.0f / 5.0f; //4/5 of cooldowntime

        if (abilityCoolDown == 0)
        {
            //Show full sprite
            spriteRend.sprite = abilityMeter[5];
            if (uiAbilityMeter != null)
            {
                uiAbilityMeter.sprite = abilityMeter[5];
            }
        }
        else if(abilityCoolDown < coolDownFractionA && abilityCoolDown > 0)
        {
            //Show 4/5
            spriteRend.sprite = abilityMeter[4];
            if (uiAbilityMeter != null)
            {
                uiAbilityMeter.sprite = abilityMeter[4];
            }
        }
        else if(abilityCoolDown < coolDownFractionB && abilityCoolDown > coolDownFractionA)
        {
            //Show 3/5
            spriteRend.sprite = abilityMeter[3];
            if (uiAbilityMeter != null)
            {
                uiAbilityMeter.sprite = abilityMeter[3];
            }
        }
        else if(abilityCoolDown < coolDownFractionC && abilityCoolDown > coolDownFractionB)
        {
            //Show 2/5
            spriteRend.sprite = abilityMeter[2];
            if (uiAbilityMeter != null)
            {
                uiAbilityMeter.sprite = abilityMeter[2];
            }
        }
        else if(abilityCoolDown < coolDownFractionD && abilityCoolDown > coolDownFractionC)
        {
            //Show 1/5
            spriteRend.sprite = abilityMeter[1];
            if (uiAbilityMeter != null)
            {
                uiAbilityMeter.sprite = abilityMeter[1];
            }
        }
        else //abilityCoolDown < cooldownFraction
        {
            //Show 0/5
            spriteRend.sprite = abilityMeter[0];
            if (uiAbilityMeter != null)
            {
                uiAbilityMeter.sprite = abilityMeter[0];
            }
        }
    }

    void TESTDisplayCoolDown(float abilityCoolDown, float abilityCoolDownTime)
    {
        float coolDownFractionA = abilityCoolDownTime * 1.0f / 5.0f; //1/5 of cooldowntime
        float coolDownFractionB = abilityCoolDownTime * 2.0f / 5.0f; //2/5 of cooldowntime
        float coolDownFractionC = abilityCoolDownTime * 3.0f / 5.0f; //3/5 of cooldowntime
        float coolDownFractionD = abilityCoolDownTime * 4.0f / 5.0f; //4/5 of cooldowntime

        if (abilityCoolDown == 0)
        {
            //Show full sprite
            spriteRend.sprite = abilityMeter[5];
            if (uiAbilityMeter != null)
            {
                uiAbilityMeter.sprite = abilityMeter[5];
            }
        }
        else if (abilityCoolDown < coolDownFractionA && abilityCoolDown > 0)
        {
            //Show 4/5
            spriteRend.sprite = abilityMeter[4];
            if (uiAbilityMeter != null)
            {
                uiAbilityMeter.sprite = abilityMeter[4];
            }
        }
        else if (abilityCoolDown < coolDownFractionB && abilityCoolDown > coolDownFractionA)
        {
            //Show 3/5
            spriteRend.sprite = abilityMeter[3];
            if (uiAbilityMeter != null)
            {
                uiAbilityMeter.sprite = abilityMeter[3];
            }
        }
        else if (abilityCoolDown < coolDownFractionC && abilityCoolDown > coolDownFractionB)
        {
            //Show 2/5
            spriteRend.sprite = abilityMeter[2];
            if (uiAbilityMeter != null)
            {
                uiAbilityMeter.sprite = abilityMeter[2];
            }
        }
        else if (abilityCoolDown < coolDownFractionD && abilityCoolDown > coolDownFractionC)
        {
            //Show 1/5
            spriteRend.sprite = abilityMeter[1];
            if (uiAbilityMeter != null)
            {
                uiAbilityMeter.sprite = abilityMeter[1];
            }
        }
        else //abilityCoolDown < cooldownFraction
        {
            //Show 0/5
            spriteRend.sprite = abilityMeter[0];
            if (uiAbilityMeter != null)
            {
                uiAbilityMeter.sprite = abilityMeter[0];
            }
        }
    }
    */
    /*
    //Grav Push
    void GravPushAbility()
    {
        //If the cooldown is 0 and we're not dead, read input.
        if (gravPushCoolDown < 1 && !isDead)
        {
            if (Input.GetAxis(yInput) > 0.1)
            {
                GravPushEffect();
            }
        }

        //If there are things to grav hault, grav hault will work, this is here to trigger the image flash.
        if (gravPushWorked == true)
        {
            gravPushGraphic.color = flashColor;
        }
        else //This lerps the graphic back to nothing.
        {
            gravPushGraphic.color = Color.Lerp(gravPushGraphic.color, Color.clear, flashSpeed * Time.deltaTime);
        }
        gravPushWorked = false; //This sets the boolean immediately to false after activation, triggering the else statement above.

        //If the cooldown is greater than 0, reduce it.
        if (gravPushCoolDown > 0)
        {
            gravPushCoolDown -= 1;
        }
    }
    void GravPushEffect()
    {
        //Cast a overlapcircleAll to get all the 2D colliders in the immediate area.
        pushedThings = Physics2D.OverlapCircleAll(transform.position, gravPushEffRange, toBeEffectedLayer);

        //If we hit something, continue.
        if (pushedThings != null)
        {
            //Set the flash image boolean
            gravPushWorked = true;
            //Reset the cooldown timer
            gravPushCoolDown = gravPushCoolDownTime;
            //For every hit thing we check what it is
            for (int i = 0; i < pushedThings.Length; i++)
            {
                Debug.Log("PushHit: " + pushedThings[i]);
                if (pushedThings[i].tag == "PlayerPhysicsCollider")
                {
                    
                }
                if (pushedThings[i].tag == "RocketAmmo") //If we hit Rocket Ammo, stop it
                {
                    RemoteLauncherShot shot = pushedThings[i].GetComponent<RemoteLauncherShot>();
                    shot.notMoving = true;

                }
                else if (pushedThings[i].tag == "SniperBullet") //If we hit Sniper bullet stop it.
                {
                    LineSniperShot shot = pushedThings[i].GetComponent<LineSniperShot>();
                    shot.notMoving = true;

                }
                else if (pushedThings[i].tag == "Player1(Black)" || pushedThings[i].tag == "Player2(White)" || pushedThings[i].tag == "Player3" || pushedThings[i].tag == "Player4")
                {
                    //If player, grab teamCount, if it's ours, return, else stop them!
                    otherPlayerHealthScript = GetComponent<PlayerHealth>();
                    if (otherPlayerHealthScript.teamCount != teamCount)
                    {
                        hitRB = hitThings[i].GetComponent<Rigidbody2D>();
                        //hitRB.velocity = stopVelocity;
                        //hitRB.angularVelocity = 0;
                        Vector2 backward = pushedThings[i].transform.position - transform.position;
                        hitRB.AddForce(backward.normalized * gravPushAmount);
                    }
                }
                else
                {
                    hitRB = pushedThings[i].GetComponent<Rigidbody2D>();
                    if (hitRB != null)
                    {
                        //hitRB.velocity = stopVelocity;
                        //hitRB.angularVelocity = 0;
                        Vector2 backward = pushedThings[i].transform.position - transform.position;
                        hitRB.AddForce(backward.normalized * gravPushAmount);
                    }
                }
            }
        }
        else
        {

        }
    }
    */
    void TripMineAbility()
    {
        if (tripMineCoolDown > 0)
        {
            tripMineCoolDown -= Time.deltaTime;
        }
        else
        {
            tripMineCoolDown = 0;
        }

        if (tripMineCoolDown < 1 && !isDead)
        {
            if (playerInput.GetAxis("LeftTrigger") > 0.1f)
            {
                //pull out ability launcher
                abilityPlacer.enabled = true;
                
                AbilityPlacerFunction(tripMineDummy);

            }
            else //Trigger up
            {
                //put away launcher
                abilityPlacer.enabled = false;
                Destroy(abilityDummyCopy);

                //Instantiate new one if
                if (placeAble)
                {
                    placeAble = false;

                    if (tripMineCopy1 == null) //if copy1 isn't out, use that
                    {
                        //place 1
                        PlaceMine1();
                        flip = 1;
                    }
                    else //if it is out,
                    {
                        if (tripMineCopy2 == null) //and copy2 isn't out, use that
                        {
                            //place 2
                            PlaceMine2();
                            flip = 0;
                        }
                        else //if it is out,
                        {
                            if (flip == 0)
                            {
                                tripMineScript1.BlowUp(); //Destroy copy 1
                                PlaceMine1();
                                flip = 1;
                            }
                            else // 1
                            {
                                tripMineScript2.BlowUp(); //Destroy copy 2
                                PlaceMine2();
                                flip = 0;
                            }
                        }
                    }
                    //reset cooldown
                    tripMineCoolDown = tripMineCoolDownTime;
                }
            }
        }

    }
    void PlaceMine1()
    {
        tripMineCopy1 = Instantiate(tripMine, finalPosition, Quaternion.identity) as GameObject;
        //turn
        tripMineCopy1.transform.eulerAngles = new Vector3(tripMineCopy1.transform.eulerAngles.x, tripMineCopy1.transform.eulerAngles.y, finalAngle);
        tripMineCopy1.transform.SetParent(mapTransform);

        tripMineScript1 = tripMineCopy1.GetComponent<TripMine>();
        tripMineScript1.teamCount = teamCount;
        tripMineScript1.finalAngle = finalAngle;
        tripMineScript1.laser.SetColors(teamColors[teamCount], teamColors[teamCount]);
        tripMineScript1.colorStrip.color = teamColors[teamCount];
    }
    void PlaceMine2()
    {
        tripMineCopy2 = Instantiate(tripMine, finalPosition, Quaternion.identity) as GameObject;
        //turn
        tripMineCopy2.transform.eulerAngles = new Vector3(tripMineCopy2.transform.eulerAngles.x, tripMineCopy2.transform.eulerAngles.y, finalAngle);
        tripMineCopy2.transform.SetParent(mapTransform);

        tripMineScript2 = tripMineCopy2.GetComponent<TripMine>();
        tripMineScript2.teamCount = teamCount;
        tripMineScript2.finalAngle = finalAngle;
        tripMineScript2.laser.SetColors(teamColors[teamCount], teamColors[teamCount]);
        tripMineScript2.colorStrip.color = teamColors[teamCount];
    }

    void AbilityPlacerFunction(GameObject dummy)
    {
        placerStart = abilityPlacerStart.transform.position;
        abilityPlacer.SetPosition(0, placerStart);

        placerEnd = abilityPlacerEnd.transform.position;
        

        //Check for target
        placerHit = Physics2D.Linecast(placerStart, placerEnd, toBeEffectedLayer);

        if (placerHit)
        {
            hitPoint = placerHit.point;
            abilityPlacer.SetPosition(1, hitPoint);
            if (placerHit.transform.tag == "Map" || placerHit.transform.tag == "Asteroid")
            {
                if (abilityDummyCopy == null)
                {
                    abilityDummyCopy = Instantiate(dummy, hitPoint, Quaternion.identity) as GameObject;
                    dummyColor = abilityDummyCopy.GetComponentInChildren<SpriteRenderer>();
                    dummyColor.color = teamColors[teamCount];
                }
                else
                {
                    abilityDummyCopy.transform.position = hitPoint;
                }
                //We have to convert this X and Y into a degrees
                //First we have to find the ArcTan of both then multiply to get the degrees.
                float angleDir = Mathf.Atan2(placerHit.normal.y, placerHit.normal.x) * Mathf.Rad2Deg;
                angleDir -= 90;
                abilityDummyCopy.transform.eulerAngles = new Vector3(abilityDummyCopy.transform.eulerAngles.x, abilityDummyCopy.transform.eulerAngles.y, angleDir);

                finalAngle = angleDir;
                finalPosition = placerHit.point;
                mapTransform = placerHit.transform;

                placeAble = true;
            }
            else
            {
                placeAble = false;
            }
        }
        else
        {
            Destroy(abilityDummyCopy);
            abilityPlacer.SetPosition(1, placerEnd);
            placeAble = false;
        }
    }

    //Grav_Hault
    void GravHaultAbility()
    {
        //If the cooldown is 0 and we're not dead, read input.
        if (gravHaultCoolDown == 0 && !isDead)
        {
            if (playerInput.GetAxis("LeftTrigger") > 0.1)
            {
                gravHaultAnimator.SetTrigger("gravHaultActivate");
                StartCoroutine(GravHaultRun());
            }
        }

        /*
        //If there are things to grav hault, grav hault will work, this is here to trigger the image flash.
        if (gravHaultWorked == true)
        {
            gravHaultGraphic.color = flashColor;
        }
        else //This lerps the graphic back to nothing.
        {
            gravHaultGraphic.color = Color.Lerp(gravHaultGraphic.color, Color.clear, flashSpeed * Time.deltaTime);
        }
        gravHaultWorked = false; //This sets the boolean immediately to false after activation, triggering the else statement above.
        */

        //If the cooldown is greater than 0, reduce it.
        if (gravHaultCoolDown > 0)
        {
            gravHaultCoolDown -= Time.deltaTime;
        }
        else
        {
            gravHaultCoolDown = 0;
        }
    }
    IEnumerator GravHaultRun()
    {
        gravHaultCoolDown = gravHaultCoolDownTime;
        GravHaultEffect();
        yield return gravWait;
        GravHaultEffect();
    }
    void GravHaultEffect()
    {
        //Cast a overlapcircleAll to get all the 2D colliders in the immediate area.
        hitThings = Physics2D.OverlapCircleAll(transform.position, gravHaultEffRange, toBeEffectedLayer);
        
        //If we hit something, continue.
        if (hitThings != null)
        {
            //Set the flash image boolean
            gravHaultWorked = true;
            //Reset the cooldown timer
            
            //For every hit thing we check what it is
            for (int i = 0; i < hitThings.Length; i++)
            {
                //Debug.Log("PreGravHit: " + hitThings[i]);
                if (hitThings[i].tag == "PlayerPhysicsCollider")
                {
                    
                }
                if (hitThings[i].tag == "RocketAmmo") //If we hit Rocket Ammo, stop it
                {
                    shot = hitThings[i].GetComponent<RemoteLauncherShot>();
                    if (shot.teamCount != teamCount)
                    {
                        shot.notMoving = true;
                        indicatorInstance = Instantiate(gravHaultIndicator, hitThings[i].transform.position, Quaternion.identity) as GameObject;
                        indicatorSys = indicatorInstance.GetComponent<ParticleSystem>();
                        indicatorSys.startColor = teamColors[playerHealthScript.teamCount];
                    }
                }
                else if (hitThings[i].tag == "Missile")
                {
                    missile = hitThings[i].GetComponent<EnemySeekerShot>();
                    if (missile.teamCount != teamCount)
                    {
                        missileScript = hitThings[i].GetComponent<EnemyFollow>();
                        missileScript.KillFollow();
                        indicatorInstance = Instantiate(gravHaultIndicator, hitThings[i].transform.position, Quaternion.identity) as GameObject;
                        indicatorSys = indicatorInstance.GetComponent<ParticleSystem>();
                        indicatorSys.startColor = teamColors[playerHealthScript.teamCount];
                    }
                }
                else if (hitThings[i].tag == "Shot")
                {
                    rapidShot = hitThings[i].GetComponent<ProtoShotPrefab>();
                    if (rapidShot.teamCount != teamCount)
                    {
                        hitRB = hitThings[i].GetComponent<Rigidbody2D>();
                        hitRB.velocity = stopVelocity;
                        hitRB.angularVelocity = 0;
                        indicatorInstance = Instantiate(gravHaultIndicator, hitThings[i].transform.position, Quaternion.identity) as GameObject;
                        indicatorSys = indicatorInstance.GetComponent<ParticleSystem>();
                        indicatorSys.startColor = teamColors[playerHealthScript.teamCount];
                    }
                }
                else if (hitThings[i].tag == "SniperBullet") //If we hit Sniper bullet stop it.
                {
                    sniperShot = hitThings[i].GetComponent<LineSniperShot>();
                    if (sniperShot.teamCount != teamCount)
                    {
                        sniperShot.notMoving = true;
                        indicatorInstance = Instantiate(gravHaultIndicator, hitThings[i].transform.position, Quaternion.identity) as GameObject;
                        indicatorSys = indicatorInstance.GetComponent<ParticleSystem>();
                        indicatorSys.startColor = teamColors[playerHealthScript.teamCount];
                    }
                }
                else if (hitThings[i].tag == "Player1(Black)" || hitThings[i].tag == "Player2(White)" || hitThings[i].tag == "Player3" || hitThings[i].tag == "Player4")
                {
                    //If player, grab teamCount, if it's ours, return, else stop them!
                    otherPlayerHealthScript = hitThings[i].GetComponent<PlayerHealth>();
                    if (otherPlayerHealthScript.teamCount != teamCount)
                    {
                        hitRB = hitThings[i].GetComponent<Rigidbody2D>();
                        hitRB.velocity = stopVelocity;
                        hitRB.angularVelocity = 0;
                        indicatorInstance = Instantiate(gravHaultIndicator, hitThings[i].transform.position, Quaternion.identity) as GameObject;
                        indicatorSys = indicatorInstance.GetComponent<ParticleSystem>();
                        indicatorSys.startColor = teamColors[playerHealthScript.teamCount];
                    }
                }
                else if (hitThings[i].tag == "PhaseBlast")
                {
                    phaseBlastShot = hitThings[i].GetComponent<PhaseBlast>();
                    if (phaseBlastShot.teamCount != teamCount)
                    {
                        phaseBlastShot.AddDrag();
                        hitRB = hitThings[i].GetComponent<Rigidbody2D>();
                        hitRB.velocity = stopVelocity;
                        hitRB.angularVelocity = 0;
                        indicatorInstance = Instantiate(gravHaultIndicator, hitThings[i].transform.position, Quaternion.identity) as GameObject;
                        indicatorSys = indicatorInstance.GetComponent<ParticleSystem>();
                        indicatorSys.startColor = teamColors[playerHealthScript.teamCount];
                    }
                }
                else
                {
                    hitRB = hitThings[i].GetComponent<Rigidbody2D>();
                    if (hitRB != null)
                    {
                        if (hitThings[i].tag != "Map")
                        {
                            hitRB.velocity = stopVelocity;
                            hitRB.angularVelocity = 0;
                            indicatorInstance = Instantiate(gravHaultIndicator, hitThings[i].transform.position, Quaternion.identity) as GameObject;
                            indicatorSys = indicatorInstance.GetComponent<ParticleSystem>();
                            indicatorSys.startColor = teamColors[playerHealthScript.teamCount];
                        }
                    }
                }
            }
        }
        else
        {
            //Debug.Log(hitThings[i]);
        }
    }

    //Shield_Mines
    void ShieldMinesAbility()
    {
        if (minesDeployed)
        {
            mineSpinner.transform.Rotate(0, 0, minesSpinSpeed * Time.deltaTime);

            if (mine1 == null && mine2 == null && mine3 == null && mine4 == null)
            {
                minesDeployed = false;
                shieldMinesCoolDown = shieldMinesCoolDownTime;
            }
        }

        if (shieldMinesCoolDown == 0 && !isDead && !minesDeployed)
        {
            if (playerInput.GetAxis("LeftTrigger") > 0.1)
            {
                ShieldMines();
            }
        }

        if (shieldMinesCoolDown > 0 && !minesDeployed)
        {
            shieldMinesCoolDown -= Time.deltaTime;
        }
        else
        {
            shieldMinesCoolDown = 0;
        }
    }
    void ShieldMines()
    {
        //we need a reference to the spinner that we're using. an empty gameobject to use as the parent.
        //Then we start a rotation of the mines that runs until there are no mines anymore.
        
        minesDeployed = true;
        //Mine1
        mine1 = Instantiate(shieldMine, mineSpinner.transform.position, Quaternion.identity)as GameObject;
        mine1.transform.SetParent(mineSpinner.transform);
        mine1.transform.localPosition = mine1Pos;
        mineScript1 = mine1.GetComponent<ShieldMine>();
        mineScript1.myColor = teamColors[playerHealthScript.teamCount];
        mineScript1.teamCount = playerHealthScript.teamCount;
        mine1.transform.localScale = norm;

        mineTrail1 = mine1.GetComponent<ParticleSystem>();
        mineTrail1.startColor = teamColors[playerHealthScript.teamCount];

        //Mine2
        mine2 = Instantiate(shieldMine, mineSpinner.transform.position, Quaternion.identity)as GameObject;
        mine2.transform.SetParent(mineSpinner.transform);
        mine2.transform.localPosition = mine2Pos;
        mineScript2 = mine2.GetComponent<ShieldMine>();
        mineScript2.myColor = teamColors[playerHealthScript.teamCount];
        mineScript2.teamCount = playerHealthScript.teamCount;
        mine2.transform.localScale = norm;

        mineTrail2 = mine2.GetComponent<ParticleSystem>();
        mineTrail2.startColor = teamColors[playerHealthScript.teamCount];
        //Mine3
        mine3 = Instantiate(shieldMine, mineSpinner.transform.position, Quaternion.identity)as GameObject;
        mine3.transform.SetParent(mineSpinner.transform);
        mine3.transform.localPosition = mine3Pos;
        mineScript3 = mine3.GetComponent<ShieldMine>();
        mineScript3.myColor = teamColors[playerHealthScript.teamCount];
        mineScript3.teamCount = playerHealthScript.teamCount;
        mine3.transform.localScale = norm;

        mineTrail3 = mine3.GetComponent<ParticleSystem>();
        mineTrail3.startColor = teamColors[playerHealthScript.teamCount];
        //Mine4
        mine4 = Instantiate(shieldMine, mineSpinner.transform.position, Quaternion.identity) as GameObject;
        mine4.transform.SetParent(mineSpinner.transform);
        mine4.transform.localPosition = mine4Pos;
        mineScript4 = mine4.GetComponent<ShieldMine>();
        mineScript4.myColor = teamColors[playerHealthScript.teamCount];
        mineScript4.teamCount = playerHealthScript.teamCount;
        mine4.transform.localScale = norm;

        mineTrail4 = mine4.GetComponent<ParticleSystem>();
        mineTrail4.startColor = teamColors[playerHealthScript.teamCount];
    }
    
    //Tele_Beacon
    void TeleBeaconAbility()
    {
        if (teleBeaconCoolDown > 0 && !beaconDeployed)
        {
            teleBeaconCoolDown -= Time.deltaTime;
        }
        else
        {
            teleBeaconCoolDown = 0;
        }

        if (playerInput.GetAxis("LeftTrigger") > 0.5f)
        {
            if (teleBeaconCoolDown == 0 || beaconDeployed)
            {
                if (teleBeaconBool)
                {
                    TeleBeacon();
                    teleBeaconBool = false;
                }
            }
        }
        else
        {
            teleBeaconBool = true;
        }
    }
    void TeleBeacon()
    {
        if (beaconDeployed && beaconInstance != null)
        {
            Destroy(beaconInstance);
            transform.position = beaconLoc.position;
            beaconDeployed = false;
            teleBeaconCoolDown = teleBeaconCoolDownTime;
            teleFxGO1Copy = Instantiate(teleFxGO1, transform.position, Quaternion.identity) as GameObject;
            teleFxGO2Copy = Instantiate(teleFxGO2, transform.position, Quaternion.identity) as GameObject;
            teleFx1 = teleFxGO1Copy.GetComponent<ParticleSystem>();
            teleFx2 = teleFxGO2Copy.GetComponent<ParticleSystem>();
            teleFx1.startColor = teamColors[teamCount];
            teleFx2.startColor = teamColors[teamCount];
            //Debug.Log("Tele'd");
        }
        else
        {
            beaconInstance = Instantiate(beacon, transform.position, Quaternion.identity) as GameObject;
            beaconLoc = beaconInstance.transform;
            //Script
            beaconScript = beaconInstance.GetComponent<TeleBeacon>();
            //Sprite
            beaconSprite = beaconInstance.GetComponent<SpriteRenderer>();
            beaconSprite.color = teamColors[playerHealthScript.teamCount];
            //Dial Image
            beaconImage = beaconInstance.GetComponentInChildren<Image>();
            beaconImage.color = teamColors[playerHealthScript.teamCount];
            //Particles
            beaconParticles = beaconInstance.GetComponent<ParticleSystem>();
            beaconParticles.startColor = teamColors[playerHealthScript.teamCount];
            beaconDeployed = true;
            //Debug.Log("Tele Deployed");
        }
    }

    //Auto_Turret
    void AutoTurretAbility()
    {
        if (autoTurretCoolDown > 0)
        {
            autoTurretCoolDown -= Time.deltaTime;
        }
        else
        {
            autoTurretCoolDown = 0;
        }

        if (autoTurretCoolDown == 0 && !isDead)
        {
            if (playerInput.GetAxis("LeftTrigger") > 0.1f)
            {
                //pull out ability launcher
                abilityPlacer.enabled = true;

                AbilityPlacerFunction(autoTurretDummy);

            }
            else //Trigger up
            {
                //put away launcher
                abilityPlacer.enabled = false;
                Destroy(abilityDummyCopy);

                //Instantiate new one if
                if (placeAble)
                {
                    placeAble = false;

                    if (autoTurretCopy1 == null) //if copy1 isn't out, use that
                    {
                        //place 1
                        PlaceTurret1();
                        flip = 1;
                    }
                    else //if it is out,
                    {
                        if (autoTurretCopy2 == null) //and copy2 isn't out, use that
                        {
                            //place 2
                            PlaceTurret2();
                            flip = 0;
                        }
                        else //if it is out,
                        {
                            if (flip == 0)
                            {
                                autoTurretScript1.DestroyThis(); //Destroy copy 1
                                PlaceTurret1();
                                flip = 1;
                            }
                            else // 1
                            {
                                autoTurretScript2.DestroyThis(); //Destroy copy 2
                                PlaceTurret2();
                                flip = 0;
                            }
                        }
                    }
                    //reset cooldown
                    autoTurretCoolDown = autoTurretCoolDownTime;
                }
            }
        }
    }
    void PlaceTurret1()
    {
        autoTurretCopy1 = Instantiate(autoTurret, finalPosition, Quaternion.identity) as GameObject;
        //turn
        autoTurretCopy1.transform.eulerAngles = new Vector3(autoTurretCopy1.transform.eulerAngles.x, autoTurretCopy1.transform.eulerAngles.y, finalAngle);
        autoTurretCopy1.transform.SetParent(mapTransform);

        autoTurretScript1 = autoTurretCopy1.GetComponent<AutoTurret>();
        autoTurretScript1.teamCount = teamCount;
        autoTurretScript1.laser.SetColors(teamColors[teamCount], teamColors[teamCount]);
        autoTurretScript1.colorCircle.color = teamColors[teamCount];
        autoTurretScript1.shotParticles.startColor = teamColors[teamCount];
        autoTurretScript1.rangeIndicator.startColor = teamColors[teamCount];
    }
    void PlaceTurret2()
    {
        autoTurretCopy2 = Instantiate(autoTurret, finalPosition, Quaternion.identity) as GameObject;
        //turn
        autoTurretCopy2.transform.eulerAngles = new Vector3(autoTurretCopy2.transform.eulerAngles.x, autoTurretCopy2.transform.eulerAngles.y, finalAngle);
        autoTurretCopy2.transform.SetParent(mapTransform);

        autoTurretScript2 = autoTurretCopy2.GetComponent<AutoTurret>();
        autoTurretScript2.teamCount = teamCount;
        autoTurretScript2.laser.SetColors(teamColors[teamCount], teamColors[teamCount]);
        autoTurretScript2.colorCircle.color = teamColors[teamCount];
        autoTurretScript2.shotParticles.startColor = teamColors[teamCount];
        autoTurretScript2.rangeIndicator.startColor = teamColors[teamCount];
    }

    //DamageBugs
    void DamageBugsAbility()
    {
        if (damageBugCoolDown > 0)
        {
            damageBugCoolDown -= Time.deltaTime;
        }
        else
        {
            damageBugCoolDown = 0;
        }

        if (damageBugCoolDown == 0 && !isDead)
        {
            abilityPlacer.SetPosition(0, abilityPlacerStart.transform.position);
            abilityPlacer.SetPosition(1, abilityPlacerEnd.transform.position);
            if (playerInput.GetAxis("LeftTrigger") > 0.5f && sightOut == false)
            {
                //Show little laser guide
                
                dBugThrowFx.SetActive(true);
                sightOut = true;
            }
            else if (playerInput.GetAxis("LeftTrigger") < 0.5f) //Trigger up
            {
                
                dBugThrowFx.SetActive(false);
                //Fire bug
                if (sightOut)
                {
                    sightOut = false;
                    if (damageBugCopy1 == null) //if copy1 isn't out, use that
                    {
                        //place 1
                        FireDamageBug1();
                        flip = 1;
                    }
                    else //if it is out,
                    {
                        if (damageBugCopy2 == null) //and copy2 isn't out, use that
                        {
                            //place 2
                            FireDamageBug2();
                            flip = 0;
                        }
                        else //if it is out,
                        {
                            if (flip == 0)
                            {
                                damageBugScript1.DestroyThis(); //Destroy copy 1
                                FireDamageBug1();
                                flip = 1;
                            }
                            else // 1
                            {
                                damageBugScript2.DestroyThis(); //Destroy copy 2
                                FireDamageBug2();
                                flip = 0;
                            }
                        }
                    }
                    //reset cooldown
                    damageBugCoolDown = damageBugCoolDownTime;
                }
            }
        }
    }
    void FireDamageBug1()
    {
        //Fx
        
        //Create shotInstance
        damageBugCopy1 = Instantiate(damageBug, abilityPlacerStart.transform.position, Quaternion.identity) as GameObject;

        //Grab script
        damageBugScript1 = damageBugCopy1.GetComponent<DamageBug>();
        damageBugScript1.teamCount = teamCount;
        damageBugScript1.mySprite.color = teamColors[teamCount];
        damageBugScript1.healthSprite.color = teamColors[teamCount];
        damageBugScript1.moveTrail.startColor = teamColors[teamCount];
        //Grab and set Rotation
        angleDir = playerRotateScript.MyAngle();

        damageBugCopy1.transform.eulerAngles = new Vector3(transform.eulerAngles.x, transform.eulerAngles.y, angleDir);

        //Get Rigidbody, add force.
        damageBugRb2D = damageBugCopy1.GetComponent<Rigidbody2D>();
        straight = abilityPlacerEnd.transform.position - abilityPlacerStart.transform.position;
        damageBugRb2D.AddForce(straight.normalized * damageBugShotSpeed);
    }
    void FireDamageBug2()
    {
        //Fx

        //Create shotInstance
        damageBugCopy2 = Instantiate(damageBug, abilityPlacerStart.transform.position, Quaternion.identity) as GameObject;

        //Grab script
        damageBugScript2 = damageBugCopy2.GetComponent<DamageBug>();
        damageBugScript2.teamCount = teamCount;
        damageBugScript2.mySprite.color = teamColors[teamCount];
        damageBugScript2.healthSprite.color = teamColors[teamCount];
        damageBugScript2.moveTrail.startColor = teamColors[teamCount];
        //Grab and set Rotation
        angleDir = playerRotateScript.MyAngle();

        damageBugCopy2.transform.eulerAngles = new Vector3(transform.eulerAngles.x, transform.eulerAngles.y, angleDir);

        //Get Rigidbody, add force.
        damageBugRb2D = damageBugCopy2.GetComponent<Rigidbody2D>();
        straight = abilityPlacerEnd.transform.position - abilityPlacerStart.transform.position;
        damageBugRb2D.AddForce(straight.normalized * damageBugShotSpeed);
    }

    //DropJunk
    void DropJunkAbility()
    {
        if (dropJunkCoolDown > 0)
        {
            dropJunkCoolDown -= Time.deltaTime;
        }
        else
        {
            dropJunkCoolDown = 0;
        }

        if (dropJunkCoolDown == 0 && !isDead)
        {
            if (playerInput.GetAxis("LeftTrigger") > 0.1f)
            {
                StartCoroutine(DropJunk());
            }
        }
    }
    IEnumerator DropJunk()
    {
        dropJunkCoolDown = dropJunkCoolDownTime;
        Drop();
        yield return dropWait;
        Drop();
        yield return dropWait;
        Drop();
        yield return dropWait;
        Drop();
        yield return dropWait;
        Drop();
    }
    void Drop()
    {
        junkInstance = Instantiate(GenerateRandomJunk(), dropLocation.transform.position, Quaternion.identity)as GameObject;
    }
    //To generate a random piece of space junk.
    GameObject GenerateRandomJunk()
    {
        //pick a random number in the range of total weight
        randomNumber = Random.Range(0.0f, totalWeight);
        index = 0;
        //loop through the spaceJunk array checking to see if the number was less than cumulative weight
        for (int i = 0; i < droppableJunk.Length; i++)
        {
            index += droppableJunk[i].weight;
            //if the random number is less than the index than i = to corresponding junk
            if (randomNumber < index)
            {
                return droppableJunk[i].junkType;
            }
        }
        return droppableJunk[Random.Range(0, droppableJunk.Length)].junkType;
    }
    void SetUpDropJunk()
    {
        totalWeight = 0;

        for (int i = 0; i < droppableJunk.Length; i++)
        {
            totalWeight += droppableJunk[i].weight;
        }
    }

    //TractorBeam
    void TractorBeamAbility()
    {
        if (tractorBeamHitRB != null)
        {
            
        }
        else
        {
            retracting = false;
        }

        if (tractorBeamCoolDown > 0)
        {
            tractorBeamCoolDown -= Time.deltaTime;
        }
        else
        {
            tractorBeamCoolDown = 0;
            tractorBeamActive = true;
        }
        
        if (tractorBeamCoolDown == 0 && !isDead)
        {
            //if trigger is pulled and we haven't already nabbed something
            if (playerInput.GetAxis("LeftTrigger") > 0.1f && tractorBeamActive)
            {
                tractorBeamFx.SetActive(true);
                TractorBeamMonitor();
            }
            else
            {
                if (!retracting)
                {
                    tractorBeamFx.SetActive(false);
                }
            }
        }
        else
        {
            if (!retracting)
            {
                tractorBeamFx.SetActive(false);
                abilityPlacer.enabled = false;
            }
        }
    }
    /*
    IEnumerator TractorBeam()
    {
        //visible tractor light
        tractorBeamActive = true;
        yield return beamWait;
        tractorBeamActive = false;
        //not visible tractor light
        tractorBeamCoolDown = tractorBeamCoolDownTime;
    }
    */
    void TractorBeamMonitor()
    {
        if (tractorBeamActive)
        {
            //Linecast
            tractorBeamHit = Physics2D.Linecast(tractorBeamStart.transform.position, tractorBeamEnd.transform.position, toBeEffectedLayer);
            if (tractorBeamHit)
            {
                //if it hits something
                //if we hit a player/asteroid/healthBall/Anything?
                if (tractorBeamHit.transform.tag == "Player1(Black)" || tractorBeamHit.transform.tag == "Player2(White)" || tractorBeamHit.transform.tag == "Player3" || tractorBeamHit.transform.tag == "Player4" || tractorBeamHit.transform.tag == "Asteroid" || tractorBeamHit.transform.tag == "MineralPickup"/*we hit something we want to hit*/)
                {
                    /*
                    if (tractorBeamHit.transform.tag == "Player1(Black)" || tractorBeamHit.transform.tag == "Player2(White)" || tractorBeamHit.transform.tag == "Player3" || tractorBeamHit.transform.tag == "Player4")
                    {
                        otherPlayerHealthScript = tractorBeamHit.transform.GetComponent<PlayerHealth>();
                        if (otherPlayerHealthScript.teamCount != teamCount)
                        {
                            tractorBeamHitRB = tractorBeamHit.transform.GetComponent<Rigidbody2D>();
                            tractorBeamIndCopy = Instantiate(tractorBeamInd, tractorBeamHit.transform.position, Quaternion.identity) as GameObject;
                            tractorBeamIndCopy.transform.SetParent(tractorBeamHit.transform);
                            beamIndSprite = tractorBeamIndCopy.GetComponent<SpriteRenderer>();
                            beamIndSprite.color = teamColors[teamCount];
                            StartCoroutine(TractorBeamActivate());
                            tractorBeamCoolDown = tractorBeamCoolDownTime;
                            tractorBeamActive = false;
                        }
                    }
                    */
                    if (tractorBeamHit.transform.tag == "Asteroid" || tractorBeamHit.transform.tag == "MineralPickup")
                    {
                        tractorBeamHitRB = tractorBeamHit.transform.GetComponent<Rigidbody2D>();
                        tractorBeamIndCopy = Instantiate(tractorBeamInd, tractorBeamHit.transform.position, Quaternion.identity) as GameObject;
                        tractorBeamIndCopy.transform.SetParent(tractorBeamHit.transform);
                        beamIndSprite = tractorBeamIndCopy.GetComponent<SpriteRenderer>();
                        beamIndSprite.color = teamColors[teamCount];
                        StartCoroutine(TractorBeamActivate());
                        tractorBeamCoolDown = tractorBeamCoolDownTime;
                        tractorBeamActive = false;
                    }
                }
            }
        }
        else
        {

        }
    }
    IEnumerator TractorBeamActivate()
    {
        abilityPlacer.enabled = true;
        if (tractorBeamHitRB != null)
        {
            while (Vector2.Distance(transform.position, tractorBeamHitRB.position) > 4f)
            {
                retracting = true;
                if (tractorBeamHitRB != null)
                {
                    abilityPlacer.SetPosition(0, transform.position);
                    abilityPlacer.SetPosition(1, tractorBeamHit.transform.position);
                    tractorBeamHitRB.MovePosition(Vector2.Lerp(tractorBeamHitRB.position, tractorBeamStart.transform.position, tractorBeamPullSpeed * Time.deltaTime));
                    yield return null;
                }
                else
                {
                    abilityPlacer.enabled = false;
                    tractorBeamFx.SetActive(false);
                    retracting = false;
                    yield break;
                }
            }
            tractorBeamHitRB.velocity = Vector2.zero;
        }

        Destroy(tractorBeamIndCopy);
        abilityPlacer.enabled = false;
        retracting = false;
    }

    void WallRidePushCoolDowns()
    {
        if (pushCoolDown > 0)
        {
            pushCoolDown -= Time.deltaTime;
        }
        else
        {
            pushCoolDown = 0;
        }

        if (pullCoolDown > 0)
        {
            pullCoolDown -= Time.deltaTime;
        }
        else
        {
            pullCoolDown = 0;
        }
    }

    //WallRide
    void WallRideAbility()
    {
        WallRidePushCoolDowns();
        if (playerInput.GetAxis("LeftTrigger") > 0.1f)
        {
            //wallcheck collider enabled
            //wallRideCollider2D.enabled = true;
            if (wallInRange && cachedCollider2D != null)
            {
                //Wall is in range
                WallRide();
                //Activate Indicator
                wallRideSprite.enabled = true;
            }
            else
            {
                //deactivate
                wallRideSprite.enabled = false;
            }
        }
        else
        {
            //wallRideCollider2D.enabled = false;
            wallRideSprite.enabled = false;
        }
    }

    void WallRide()
    {
        //cast a ray at the map chunk connected to the collider2D
        if (cachedCollider2D != null)
        {
            RaycastHit2D[] hitInfo = Physics2D.LinecastAll(transform.position, cachedCollider2D.transform.position, toBeEffectedLayer);
            for (int i = 0; i < hitInfo.Length; i++)
            {
                //if we hit a map wall
                if (hitInfo[i].transform.tag == "Map")
                {
                    //log that hitinfo
                    wallHit = hitInfo[i];
                }
            }

            //use that to find that point on the wall
            wallLocation = wallHit.point;
            //find the distance between the player and the point on the wall
            playerDistanceToWall = Vector2.Distance(transform.position, wallLocation);
            //Debug.Log("distToWall:" + playerDistanceToWall);

            if (playerDistanceToWall > maxDistFromWall)
            {
                //tell the player to move towards the wall
                pushDirection = -wallHit.normal.normalized;
                if (pullCoolDown == 0)
                {
                    playerRB.AddForce(pushDirection * adjustmentForcePull * Time.deltaTime, ForceMode2D.Force);
                    pullCoolDown = pullCoolDownTime;
                }
            }
            else if (playerDistanceToWall < minDistFromWall)
            {
                //tell the player to move away from the wall
                pushDirection = wallHit.normal.normalized;
                if (pushCoolDown == 0)
                {
                    playerRB.AddForce(pushDirection * adjustmentForcePush * Time.deltaTime, ForceMode2D.Force);
                    pushCoolDown = pushCoolDownTime;
                }
            }
        }
    }
    
    void WarpFieldAbility()
    {
        if (warpFieldCoolDown > 0)
        {
            warpFieldCoolDown -= Time.deltaTime;
        }
        else
        {
            warpFieldCoolDown = 0;
        }

        if (playerInput.GetAxis("LeftTrigger") > 0.25f)
        {
            if (warpFieldCopy != null && warpFieldFlip)
            {
                //The warp field is out
                warpFieldScript.FlipState();
                warpFieldFlip = false;
            }
            else if (warpFieldCoolDown == 0 && warpFieldCopy == null)
            {
                WarpField();
            }
        }
        else
        {
            warpFieldFlip = true;
        }
    }
    void WarpField()
    {
        warpFieldCopy = Instantiate(warpField, transform.position, Quaternion.identity) as GameObject;
        warpFieldScript = warpFieldCopy.GetComponent<WarpField>();
        warpFieldScript.SetState(false);
        warpFieldScript.SetColors(teamColors[teamCount]);
        warpFieldCoolDown = warpFieldCoolDownTime;
        warpFieldCopy.transform.SetParent(transform);
    }
    
    public void SwapAbilityTeam(int newTeamCount)
    {
        switch(defenseSystem)
        {
            case (0):
                //TripMine
                if (tripMineCopy1 != null)
                {
                    tripMineScript1.laser.SetColors(teamColors[newTeamCount], teamColors[newTeamCount]);
                    tripMineScript1.colorStrip.color = teamColors[newTeamCount];
                    tripMineScript1.teamCount = teamCount;
                }

                if (tripMineCopy2 != null)
                {
                    tripMineScript2.laser.SetColors(teamColors[newTeamCount], teamColors[newTeamCount]);
                    tripMineScript2.colorStrip.color = teamColors[newTeamCount];
                    tripMineScript2.teamCount = teamCount;
                }
                break;
            case (1):
                //GravHault
                break;
            case (2):
                //shieldMines
                if (mine1 != null)
                {
                    mineScript1.myColor = teamColors[newTeamCount];
                    mineScript1.teamCount = newTeamCount;
                    mine1.transform.localScale = norm;
                    mineTrail1.startColor = teamColors[newTeamCount];
                }
                if (mine2 != null)
                {
                    mineScript2.myColor = teamColors[newTeamCount];
                    mineScript2.teamCount = newTeamCount;
                    mine2.transform.localScale = norm;
                    mineTrail2.startColor = teamColors[newTeamCount];
                }
                if (mine3 != null)
                {
                    mineScript3.myColor = teamColors[newTeamCount];
                    mineScript3.teamCount = newTeamCount;
                    mine3.transform.localScale = norm;
                    mineTrail3.startColor = teamColors[newTeamCount];
                }
                if (mine4 != null)
                {
                    mineScript4.myColor = teamColors[newTeamCount];
                    mineScript4.teamCount = newTeamCount;
                    mine4.transform.localScale = norm;
                    mineTrail4.startColor = teamColors[newTeamCount];
                }
                break;
            case (3):
                //telebeacon
                if (beaconInstance != null)
                {
                    beaconSprite.color = teamColors[newTeamCount];
                    beaconImage.color = teamColors[newTeamCount];
                    beaconParticles.startColor = teamColors[newTeamCount];
                }
                break;
            case (4):
                //autoturret
                if (autoTurretCopy1 != null)
                {
                    autoTurretScript1.teamCount = newTeamCount;
                    autoTurretScript1.laser.SetColors(teamColors[newTeamCount], teamColors[newTeamCount]);
                    autoTurretScript1.colorCircle.color = teamColors[newTeamCount];
                    autoTurretScript1.shotParticles.startColor = teamColors[newTeamCount];
                    autoTurretScript1.rangeIndicator.startColor = teamColors[newTeamCount];
                }

                if (autoTurretCopy2 != null)
                {
                    autoTurretScript2.teamCount = newTeamCount;
                    autoTurretScript2.laser.SetColors(teamColors[newTeamCount], teamColors[newTeamCount]);
                    autoTurretScript2.colorCircle.color = teamColors[newTeamCount];
                    autoTurretScript2.shotParticles.startColor = teamColors[newTeamCount];
                    autoTurretScript2.rangeIndicator.startColor = teamColors[newTeamCount];
                }
                break;
            case (5):
                //damageBugs
                if (damageBugCopy1 != null)
                {
                    damageBugScript1.teamCount = newTeamCount;
                    damageBugScript1.mySprite.color = teamColors[newTeamCount];
                    damageBugScript1.healthSprite.color = teamColors[newTeamCount];
                    damageBugScript1.moveTrail.startColor = teamColors[newTeamCount];
                }

                if (damageBugCopy2 != null)
                {
                    damageBugScript2.teamCount = newTeamCount;
                    damageBugScript2.mySprite.color = teamColors[newTeamCount];
                    damageBugScript2.healthSprite.color = teamColors[newTeamCount];
                    damageBugScript2.moveTrail.startColor = teamColors[newTeamCount];
                }
                break;
            case (6):
                //drop junk
                break;
            case (7):
                //tractorbeam
                break;
            case (8):
                //Wallride
                break;
        }
    }

    public void DestroyCurrentAbilites()
    {
        //GravHault
        //nothing
        //ShieldMines
        if (mine1 != null)
        {
            Destroy(mine1);
        }
        if (mine2 != null)
        {
            Destroy(mine2);
        }
        if (mine3 != null)
        {
            Destroy(mine3);
        }
        if (mine4 != null)
        {
            Destroy(mine4);
        }
        
        //TeleBeacon
        if (beaconInstance != null)
        {
            Destroy(beaconInstance);
        }

        //TripMine
        if (tripMineCopy1 != null)
        {
            Destroy(tripMineCopy1);
        }
        if (tripMineCopy2 != null)
        {
            Destroy(tripMineCopy2);
        }

        //DamageBugs
        if (damageBugCopy1 != null)
        {
            Destroy(damageBugCopy1);
        }
        if (damageBugCopy2 != null)
        {
            Destroy(damageBugCopy2);
        }

        //AutoTurret
        if (autoTurretCopy1 != null)
        {
            Destroy(autoTurretCopy1);
        }
        if (autoTurretCopy2 != null)
        {
            Destroy(autoTurretCopy2);
        }

        //TracBeam
        //WallRide
        //DropJunk
        
    }



    //This is the graveyard of this script. RIP
    /*

    //Here lies the time shield.  This broken ability gave the player invulnerability for 3 seconds every 8-10 seconds. Not too fun. Cheers time shield.

    //Time_Shield
    void TimeShieldAbility()
    {
        if (timeShieldHit == true)
        {
            timeShieldHitGraphic.color = flashColor;
        }
        else
        {
            timeShieldHitGraphic.color = Color.Lerp(timeShieldHitGraphic.color, Color.clear, flashSpeed * Time.deltaTime);
        }
        timeShieldHit = false;

        if (timeShieldCoolDown > 0)
        {
            timeShieldCoolDown -= 1;
        }

        if (timeShieldCoolDown < 1 && !isDead)
        {
            if (Input.GetAxis(yInput) > 0.1)
            {
                StartCoroutine("TimeShieldEffect");
            }
        }
    }
    IEnumerator TimeShieldEffect()
    {
        //We've got to link this up with a 'Health Script' for it to actually function.
        timeShieldCoolDown = timeShieldCoolDownTime;
        playerHealthScript.invulnerable = true;
        timeShieldGraphic.color = flashColor;
        yield return new WaitForSeconds(timeShieldDuration);
        timeShieldGraphic.color = Color.clear;
        playerHealthScript.invulnerable = false;
    }

    //This next entry, the jetPack, was replaced by a speed parameter.
    //Jet_Pack
    void JetPackAbility()
    {
        if (jetPackCoolDown < jetPackCoolDownTime && jetPackActivated != true)
        {
            jetPackCoolDown += 1 * Time.deltaTime * jetPackIncreaseRate;
        }

        if (jetPackCoolDown > 0 && !isDead)
        {
            if (Input.GetButton(yInput))
            {
                JetPackEffect();
            }
            else
            { JetPackNonEffect(); }
        }
        else
        {
            JetPackNonEffect();
        }
    }
    void JetPackEffect()
    {
        if (jetPackCoolDown < 1)
        {
            return;
        }
        else
        {
            Debug.Log("JetPackActive");
            playerMoveScript.jetPack = true;
            jetPackCoolDown -= jetPackCost * Time.deltaTime * jetPackDecreaseRate;
        }
        jetPackActivated = true;
    }
    void JetPackNonEffect()
    {
        //Debug.Log("EffectOFF");
        jetPackActivated = false;
        playerMoveScript.jetPack = false;
    }
    
    void JetPackNotActive()
    {
        if (playerMoveScript.jetPack != false || jetPackActivated != false)
        {
            JetPackNonEffect();
        }
    }

    //The old shield mines
    
    void ShieldMines()
    {
        shieldMinesCoolDown = shieldMinesCoolDownTime;
        
        //Send a single disc out.
        GameObject discInstance = Instantiate(disc, transform.position, Quaternion.identity) as GameObject;
        StartCoroutine(MoveTo(discInstance, discGoToLoc.transform.position));
        
        
    }
    IEnumerator MoveTo(GameObject instance, Vector2 endPos)
    {
        Vector2 location = endPos;
        while (Vector2.Distance(instance.transform.position, location) > 0.5f)
        {
            Debug.Log("Working...");
            instance.transform.position = Vector2.Lerp(instance.transform.position, location, smoothValue * Time.deltaTime);
            yield return null;
        }
        Debug.Log("MoveFinished");
        //Make Vector2 vars for each spot around player.
        minePositions[0] = new Vector2(instance.transform.position.x - 1.5f, instance.transform.position.y + 1.5f);
        minePositions[1] = new Vector2(instance.transform.position.x, instance.transform.position.y + 2f);
        minePositions[2] = new Vector2(instance.transform.position.x + 1.5f, instance.transform.position.y + 1.5f);
        minePositions[3] = new Vector2(instance.transform.position.x - 2f, instance.transform.position.y);
        minePositions[4] = new Vector2(instance.transform.position.x + 2f, instance.transform.position.y);
        minePositions[5] = new Vector2(instance.transform.position.x - 1.5f, instance.transform.position.y - 1.5f);
        minePositions[6] = new Vector2(instance.transform.position.x, instance.transform.position.y - 2f);
        minePositions[7] = new Vector2(instance.transform.position.x + 1.5f, instance.transform.position.y - 1.5f);
        //Debug.Log("MovedTo");
        for (int i = 0; i < 8; i++)
        {
            ShieldMineEffect(instance, minePositions[i]);
        }
    }
    void ShieldMineEffect(GameObject instance, Vector2 pos)
    {
        GameObject mineInstance = Instantiate(shieldMine, instance.transform.position, Quaternion.identity) as GameObject;
        mine = mineInstance.GetComponent<ShieldMine>();
        mine.teamCount = teamCount;
        mine.goToPosition = pos;
    }
    */
}
