using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ProtoPlayerShooting : MonoBehaviour {

    public int teamCount;
    public int shotHealthCount;

    public GameManager gameManagerScript;

    //Depending on whether we're using the offensive ability or the defensive ability
    //offensive == true  /  defensive == false
    private bool systems;
    public int offenseSystem; // 1-RapidShot, 2-RemoteLauncher, 3-LineSniper, 4-FocusBeam
    public int defenseSystem; // 1-TimeShield, 2-GravHault, 3-MineSpread, 4-(UNKNOWN)

    //Offense Systems
    //RapidShot
    public int rapidShotCoolDownTime;
    private int rapidShotCoolDown;
    public int rapidShotCost;
    public float rapidShotShotSpeed;
    public GameObject rapidShotShotPrefab;
    private ProtoShotPrefab shotPrefabScript;
    //RemoteLauncher
    public int remoteLauncherCoolDownTime;
    private int remoteLauncherCoolDown;
    public int remoteLauncherCost;
    public float remoteLauncherShotSpeed;
    public GameObject remoteLauncherShotPrefab;
    private RemoteLauncherShot remoteLauncherShotScript;
    public bool rocketFired;
    //LineSniper
    public int lineSniperCoolDownTime;
    private int lineSniperCoolDown;
    public int lineSniperCost;
    public float lineSniperShotSpeed;
    public GameObject lineSniperShotPrefab;
    private LineSniperShot lineSniperShotScript;
    //FocusBeam
    private int focusBeamEnemyDamageCurrent;
    public int focusBeamEnemyBaseDamage;
    public int focusBeamEnemyDamageMultiplier;
    private int focusBeamEnemyTimeHold;
    public int focusBeamMultiplyTime;
    public int focusBeamDamageCoolDownTime;
    private int focusBeamDamageCoolDown;
    public int focusBeamCost;
    private LineRenderer focusBeamRenderer;
    private bool focusBeamInUse;
    public GameObject focusBeamEnd;
    private RaycastHit2D focusBeamHitSomething;
    public GameObject focusBeamHitParticles;
    public GameObject focusBeamSmokeParticles;

    private int hitCoolDown;
    private int hitCoolDownTime = 10;
    

    private Text numberText;
    private RectTransform numberTextTransform;

    public GameObject numSpawnPos;
    public GameObject damageNum;
    private Text damageNumText;

    //public GameObject shotPrefab;
    public GameObject barrelEnd;
    public float angleDir;

    private Asteroid asteroidScript;
    private ProtoPlayerShooting enemyShootScript;
    private ProtoPlayerMovement playerMoveScript;
    private Rigidbody2D playerRB;

    public Color blackText;
    public Color whiteText;
    public Color hurtColor;
    public Color healedColor;
    public float flashSpeed;
    public Vector2 textFlashSize;

    public Color flashColor;
    public SpriteRenderer damageImage;
    public SpriteRenderer healImage;
    public SpriteRenderer abilityLight;

    private int abilityLightCoolDown;
    private int abilityLightCoolDownTime = 30;
    private bool abilityReady;

    private Vector2 textNormalSize = new Vector2(.05f, .05f);

    private bool damaged = false;
    private bool healed = false;

    //Defense Systems
    //_Time Shield
    public int timeShieldCoolDownTime;
    private int timeShieldCoolDown;
    private bool invulnerable = false;
    private bool timeShieldHit = false;
    public float timeShieldDuration;
    public SpriteRenderer timeShieldGraphic;
    public SpriteRenderer timeShieldHitGraphic;
    //_Grav Hault
    public int gravHaultCoolDownTime;
    private int gravHaultCoolDown;
    public float gravHaultEffRange;
    private int toBeEffectedLayer;
    private bool gravHaultWorked = false;
    public SpriteRenderer gravHaultGraphic;
    //ShieldMines
    public int shieldMinesCoolDownTime;
    private int shieldMinesCoolDown;
    public GameObject shieldMine;
    private Vector2 pos1;
    private ShieldMine mine1;
    private Vector2 pos2;
    private ShieldMine mine2;
    private Vector2 pos3;
    private ShieldMine mine3;
    private Vector2 pos4;
    private ShieldMine mine4;
    private Vector2 pos5;
    private ShieldMine mine5;
    private Vector2 pos6;
    private ShieldMine mine6;
    private Vector2 pos7;
    private ShieldMine mine7;
    private Vector2 pos8;
    private ShieldMine mine8;
    //JetPack
    public float jetPackCoolDownTime;
    private float jetPackCoolDown = 100f;
    public float jetPackCost;
    public float dragSwitch;
    public float angularDragSwitch;
    private float originalDrag = 2;
    private float originalAngularDrag = 2;
    public float jetPackIncreaseRate;
    public float jetPackDecreaseRate;
    private bool jetPackActivated;


    //Audio
    private AudioSource audioSource;

    //RapidShot
    public AudioClip rapidShotSound;
    //RemoteLauncher
    public AudioClip launchSound;
    public AudioClip rocketBoosters;
    public AudioClip remoteDetonation;
    //LineSniper
    public AudioClip lineSniperSound;
    public AudioClip bulletTrail;
    //FocusBeam
    public AudioClip focusBeamSound;


    void Awake()
    {
        numberText = GetComponentInChildren<Text>();
        playerMoveScript = GetComponent<ProtoPlayerMovement>();
        numberTextTransform = numberText.GetComponent<RectTransform>();
        shotHealthCount = 100;
        Display(shotHealthCount);
        toBeEffectedLayer = LayerMask.GetMask("ThingsToBeEffected");
        playerRB = GetComponent<Rigidbody2D>();
        focusBeamRenderer = GetComponentInChildren<LineRenderer>();
        audioSource = GetComponent<AudioSource>();
    }

    void Start()
    {
        teamCount = playerMoveScript.teamCount; //set teamCount
        rocketFired = false;
        jetPackActivated = false;
        focusBeamRenderer.enabled = false;
        hitCoolDown = 0;
    }

    void FixedUpdate()
    {
        OffensiveSystem(teamCount);
        DefensiveSystem(teamCount);
        if (damaged == true)
        {
            damageImage.color = flashColor;
            numberText.color = hurtColor;
            numberTextTransform.localScale = textFlashSize;
        }
        else if (healed == true)
        {
            healImage.color = flashColor;
            numberText.color = healedColor;
            numberTextTransform.localScale = textFlashSize;
        }
        else
        {
            damageImage.color = Color.Lerp(damageImage.color, Color.clear, flashSpeed * Time.deltaTime);
            healImage.color = Color.Lerp(healImage.color, Color.clear, flashSpeed * Time.deltaTime);
            numberTextTransform.localScale = Vector2.Lerp(numberTextTransform.localScale, textNormalSize, flashSpeed * Time.deltaTime);
            /*
            _________________________________________
            This Separation of text color should disapear in the future
            because of the removal of text in the player health readout
            This code in general will dissapear in the future as well given
            we won't have a constant text. Just pop in and out text.
            _________________________________________
            */
            if (teamCount == 1)
            {
                numberText.color = Color.Lerp(numberText.color, blackText, flashSpeed * Time.deltaTime);
                //numberText.color = Color.white;
            }
            else //teamCount == 2
            {
                numberText.color = Color.Lerp(numberText.color, whiteText, flashSpeed * Time.deltaTime);
                //numberText.color = Color.black;
            }
        }
        damaged = false;
        healed = false;

        if (abilityReady == true)
        {
            abilityLight.color = Color.white;
        }
        else
        {
            abilityLight.color = Color.Lerp(abilityLight.color, Color.clear, flashSpeed * Time.deltaTime);
        }
        abilityReady = false;
    }

    public void Damage(int amountDamaged, bool selfInflicted)
    {
        //Time Shield
        if (invulnerable == true && selfInflicted != true)
        {
            timeShieldHit = true;
            Debug.Log("HitShield");
            return;
        }
        
        
        shotHealthCount -= amountDamaged;
        if (shotHealthCount < 1)
        {
            Destroy(this.gameObject);
        }
        Display(shotHealthCount);
        if (selfInflicted != true)
        {
            damaged = true;
            DamageNumbers(amountDamaged);
        }
    }

    void DamageNumbers(int amtDmg)
    {
        GameObject damageNumCopy;
        damageNumCopy = Instantiate(damageNum, numSpawnPos.transform.position, Quaternion.identity) as GameObject;
        damageNumText = damageNumCopy.GetComponentInChildren<Text>();
        string i = amtDmg.ToString();
        damageNumText.text = i;
    }

    public void PickUp(int amountPickedUp)
    {
        shotHealthCount += amountPickedUp;
        if (shotHealthCount > 100)
        {
            shotHealthCount = 100;
        }
        Display(shotHealthCount);
        healed = true;
    }

    /*
    ____________________________________________________________
    This script is a mess.  There should really be multiple functions being called below.
    There's a lot of copied code that could cause some balance bugs because something was changed only in one player
    So we need to go back through this code when we launch the video and smooth all of this out. 
    ____________________________________________________________
    */

    void OffensiveSystem(int teamCount)
    {
        if (teamCount == 1)
        {
            //RapidShot
            if (offenseSystem == 1)
            {
                if (rapidShotCoolDown > 0)
                {
                    rapidShotCoolDown -= 1;
                }
                if (Input.GetAxis("BlackFire1") > 0.1 && rapidShotCoolDown < 1 && shotHealthCount > 1)
                {
                    RapidShot();
                }
            }
            //RemoteLauncher
            else if (offenseSystem == 2)
            {
                if (remoteLauncherCoolDown > 0)
                {
                    remoteLauncherCoolDown -= 1;
                }
                
                if (Input.GetButtonDown("BlackFire1"))
                {
                    //Debug.Log("remoteLauncherCoolDown: " + remoteLauncherCoolDown);
                    //Debug.Log("rocketFired: " +rocketFired);
                    if (rocketFired != true)
                    {
                        if (remoteLauncherCoolDown < 1 && shotHealthCount > 3)
                        {
                            Debug.Log("rocket fired");
                            RemoteLauncher();
                        }
                        else
                        {
                            Debug.Log("notYet");
                        }
                    }
                    else
                    {
                        remoteLauncherShotScript.ExplosiveTrigger();
                        Debug.Log("trigger called");
                        rocketFired = false;
                    }
                }
            }
            //LineSniper
            else if (offenseSystem == 3)
            {
                if (lineSniperCoolDown > 0)
                {
                    lineSniperCoolDown -= 1;
                }

                if (Input.GetButton ("BlackFire1") && lineSniperCoolDown < 1 && shotHealthCount > 5)
                {
                    LineSniper();
                }
            }
            //FocusBeam
            else if (offenseSystem == 4)
            {
                if (hitCoolDown > 0)
                {
                    hitCoolDown -= 1;
                }
                //damaging player and the attack are separated here.
                //Constant cooldown
                if (focusBeamDamageCoolDown > 0)
                {
                    focusBeamDamageCoolDown -= 1;
                }
                //If we are using the attack and the cooldown is ready, damage the player
                if (Input.GetAxis("BlackFire1") > 0.1f && focusBeamDamageCoolDown == 0 && shotHealthCount > 1)
                {
                    focusBeamDamageCoolDown = focusBeamDamageCoolDownTime;
                    Damage(focusBeamCost, true);
                }
                //If we are using the attack here is the attack.
                //Happens whenever the button is held down, reguardless of cooldown.
                if (Input.GetAxis("BlackFire1") > 0.1f && shotHealthCount > 1)
                {
                    focusBeamInUse = true;
                    if (hitCoolDown == 0)
                    {
                        hitCoolDown = hitCoolDownTime;
                        FocusBeam(true);
                    }
                    else
                    {
                        FocusBeam(false);
                    }
                }
                else
                {
                    focusBeamInUse = false;
                }

                if (focusBeamInUse == true)
                {
                    //Debug.Log("InUse");
                    focusBeamRenderer.enabled = true;
                }
                else
                {
                    //Debug.Log("Not");
                    focusBeamRenderer.enabled = false;
                }
            }
            else
            {
                if (Input.GetButton("BlackFire1"))
                {
                    Debug.Log("WeaponNotSelected");
                }
            }
        }
        else if(teamCount == 2)
        {
            //RapidShot
            if (offenseSystem == 1)
            {
                if (rapidShotCoolDown > 0)
                {
                    rapidShotCoolDown -= 1;
                }
                if (Input.GetAxis("WhiteFire1") > 0.1 && rapidShotCoolDown < 1 && shotHealthCount > 1)
                {
                    RapidShot();
                }
            }
            //RemoteLauncher
            else if (offenseSystem == 2)
            {
                if (remoteLauncherCoolDown > 0)
                {
                    remoteLauncherCoolDown -= 1;
                }
                if (Input.GetButtonDown("WhiteFire1"))
                {
                    Debug.Log("remoteLauncherCoolDown: " + remoteLauncherCoolDown);
                    Debug.Log("rocketFired: " + rocketFired);
                    if (rocketFired != true)
                    {
                        if (remoteLauncherCoolDown < 1 && shotHealthCount > 3)
                        {
                            Debug.Log("rocket fired");
                            RemoteLauncher();
                        }
                    }
                    else
                    {
                        remoteLauncherShotScript.ExplosiveTrigger();
                        Debug.Log("trigger called");
                        rocketFired = false;
                    }
                }
            }
            //LineSniper
            else if (offenseSystem == 3)
            {
                if (lineSniperCoolDown > 0)
                {
                    lineSniperCoolDown -= 1;
                }

                if (Input.GetButton("WhiteFire1") && lineSniperCoolDown < 1 && shotHealthCount > 5)
                {
                    LineSniper();
                }
            }
            //FocusBeam
            else if (offenseSystem == 4)
            {
                if (hitCoolDown > 0)
                {
                    hitCoolDown -= 1;
                }
                if (focusBeamDamageCoolDown > 0)
                {
                    focusBeamDamageCoolDown -= 1;
                }
                if (Input.GetAxis("WhiteFire1") > 0.1f && focusBeamDamageCoolDown == 0)
                {
                    focusBeamDamageCoolDown = focusBeamDamageCoolDownTime;
                    Damage(focusBeamCost, true);
                }
                if (Input.GetAxis("WhiteFire1") > 0.1f && shotHealthCount > 1)
                {
                    focusBeamInUse = true;
                    if (hitCoolDown == 0)
                    {
                        hitCoolDown = hitCoolDownTime;
                        FocusBeam(true);
                    }
                    else
                    {
                        FocusBeam(false);
                    }
                }
                else
                {
                    focusBeamInUse = false;
                }

                if (focusBeamInUse == true)
                {
                    //Debug.Log("InUse");
                    focusBeamRenderer.enabled = true;
                }
                else
                {
                    //Debug.Log("Not");
                    focusBeamRenderer.enabled = false;
                }
            }
            else
            {
                if (Input.GetButton("WhiteFire1"))
                {
                    Debug.Log("WeaponNotSelected");
                }
            }
        }

        else if (teamCount == 3)
        {
            //RapidShot
            if (offenseSystem == 1)
            {
                if (rapidShotCoolDown > 0)
                {
                    rapidShotCoolDown -= 1;
                }
                if (Input.GetAxis("Player3Fire1") > 0.1 && rapidShotCoolDown < 1 && shotHealthCount > 1)
                {
                    RapidShot();
                }
            }
            //RemoteLauncher
            else if (offenseSystem == 2)
            {
                if (remoteLauncherCoolDown > 0)
                {
                    remoteLauncherCoolDown -= 1;
                }
                if (Input.GetButtonDown("Player3Fire1"))
                {
                    Debug.Log("remoteLauncherCoolDown: " + remoteLauncherCoolDown);
                    Debug.Log("rocketFired: " + rocketFired);
                    if (rocketFired != true)
                    {
                        if (remoteLauncherCoolDown < 1 && shotHealthCount > 3)
                        {
                            Debug.Log("rocket fired");
                            RemoteLauncher();
                        }
                    }
                    else
                    {
                        remoteLauncherShotScript.ExplosiveTrigger();
                        Debug.Log("trigger called");
                        rocketFired = false;
                    }
                }
            }
            //LineSniper
            else if (offenseSystem == 3)
            {
                if (lineSniperCoolDown > 0)
                {
                    lineSniperCoolDown -= 1;
                }

                if (Input.GetButton("Player3Fire1") && lineSniperCoolDown < 1 && shotHealthCount > 5)
                {
                    LineSniper();
                }
            }
            //FocusBeam
            else if (offenseSystem == 4)
            {
                if (hitCoolDown > 0)
                {
                    hitCoolDown -= 1;
                }
                if (focusBeamDamageCoolDown > 0)
                {
                    focusBeamDamageCoolDown -= 1;
                }
                if (Input.GetAxis("Player3Fire1") > 0.1f && focusBeamDamageCoolDown == 0)
                {
                    focusBeamDamageCoolDown = focusBeamDamageCoolDownTime;
                    Damage(focusBeamCost, true);
                }
                if (Input.GetAxis("Player3Fire1") > 0.1f && shotHealthCount > 1)
                {
                    focusBeamInUse = true;
                    if (hitCoolDown == 0)
                    {
                        hitCoolDown = hitCoolDownTime;
                        FocusBeam(true);
                    }
                    else
                    {
                        FocusBeam(false);
                    }
                }
                else
                {
                    focusBeamInUse = false;
                }

                if (focusBeamInUse == true)
                {
                    //Debug.Log("InUse");
                    focusBeamRenderer.enabled = true;
                }
                else
                {
                    //Debug.Log("Not");
                    focusBeamRenderer.enabled = false;
                }
            }
            else
            {
                if (Input.GetButton("Player3Fire1"))
                {
                    Debug.Log("WeaponNotSelected");
                }
            }
        }

        else if (teamCount == 4)
        {
            //RapidShot
            if (offenseSystem == 1)
            {
                if (rapidShotCoolDown > 0)
                {
                    rapidShotCoolDown -= 1;
                }
                if (Input.GetAxis("Player4Fire1") > 0.1 && rapidShotCoolDown < 1 && shotHealthCount > 1)
                {
                    RapidShot();
                }
            }
            //RemoteLauncher
            else if (offenseSystem == 2)
            {
                if (remoteLauncherCoolDown > 0)
                {
                    remoteLauncherCoolDown -= 1;
                }
                if (Input.GetButtonDown("Player4Fire1"))
                {
                    Debug.Log("remoteLauncherCoolDown: " + remoteLauncherCoolDown);
                    Debug.Log("rocketFired: " + rocketFired);
                    if (rocketFired != true)
                    {
                        if (remoteLauncherCoolDown < 1 && shotHealthCount > 3)
                        {
                            Debug.Log("rocket fired");
                            RemoteLauncher();
                        }
                    }
                    else
                    {
                        remoteLauncherShotScript.ExplosiveTrigger();
                        Debug.Log("trigger called");
                        rocketFired = false;
                    }
                }
            }
            //LineSniper
            else if (offenseSystem == 3)
            {
                if (lineSniperCoolDown > 0)
                {
                    lineSniperCoolDown -= 1;
                }

                if (Input.GetButton("Player4Fire1") && lineSniperCoolDown < 1 && shotHealthCount > 5)
                {
                    LineSniper();
                }
            }
            //FocusBeam
            else if (offenseSystem == 4)
            {
                if (hitCoolDown > 0)
                {
                    hitCoolDown -= 1;
                }
                if (focusBeamDamageCoolDown > 0)
                {
                    focusBeamDamageCoolDown -= 1;
                }
                if (Input.GetAxis("Player4Fire1") > 0.1f && focusBeamDamageCoolDown == 0)
                {
                    focusBeamDamageCoolDown = focusBeamDamageCoolDownTime;
                    Damage(focusBeamCost, true);
                }
                if (Input.GetAxis("Player4Fire1") > 0.1f && shotHealthCount > 1)
                {
                    focusBeamInUse = true;
                    if (hitCoolDown == 0)
                    {
                        hitCoolDown = hitCoolDownTime;
                        FocusBeam(true);
                    }
                    else
                    {
                        FocusBeam(false);
                    }
                }
                else
                {
                    focusBeamInUse = false;
                }

                if (focusBeamInUse == true)
                {
                    //Debug.Log("InUse");
                    focusBeamRenderer.enabled = true;
                }
                else
                {
                    //Debug.Log("Not");
                    focusBeamRenderer.enabled = false;
                }
            }
            else
            {
                if (Input.GetButton("Player4Fire1"))
                {
                    Debug.Log("WeaponNotSelected");
                }
            }
        }
    }
    
    void RapidShot()
    {
        Damage(rapidShotCost, true);
        audioSource.PlayOneShot(rapidShotSound);
        rapidShotCoolDown = rapidShotCoolDownTime;
        Debug.Log("Rapid_Shot Fired");
        //Create shotInstance
        GameObject shotInstance;
        shotInstance = Instantiate(rapidShotShotPrefab, barrelEnd.transform.position, Quaternion.identity) as GameObject;
        //Grab shotScript
        shotPrefabScript = shotInstance.GetComponent<ProtoShotPrefab>();
        shotPrefabScript.teamCount = teamCount;
        //Grab and set Rotation
        angleDir = playerMoveScript.myAngle();
        //Debug.Log("SHOT angleDir: " + angleDir);
        shotInstance.transform.eulerAngles = new Vector3(transform.eulerAngles.x, transform.eulerAngles.y, angleDir);
        //Get Rigidbody, add force.
        Rigidbody2D rb2d = shotInstance.GetComponent<Rigidbody2D>();
        Vector2 straight = barrelEnd.transform.position - transform.position;
        rb2d.AddForce(straight * rapidShotShotSpeed);
    }

    void RemoteLauncher()
    {
        rocketFired = true;
        Damage(remoteLauncherCost, true);
        //audioSource.PlayOneShot(launchSound);
        remoteLauncherCoolDown = remoteLauncherCoolDownTime;
        //Debug.Log("Remote_Launcher Fired, rocketFired=" + rocketFired);
        //Create shotInstance
        GameObject shotInstance;
        shotInstance = Instantiate(remoteLauncherShotPrefab, barrelEnd.transform.position, Quaternion.identity) as GameObject;
        //Grab remoteLauncherShotScript
        remoteLauncherShotScript = shotInstance.GetComponent<RemoteLauncherShot>();
        remoteLauncherShotScript.teamCount = teamCount;
        //remoteLauncherShotScript.myShotScript = this;
        //Grab and set Rotation
        angleDir = playerMoveScript.myAngle();
        shotInstance.transform.eulerAngles = new Vector3(transform.eulerAngles.x, transform.eulerAngles.y, angleDir);
        //Get Rigidbody, add force.
        Rigidbody2D rb2d = shotInstance.GetComponent<Rigidbody2D>();
        Vector2 straight = barrelEnd.transform.position - transform.position;
        rb2d.AddForce(straight * remoteLauncherShotSpeed);
        Debug.Log("rocketFired: " + rocketFired);
    }

    void LineSniper()
    {
        Damage(lineSniperCost, true);
        audioSource.PlayOneShot(lineSniperSound);
        lineSniperCoolDown = lineSniperCoolDownTime;
        Debug.Log("Line_Sniper Fired");
        //Create shotInstance
        GameObject shotInstance;
        shotInstance = Instantiate(lineSniperShotPrefab, barrelEnd.transform.position, Quaternion.identity) as GameObject;
        //Grab shotScript
        lineSniperShotScript = shotInstance.GetComponent<LineSniperShot>();
        lineSniperShotScript.teamCount = teamCount;
        //Grab and set Rotation
        angleDir = playerMoveScript.myAngle();
        //Debug.Log("SHOT angleDir: " + angleDir);
        shotInstance.transform.eulerAngles = new Vector3(transform.eulerAngles.x, transform.eulerAngles.y, angleDir);
        //Get Rigidbody, add force.
        Rigidbody2D rb2d = shotInstance.GetComponent<Rigidbody2D>();
        Vector2 straight = barrelEnd.transform.position - transform.position;
        rb2d.AddForce(straight * lineSniperShotSpeed);
    }

    void FocusBeam(bool hit)
    {
        Debug.Log("Focus_Beam Fired");
        
        focusBeamHitSomething = Physics2D.Linecast(barrelEnd.transform.position, focusBeamEnd.transform.position, toBeEffectedLayer);
        if (focusBeamHitSomething)
        {
            Debug.Log(focusBeamHitSomething.transform.name);
            if (focusBeamHitSomething.transform.tag == "Player1(Black)" || focusBeamHitSomething.transform.tag == "Player2(White)" || focusBeamHitSomething.transform.tag == "Player3" || focusBeamHitSomething.transform.tag == "Player4")
            {
                focusBeamEnemyTimeHold += 1;
                if (focusBeamEnemyTimeHold > focusBeamMultiplyTime)
                {
                    //if held for more than 'time' frames, multiply damage.
                    focusBeamEnemyDamageCurrent = focusBeamEnemyBaseDamage * focusBeamEnemyDamageMultiplier;
                }
                else
                {
                    //if not held for 'time' frames, reset the damage.
                    focusBeamEnemyDamageCurrent = focusBeamEnemyBaseDamage;
                }
                Debug.Log("HittingEnemy");
                Debug.Log("HitPoint: " + focusBeamHitSomething.point);
                focusBeamRenderer.SetPosition(0, barrelEnd.transform.position);
                focusBeamRenderer.SetPosition(1, focusBeamHitSomething.point);
                enemyShootScript = focusBeamHitSomething.transform.GetComponent<ProtoPlayerShooting>();

                //DamageEnemy
                if (hit == true)
                {
                    enemyShootScript.Damage(focusBeamEnemyDamageCurrent, false);
                    Instantiate(focusBeamHitParticles, focusBeamHitSomething.point, Quaternion.identity);
                    Instantiate(focusBeamSmokeParticles, focusBeamHitSomething.point, Quaternion.identity);
                }
                else
                {
                    //no hit
                }

            }
            else if(focusBeamHitSomething.transform.tag == "Asteroid")
            {
                focusBeamRenderer.SetPosition(0, barrelEnd.transform.position);
                focusBeamRenderer.SetPosition(1, focusBeamHitSomething.point);
                if (hit == true)
                {
                    asteroidScript = focusBeamHitSomething.transform.GetComponent<Asteroid>();
                    asteroidScript.DamageAsteroid(focusBeamEnemyBaseDamage);
                    Instantiate(focusBeamHitParticles, focusBeamHitSomething.point, Quaternion.identity);
                    Instantiate(focusBeamSmokeParticles, focusBeamHitSomething.point, Quaternion.identity);
                }
            }
            else if (focusBeamHitSomething.transform.tag == "Ibeam")
            {
                focusBeamRenderer.SetPosition(0, barrelEnd.transform.position);
                focusBeamRenderer.SetPosition(1, focusBeamHitSomething.point);
                if (hit == true)
                {
                    Instantiate(focusBeamHitParticles, focusBeamHitSomething.point, Quaternion.identity);
                    Instantiate(focusBeamSmokeParticles, focusBeamHitSomething.point, Quaternion.identity);
                }
            }
        }
        else
        {
            focusBeamEnemyTimeHold = 0;
            focusBeamRenderer.SetPosition(0, barrelEnd.transform.position);
            focusBeamRenderer.SetPosition(1, focusBeamEnd.transform.position);
        }
    }

    void DefensiveSystem(int teamCount)
    {
        if (teamCount == 1)
        {
            //TimeShield
            if (defenseSystem == 1)
            {
                if (timeShieldHit == true)
                {
                    Debug.Log("timeShieldHit = true");
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
                else
                {

                    AbilityReadyFlash();
                }

                if (Input.GetAxis("BlackYButton") > 0.1 && timeShieldCoolDown < 1 && shotHealthCount > 0)
                {
                    timeShieldCoolDown = timeShieldCoolDownTime;
                    StartCoroutine("TimeShield");
                }
            }
            //GravHault
            else if (defenseSystem == 2)
            {
                if (gravHaultWorked == true)
                {
                    Debug.Log("GravHaultActivated");
                    gravHaultGraphic.color = flashColor;
                }
                else
                {
                    gravHaultGraphic.color = Color.Lerp(gravHaultGraphic.color, Color.clear, flashSpeed * Time.deltaTime);
                }
                gravHaultWorked = false;
                if (gravHaultCoolDown > 0)
                {
                    gravHaultCoolDown -= 1;
                }
                else
                {
                    AbilityReadyFlash();
                }
                if (Input.GetAxis("BlackYButton") > 0.1 && gravHaultCoolDown < 1 && shotHealthCount > 0)
                {
                    Collider2D[] hitThings = Physics2D.OverlapCircleAll(transform.position, gravHaultEffRange, toBeEffectedLayer);
                    if (hitThings != null)
                    {
                        Vector2 stopVelocity = new Vector2(0, 0);
                        gravHaultWorked = true;
                        gravHaultCoolDown = gravHaultCoolDownTime;
                        //playerRB.velocity = stopVelocity;
                        //playerRB.inertia = 0;
                        for (int i = 0; i < hitThings.Length; i++)
                        {
                            if (hitThings[i].tag == "RocketAmmo")
                            {
                                RemoteLauncherShot shot = hitThings[i].GetComponent<RemoteLauncherShot>();
                                shot.notMoving = true;
                            }
                            else if (hitThings[i].tag == "SniperBullet")
                            {
                                LineSniperShot shot = hitThings[i].GetComponent<LineSniperShot>();
                                shot.notMoving = true;
                            }
                            if (hitThings[i].tag == "Ibeam" || hitThings[i].tag == "Player1(Black)")
                            {
                                
                            }
                            else
                            {
                                Rigidbody2D hitRB;
                                hitRB = hitThings[i].GetComponent<Rigidbody2D>();
                                hitRB.velocity = stopVelocity;
                                hitRB.inertia = 0;
                                Debug.Log("Stopped: " + hitThings[i].name);
                            }
                        }
                    }
                    else
                    {
                        return;
                    }
                }
            }
            //ShieldMines
            else if (defenseSystem == 3)
            {
                if (shieldMinesCoolDown > 0)
                {
                    shieldMinesCoolDown -= 1;
                }
                else
                {
                    AbilityReadyFlash();
                }

                if (Input.GetButton("BlackYButton") && shieldMinesCoolDown < 1)
                {
                    ShieldMines();
                }
            }
            //JetPack
            else if (defenseSystem == 4)
            {
                if (jetPackCoolDown < jetPackCoolDownTime && jetPackActivated != true)
                {
                    jetPackCoolDown += 1 * Time.deltaTime * jetPackIncreaseRate;
                    Debug.Log("jetPackCoolDown: " + jetPackCoolDown);
                }
                else
                {
                    AbilityReadyFlash();
                }

                if (Input.GetButton("BlackYButton") && jetPackCoolDown > 2)
                {
                    Debug.Log("JetPackGoing");
                    JetPack();
                }
                else
                {
                    jetPackActivated = false;
                    playerMoveScript.jetPack = false;
                    playerRB.angularDrag = originalAngularDrag;
                    playerRB.drag = originalDrag;
                }
            }
            else
            {
                if (Input.GetButton("BlackYButton"))
                {
                    Debug.Log("NoAbilitySelected");
                }
            }
        }
        else if(teamCount == 2)
        {
            //TimeShield
            if (defenseSystem == 1)
            {
                if (timeShieldHit == true)
                {
                    Debug.Log("timeShieldHit = true");
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
                else
                {
                    AbilityReadyFlash();
                }

                if (Input.GetAxis("WhiteYButton") > 0.1 && timeShieldCoolDown < 1 && shotHealthCount > 0)
                {
                    timeShieldCoolDown = timeShieldCoolDownTime;
                    StartCoroutine("TimeShield");
                }
            }
            //GravHault
            else if (defenseSystem == 2)
            {
                if (gravHaultWorked == true)
                {
                    Debug.Log("GravHaultActivated");
                    gravHaultGraphic.color = flashColor;
                }
                else
                {
                    gravHaultGraphic.color = Color.Lerp(gravHaultGraphic.color, Color.clear, flashSpeed * Time.deltaTime);
                }
                gravHaultWorked = false;
                if (gravHaultCoolDown > 0)
                {
                    gravHaultCoolDown -= 1;
                }
                else
                {
                    AbilityReadyFlash();
                }
                if (Input.GetAxis("WhiteYButton") > 0.1 && gravHaultCoolDown < 1 && shotHealthCount > 0)
                {
                    Collider2D[] hitThings = Physics2D.OverlapCircleAll(transform.position, gravHaultEffRange, toBeEffectedLayer);
                    if (hitThings != null)
                    {
                        Vector2 stopVelocity = new Vector2(0, 0);
                        gravHaultWorked = true;
                        gravHaultCoolDown = gravHaultCoolDownTime;
                        //playerRB.velocity = stopVelocity;
                        //playerRB.inertia = 0;
                        for (int i = 0; i < hitThings.Length; i++)
                        {
                            if (hitThings[i].tag == "RocketAmmo")
                            {
                                RemoteLauncherShot shot = hitThings[i].GetComponent<RemoteLauncherShot>();
                                shot.notMoving = true;
                            }
                            else if (hitThings[i].tag == "SniperBullet")
                            {
                                LineSniperShot shot = hitThings[i].GetComponent<LineSniperShot>();
                                shot.notMoving = true;
                            }


                            if (hitThings[i].tag == "Ibeam" || hitThings[i].tag == "Player2(White)")
                            {

                            }
                            else
                            {
                                Rigidbody2D hitRB;
                                hitRB = hitThings[i].GetComponent<Rigidbody2D>();
                                hitRB.velocity = stopVelocity;
                                hitRB.inertia = 0;
                                Debug.Log("Stopped: " + hitThings[i].name);
                            }
                        }
                    }
                    else
                    {
                        return;
                    }
                }
            }
            //ShieldMines
            else if (defenseSystem == 3)
            {
                if (shieldMinesCoolDown > 0)
                {
                    shieldMinesCoolDown -= 1;
                }
                else
                {
                    AbilityReadyFlash();
                }
                if (Input.GetButton("WhiteYButton") && shieldMinesCoolDown < 1)
                {
                    ShieldMines();
                }
            }
            //JetPack
            else if (defenseSystem == 4)
            {
                if (jetPackCoolDown < jetPackCoolDownTime && jetPackActivated != true)
                {
                    jetPackCoolDown += 1 * Time.deltaTime * jetPackIncreaseRate;
                    Debug.Log("jetPackCoolDown: " + jetPackCoolDown);
                }
                else
                {
                    AbilityReadyFlash();
                }

                if (Input.GetButton("WhiteYButton") && jetPackCoolDown > 2)
                {
                    Debug.Log("JetPackGoing");
                    JetPack();
                }
                else
                {
                    jetPackActivated = false;
                    playerMoveScript.jetPack = false;
                    //playerRB.angularDrag = originalAngularDrag;
                    //playerRB.drag = originalDrag;
                }
            }
            else
            {
                if (Input.GetButton("WhiteYButton"))
                {
                    Debug.Log("NoAbilitySelected");
                }
            }
        }
        else if (teamCount == 3)
        {
            //TimeShield
            if (defenseSystem == 1)
            {
                if (timeShieldHit == true)
                {
                    Debug.Log("timeShieldHit = true");
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
                else
                {
                    AbilityReadyFlash();
                }

                if (Input.GetAxis("Player3YButton") > 0.1 && timeShieldCoolDown < 1 && shotHealthCount > 0)
                {
                    timeShieldCoolDown = timeShieldCoolDownTime;
                    StartCoroutine("TimeShield");
                }
            }
            //GravHault
            else if (defenseSystem == 2)
            {
                if (gravHaultWorked == true)
                {
                    Debug.Log("GravHaultActivated");
                    gravHaultGraphic.color = flashColor;
                }
                else
                {
                    gravHaultGraphic.color = Color.Lerp(gravHaultGraphic.color, Color.clear, flashSpeed * Time.deltaTime);
                }
                gravHaultWorked = false;
                if (gravHaultCoolDown > 0)
                {
                    gravHaultCoolDown -= 1;
                }
                else
                {
                    AbilityReadyFlash();
                }
                if (Input.GetAxis("Player3YButton") > 0.1 && gravHaultCoolDown < 1 && shotHealthCount > 0)
                {
                    Collider2D[] hitThings = Physics2D.OverlapCircleAll(transform.position, gravHaultEffRange, toBeEffectedLayer);
                    if (hitThings != null)
                    {
                        Vector2 stopVelocity = new Vector2(0, 0);
                        gravHaultWorked = true;
                        gravHaultCoolDown = gravHaultCoolDownTime;
                        //playerRB.velocity = stopVelocity;
                        //playerRB.inertia = 0;
                        for (int i = 0; i < hitThings.Length; i++)
                        {
                            if (hitThings[i].tag == "RocketAmmo")
                            {
                                RemoteLauncherShot shot = hitThings[i].GetComponent<RemoteLauncherShot>();
                                shot.notMoving = true;
                            }
                            else if (hitThings[i].tag == "SniperBullet")
                            {
                                LineSniperShot shot = hitThings[i].GetComponent<LineSniperShot>();
                                shot.notMoving = true;
                            }


                            if (hitThings[i].tag == "Ibeam" || hitThings[i].tag == "Player3")
                            {

                            }
                            else
                            {
                                Rigidbody2D hitRB;
                                hitRB = hitThings[i].GetComponent<Rigidbody2D>();
                                hitRB.velocity = stopVelocity;
                                hitRB.inertia = 0;
                                Debug.Log("Stopped: " + hitThings[i].name);
                            }
                        }
                    }
                    else
                    {
                        return;
                    }
                }
            }
            //ShieldMines
            else if (defenseSystem == 3)
            {
                if (shieldMinesCoolDown > 0)
                {
                    shieldMinesCoolDown -= 1;
                }
                else
                {
                    AbilityReadyFlash();
                }
                if (Input.GetButton("Player3YButton") && shieldMinesCoolDown < 1)
                {
                    ShieldMines();
                }
            }
            //JetPack
            else if (defenseSystem == 4)
            {
                if (jetPackCoolDown < jetPackCoolDownTime && jetPackActivated != true)
                {
                    jetPackCoolDown += 1 * Time.deltaTime * jetPackIncreaseRate;
                    Debug.Log("jetPackCoolDown: " + jetPackCoolDown);
                }
                else
                {
                    AbilityReadyFlash();
                }

                if (Input.GetButton("Player3YButton") && jetPackCoolDown > 2)
                {
                    Debug.Log("JetPackGoing");
                    JetPack();
                }
                else
                {
                    jetPackActivated = false;
                    playerMoveScript.jetPack = false;
                    //playerRB.angularDrag = originalAngularDrag;
                    //playerRB.drag = originalDrag;
                }
            }
            else
            {
                if (Input.GetButton("Player3YButton"))
                {
                    Debug.Log("NoAbilitySelected");
                }
            }
        }
        else //if (teamCount == 4)
        {
            //TimeShield
            if (defenseSystem == 1)
            {
                if (timeShieldHit == true)
                {
                    Debug.Log("timeShieldHit = true");
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
                else
                {
                    AbilityReadyFlash();
                }

                if (Input.GetAxis("Player4YButton") > 0.1 && timeShieldCoolDown < 1 && shotHealthCount > 0)
                {
                    timeShieldCoolDown = timeShieldCoolDownTime;
                    StartCoroutine("TimeShield");
                }
            }
            //GravHault
            else if (defenseSystem == 2)
            {
                if (gravHaultWorked == true)
                {
                    Debug.Log("GravHaultActivated");
                    gravHaultGraphic.color = flashColor;
                }
                else
                {
                    gravHaultGraphic.color = Color.Lerp(gravHaultGraphic.color, Color.clear, flashSpeed * Time.deltaTime);
                }
                gravHaultWorked = false;
                if (gravHaultCoolDown > 0)
                {
                    gravHaultCoolDown -= 1;
                }
                else
                {
                    AbilityReadyFlash();
                }
                if (Input.GetAxis("Player4YButton") > 0.1 && gravHaultCoolDown < 1 && shotHealthCount > 0)
                {
                    Collider2D[] hitThings = Physics2D.OverlapCircleAll(transform.position, gravHaultEffRange, toBeEffectedLayer);
                    if (hitThings != null)
                    {
                        Vector2 stopVelocity = new Vector2(0, 0);
                        gravHaultWorked = true;
                        gravHaultCoolDown = gravHaultCoolDownTime;
                        //playerRB.velocity = stopVelocity;
                        //playerRB.inertia = 0;
                        for (int i = 0; i < hitThings.Length; i++)
                        {
                            if (hitThings[i].tag == "RocketAmmo")
                            {
                                RemoteLauncherShot shot = hitThings[i].GetComponent<RemoteLauncherShot>();
                                shot.notMoving = true;
                            }
                            else if (hitThings[i].tag == "SniperBullet")
                            {
                                LineSniperShot shot = hitThings[i].GetComponent<LineSniperShot>();
                                shot.notMoving = true;
                            }


                            if (hitThings[i].tag == "Ibeam" || hitThings[i].tag == "Player4")
                            {

                            }
                            else
                            {
                                Rigidbody2D hitRB;
                                hitRB = hitThings[i].GetComponent<Rigidbody2D>();
                                hitRB.velocity = stopVelocity;
                                hitRB.inertia = 0;
                                Debug.Log("Stopped: " + hitThings[i].name);
                            }
                        }
                    }
                    else
                    {
                        return;
                    }
                }
            }
            //ShieldMines
            else if (defenseSystem == 3)
            {
                if (shieldMinesCoolDown > 0)
                {
                    shieldMinesCoolDown -= 1;
                }
                else
                {
                    AbilityReadyFlash();
                }
                if (Input.GetButton("Player4YButton") && shieldMinesCoolDown < 1)
                {
                    ShieldMines();
                }
            }
            //JetPack
            else if (defenseSystem == 4)
            {
                if (jetPackCoolDown < jetPackCoolDownTime && jetPackActivated != true)
                {
                    jetPackCoolDown += 1 * Time.deltaTime * jetPackIncreaseRate;
                    Debug.Log("jetPackCoolDown: " + jetPackCoolDown);
                }
                else
                {
                    AbilityReadyFlash();
                }

                if (Input.GetButton("Player4YButton") && jetPackCoolDown > 2)
                {
                    Debug.Log("JetPackGoing");
                    JetPack();
                }
                else
                {
                    jetPackActivated = false;
                    playerMoveScript.jetPack = false;
                    //playerRB.angularDrag = originalAngularDrag;
                    //playerRB.drag = originalDrag;
                }
            }
            else
            {
                if (Input.GetButton("Player4YButton"))
                {
                    Debug.Log("NoAbilitySelected");
                }
            }
        }
    }

    void AbilityReadyFlash()
    {
        if (abilityLightCoolDown > 0)
        {
            abilityLightCoolDown -= 1;
        }
        else
        {
            abilityReady = true;
            abilityLightCoolDown = abilityLightCoolDownTime;
        }
    }

    IEnumerator TimeShield()
    {
        invulnerable = true;
        timeShieldGraphic.color = flashColor;
        yield return new WaitForSeconds(timeShieldDuration);
        timeShieldGraphic.color = Color.clear;
        invulnerable = false;
    }

    void ShieldMines()
    {
        shieldMinesCoolDown = shieldMinesCoolDownTime;
        //Make Vector2 vars for each spot around player.
        pos1 = new Vector2(transform.position.x - 1.5f, transform.position.y + 1.5f);
        pos2 = new Vector2(transform.position.x, transform.position.y + 2f);
        pos3 = new Vector2(transform.position.x + 1.5f, transform.position.y + 1.5f);
        pos4 = new Vector2(transform.position.x - 2f, transform.position.y);
        pos5 = new Vector2(transform.position.x + 2f, transform.position.y);
        pos6 = new Vector2(transform.position.x - 1.5f, transform.position.y - 1.5f);
        pos7 = new Vector2(transform.position.x, transform.position.y - 2f);
        pos8 = new Vector2(transform.position.x + 1.5f, transform.position.y - 1.5f);

        GameObject mineInstance1 = Instantiate(shieldMine, transform.position, Quaternion.identity)as GameObject;
        mine1 = mineInstance1.GetComponent<ShieldMine>();
        mine1.teamCount = teamCount;
        mine1.goToPosition = pos1;
        GameObject mineInstance2 = Instantiate(shieldMine, transform.position, Quaternion.identity) as GameObject;
        mine2 = mineInstance2.GetComponent<ShieldMine>();
        mine2.teamCount = teamCount;
        mine2.goToPosition = pos2;
        GameObject mineInstance3 = Instantiate(shieldMine, transform.position, Quaternion.identity) as GameObject;
        mine3 = mineInstance3.GetComponent<ShieldMine>();
        mine3.teamCount = teamCount;
        mine3.goToPosition = pos3;
        GameObject mineInstance4 = Instantiate(shieldMine, transform.position, Quaternion.identity) as GameObject;
        mine4 = mineInstance4.GetComponent<ShieldMine>();
        mine4.teamCount = teamCount;
        mine4.goToPosition = pos4;
        GameObject mineInstance5 = Instantiate(shieldMine, transform.position, Quaternion.identity) as GameObject;
        mine5 = mineInstance5.GetComponent<ShieldMine>();
        mine5.teamCount = teamCount;
        mine5.goToPosition = pos5;
        GameObject mineInstance6 = Instantiate(shieldMine, transform.position, Quaternion.identity) as GameObject;
        mine6 = mineInstance6.GetComponent<ShieldMine>();
        mine6.teamCount = teamCount;
        mine6.goToPosition = pos6;
        GameObject mineInstance7 = Instantiate(shieldMine, transform.position, Quaternion.identity) as GameObject;
        mine7 = mineInstance7.GetComponent<ShieldMine>();
        mine7.teamCount = teamCount;
        mine7.goToPosition = pos7;
        GameObject mineInstance8 = Instantiate(shieldMine, transform.position, Quaternion.identity) as GameObject;
        mine8 = mineInstance8.GetComponent<ShieldMine>();
        mine8.teamCount = teamCount;
        mine8.goToPosition = pos8;
    }

    void JetPack()
    {
        if (jetPackCoolDown < 0)
        {
            return;
        }
        else
        {
            playerMoveScript.jetPack = true;
            //playerRB.angularDrag = angularDragSwitch;
            //playerRB.drag = dragSwitch;
            jetPackCoolDown -= jetPackCost * Time.deltaTime * jetPackDecreaseRate;
        }
        Debug.Log("JetPackWorks:" + jetPackCoolDown);
        jetPackActivated = true;
    }

    public void Display(int num)
    {
        string i = num.ToString();
        numberText.text = i;
    }
}
