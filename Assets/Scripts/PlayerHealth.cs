using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using Rewired;

public class PlayerHealth : MonoBehaviour {

    //Rewired
    private Player playerInput;

    //We should set these on instantiation, and through what team the players pick.
    public int numPlayer;
    public int teamCount; //Set at Beginning
    public int shotHealthCount;
    public Image healthBar;
    public Text healthNum;
    public bool invulnerable;

    private string aButtonInput;

    //OnDeath
    public GameObject deathParticlesObj1;
    private GameObject deathParticles1Copy;
    private ParticleSystem deathParticles1;

    public GameObject deathParticlesObj2;
    private GameObject deathParticles2Copy;
    private ParticleSystem deathParticles2;

    /*
    //Shields
    public int shields; //your shield supply
    public int shieldsCap; //the total amount of shields you can have at once
    public float shieldDepleteRate; //The amount of seconds till the next shield point lost
    private float shieldTimer; //Keeps track of seconds
    public Image shieldsBar;
    public Text shieldsNum;
    */

    //private bool shieldsUp;

    public GameObject buffTextGoToPos;
    public GameObject damageTextGoTo;
    private Text buffText;
    private GameObject overChargeBuffCopy;
    private GameObject speedBuffCopy;

    public float textUpSpeed;

    //OverCharge
    public GameObject overchargeIndicator;
    public bool overchargeActive;
    public float overchargeTime = 10.0f;
    private float overchargeTimer;
    public Image overchargeBar;

    public GameObject overchargeRing;


    //SpeedBuff
    public GameObject speedBuffIndicator;
    public bool speedBuffActive; //if the speedBuff is currently active.
    public float speedBuffTime = 10.0f; //The amount of seconds till speedBuff becomes inactive
    private float speedBuffTimer; //Keeps track of seconds, counts up.
    public Image speedBar;
    public Text speedNum;

    public GameObject speedboostRing;

    private PlayerMovement playerMoveScript;
    private SpriteManager spriteManagerScript;
    public GameManager gameManagerScript; //Here's the gamemanager
    
    private Text numberText;
    private RectTransform numberTextTransform;

    public GameObject numSpawnPos;
    public GameObject damageNum;
    private Text damageNumText;
    private Text healNumText;
    private GameObject damageNumCopy;
    private GameObject healNumCopy;
    private Rigidbody2D damageNumRB;
    private Vector2 damageNumSize = new Vector2(0.5f, 0.5f);
    private Vector2 damageNumLarge = new Vector2(1f, 1f);
    private ThrowNums textThrowScript;
    private ThrowNums healNumScript;
    private ThrowNums damageNumScript;



    public SpriteRenderer statusLight;
    public Color hurtColor;
    public Color healColor;
    public Color overchargeColor;
    public Color speedBuffColor;
    public float flashSpeed;
    public float textFlashSpeed;
    public Vector2 textFlashSize;

    //public Color flashColor;
    public SpriteRenderer healthCircle;
    public SpriteRenderer backgroundPanel;
    public Color panelColor;
    public SpriteRenderer spinnerSprite;
    public Sprite[] healthCircleSprites;
    public GameObject playerHealthCircle;

    public Animator spinnerAnim;
    public GameObject below30;
    public GameObject below20;
    public GameObject below10;

    private bool anyDamage = false; //Any damage at all, self inflicted or not.

    private bool damaged = false;
    private bool healed = false;
    private bool overchargeHit = false;
    private bool speedBoost = false;

    public bool boosted;
    private Vector2 largerSize = new Vector2(1.75f, 1.75f);
    private Vector2 normalSize = new Vector2(1.5f, 1.5f);

    public Transform backgroundPanelTransform;
    public Transform spinnerTransform;
    public Transform uiPanelTransform;
    private Vector3 regularSize = new Vector3(1f, 1f, 1f);

    //Audio
    public AudioSource audioSource;
    

    //Animation
    public Animator healHurtFlashAnim;

    //Environment Effects
    private bool onFire;
    private float timeRemaining;
    public GameObject onFireParticles;

    public bool poisoned;

    public int weaponSelected;
    public int abilitySelected;
    public bool weaponAbility; //True is weapon, false is ability;
    public bool insideSelectZone;

    //TitleScreen
    public bool titleScreen;
    public TitleScreenManager titleScreenManager;

    public bool dummy;

    void Awake()
    {
        if (!dummy)
        {
            //Rewired
            int realNum = numPlayer - 1;
            playerInput = ReInput.players.GetPlayer(realNum);

            playerMoveScript = GetComponent<PlayerMovement>();
            spriteManagerScript = GetComponent<SpriteManager>();
            audioSource = GetComponent<AudioSource>(); //Never know what kind of sounds we could play!
        }
    }

    void Start()
    {
        //SelectTileInputSetUp();
        if (!dummy)
        {
            shotHealthCount = 100;
        }
    }

    void Update()
    {
        if (!dummy)
        {
            DisplayHealth();
            DisplayOvercharge();
            DisplaySpeedBuff();
            MonitorEnvironmentalEffects();
            MonitorLerpingSprites();
            MonitorLowPower();

            if (!titleScreen)
            {
                MonitorSelectTileInput();
            }

            //Overcharge
            if (overchargeActive)
            {
                MonitorOvercharge();
                healthCircle.color = overchargeColor;
                overchargeRing.SetActive(true);
                //overchargeIndicator.SetActive(true);
            }
            else
            {
                healthCircle.color = spriteManagerScript.playerColor[teamCount];
                overchargeRing.SetActive(false);
                //overchargeIndicator.SetActive(false);
            }


            //SpeedBuff
            if (speedBuffActive)
            {
                MonitorSpeedBuff();
                speedBuffIndicator.SetActive(true);
                speedboostRing.SetActive(true);
            }
            else
            {
                speedBuffIndicator.SetActive(false);
                speedboostRing.SetActive(false);
            }


            
        }
        MonitorStatus();
    }

    void MonitorLowPower()
    {
        spinnerAnim.SetInteger("playerHp", shotHealthCount);
        
        if (shotHealthCount > 30)
        {
            below30.SetActive(false);
            below20.SetActive(false);
            below10.SetActive(false);
        }
        else if (shotHealthCount <= 30 && shotHealthCount > 20)
        {
            below30.SetActive(true);
            below20.SetActive(false);
            below10.SetActive(false);
        }
        else if (shotHealthCount <= 20 && shotHealthCount > 10)
        {
            below30.SetActive(false);
            below20.SetActive(true);
            below10.SetActive(false);
        }
        else if (shotHealthCount <= 10)
        {
            //below 10
            below30.SetActive(false);
            below20.SetActive(false);
            below10.SetActive(true);
        }
    }

    void MonitorLerpingSprites()
    {
        /*
        backgroundPanel
        spinnerSprite
        uiPanel
        */

        if (damaged)
        {
            float backgroundPanelX = Random.Range(0.5f, 1.5f);
            float backgroundPanelY = Random.Range(0.5f, 1.5f);
            Vector3 backgroundPanelVector = new Vector3(backgroundPanelX, backgroundPanelY, 1f);
            backgroundPanelTransform.localScale = backgroundPanelVector;

            float uiPanelX = Random.Range(0.5f, 1.5f);
            float uiPanelY = Random.Range(0.5f, 1.5f);
            Vector3 uiPanelVector = new Vector3(uiPanelX, uiPanelY, 1f);
            uiPanelTransform.localScale = uiPanelVector;

            float spinnerX = Random.Range(0.5f, 1.5f);
            float spinnerY = Random.Range(0.5f, 1.5f);
            Vector3 spinnerVector = new Vector3(backgroundPanelX, backgroundPanelY, 1f);
            spinnerTransform.localScale = spinnerVector;
        }
        else
        {
            backgroundPanelTransform.localScale = Vector3.Lerp(backgroundPanelTransform.localScale, regularSize, flashSpeed * Time.deltaTime);
            spinnerTransform.localScale = Vector3.Lerp(spinnerTransform.localScale, regularSize, flashSpeed * Time.deltaTime);
            uiPanelTransform.localScale = Vector3.Lerp(uiPanelTransform.localScale, regularSize, flashSpeed * Time.deltaTime);
        }
    }

    void MonitorSelectTileInput()
    {
        //if we get a button input
        if (playerInput.GetButton("AButton") && gameManagerScript.classSelectScreen && insideSelectZone)
        {
            //if weapon tile
            if (weaponAbility)
            {
                gameManagerScript.SetOffensiveSystem(weaponSelected, numPlayer);
            }
            else //We're on an ability tile
            {
                gameManagerScript.SetDefensiveSystem(abilitySelected, numPlayer);
            }
        }
    }
    /*
    void SelectTileInputSetUp()
    {
        switch(numPlayer)
        {
            case (1):
                aButtonInput = "BlackAButton";
                break;
            case (2):
                aButtonInput = "WhiteAButton";
                break;
            case (3):
                aButtonInput = "Player3AButton";
                break;
            case (4):
                aButtonInput = "Player4AButton";
                break;
        }
    }
    */

    void MonitorEnvironmentalEffects()
    {
        if (onFire)
        {
            timeRemaining -= Time.deltaTime;
            if (timeRemaining < 0)
            {
                //DamagePlayer
                if (!titleScreen)
                {
                    Damage(gameManagerScript.fireDPS, false);
                }
                else
                {
                    Damage(titleScreenManager.fireDPS, false);
                }
                timeRemaining = 1;
            }
        }
        else if(poisoned)
        {
            timeRemaining -= Time.deltaTime;
            if (timeRemaining < 0)
            {
                //DamagePlayer
                if (!titleScreen)
                {
                    Damage(gameManagerScript.poisonDPS, false);
                }
                else
                {
                    Damage(titleScreenManager.poisonDPS, false);
                }
                timeRemaining = 1;
                poisoned = false;
            }
        }
        else
        {
            //no Effects
        }
    }
    void MonitorStatus()
    {
        //Damaged
        if (damaged == true)
        {
            if (statusLight != null)
            {
                statusLight.color = hurtColor;
            }
            if (damageNumText != null)
            {
                damageNumText.color = Color.red;
            }
        }
        //Healed
        else if (healed == true)
        {
            if (statusLight != null)
            {
                statusLight.color = healColor;
            }
        }
        //Overcharge
        else if (overchargeHit == true)
        {
            if (statusLight != null)
            {
                statusLight.color = overchargeColor;
            }
        }
        //SpeedBuff
        else if (speedBoost == true)
        {
            if (statusLight != null)
            {
                statusLight.color = speedBuffColor;
            }
        }
        //Else lerp back to no image. simple. we could also use this for the damage numbers fading to nothing.
        else
        {
            if (statusLight != null)
            {
                statusLight.color = Color.Lerp(statusLight.color, Color.clear, flashSpeed * Time.deltaTime);
            }
            
            if (damageNumCopy != null)
            {
                damageNumCopy.transform.localPosition = Vector2.Lerp(damageNumCopy.transform.localPosition, damageTextGoTo.transform.localPosition, textUpSpeed * Time.deltaTime);
            }

            if (healNumCopy != null)
            {
                healNumCopy.transform.localPosition = Vector2.Lerp(healNumCopy.transform.localPosition, buffTextGoToPos.transform.localPosition, textUpSpeed * Time.deltaTime);
            }

            if (speedBuffCopy != null)
            {
                speedBuffCopy.transform.localPosition = Vector2.Lerp(speedBuffCopy.transform.localPosition, buffTextGoToPos.transform.localPosition, textUpSpeed * Time.deltaTime);
            }

            if (overChargeBuffCopy != null)
            {
                overChargeBuffCopy.transform.localPosition = Vector2.Lerp(overChargeBuffCopy.transform.localPosition, buffTextGoToPos.transform.localPosition, textUpSpeed * Time.deltaTime);
            }
        }

        if (!dummy)
        {
            if (anyDamage)
            {
                //Flash white
                healthCircle.color = Color.black;
                spinnerSprite.color = Color.black;
                backgroundPanel.color = Color.white;
            }
            else
            {
                //Damageflash back to normal
                healthCircle.color = Color.Lerp(healthCircle.color, spriteManagerScript.playerColor[teamCount], textFlashSpeed * Time.deltaTime);
                spinnerSprite.color = Color.Lerp(spinnerSprite.color, spriteManagerScript.playerColor[teamCount], textFlashSpeed * Time.deltaTime);
                backgroundPanel.color = Color.Lerp(backgroundPanel.color, panelColor, textFlashSpeed * Time.deltaTime);
            }
        }
        //We always set the boolean back to false so 'else' gets triggered.
        anyDamage = false;
        damaged = false;
        healed = false;
        overchargeHit = false;
        speedBoost = false;
        boosted = false;
    }

    public void TouchedLava(float onFireTime)
    {
        if (!onFire && !dummy)
        {
            StartCoroutine(LavaTouched(onFireTime));
        }
        else
        {
            //already on fire
        }
    }
    IEnumerator LavaTouched(float fireTime)
    {
        //use a timer function and have something in update damage player every second
        onFire = true;
        //enable particle system
        onFireParticles.SetActive(true);

        yield return new WaitForSeconds(fireTime);

        onFire = false;
        //disable particle system
        onFireParticles.SetActive(false);
    }

    public void PickUpOvercharge()
    {
        overchargeTimer = overchargeTime;
        overchargeActive = true;

        overChargeBuffCopy = Instantiate(damageNum, numSpawnPos.transform.position, Quaternion.identity) as GameObject;
        overChargeBuffCopy.transform.SetParent(this.transform);
        buffText = overChargeBuffCopy.GetComponentInChildren<Text>();
        buffText.color = overchargeColor;
        textThrowScript = overChargeBuffCopy.GetComponent<ThrowNums>();
        textThrowScript.numbers = false;

        buffText.text = "OVERCHARGED!";

        overchargeRing.GetComponent<SpriteRenderer>().color = overchargeColor;

        //DisplayShieldsUI
        DisplayOvercharge();
        //Shields animation
        overchargeHit = true;
    }
    void MonitorOvercharge()
    {
        if (overchargeTimer > 0)
        {
            //Debug.Log(overchargeTimer);
            overchargeTimer -= Time.deltaTime;
        }
        else
        {
            overchargeActive = false;
        }
    }
    void DisplayOvercharge()
    {
        if (overchargeBar != null)
        {
            //We can lerp this value later.
            float num; //num for final float fill amount
            float resize; //num for scale factor of current fill amount
            resize = overchargeTimer * 10f; //scale the value to work in 1/100
            num = (float)resize * 0.01f; //multiply by .01 to put the 1/100 value to 0.00/1.00
            overchargeBar.fillAmount = num; //Set the fill amount of the shield bar to the new value
            /*
            Debug.Log(overchargeTimer);
            Debug.Log(resize);
            Debug.Log(num);
            */
        }
    }

    
    void MonitorSpeedBuff()
    {
        //If the speedBufftimer is greater than 0 than decrease it by every second that passes.
        if (speedBuffTimer > 0)
        {
            speedBuffTimer -= Time.deltaTime;
            //while this is going on we'll activate the speedbuffeffect 
            SpeedBuffEffect(true);
        }
        else //When the timer is out /ie < 0/ speedBuffActive = false 
        {
            //Debug.Log("buff OFF");
            //deactivate speedBuff
            SpeedBuffEffect(false);
            speedBuffActive = false;
        }
    }
    void SpeedBuffEffect(bool active)
    {
        playerMoveScript.SpeedBuffEffect(active);
    }
    public void PickUpSpeedBuff()
    {
        //reset speedbuff deplete timer.
        speedBuffTimer = speedBuffTime; //probably around 15 seconds
        //Set SpeedBuffActive to true
        speedBuffActive = true;

        speedBuffCopy = Instantiate(damageNum, numSpawnPos.transform.position, Quaternion.identity) as GameObject;
        speedBuffCopy.transform.SetParent(this.transform);
        buffText = speedBuffCopy.GetComponentInChildren<Text>();
        buffText.color = speedBuffColor;
        textThrowScript = speedBuffCopy.GetComponent<ThrowNums>();
        textThrowScript.numbers = false;

        buffText.text = "SPEED BOOST!";

        speedboostRing.GetComponent<SpriteRenderer>().color = speedBuffColor;

        //Animation flash
        speedBoost = true;
    }
    void DisplaySpeedBuff()
    {
        //All we need is to decrease the radial dial showing how much is left here.
        if (speedBar != null)
        {
            //We can lerp this value later.
            float num; //num for final float fill amount
            float resize; //num for scale factor of current fill amount
            resize = speedBuffTimer * 10.0f; //scale the value to work in 1/100
            num = (float)resize * 0.01f; //multiply by .01 to put the 1/100 value to 0.00/1.00
            speedBar.fillAmount = num; //Set the fill amount of the shield bar to the new value
            /*
            Debug.Log(speedBuffTimer);
            Debug.Log(resize);
            Debug.Log(num);
            */
        }
    }

    //Damaged
    public void Damage(int amountDamaged, bool selfInflicted)
    {
        if (!dummy)
        {
            anyDamage = true;
        }
        
        if (!selfInflicted) //if not self inflicted
        {
            if (!dummy)
            {
                playerMoveScript.frozen = false;
                //subtract from health.

                shotHealthCount -= amountDamaged;
                DisplayHealth(); //display health.
            }
            damaged = true;
            
            if (titleScreenManager != null)
            {
                titleScreenManager.HitShake();
            }
            else if(gameManagerScript != null)
            {
                gameManagerScript.HitShake();
            }


            //Debug.Log("Hit");
            DamageNumbers(amountDamaged);
        }
        else //if self inflicted shot
        {
            //subtract from health.
            
            if (!overchargeActive) //Find a better name for this.
            {
                shotHealthCount -= amountDamaged;
                DisplayHealth(); //display health.
                DamageNumbers(amountDamaged);
            }
            else
            {
                //No hurt for shooting while overcharge is active.
            }
        }

        //If they are dead
        if (!dummy)
        {
            if (shotHealthCount < 1)
            {
                //First Death particles set color
                deathParticles1Copy = Instantiate(deathParticlesObj1, transform.position, Quaternion.identity) as GameObject;
                deathParticles1 = deathParticles1Copy.GetComponentInChildren<ParticleSystem>();
                deathParticles1.startColor = spriteManagerScript.playerColor[teamCount];
                //Seconds Death particles set color
                deathParticles2Copy = Instantiate(deathParticlesObj2, transform.position, Quaternion.identity) as GameObject;
                deathParticles2 = deathParticles2Copy.GetComponentInChildren<ParticleSystem>();
                deathParticles2.startColor = spriteManagerScript.playerColor[teamCount];

                if (!titleScreen) //if not titlescreen
                {
                    switch (gameManagerScript.classSelectScreen)
                    {
                        case (true):
                            switch (numPlayer)
                            {
                                case (1):
                                    transform.position = gameManagerScript.player1SpawnPosition.transform.position;
                                    break;
                                case (2):
                                    transform.position = gameManagerScript.player2SpawnPosition.transform.position;
                                    break;
                                case (3):
                                    transform.position = gameManagerScript.player3SpawnPosition.transform.position;
                                    break;
                                case (4):
                                    transform.position = gameManagerScript.player4SpawnPosition.transform.position;
                                    break;
                            }
                            shotHealthCount = 100;
                            break;

                        case (false):
                            gameManagerScript.PlayerDied(numPlayer, teamCount, transform.position);
                            Destroy(this.gameObject);
                            break;
                    }
                }
                else //it is the title screen
                {
                    switch (numPlayer)
                    {
                        case (1):
                            transform.position = titleScreenManager.player1StartPos;
                            break;
                        case (2):
                            transform.position = titleScreenManager.player2StartPos;
                            break;
                        case (3):
                            transform.position = titleScreenManager.player3StartPos;
                            break;
                        case (4):
                            transform.position = titleScreenManager.player4StartPos;
                            break;
                    }
                    shotHealthCount = 100;
                }
            }
        }
    }

    //Damage_Numbers
    /*
    void DamageNumbers(int amtDmg)
    {
        //We should do some cool scale manipulation effect like we had previously on the health text. except for these dmg #s
        
        damageNumCopy = Instantiate(damageNum, numSpawnPos.transform.position, Quaternion.identity) as GameObject;
        damageNumCopy.transform.SetParent(this.transform);
        damageNumText = damageNumCopy.GetComponentInChildren<Text>();
        
        string i = amtDmg.ToString();
        damageNumText.text = i;
        
        damageNumScript = damageNumCopy.GetComponent<ThrowNums>();
        damageNumScript.numbers = true;
        damageNumScript.SetSign(false, amtDmg);
    }
    */
    void DamageNumbers(int amtDmg)
    {
        if (damageNumCopy != null)
        {
            // Num is already out
            damageNumScript.IncreaseNumBy(amtDmg);
        }
        else
        {
            // Num is not out
            damageNumCopy = Instantiate(damageNum, numSpawnPos.transform.position, Quaternion.identity) as GameObject;
            damageNumCopy.transform.SetParent(this.transform);
            damageNumText = damageNumCopy.GetComponentInChildren<Text>();
            damageNumScript = damageNumCopy.GetComponent<ThrowNums>();

            damageNumScript.numbers = true;
            damageNumText.color = hurtColor;
            damageNumScript.myColor = hurtColor;
            damageNumScript.IncreaseNumBy(amtDmg);
            
            damageNumScript.SetSign(false, amtDmg);
        }
    }

    /*
    old one VV
    void HealNumbers(int amtHeal)
    {
        healNumCopy = Instantiate(damageNum, numSpawnPos.transform.position, Quaternion.identity) as GameObject;
        healNumCopy.transform.SetParent(this.transform);
        healNumText = healNumCopy.GetComponentInChildren<Text>();
        healNumText.color = healColor;
        
        string i = amtHeal.ToString();
        healNumText.text = i;

        healNumScript = healNumCopy.GetComponent<ThrowNums>();
        healNumScript.numbers = true;
        healNumScript.SetSign(true, amtHeal);
    }
    */
    void HealNumbers(int amtHeal)
    {
        if (healNumCopy != null)
        {
            //Heal Num is already out
            healNumScript.IncreaseNumBy(amtHeal);
        }
        else
        {
            //Heal Num is not out
            healNumCopy = Instantiate(damageNum, numSpawnPos.transform.position, Quaternion.identity) as GameObject;
            healNumCopy.transform.SetParent(this.transform);
            healNumText = healNumCopy.GetComponentInChildren<Text>();
            healNumScript = healNumCopy.GetComponent<ThrowNums>();

            healNumText.color = healColor;
            healNumScript.myColor = healColor;
            healNumScript.IncreaseNumBy(amtHeal);
            healNumScript.numbers = true;
            healNumScript.SetSign(true, amtHeal);
        }
    }

    //Health_Pickup
    public void PickUp(int amountPickedUp)
    {
        shotHealthCount += amountPickedUp; //We should double check what's linking to this to ensure it works with this new healthScript
        if (shotHealthCount > 100)
        {
            shotHealthCount = 100;
        }
        //DisplayHealth();//Display healthCircleScale.
        //healHurtFlashAnim.SetTrigger("heal");
        HealNumbers(amountPickedUp);
        healed = true; //This is a means to an effect. 
    }

    public void DisplayHealth()
    {
        if (healthNum != null)
        {
            //We can lerp this value later.
            float fillNum; //num for final float fill amount
            fillNum = (float)shotHealthCount * 0.01f; //multiply by .01 to put the 1/100 value to 0.00/1.00
                                                      //healthBar.fillAmount = fillNum; //Set the fill amount of the shield bar to the new value
            playerHealthCircle.transform.localScale = new Vector3(fillNum, fillNum, fillNum);
            if (shotHealthCount < 0)
            {
                shotHealthCount = 0;
            }
            healthNum.text = shotHealthCount.ToString();
        }


        //The old way!
        /*
        if (shotHealthCount <= 100 && shotHealthCount > 90)
        {
            healthCircle.sprite = healthCircleSprites[0];
        }
        else if(shotHealthCount < 90 && shotHealthCount > 80)
        {
            healthCircle.sprite = healthCircleSprites[1];
        }
        else if(shotHealthCount < 80 && shotHealthCount > 70)
        {
            healthCircle.sprite = healthCircleSprites[2];
        }
        else if(shotHealthCount < 70 && shotHealthCount > 60)
        {
            healthCircle.sprite = healthCircleSprites[3];
        }
        else if (shotHealthCount < 60 && shotHealthCount > 50)
        {
            healthCircle.sprite = healthCircleSprites[4];
        }
        else if (shotHealthCount < 50 && shotHealthCount > 40)
        {
            healthCircle.sprite = healthCircleSprites[5];
        }
        else if (shotHealthCount < 40 && shotHealthCount > 30)
        {
            healthCircle.sprite = healthCircleSprites[6];
        }
        else if (shotHealthCount < 30 && shotHealthCount > 20)
        {
            healthCircle.sprite = healthCircleSprites[7];
        }
        else if (shotHealthCount < 20 && shotHealthCount > 10)
        {
            healthCircle.sprite = healthCircleSprites[8];
        }
        else if (shotHealthCount < 10 && shotHealthCount > 0)
        {
            healthCircle.sprite = healthCircleSprites[9];
        }
        */
    }
}
