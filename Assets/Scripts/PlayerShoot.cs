using UnityEngine;
using System.Collections;
using Rewired;

public class PlayerShoot : MonoBehaviour {

    private Player playerInput;

    public int numPlayer;
    public int teamCount;
    //public int shotHealthCount;

    private string shootInput;

    public GameManager gameManagerScript;

    public int offenseSystem; // 0-RapidShot, 1-RemoteLauncher, 2-LineSniper, 3-NeedleGatling, 4-LaserRifle, 5-HomingMissiles

    public Color[] teamColors; //0=blue 1=orange 2=green 3=magenta

    //Offense Systems
    private GameObject shotInstance;

    private Rigidbody2D rb2d;
    private Rigidbody2D rb2dB;
    private Rigidbody2D rb2dC;
    private Rigidbody2D rb2dD;


    //RapidShot
    private int alternateFireSpot; // 0 and 1
    public GameObject rapidShotBarrelEnd0;
    public GameObject rapidShotBarrelEnd1;
    public GameObject rapidShotFirePoint0;
    public GameObject rapidShotFirePoint1;
    public int rapidShotCoolDownTime;
    private int rapidShotCoolDown;
    public float newRapidShotCoolDownTime;
    private float newRapidShotCoolDown;
    public int rapidShotCost;
    public float rapidShotShotSpeed;
    public GameObject rapidShotShotPrefab;
    private ProtoShotPrefab shotPrefabScript;
    private Vector2 straight;

    public SpriteRenderer rightRShotIndBottom;
    public SpriteRenderer leftRShotIndBottom;
    public SpriteRenderer rightRShotInd;
    public SpriteRenderer leftRShotInd;
    public SpriteRenderer rightRSShotLight;
    public SpriteRenderer leftRSShotLight;

    //RemoteLauncher
    public GameObject remoteLauncherBarrelEnd;
    public GameObject remoteLauncherFirePoint;
    public int remoteLauncherCoolDownTime;
    private int remoteLauncherCoolDown;
    public float newRemoteLauncherCoolDownTime;
    public float newRemoteLauncherCoolDown;
    public int remoteLauncherCost;
    public float remoteLauncherShotSpeed;
    public GameObject remoteLauncherShotPrefab;
    private RemoteLauncherShot remoteLauncherShotScript;
    public bool rocketFired;
    public Transform launcherTarget;
    public bool rocketActive;

    public SpriteRenderer launcherInactive;
    public SpriteRenderer launcherActiveLight;
    public SpriteRenderer launcherActiveGlow;
    public SpriteRenderer launcherFiredInactive;
    public SpriteRenderer launcherFiredLight;
    public SpriteRenderer launcherFiredGlow;

    //LineSniper
    public GameObject lineSniperBarrelEnd;
    public GameObject lineSniperFirePoint;
    public int lineSniperCoolDownTime;
    private int lineSniperCoolDown;
    public float newLineSniperCoolDownTime;
    private float newLineSniperCoolDown;
    public int lineSniperCost;
    public float lineSniperShotSpeed;
    public GameObject lineSniperShotPrefab;
    private LineSniperShot lineSniperShotScript;
    public SpriteRenderer lineRifleSight;

    private bool sightOut;

    public SpriteRenderer lRLightOn;
    public SpriteRenderer lRLightOff;
    public SpriteRenderer lRGlow;


    //NeedleGatling
    public GameObject[] nGatlingAStartPos;
    public GameObject[] nGatlingAEndPos;
    public GameObject[] nGatlingBStartPos;
    public GameObject[] nGatlingBEndPos;
    public int needleGatlingCoolDownTime;
    private int needleGatlingCoolDown;
    public float newNeedleGatlingCoolDownTime;
    private float newNeedleGatlingCoolDown;
    public int needleGatlingCost;
    public GameObject needleGatlingShotPrefab;
    private ProtoShotPrefab needleScriptA;
    private ProtoShotPrefab needleScriptB;
    private ProtoShotPrefab needleScriptC;
    private ProtoShotPrefab needleScriptD;
    public float needleShotSpeed;
    private Vector2 needleShotVectorA;
    private Vector2 needleShotVectorB;
    private Vector2 needleShotVectorC;
    private Vector2 needleShotVectorD;

    public SpriteRenderer nGatlingLight;
    public SpriteRenderer nGatlingGlowA;
    public SpriteRenderer nGatlingGlowB;


    private int randomNumA;
    private int randomNumB;
    private int randomNumC;
    private int randomNumD;

    private float lerpedNeedleGatlingCoolDownTime;
    public float targetNGatlingCoolDown;
    public float coolDownLerp;

    private GameObject shotInstanceB;
    private GameObject shotInstanceC;
    private GameObject shotInstanceD;

    private int barrelARandNum;
    private int barrelBRandNum;

    //LaserRifle
    public GameObject laserRifleLens;
    public float laserFireTime;
    private float laserFireCount;
    public int laserRifleCost;
    public int laserRifleDamage;
    public float laserRifleRange;
    private bool laserFireable;
    private RaycastHit2D laserRifleHit;
    private RaycastHit2D laserRifleLineHit;
    public Vector2 beamVector;
    public LineRenderer laserRifleLine;
    public GameObject laserRifleLineEnd;
    public Vector2 laserRifleLineEndPos;
    private Vector2 newVector2;
    public float laserRifleLineStartWidth;
    public float laserRifleLineEndWidth;
    public float lineStartWidth;
    public float lineEndWidth;
    private float numm;
    private Vector2 pointVector;
    public float laserSmoothing;

    public SpriteRenderer laserRLightOff;
    public SpriteRenderer laserRLightOn;
    public SpriteRenderer laserRGlow;

    //Homing Missiles
    public GameObject homingMissile;
    public GameObject missileExitLeft;
    public GameObject missileExitRight;
    public GameObject missileStartLeft;
    public GameObject missileStartRight;
    public int initialMissileSpeed;
    private WaitForSeconds missileFireWait = new WaitForSeconds(0.25f);
    public Transform missilesTarget;
    private EnemyFollow missileFollowScript;
    private EnemySeekerShot missileShotScript;
    private bool missileFireable;
    public int homingMissileCoolDownTime;
    private int homingMissileCoolDown;
    public float newHomingMissileCoolDownTime;
    private float newHomingMissileCoolDown;
    public int homingMissileClusterCost;

    public SpriteRenderer rightMissileIndicator;
    public SpriteRenderer rightMissileIndBottom;
    public SpriteRenderer leftMissileIndicator;
    public SpriteRenderer leftMissileIndBottom;

    public SpriteRenderer hMLightsOff;
    public SpriteRenderer hMLaserActive;
    public SpriteRenderer hMFired;
    public SpriteRenderer hMFiredLight;
    public SpriteRenderer hMLeftMissiles;
    public SpriteRenderer hMRightMissiles;

    private float hMLaserStartWidth;
    private float hMLaserEndWidth;

    public bool fireState;
    public LineRenderer homingMissilesLaser;
    private RaycastHit2D homingMissilesLaserHit;
    public GameObject hMLaserEnd;
    public GameObject hMLaserStart;

    private GameObject missileExit;
    private GameObject missileStart;

    private Vector2 newVec;
    private Vector2 newnewVec;
    private Vector2 hitAnyVector;
    private Vector2 hit1Vector;
    private Vector2 hit2Vector;
    private Vector2 hit3Vector;


    //PhaseBlaster
    public float phaseBlasterShotSpeed;
    public float phaseBlasterCoolDownTime;
    private float phaseBlasterCoolDown;
    public GameObject phaseBlast;
    private GameObject phaseBlastCopy;
    public int phaseBlasterCost;
    public int phaseBlasterDamage;
    public GameObject phaseBlasterStart;
    public GameObject phaseBlasterEnd;
    private PhaseBlast phaseBlastScript;
    public ParticleSystem phaseBlasterFx;

    public SpriteRenderer pBLightsOff;
    public SpriteRenderer pBLightOn;
    public SpriteRenderer pBFiredLight;

    public SpriteRenderer pBFrontGlow;
    public SpriteRenderer pBBackGlow;

    //GrenadeGun
    public float grenadeGunShotSpeed;
    public float grenadeGunCoolDownTime;
    private float grenadeGunCoolDown;
    public GameObject grenade;
    private GameObject grenadeCopy1;
    private GameObject grenadeCopy2;
    public int grenadeGunCost;
    public GameObject grenadeGunStart;
    public GameObject grenadeGunEnd;
    private Grenade grenadeScript1;
    private Grenade grenadeScript2;
    public ParticleSystem grenadeGunFx;
    private bool grenadesOut;

    public SpriteRenderer gGLightsOff;
    public SpriteRenderer gGBarrelLight;
    public SpriteRenderer gGBackSquareOn;

    public SpriteRenderer gGBarrelGlow;
    public SpriteRenderer gGBackGlow;

    //PlasmaThrower
    public int plasmaThrowerCost;
    public float maxPlasmaShotSpeed;
    public float minPlasmaShotSpeed;
    public float plasmaSpread;
    private Vector2 throwPosition;
    private Vector2 finalPlasmaThrowPos;
    private Vector2 conversionVector2;
    public GameObject plasmaThrowPos;
    private float plasmaFireCoolDown;
    public float plasmaFireCoolDownTime;
    public GameObject plasmaChunk;
    public GameObject plasmaThrowerStart;
    public GameObject plasmaThrowerEnd;
    private Plasma plasmaScript;
    public ParticleSystem plasmaThrowerParticles;
    public GameObject pTParticlesGO;
    public ParticleSystem plasmaThrowerParticles2;
    public GameObject pTParticlesGO2;
    private bool plasmaEmitting;

    public float timeBetweenShots;
    private WaitForSeconds plasmaFireWait;

    public SpriteRenderer pTBackPackGlow;
    public SpriteRenderer pTBackPackColor;
    public SpriteRenderer pTNozzleOff;
    public SpriteRenderer pTNozzleOn;
    public SpriteRenderer pTNozzleGlow;



    /*
    //ChargeCannon
    public GameObject chargeCannonBarrelEnd;
    public GameObject chargeCannonFirePoint;
    public int chargeCannonCoolDownTime;
    private int chargeCannonCoolDown;
    public int[] chargeShotCost;
    public float[] chargeShotSpeed;
    public GameObject[] chargeShotPrefab;
    private ChargeCannonShot chargeShotScript;
    private bool chargingCannon; //true - check how long | false - the cannon is not being charged.
    private float chargeTime;
    

    //FocusBeam
    public GameObject focusBeamBarrelEnd;
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
    */

    //LayerMasks
    private int toBeEffectedLayer;


    public float angleDir;

    //AsteroidScripts
    private Asteroid asteroidScript;
    //EnemyScripts
    private PlayerHealth enemyHealthScript;
    private DamageBug damageBugScript;
    private AutoTurret autoTurretScript;
    private TripMine tripMineScript;
    private JunkHealth junkScript;
    //PlayerScripts
    private PlayerMovement playerMoveScript;
    private PlayerHealth playerHealthScript;
    private PlayerRotation playerRotateScript;
    private Rigidbody2D playerRB;

    //MuzzleFlashes
    public ParticleSystem RsFlashRight;
    public ParticleSystem RsFlashLeft;
    public ParticleSystem LrFlash;
    public ParticleSystem RlFlash;

    //LaserRifle
    public ParticleSystem laserRifleParticlesBottom;
    public GameObject laserRifleHitParticles;
    private GameObject hitParticlesGOCopy;
    private ParticleSystem hitParticlesCopy;

    //HomingMissiles
    public ParticleSystem hMLeftSmoke;
    public ParticleSystem hMRightSmoke;

    //Audio
    private AudioSource audioSource;

    //RapidShot
    public AudioClip rapidShotOneShot;
    //RemoteLauncher
    public AudioClip launchOneShot;
    public AudioClip rocketBoostersLoop;
    public AudioClip DetonationOneShot;
    //LineSniper
    public AudioClip lineSniperShotOneShot;
    public AudioClip bulletTrailLoop;
    public AudioClip lineSniperHitOneShot;
    //FocusBeam
    /*
    public AudioClip focusBeamFiredOneShot;
    public AudioClip focusBeamHitingLoop;
    public AudioClip focusBeamFiringLoop;
    */
    private bool showingLights;

    //Environment Effects
    public bool frozen;

    //GameStart
    public bool disabled;

    private bool weaponFired;
    public float smoothing;
    public Transform weaponTransform;
    private Vector3 weaponNorm = new Vector3(1f, 1f, 1f);
    private Vector3 weaponEnlarge;

    public float lightsSmooth = 2f;

    void Awake()
    {
        //Rewired
        int realNum = numPlayer - 1;
        playerInput = ReInput.players.GetPlayer(realNum);

        shootInput = "RightTrigger";

        //GetComponent
        playerMoveScript = GetComponent<PlayerMovement>();
        playerHealthScript = GetComponent<PlayerHealth>();
        playerRotateScript = GetComponentInChildren<PlayerRotation>();
        audioSource = GetComponent<AudioSource>();
        playerRB = GetComponent<Rigidbody2D>();
        //LayerMasks
        toBeEffectedLayer = LayerMask.GetMask("ThingsToBeEffected");
        plasmaFireWait = new WaitForSeconds(timeBetweenShots);
    }

    void Start()
    {
        //SetUpInputs();
        //focusBeamRenderer = GetComponentInChildren<LineRenderer>();
        alternateFireSpot = 0;
        rocketFired = false;
        fireState = true;
        grenadesOut = false;
        //focusBeamRenderer.enabled = false;
        //hitCoolDown = 0;
        LightsOn();
    }

    /*
    void SetUpInputs()
    {
        //Debug.Log("INPUTS SET UP");
        if (numPlayer == 1)
        {
            shootInput = "Player1RTrigger";
        }
        else if (numPlayer == 2)
        {
            shootInput = "Player2RTrigger";
        }
        else if (numPlayer == 3)
        {
            shootInput = "Player3RTrigger";
        }
        else if (numPlayer == 4)
        {
            shootInput = "Player4RTrigger";
        }
    }
    */
    public void ShowingLights(bool state)
    {
        showingLights = state;
    }

    void RapidShotLights(bool state)
    {
        rightRShotIndBottom.enabled = state;
        leftRShotIndBottom.enabled = state;
        
        rightRShotInd.enabled = state;
        leftRShotInd.enabled = state;

        rightRSShotLight.enabled = state;
        leftRSShotLight.enabled = state;
    }
    void RemoteLauncherLights(bool state)
    {
        launcherFiredGlow.enabled = state;
        launcherFiredLight.enabled = state;
        launcherActiveGlow.enabled = state;
        launcherActiveLight.enabled = state;
        launcherInactive.enabled = state;
        launcherFiredInactive.enabled = state;
    }
    void LineRifleLights(bool state)
    {
        lRLightOn.enabled = state;
        lRLightOff.enabled = state;
        lRGlow.enabled = state;
    }
    void NeedleGatlingLights(bool state)
    {
        nGatlingLight.enabled = state;
        nGatlingGlowA.enabled = state;
        nGatlingGlowB.enabled = state;
    }
    void LaserRifleLights(bool state)
    {
        laserRGlow.enabled = state;
        laserRLightOff.enabled = state;
        laserRLightOn.enabled = state;
    }
    void HomingMissilesLights(bool state)
    {
        hMFired.enabled = state;
        hMFiredLight.enabled = state;
        hMLightsOff.enabled = state;
        hMLaserActive.enabled = state;
        hMLeftMissiles.enabled = state;
        hMRightMissiles.enabled = state;

        rightMissileIndBottom.enabled = state;
        leftMissileIndBottom.enabled = state;
        rightMissileIndicator.enabled = state;
        leftMissileIndicator.enabled = state;
    }
    void PhaseBlasterLights(bool state)
    {
        pBFiredLight.enabled = state;
        pBLightsOff.enabled = state;
        pBLightOn.enabled = state;
        pBFrontGlow.enabled = state;
        pBBackGlow.enabled = state;
    }
    void GrenadeGunLights(bool state)
    {
        gGBackSquareOn.enabled = state;
        gGLightsOff.enabled = state;
        gGBarrelLight.enabled = state;
        gGBarrelGlow.enabled = state;
        gGBackGlow.enabled = state;
    }
    void PlasmaThrowerLights(bool state)
    {
        pTBackPackColor.enabled = state;
        pTBackPackGlow.enabled = state;
        pTNozzleGlow.enabled = state;
        pTNozzleOff.enabled = state;
        pTNozzleOn.enabled = state;

        pTParticlesGO.SetActive(state);
        pTParticlesGO2.SetActive(state);
    }




    void ManageLightsEngaged()
    {
        if (showingLights)
        {
            switch (offenseSystem)
            {
                case (0):
                    RapidShotLights(true);
                    RemoteLauncherLights(false);
                    LineRifleLights(false);
                    NeedleGatlingLights(false);
                    LaserRifleLights(false);
                    HomingMissilesLights(false);
                    PhaseBlasterLights(false);
                    GrenadeGunLights(false);
                    PlasmaThrowerLights(false);
                    break;
                case (1):
                    RapidShotLights(false);
                    RemoteLauncherLights(true);
                    LineRifleLights(false);
                    NeedleGatlingLights(false);
                    LaserRifleLights(false);
                    HomingMissilesLights(false);
                    PhaseBlasterLights(false);
                    GrenadeGunLights(false);
                    PlasmaThrowerLights(false);
                    break;
                case (2):
                    RapidShotLights(false);
                    RemoteLauncherLights(false);
                    LineRifleLights(true);
                    NeedleGatlingLights(false);
                    LaserRifleLights(false);
                    HomingMissilesLights(false);
                    PhaseBlasterLights(false);
                    GrenadeGunLights(false);
                    PlasmaThrowerLights(false);
                    break;
                case (3):
                    RapidShotLights(false);
                    RemoteLauncherLights(false);
                    LineRifleLights(false);
                    NeedleGatlingLights(true);
                    LaserRifleLights(false);
                    HomingMissilesLights(false);
                    PhaseBlasterLights(false);
                    GrenadeGunLights(false);
                    PlasmaThrowerLights(false);
                    break;
                case (4):
                    RapidShotLights(false);
                    RemoteLauncherLights(false);
                    LineRifleLights(false);
                    NeedleGatlingLights(false);
                    LaserRifleLights(true);
                    HomingMissilesLights(false);
                    PhaseBlasterLights(false);
                    GrenadeGunLights(false);
                    PlasmaThrowerLights(false);
                    break;
                case (5):
                    RapidShotLights(false);
                    RemoteLauncherLights(false);
                    LineRifleLights(false);
                    NeedleGatlingLights(false);
                    LaserRifleLights(false);
                    HomingMissilesLights(true);
                    PhaseBlasterLights(false);
                    GrenadeGunLights(false);
                    PlasmaThrowerLights(false);
                    break;
                case (6):
                    RapidShotLights(false);
                    RemoteLauncherLights(false);
                    LineRifleLights(false);
                    LaserRifleLights(false);
                    NeedleGatlingLights(false);
                    HomingMissilesLights(false);
                    PhaseBlasterLights(true);
                    GrenadeGunLights(false);
                    PlasmaThrowerLights(false);
                    break;
                case (7):
                    RapidShotLights(false);
                    RemoteLauncherLights(false);
                    LineRifleLights(false);
                    LaserRifleLights(false);
                    NeedleGatlingLights(false);
                    HomingMissilesLights(false);
                    PhaseBlasterLights(false);
                    GrenadeGunLights(true);
                    PlasmaThrowerLights(false);
                    break;
                case (8):
                    RapidShotLights(false);
                    RemoteLauncherLights(false);
                    LineRifleLights(false);
                    NeedleGatlingLights(false);
                    LaserRifleLights(false);
                    HomingMissilesLights(false);
                    PhaseBlasterLights(false);
                    GrenadeGunLights(false);
                    PlasmaThrowerLights(true);
                    break;
            }
        }
        else
        {
            RapidShotLights(false);
            RemoteLauncherLights(false);
            LineRifleLights(false);
            NeedleGatlingLights(false);
            LaserRifleLights(false);
            HomingMissilesLights(false);
            PhaseBlasterLights(false);
            GrenadeGunLights(false);
            PlasmaThrowerLights(false);
        }
    }

    void LightsColorToClear()
    {
        switch(offenseSystem)
        {
            case (0):
                
                break;
            case (1):
                LerpToClear(launcherFiredGlow);
                LerpToClear(launcherFiredLight);

                if (!rocketActive)
                {
                    LerpToClear(launcherActiveGlow);
                    LerpToClear(launcherActiveLight);
                }
                break;
            case (2):
                LerpToClear(lRLightOn);
                LerpToClear(lRGlow);
                break;
            case (3):
                LerpToClear(nGatlingLight);
                LerpToClear(nGatlingGlowA);
                LerpToClear(nGatlingGlowB);
                break;
            case (4):
                LerpToClear(laserRLightOn);
                LerpToClear(laserRGlow);
                break;
            case (5):
                LerpToClear(hMFiredLight);
                LerpToClear(hMFired);
                break;
            case (6):
                LerpToClear(pBLightOn);
                LerpToClear(pBFrontGlow);

                if (!rocketActive)
                {
                    LerpToClear(pBBackGlow);
                    LerpToClear(pBFiredLight);
                }
                break;
            case (7):
                LerpToClear(gGBarrelLight);
                LerpToClear(gGBarrelGlow);
                break;
            case (8):
                LerpToClear(pTNozzleOn);
                LerpToClear(pTNozzleGlow);
                break;
        }
    }

    void LightsOn()
    {
        switch (offenseSystem)
        {
            case (0):
                //Already hard coded in
                break;
            case (1):
                launcherFiredGlow.color = teamColors[teamCount];
                launcherFiredLight.color = teamColors[teamCount];

                launcherActiveGlow.color = teamColors[teamCount];
                launcherActiveLight.color = teamColors[teamCount];
                break;
            case (2):
                lRLightOn.color = teamColors[teamCount];
                lRGlow.color = teamColors[teamCount];
                break;
            case (3):
                nGatlingLight.color = teamColors[teamCount];
                nGatlingGlowA.color = teamColors[teamCount];
                nGatlingGlowB.color = teamColors[teamCount];
                break;
            case (4):
                break;
            case (5):
                hMFiredLight.color = teamColors[teamCount];
                hMFired.color = teamColors[teamCount];
                break;
            case (6):
                pBFiredLight.color = teamColors[teamCount];
                pBLightOn.color = teamColors[teamCount];
                pBFrontGlow.color = teamColors[teamCount];
                pBBackGlow.color = teamColors[teamCount];
                break;
            case (7):
                gGBackSquareOn.color = teamColors[teamCount];
                gGBarrelLight.color = teamColors[teamCount];
                gGBarrelGlow.color = teamColors[teamCount];
                gGBackGlow.color = teamColors[teamCount];
                break;
            case (8):
                pTNozzleGlow.color = teamColors[teamCount];
                pTNozzleOn.color = teamColors[teamCount];
                break;
        }
    }

    void LerpToClear(SpriteRenderer spriteRend)
    {
        spriteRend.color = Color.Lerp(spriteRend.color, Color.clear, lightsSmooth * Time.deltaTime);
    }

    void MonitorWeaponFired()
    {
        if (weaponFired)
        {
            LightsOn();

            //Set scale to 1.5f
            float weaponTransX = Random.Range(1.25f, 1.5f);
            float weaponTransY = Random.Range(1.25f, 1.5f);
            Vector3 weaponEnlarge = new Vector3(weaponTransX, weaponTransY, 1f);
            weaponTransform.localScale = weaponEnlarge;

            if (offenseSystem == 5)
            {
                homingMissilesLaser.SetWidth(1f, 1f);
            }
        }
        else
        {
            //Lerp transform.localScale back to 1,1,1
            weaponTransform.localScale = Vector3.Lerp(weaponTransform.localScale, weaponNorm, smoothing * Time.deltaTime);
            LightsColorToClear();
            if (offenseSystem == 5)
            {
                hMLaserStartWidth = Mathf.Lerp(hMLaserStartWidth, 0.1f, 2f * Time.deltaTime);
                hMLaserEndWidth = Mathf.Lerp(hMLaserEndWidth, 0.1f, 2f * Time.deltaTime);
                homingMissilesLaser.SetWidth(hMLaserStartWidth, hMLaserEndWidth);
            }
        }
        weaponFired = false;
    }

    void Update()
    {
        ManageLightsEngaged();

        if (!frozen && !disabled)
        {
            OffensiveSystem();
            MonitorWeaponFired();
        }
        else
        {
            //Don't forget to comment this out.
            //Debug.Log("Frozen");
        }
    }

    void OffensiveSystem()
    {
        if (offenseSystem == 0)
        {
            RapidShotWeapon();
            
            lineRifleSight.enabled = false;
            laserRifleLine.enabled = false;
            homingMissilesLaser.enabled = false;
            
        }
        else if (offenseSystem == 1)
        {
            RemoteLauncherWeapon();
            lineRifleSight.enabled = false;
            laserRifleLine.enabled = false;
            homingMissilesLaser.enabled = false;
            
        }
        else if (offenseSystem == 2)
        {
            LineSniperWeapon();
            laserRifleLine.enabled = false;
            homingMissilesLaser.enabled = false;
            

            if (sightOut == true)
            {
                lineRifleSight.enabled = true;
            }
            else
            {
                lineRifleSight.enabled = false;
            }
        }
        else if (offenseSystem == 3)
        {
            //NeedleGatling
            NeedleGatlingWeapon();
            lineRifleSight.enabled = false;
            laserRifleLine.enabled = false;
            homingMissilesLaser.enabled = false;
            
        }
        else if (offenseSystem == 4)
        {
            //LaserRifle
            LaserRifleWeapon();
            lineRifleSight.enabled = false;
            homingMissilesLaser.enabled = false;
            
        }
        else if (offenseSystem == 5)
        {
            //HomingMissiles
            HomingMissilesWeapon();
            lineRifleSight.enabled = false;
            laserRifleLine.enabled = false;
            //MissileIndicators
            
        }
        else if (offenseSystem == 6)
        {
            //PhaseBlaster
            NewPhaseBlasterWeapon();
            lineRifleSight.enabled = false;
            laserRifleLine.enabled = false;
            homingMissilesLaser.enabled = false;
            
        }
        else if (offenseSystem == 7)
        {
            //GrenadeGun
            NewGrenadeGunWeapon();
            lineRifleSight.enabled = false;
            laserRifleLine.enabled = false;
            homingMissilesLaser.enabled = false;
            
        }
        else if (offenseSystem == 8)
        {
            //PlasmaThrower
            PlasmaThrowerWeapon();
            lineRifleSight.enabled = false;
            laserRifleLine.enabled = false;
            homingMissilesLaser.enabled = false;
            
        }
        else
        {
            //Debug.Log("Weapon Not Set.");
        }
    }

    //Rapid_Shot
    void RapidShotWeapon()
    {
        if (newRapidShotCoolDown > 0)
        {
            newRapidShotCoolDown -= Time.deltaTime;
        }
        else
        {
            newRapidShotCoolDown = 0;
        }
        if (playerInput.GetAxis(shootInput) > 0.1 && newRapidShotCoolDown == 0)
        {
            if (playerHealthScript.shotHealthCount > 1 || playerHealthScript.overchargeActive == true)
            {
                //Debug.Log("Firing rapidShot");
                RapidShot(alternateFireSpot);
            }
        }
    }
    void RapidShot(int alt)
    {
        Vector2 barrelEnd;
        Vector2 firePoint;
        if (alt == 0)
        {
            alternateFireSpot = 1;
            //BarrelEnd
            barrelEnd = rapidShotBarrelEnd0.transform.position;
            //FirePoint
            firePoint = rapidShotFirePoint0.transform.position;
            
            leftRShotInd.color = Color.clear;
            rightRShotInd.color = teamColors[teamCount];
            leftRSShotLight.color = Color.clear;
            rightRSShotLight.color = teamColors[teamCount];
            

            RsFlashLeft.Play();
        }
        else //(alt == 1)
        {
            alternateFireSpot = 0;
            //BarrelEnd
            barrelEnd = rapidShotBarrelEnd1.transform.position;
            //FirePoint
            firePoint = rapidShotFirePoint1.transform.position;

            leftRShotInd.color = teamColors[teamCount];
            rightRShotInd.color = Color.clear;
            leftRSShotLight.color = teamColors[teamCount];
            rightRSShotLight.color = Color.clear;

            RsFlashRight.Play();
        }

        //TakeDamage
        playerHealthScript.Damage(rapidShotCost, true);
        //Sound
        //audioSource.PlayOneShot(rapidShotOneShot);
        //Set cooldown
        //rapidShotCoolDown = rapidShotCoolDownTime;
        newRapidShotCoolDown = newRapidShotCoolDownTime;

        //Create shotInstance
        shotInstance = Instantiate(rapidShotShotPrefab, barrelEnd, Quaternion.identity) as GameObject;

        //Grab shotScript
        shotPrefabScript = shotInstance.GetComponent<ProtoShotPrefab>();
        shotPrefabScript.teamCount = teamCount;
        shotPrefabScript.color = teamColors[teamCount];

        //Set Colors
        shotPrefabScript.shotSprite.color = teamColors[teamCount];
        shotPrefabScript.particleSys.startColor = teamColors[teamCount];

        //Grab and set Rotation
        angleDir = playerRotateScript.MyAngle();
        shotInstance.transform.eulerAngles = new Vector3(transform.eulerAngles.x, transform.eulerAngles.y, angleDir);

        //Get Rigidbody, add force.
        rb2d = shotInstance.GetComponent<Rigidbody2D>();
        straight = barrelEnd - firePoint;
        rb2d.AddForce(straight * rapidShotShotSpeed);
        weaponFired = true;
    }

    //Remote_Launcher
    void RemoteLauncherWeapon()
    {
        if (newRemoteLauncherCoolDown > 0)
        {
            newRemoteLauncherCoolDown -= Time.deltaTime;
        }
        else
        {
            newRemoteLauncherCoolDown = 0;
        }

        if (playerInput.GetAxis(shootInput) > 0.25f && rocketFired == false)
        {
            if (newRemoteLauncherCoolDown == 0)
            {
                if (playerHealthScript.shotHealthCount > 2 || playerHealthScript.overchargeActive == true)
                {
                    RemoteLauncher();
                }
                //RocketFired = true elsewhere
            }
        }
        else if (playerInput.GetAxis(shootInput) < 0.25f && rocketFired == true)
        {
            remoteLauncherShotScript.ExplosiveTrigger();
            rocketFired = false;

        }

        /*
        if (Input.GetButtonDown(shootInput))
        {
            if (remoteLauncherCoolDown < 1)
            {
                if (playerHealthScript.shotHealthCount > 2 || playerHealthScript.overchargeActive == true)
                RemoteLauncher();
                //RocketFired = true elsewhere
            }
        }

        if (Input.GetButtonUp(shootInput) && rocketFired == true)
        {
            remoteLauncherShotScript.ExplosiveTrigger();
            rocketFired = false;
        }
        */
    }
    void RemoteLauncher()
    {
        RlFlash.Play();

        rocketFired = true;
        playerHealthScript.Damage(remoteLauncherCost, true);
        //audioSource.PlayOneShot(launchSound);
        newRemoteLauncherCoolDown = newRemoteLauncherCoolDownTime;
        //Debug.Log("Remote_Launcher Fired, rocketFired=" + rocketFired);
        //Create shotInstance
        shotInstance = Instantiate(remoteLauncherShotPrefab, remoteLauncherBarrelEnd.transform.position, Quaternion.identity) as GameObject;
        //Grab remoteLauncherShotScript
        remoteLauncherShotScript = shotInstance.GetComponent<RemoteLauncherShot>();
        remoteLauncherShotScript.teamCount = teamCount;
        remoteLauncherShotScript.myShotScript = this;
        remoteLauncherShotScript.target = launcherTarget;

        //Set Colors
        remoteLauncherShotScript.rocketImage.color = teamColors[teamCount];
        remoteLauncherShotScript.particleSys.startColor = teamColors[teamCount];
        //Grab and set Rotation
        angleDir = playerRotateScript.MyAngle();
        shotInstance.transform.eulerAngles = new Vector3(transform.eulerAngles.x, transform.eulerAngles.y, angleDir);
        //From here we'll do it differently.
        /*
        Okay, so we want the rocket to move to a point attached to the player's rotate component.
        We're going to need to set a reference of that point on this script to pass down to the shotInstance
        Then we're going to need to set the shot instance to move towards that target at a consistent speed.
        How to get consistent speed? No drag.
        We also have the make the rocket travel at a forward velocity. then the rocket just points to that point all the time.
        How do we get the rocket to always face the point? 
        
        Quaternion rotation = Quaternion.LookRotation(Target.transform.position - transform.position, transform.right);
        transform.rotation = new Quaternion(0, 0, rotation.z, rotation.w);

        */

        //Get Rigidbody, add force
        rb2d = shotInstance.GetComponent<Rigidbody2D>();
        straight = remoteLauncherBarrelEnd.transform.position - remoteLauncherFirePoint.transform.position;
        //rb2d.AddForce(straight * remoteLauncherShotScript.moveSpeed);
        rb2d.AddForce(straight * remoteLauncherShotSpeed);
        weaponFired = true;
    }

    //Line_Sniper
    void LineSniperWeapon()
    {
        if (newLineSniperCoolDown > 0)
        {
            newLineSniperCoolDown -= Time.deltaTime;
        }
        else
        {
            newLineSniperCoolDown = 0;
        }

        if (playerInput.GetAxis(shootInput) > 0.25f && sightOut == false)
        {
            sightOut = true;
        }

        if (playerInput.GetAxis(shootInput) < 0.25f && sightOut == true)
        {
            sightOut = false;
            if (playerHealthScript.shotHealthCount > 3 && newLineSniperCoolDown == 0 || playerHealthScript.overchargeActive == true && newLineSniperCoolDown == 0)
            {
                LineSniper();
            }
        }

        /*
        if (Input.GetButton(shootInput) && lineSniperCoolDown < 1)
        {
            if (playerHealthScript.shotHealthCount > 3 || playerHealthScript.overchargeActive == true)
            {
                Debug.Log("Firing lineSniper");
                LineSniper();
            }
        }

        //LaserSight
        //If the rotate script detects a 'fully extended stick push' (need to find a better desc. of this...)
        if (playerRotateScript.sightUp)
        {
            lineRifleSight.enabled = true;
        }
        else
        {
            lineRifleSight.enabled = false;
        }
        */
    }
    void LineSniper()
    {
        LrFlash.Play();
        playerHealthScript.Damage(lineSniperCost, true);
        //audioSource.PlayOneShot(lineSniperShotOneShot);
        newLineSniperCoolDown = newLineSniperCoolDownTime;
        //Debug.Log("Line_Sniper Fired");
        //Create shotInstance
        shotInstance = Instantiate(lineSniperShotPrefab, lineSniperBarrelEnd.transform.position, Quaternion.identity) as GameObject;
        //Grab shotScript
        lineSniperShotScript = shotInstance.GetComponent<LineSniperShot>();
        lineSniperShotScript.teamCount = teamCount;
        lineSniperShotScript.color = teamColors[teamCount];
        //Color
        lineSniperShotScript.shotSprite.color = teamColors[teamCount];
        lineSniperShotScript.particleSys.startColor = teamColors[teamCount];
        //Grab and set Rotation
        angleDir = playerRotateScript.MyAngle();
        //Debug.Log("SHOT angleDir: " + angleDir);
        shotInstance.transform.eulerAngles = new Vector3(transform.eulerAngles.x, transform.eulerAngles.y, angleDir);
        //Get Rigidbody, add force.
        rb2d = shotInstance.GetComponent<Rigidbody2D>();
        straight = lineSniperBarrelEnd.transform.position - lineSniperFirePoint.transform.position;
        rb2d.AddForce(straight * lineSniperShotSpeed);
        weaponFired = true;
    }

    void NeedleGatlingWeapon()
    {
        if (newNeedleGatlingCoolDown > 0)
        {
            newNeedleGatlingCoolDown -= Time.deltaTime;
        }
        else
        {
            newNeedleGatlingCoolDown = 0;
        }

        if (playerHealthScript.shotHealthCount > 1 || playerHealthScript.overchargeActive == true)
        {
            if (playerInput.GetAxis(shootInput) > 0.1)
            {

                if (newNeedleGatlingCoolDown == 0)
                {
                    NewNeedleGatling();
                }
                lerpedNeedleGatlingCoolDownTime = Mathf.Lerp(lerpedNeedleGatlingCoolDownTime, targetNGatlingCoolDown, coolDownLerp * Time.deltaTime);
            }
            else
            {
                lerpedNeedleGatlingCoolDownTime = newNeedleGatlingCoolDownTime;
            }
        }
        else
        {
            lerpedNeedleGatlingCoolDownTime = newNeedleGatlingCoolDownTime;
        }
    }
    /*
    IEnumerator NeedleGatling()
    {
        randomNum1 = Random.Range(0, needleGatlingBarrelEnds.Length);
        randomNum2 = Random.Range(0, needleGatlingBarrelEnds.Length);

        //TakeDamage
        playerHealthScript.Damage(needleGatlingCost, true);
        //Sound
        //audioSource.PlayOneShot();
        //Set cooldown
        newNeedleGatlingCoolDown = newNeedleGatlingCoolDownTime;

        //Create Vector2
        needleShotVector = needleGatlingBarrelEnds[randomNum1].transform.position - needleGatlingFirePoint.transform.position;
        angleDir = Vector2.Angle(needleGatlingBarrelEnds[randomNum1].transform.position, needleGatlingFirePoint.transform.position);

        //Create shotInstance
        shotInstance = Instantiate(needleGatlingShotPrefab, needleGatlingFirePoint.transform.position, Quaternion.identity) as GameObject;

        //Grab shotScript
        needleScript = shotInstance.GetComponent<ProtoShotPrefab>();
        needleScript.teamCount = teamCount;
        needleScript.color = teamColors[teamCount];

        //Set Colors
        needleScript.shotSprite.color = teamColors[teamCount];
        needleScript.particleSys.startColor = teamColors[teamCount];

        angleDir = playerRotateScript.MyAngle();

        shotInstance.transform.eulerAngles = new Vector3(transform.eulerAngles.x, transform.eulerAngles.y, angleDir);

        //Get Rigidbody, add force.
        rb2d = shotInstance.GetComponent<Rigidbody2D>();
        rb2d.AddForce(needleShotVector * needleShotSpeed);
        weaponFired = true;
        //Wait for second shot
        yield return needleGatlingWait;

        //Sound
        //audioSource.PlayOneShot();

        //Create Vector2
        needleShotVector = needleGatlingBarrelEnds[randomNum2].transform.position - needleGatlingFirePoint.transform.position;

        //Create shotInstance
        shotInstance = Instantiate(needleGatlingShotPrefab, needleGatlingFirePoint.transform.position, Quaternion.identity) as GameObject;

        //Grab shotScript
        needleScript = shotInstance.GetComponent<ProtoShotPrefab>();
        needleScript.teamCount = teamCount;

        //Set Colors
        needleScript.shotSprite.color = teamColors[teamCount];
        needleScript.particleSys.startColor = teamColors[teamCount];

        angleDir = playerRotateScript.MyAngle();

        shotInstance.transform.eulerAngles = new Vector3(transform.eulerAngles.x, transform.eulerAngles.y, angleDir);

        //Get Rigidbody, add force.
        rb2d = shotInstance.GetComponent<Rigidbody2D>();
        rb2d.AddForce(needleShotVector * needleShotSpeed);
        weaponFired = true;
    }
    */
    void NewNeedleGatling()
    {
        //TakeDamage
        playerHealthScript.Damage(needleGatlingCost, true);
        //Sound
        //audioSource.PlayOneShot();
        //Set cooldown
        newNeedleGatlingCoolDown = lerpedNeedleGatlingCoolDownTime;
        //-------------------------------------------------------------------------

        randomNumA = Random.Range(0, 3);

        //Create Vector2
        needleShotVectorA = nGatlingAEndPos[randomNumA].transform.position - nGatlingAStartPos[randomNumA].transform.position;

        //Create shotInstanceA
        shotInstance = Instantiate(needleGatlingShotPrefab, nGatlingAStartPos[randomNumA].transform.position, Quaternion.identity) as GameObject;


        //--Don't change-------------------------------------------------------------
        angleDir = playerRotateScript.MyAngle();

        //Barrel A
        //Grab shotScript
        needleScriptA = shotInstance.GetComponent<ProtoShotPrefab>();
        needleScriptA.teamCount = teamCount;
        needleScriptA.color = teamColors[teamCount];

        //Set Colors
        needleScriptA.shotSprite.color = teamColors[teamCount];
        needleScriptA.particleSys.startColor = teamColors[teamCount];
        

        shotInstance.transform.eulerAngles = new Vector3(transform.eulerAngles.x, transform.eulerAngles.y, angleDir);

        //Get Rigidbody, add force.
        rb2d = shotInstance.GetComponent<Rigidbody2D>();
        rb2d.AddForce(needleShotVectorA * needleShotSpeed);
        //----------------------------


        //-----------------------------
        randomNumB = Random.Range(0, 3);

        //Create Vector2
        needleShotVectorB = nGatlingBEndPos[randomNumB].transform.position - nGatlingBStartPos[randomNumB].transform.position;

        //Create shotInstanceB
        shotInstanceB = Instantiate(needleGatlingShotPrefab, nGatlingBStartPos[randomNumB].transform.position, Quaternion.identity) as GameObject;


        //BarrelB
        //Grab shotScript
        needleScriptB = shotInstanceB.GetComponent<ProtoShotPrefab>();
        needleScriptB.teamCount = teamCount;
        needleScriptB.color = teamColors[teamCount];

        //Set Colors
        needleScriptB.shotSprite.color = teamColors[teamCount];
        needleScriptB.particleSys.startColor = teamColors[teamCount];

        shotInstanceB.transform.eulerAngles = new Vector3(transform.eulerAngles.x, transform.eulerAngles.y, angleDir);
        
        //Get Rigidbody, add force.
        rb2dB = shotInstanceB.GetComponent<Rigidbody2D>();
        rb2dB.AddForce(needleShotVectorB * needleShotSpeed);
        

        randomNumC = Random.Range(0, 3);

        //Create Vector2
        needleShotVectorC = nGatlingAEndPos[randomNumC].transform.position - nGatlingAStartPos[randomNumC].transform.position;

        //Create shotInstanceA
        shotInstanceC = Instantiate(needleGatlingShotPrefab, nGatlingAStartPos[randomNumC].transform.position, Quaternion.identity) as GameObject;


        //--Don't change-------------------------------------------------------------
        angleDir = playerRotateScript.MyAngle();

        //Barrel A
        //Grab shotScript
        needleScriptC = shotInstanceC.GetComponent<ProtoShotPrefab>();
        needleScriptC.teamCount = teamCount;
        needleScriptC.color = teamColors[teamCount];

        //Set Colors
        needleScriptC.shotSprite.color = teamColors[teamCount];
        needleScriptC.particleSys.startColor = teamColors[teamCount];


        shotInstanceC.transform.eulerAngles = new Vector3(transform.eulerAngles.x, transform.eulerAngles.y, angleDir);

        //Get Rigidbody, add force.
        rb2dC = shotInstanceC.GetComponent<Rigidbody2D>();
        rb2dC.AddForce(needleShotVectorC * needleShotSpeed);
        //----------------------------


        //-----------------------------
        randomNumD = Random.Range(0, 3);

        //Create Vector2
        needleShotVectorD = nGatlingBEndPos[randomNumD].transform.position - nGatlingBStartPos[randomNumD].transform.position;

        //Create shotInstanceB
        shotInstanceD = Instantiate(needleGatlingShotPrefab, nGatlingBStartPos[randomNumD].transform.position, Quaternion.identity) as GameObject;


        //BarrelB
        //Grab shotScript
        needleScriptD = shotInstanceD.GetComponent<ProtoShotPrefab>();
        needleScriptD.teamCount = teamCount;
        needleScriptD.color = teamColors[teamCount];

        //Set Colors
        needleScriptD.shotSprite.color = teamColors[teamCount];
        needleScriptD.particleSys.startColor = teamColors[teamCount];

        shotInstanceD.transform.eulerAngles = new Vector3(transform.eulerAngles.x, transform.eulerAngles.y, angleDir);

        //Get Rigidbody, add force.
        rb2dD = shotInstanceD.GetComponent<Rigidbody2D>();
        rb2dD.AddForce(needleShotVectorD * needleShotSpeed);

        weaponFired = true;
    }
    

    void TESTLaserRifleWeapon() //Old version worked worse
    {
        //This is the one going by frames.
        if (playerInput.GetAxis(shootInput) > 0.1f)
        {
            beamVector = laserRifleLens.transform.position - transform.position;
            if (laserFireable)
            {
                //Start shoot timer
                if (laserFireCount > 0)
                {
                    laserFireCount--;
                    //Lerp the width of the line from thick to thin
                    lineStartWidth = Mathf.Lerp(lineStartWidth, laserRifleLineEndWidth, laserSmoothing * Time.deltaTime);
                    lineEndWidth = Mathf.Lerp(lineEndWidth, laserRifleLineEndWidth, laserSmoothing * Time.deltaTime);
                    laserRifleLine.SetWidth(lineStartWidth, lineEndWidth);

                    //Raycast out to see what's in front of us
                    laserRifleLineHit = Physics2D.Raycast(laserRifleLens.transform.position, beamVector, laserRifleRange, toBeEffectedLayer);

                    //Set origin position of the laser line

                    if (laserRifleLineHit.collider != null)
                    {
                        pointVector = transform.position;
                        newVector2 = pointVector - laserRifleLineHit.point;

                        numm = Mathf.Abs(newVector2.magnitude);
                        if (numm > 21f)
                        {
                            numm = 21f;
                        }
                        laserRifleLine.SetPosition(1, new Vector2(0, numm));

                    }
                    else
                    {
                        laserRifleLine.SetPosition(1, laserRifleLineEndPos);

                    }
                    laserRifleLine.enabled = true;
                }
                else
                {

                    //disable firing again
                    laserFireable = false;
                    //fire laser
                    LaserRifle();
                    //Debug.Log("worked");
                }
            }
            //Once timer == 0 Call LaserRifle();
        }
        else
        {
            //Reset shoot timer
            laserFireCount = laserFireTime;
            laserFireable = true;
            //disable the line
            laserRifleLine.enabled = false;
            lineStartWidth = laserRifleLineStartWidth;
            lineEndWidth = laserRifleLineStartWidth;

        }
    }
    void TESTLaserRifle()
    {
        //disable the line
        laserRifleLine.enabled = false;

        beamVector = laserRifleLens.transform.position - transform.position;
        //Fire raycast forward
        laserRifleHit = Physics2D.Raycast(laserRifleLens.transform.position, beamVector, laserRifleRange, toBeEffectedLayer);
        //Did we hit the target?

        if (laserRifleHit.collider != null)
        {
            if (laserRifleHit.transform.tag == "Player1(Black)" || laserRifleHit.transform.tag == "Player2(White)" || laserRifleHit.transform.tag == "Player3" || laserRifleHit.transform.tag == "Player4")
            {
                //instantiate hit particles at laserRifleHit.point
                //Debug.Log("hit Player");
                enemyHealthScript = laserRifleHit.transform.GetComponent<PlayerHealth>();
                enemyHealthScript.Damage(laserRifleDamage, false);

            }
            else if (laserRifleHit.transform.tag == "Asteroid")
            {
                //grab asteroid script, damage it
                //Debug.Log("Hit Asteroid");
                asteroidScript = laserRifleHit.transform.GetComponent<Asteroid>();
                asteroidScript.DamageAsteroid(laserRifleDamage);
                //instantiate hit particles
            }
            else
            {

            }
        }
    } //Old version not using

    void LaserRifleWeapon()
    {
        //This is the one going by frames.
        if (playerInput.GetAxis(shootInput) > 0.1f)
        {
            if (playerHealthScript.shotHealthCount > 2 || playerHealthScript.overchargeActive == true)
            {
                
                beamVector = laserRifleLens.transform.position - transform.position;

                newVec = laserRifleLineEnd.transform.position;

                newnewVec = laserRifleLens.transform.position;
                laserRifleLine.SetPosition(0, newnewVec);

                //Check for target
                laserRifleLineHit = Physics2D.Linecast(newnewVec, newVec, toBeEffectedLayer);

                if (laserRifleLineHit)
                {
                    hitAnyVector = laserRifleLineHit.point;
                    laserRifleLine.SetPosition(1, hitAnyVector);
                }
                else
                {
                    laserRifleLine.SetPosition(1, newVec);
                }

                if (laserFireable)
                {
                    //Start shoot timer
                    if (laserFireCount > 0)
                    {
                        laserRGlow.color = teamColors[teamCount];
                        laserRLightOn.color = teamColors[teamCount];
                        laserFireCount -= Time.deltaTime;
                        //Lerp the width of the line from thick to thin
                        lineStartWidth = Mathf.Lerp(lineStartWidth, laserRifleLineEndWidth, laserSmoothing * Time.deltaTime);
                        lineEndWidth = Mathf.Lerp(lineEndWidth, laserRifleLineEndWidth, laserSmoothing * Time.deltaTime);
                        laserRifleLine.SetWidth(lineStartWidth, lineEndWidth);

                        laserRifleLine.enabled = true;
                    }
                    else
                    {

                        //disable firing again
                        laserFireable = false;
                        laserRifleLine.SetWidth(1, 1);
                        //fire laser
                        LaserRifle();
                        weaponFired = true;
                        //Debug.Log("worked");
                    }
                }
                //Once timer == 0 Call LaserRifle();
            }
        }
        else
        {
            //Reset shoot timer
            laserFireCount = laserFireTime;
            laserFireable = true;
            //disable the line
            laserRifleLine.enabled = false;
            lineStartWidth = laserRifleLineStartWidth;
            lineEndWidth = laserRifleLineStartWidth;

        }
    }
    void LaserRifle()
    {
        //disable the line
        laserRifleLine.enabled = false;

        laserRifleParticlesBottom.Play();

        //Cost
        playerHealthScript.Damage(laserRifleCost, true);

        beamVector = laserRifleLens.transform.position - transform.position;
        //Fire raycast forward
        laserRifleHit = Physics2D.Raycast(laserRifleLens.transform.position, beamVector, laserRifleRange, toBeEffectedLayer);
        //Did we hit the target?

        if (laserRifleHit.collider != null)
        {
            if (laserRifleHit.transform.tag == "Player1(Black)" || laserRifleHit.transform.tag == "Player2(White)" || laserRifleHit.transform.tag == "Player3" || laserRifleHit.transform.tag == "Player4")
            {
                //instantiate hit particles at laserRifleHit.point
                hitParticlesGOCopy = Instantiate(laserRifleHitParticles, laserRifleHit.point, Quaternion.identity) as GameObject;
                hitParticlesCopy = hitParticlesGOCopy.GetComponent<ParticleSystem>();
                hitParticlesCopy.startColor = teamColors[teamCount];


                //Debug.Log("hit Player");
                enemyHealthScript = laserRifleHit.transform.GetComponent<PlayerHealth>();
                enemyHealthScript.Damage(laserRifleDamage, false);

            }
            else if (laserRifleHit.transform.tag == "Asteroid")
            {
                //grab asteroid script, damage it
                //Debug.Log("Hit Asteroid");
                asteroidScript = laserRifleHit.transform.GetComponent<Asteroid>();
                asteroidScript.DamageAsteroid(laserRifleDamage);
                hitParticlesGOCopy = Instantiate(laserRifleHitParticles, laserRifleHit.point, Quaternion.identity) as GameObject;
                hitParticlesCopy = hitParticlesGOCopy.GetComponent<ParticleSystem>();
                hitParticlesCopy.startColor = teamColors[teamCount];
                //instantiate hit particles
            }
            else if (laserRifleHit.transform.tag == "DamageBug")
            {
                damageBugScript = laserRifleHit.transform.GetComponent<DamageBug>();
                if (damageBugScript.teamCount != teamCount)
                {
                    hitParticlesGOCopy = Instantiate(laserRifleHitParticles, laserRifleHit.point, Quaternion.identity) as GameObject;
                    hitParticlesCopy = hitParticlesGOCopy.GetComponent<ParticleSystem>();
                    hitParticlesCopy.startColor = teamColors[teamCount];
                    //damage the bug
                    damageBugScript.DamageThis(laserRifleDamage);
                }
            }
            else if (laserRifleHit.transform.tag == "AutoTurretHitBox")
            {
                autoTurretScript = laserRifleHit.transform.GetComponentInParent<AutoTurret>();
                if (autoTurretScript.teamCount != teamCount)
                {
                    hitParticlesGOCopy = Instantiate(laserRifleHitParticles, laserRifleHit.point, Quaternion.identity) as GameObject;
                    hitParticlesCopy = hitParticlesGOCopy.GetComponent<ParticleSystem>();
                    hitParticlesCopy.startColor = teamColors[teamCount];
                    //damage the Turret
                    autoTurretScript.DamageThis(laserRifleDamage);
                }
            }
            else if (laserRifleHit.transform.tag == "TripMine")
            {
                tripMineScript = laserRifleHit.transform.GetComponent<TripMine>();
                if (tripMineScript.teamCount != teamCount)
                {
                    hitParticlesGOCopy = Instantiate(laserRifleHitParticles, laserRifleHit.point, Quaternion.identity) as GameObject;
                    hitParticlesCopy = hitParticlesGOCopy.GetComponent<ParticleSystem>();
                    hitParticlesCopy.startColor = teamColors[teamCount];
                    //Blow up tripMine
                    tripMineScript.BlowUp();
                }
            }
            else if (laserRifleHit.transform.tag == "RocketAmmo")
            {
                remoteLauncherShotScript = laserRifleHit.transform.GetComponent<RemoteLauncherShot>();
                if (remoteLauncherShotScript.teamCount != teamCount)
                {
                    hitParticlesGOCopy = Instantiate(laserRifleHitParticles, laserRifleHit.point, Quaternion.identity) as GameObject;
                    hitParticlesCopy = hitParticlesGOCopy.GetComponent<ParticleSystem>();
                    hitParticlesCopy.startColor = teamColors[teamCount];
                    remoteLauncherShotScript.ExplosiveTrigger();
                }
            }
            else if (laserRifleHit.transform.tag == "Grenade")
            {
                grenadeScript1 = laserRifleHit.transform.GetComponent<Grenade>();
                if (grenadeScript1.teamCount != teamCount)
                {
                    hitParticlesGOCopy = Instantiate(laserRifleHitParticles, laserRifleHit.point, Quaternion.identity) as GameObject;
                    hitParticlesCopy = hitParticlesGOCopy.GetComponent<ParticleSystem>();
                    hitParticlesCopy.startColor = teamColors[teamCount];
                    grenadeScript1.BlowUp();
                }
            }
            else if (laserRifleHit.transform.tag == "Junk")
            {
                junkScript = laserRifleHit.transform.GetComponent<JunkHealth>();
                junkScript.DamageJunk(laserRifleDamage);
            }
        }
    }

    void HomingMissilesWeapon()
    {
        if (newHomingMissileCoolDown > 0)
        {
            newHomingMissileCoolDown -= Time.deltaTime;
        }
        else
        {
            newHomingMissileCoolDown = 0;
        }

        newnewVec = hMLaserStart.transform.position;
        homingMissilesLaser.SetPosition(0, newnewVec);
        //When we hold down the button
        if (playerInput.GetAxis(shootInput) > 0.1f)
        {
            hMLaserActive.enabled = true;
            if (playerHealthScript.shotHealthCount > 3 || playerHealthScript.overchargeActive == true)
            {
                //Show Laser
                //Debug.Log("ShowLaser");
                //homingMissilesLaser.enabled = true;
                newVec = hMLaserEnd.transform.position;
                homingMissilesLaser.enabled = true;

                //homingMissilesLaser.SetPosition(0, transform.position);
                homingMissilesLaser.SetPosition(0, newnewVec);

                //Check for target
                homingMissilesLaserHit = Physics2D.Linecast(newnewVec, newVec, toBeEffectedLayer);

                if (homingMissilesLaserHit)
                {
                    hitAnyVector = homingMissilesLaserHit.point;
                    homingMissilesLaser.SetPosition(1, hitAnyVector);
                }
                else
                {
                    homingMissilesLaser.SetPosition(1, newVec);
                }

                //If cooldown is ready
                if (newHomingMissileCoolDown == 0 && !missileFireable)
                {

                    if (homingMissilesLaserHit)
                    {

                        //If we hit a player or an asteroid tag that target
                        if (homingMissilesLaserHit.transform.tag == "Player1(Black)" || homingMissilesLaserHit.transform.tag == "Player2(White)" || homingMissilesLaserHit.transform.tag == "Player3" || homingMissilesLaserHit.transform.tag == "Player4" || homingMissilesLaserHit.transform.tag == "Asteroid" || homingMissilesLaserHit.transform.tag == "DamageBug" || homingMissilesLaserHit.transform.tag == "TripMine" || homingMissilesLaserHit.transform.tag == "AutoTurretHitBox" || homingMissilesLaserHit.transform.tag == "RocketAmmo" || homingMissilesLaserHit.transform.tag == "Junk")
                        {

                            if (homingMissilesLaserHit.transform.tag == "Player1(Black)" || homingMissilesLaserHit.transform.tag == "Player2(White)" || homingMissilesLaserHit.transform.tag == "Player3" || homingMissilesLaserHit.transform.tag == "Player4")
                            {
                                enemyHealthScript = homingMissilesLaserHit.transform.GetComponent<PlayerHealth>();
                                if (enemyHealthScript.teamCount != teamCount)
                                {

                                    hit1Vector = homingMissilesLaserHit.point;
                                    homingMissilesLaser.SetPosition(1, hit1Vector);
                                    missilesTarget = homingMissilesLaserHit.transform;
                                }
                            }
                            else if (homingMissilesLaserHit.transform.tag == "RocketAmmo")
                            {
                                remoteLauncherShotScript = homingMissilesLaserHit.transform.GetComponent<RemoteLauncherShot>();
                                if (remoteLauncherShotScript.teamCount != teamCount)
                                {
                                    hit1Vector = homingMissilesLaserHit.point;
                                    homingMissilesLaser.SetPosition(1, hit1Vector);
                                    missilesTarget = homingMissilesLaserHit.transform;
                                }
                            }
                            else if (homingMissilesLaserHit.transform.tag == "DamageBug")
                            {
                                damageBugScript = homingMissilesLaserHit.transform.GetComponent<DamageBug>();
                                if (damageBugScript.teamCount != teamCount)
                                {

                                    hit1Vector = homingMissilesLaserHit.point;
                                    homingMissilesLaser.SetPosition(1, hit1Vector);
                                    missilesTarget = homingMissilesLaserHit.transform;
                                }
                            }
                            else if (homingMissilesLaserHit.transform.tag == "AutoTurretHitBox")
                            {
                                autoTurretScript = homingMissilesLaserHit.transform.GetComponentInParent<AutoTurret>();
                                if (autoTurretScript.teamCount != teamCount)
                                {
                                    hit1Vector = homingMissilesLaserHit.point;
                                    homingMissilesLaser.SetPosition(1, hit1Vector);
                                    missilesTarget = homingMissilesLaserHit.transform;
                                }
                            }
                            else if (homingMissilesLaserHit.transform.tag == "TripMine")
                            {
                                tripMineScript = homingMissilesLaserHit.transform.GetComponent<TripMine>();
                                if (tripMineScript.teamCount != teamCount)
                                {

                                    hit1Vector = homingMissilesLaserHit.point;
                                    homingMissilesLaser.SetPosition(1, hit1Vector);
                                    missilesTarget = homingMissilesLaserHit.transform;
                                }
                            }
                            else if (homingMissilesLaserHit.transform.tag == "Asteroid")
                            {
                                missilesTarget = homingMissilesLaserHit.transform;
                                
                                hit2Vector = homingMissilesLaserHit.point;
                                homingMissilesLaser.SetPosition(1, hit2Vector);
                            }
                            else if (homingMissilesLaserHit.transform.tag == "Junk")
                            {
                                //Debug.Log("runnin");
                                missilesTarget = homingMissilesLaserHit.transform;

                                hit2Vector = homingMissilesLaserHit.point;
                                homingMissilesLaser.SetPosition(1, hit2Vector);
                            }
                            //Now we have a target, missileFireable is now true with the target selected.
                            missileFireable = true;
                            homingMissilesLaser.enabled = false;
                        }
                        else
                        {

                            hit3Vector = homingMissilesLaserHit.point;
                            //homingMissilesLaser.SetPosition(0, newnewVec);
                            homingMissilesLaser.SetPosition(1, hit3Vector);
                        }
                    }
                    else
                    {
                        //We didn't hit anything and have the laser at max distance
                        //homingMissilesLaser.SetPosition(0, newnewVec);
                        homingMissilesLaser.SetPosition(1, newVec);
                    }
                }
                else if (missileFireable && fireState)
                {
                    //So we don't fire continuously
                    fireState = false;
                    //FireMissilesAtTarget
                    HomingMissiles(missilesTarget, alternateFireSpot);
                }
            }
        }
        else
        {
            homingMissilesLaser.enabled = false;
            hMLaserActive.enabled = false;
        }
    }
    void HomingMissiles(Transform target, int alt)
    {
        if (alt == 0)
        {
            leftMissileIndicator.enabled = false;
            leftMissileIndBottom.enabled = false;
            rightMissileIndicator.enabled = true;
            rightMissileIndBottom.enabled = true;
            hMLeftMissiles.color = Color.clear;
            leftMissileIndBottom.color = Color.clear;
            leftMissileIndicator.color = Color.clear;
            hMRightMissiles.color = teamColors[teamCount];
            rightMissileIndBottom.color = teamColors[teamCount];
            rightMissileIndicator.color = teamColors[teamCount];
            alternateFireSpot = 1;
            //Exit
            missileExit = missileExitLeft;
            missileStart = missileStartLeft;
        }
        else //(alt == 1)
        {
            leftMissileIndicator.enabled = true;
            leftMissileIndBottom.enabled = true;
            rightMissileIndicator.enabled = false;
            rightMissileIndBottom.enabled = false;
            hMLeftMissiles.color = teamColors[teamCount];
            leftMissileIndBottom.color = teamColors[teamCount];
            leftMissileIndicator.color = teamColors[teamCount];
            hMRightMissiles.color = Color.clear;
            rightMissileIndBottom.color = Color.clear;
            rightMissileIndicator.color = Color.clear;
            alternateFireSpot = 0;
            //Exit
            missileExit = missileExitRight;
            missileStart = missileStartRight;
        }

        //Deduct hp
        playerHealthScript.Damage(homingMissileClusterCost, true);

        //Debug.Log("FireMissile1");
        FireMissile(target);
        switch (alt)
        {
            case (0):
                hMLeftSmoke.Play();
                break;
            case (1):
                hMRightSmoke.Play();
                break;
        }
        /*
        yield return missileFireWait;
        //Debug.Log("FireMissile2");
        FireMissile(target);
        switch (alt)
        {
            case (0):
                hMLeftSmoke.Play();
                break;
            case (1):
                hMRightSmoke.Play();
                break;
        }
        yield return missileFireWait;
        //Debug.Log("FireMissile3");
        FireMissile(target);
        switch (alt)
        {
            case (0):
                hMLeftSmoke.Play();
                break;
            case (1):
                hMRightSmoke.Play();
                break;
        }
        */

        missileFireable = false;
        newHomingMissileCoolDown = newHomingMissileCoolDownTime;
        fireState = true;

    }
    void FireMissile(Transform target)
    {
        //Create a smoke effect

        //playAudio

        //Create shotInstance
        shotInstance = Instantiate(homingMissile, missileExit.transform.position, Quaternion.identity) as GameObject;

        //Grab ShotScript
        missileShotScript = shotInstance.GetComponent<EnemySeekerShot>();
        missileShotScript.teamCount = teamCount;

        //Grab FollowScript
        missileFollowScript = shotInstance.GetComponent<EnemyFollow>();
        missileFollowScript.target = target;

        //Set Colors
        missileShotScript.mySprite.color = teamColors[teamCount];
        missileShotScript.myParticleTrail.startColor = teamColors[teamCount];

        //Grab and set Rotation
        angleDir = playerRotateScript.MyAngle();
        shotInstance.transform.eulerAngles = new Vector3(transform.eulerAngles.x, transform.eulerAngles.y, angleDir);

        //Get Rigidbody, add force
        rb2d = shotInstance.GetComponent<Rigidbody2D>();
        
        straight = missileExit.transform.position - missileStart.transform.position;

        rb2d.AddForce(straight * initialMissileSpeed);
        weaponFired = true;

    }

    /*
    void PhaseBlasterWeapon()
    {
        if (phaseBlasterCoolDown > 0)
        {
            phaseBlasterCoolDown -= Time.deltaTime;
        }
        else
        {
            phaseBlasterCoolDown = 0;
        }
        if (playerInput.GetAxis(shootInput) > 0.1f && phaseBlasterCoolDown == 0)
        {
            if (playerHealthScript.shotHealthCount > 2 || playerHealthScript.overchargeActive == true)
            {
                PhaseBlaster();
                
            }
        }
    }
    void PhaseBlaster()
    {
        //Play fx
        phaseBlasterFx.Play();
        playerHealthScript.Damage(phaseBlasterCost, true);
        //audio
        phaseBlasterCoolDown = phaseBlasterCoolDownTime;

        //Create shotInstance
        shotInstance = Instantiate(phaseBlast, phaseBlasterStart.transform.position, Quaternion.identity) as GameObject;
        //Grab shotScript
        phaseBlastScript = shotInstance.GetComponent<PhaseBlast>();
        //set teamcount
        phaseBlastScript.teamCount = teamCount;
        
        //set color
        phaseBlastScript.mySprite.color = teamColors[teamCount];
        //phaseBlastScript.orbitSprite1.color = teamColors[teamCount];
        //phaseBlastScript.orbitSprite2.color = teamColors[teamCount];
        //Color
        //set shot color
        //set particle system color
        //Grab and set Rotation
        angleDir = playerRotateScript.MyAngle();
        //Debug.Log("SHOT angleDir: " + angleDir);
        shotInstance.transform.eulerAngles = new Vector3(transform.eulerAngles.x, transform.eulerAngles.y, angleDir);
        //Get Rigidbody, add force.
        rb2d = shotInstance.GetComponent<Rigidbody2D>();
        straight = phaseBlasterEnd.transform.position - phaseBlasterStart.transform.position;
        rb2d.AddForce(straight * phaseBlasterShotSpeed);
        weaponFired = true;
    }
    */
    void NewPhaseBlasterWeapon()
    {
        if (phaseBlasterCoolDown > 0)
        {
            phaseBlasterCoolDown -= Time.deltaTime;
        }
        else
        {
            phaseBlasterCoolDown = 0;
        }

        if (playerInput.GetAxis(shootInput) > 0.25f && rocketFired == false)
        {
            if (phaseBlasterCoolDown == 0)
            {
                if (playerHealthScript.shotHealthCount > phaseBlasterCost || playerHealthScript.overchargeActive == true)
                {
                    NewPhaseBlaster();
                    rocketActive = true;
                }
            }
        }
        else if (playerInput.GetAxis(shootInput) < 0.25f && rocketFired == true)
        {
            phaseBlastScript.AddDrag();
            rocketFired = false;
        }
    }
    void NewPhaseBlaster()
    {
        //Play fx
        phaseBlasterFx.Play();
        rocketFired = true;
        playerHealthScript.Damage(phaseBlasterCost, true);
        //audio
        phaseBlasterCoolDown = phaseBlasterCoolDownTime;

        //Create shotInstance
        shotInstance = Instantiate(phaseBlast, phaseBlasterStart.transform.position, Quaternion.identity) as GameObject;
        //Grab shotScript
        phaseBlastScript = shotInstance.GetComponent<PhaseBlast>();
        //set teamcount
        phaseBlastScript.teamCount = teamCount;
        //Set Shootscript
        phaseBlastScript.shootScript = this;
        //set color
        phaseBlastScript.mySprite.color = teamColors[teamCount];
        //phaseBlastScript.orbitSprite1.color = teamColors[teamCount];
        //phaseBlastScript.orbitSprite2.color = teamColors[teamCount];
        //Color
        //set shot color
        //set particle system color
        //Grab and set Rotation
        angleDir = playerRotateScript.MyAngle();
        //Debug.Log("SHOT angleDir: " + angleDir);
        shotInstance.transform.eulerAngles = new Vector3(transform.eulerAngles.x, transform.eulerAngles.y, angleDir);
        //Get Rigidbody, add force.
        rb2d = shotInstance.GetComponent<Rigidbody2D>();
        straight = phaseBlasterEnd.transform.position - phaseBlasterStart.transform.position;
        rb2d.AddForce(straight * phaseBlasterShotSpeed);
        weaponFired = true;
    }

    void GrenadeGunWeapon()
    {
        if (grenadeGunCoolDown > 0)
        {
            grenadeGunCoolDown -= Time.deltaTime;
        }
        else
        {
            grenadeGunCoolDown = 0;
        }

        if (playerInput.GetAxis(shootInput) > 0.1f && !grenadesOut)
        {
            if (grenadeGunCoolDown == 0)
            {
                if (playerHealthScript.shotHealthCount > grenadeGunCost || playerHealthScript.overchargeActive == true)
                {
                    FireGrenadeGun();
                }
            }
        }
        else if (playerInput.GetAxis(shootInput) < 0.25f && grenadesOut)
        {
            if (grenadeCopy1 != null)
            {
                grenadeScript1.BlowUp();
            }

            if (grenadeCopy2 != null)
            {
                grenadeScript2.BlowUp();
            }

            grenadesOut = false;
        }
    }
    void NewGrenadeGunWeapon()
    {
        if (grenadeGunCoolDown > 0)
        {
            grenadeGunCoolDown -= Time.deltaTime;
        }
        else
        {
            grenadeGunCoolDown = 0;
        }

        if (playerInput.GetAxis(shootInput) > 0.1f)
        {
            if (grenadeGunCoolDown == 0)
            {
                if (playerHealthScript.shotHealthCount > grenadeGunCost || playerHealthScript.overchargeActive == true)
                {
                    FireGrenadeGun();
                }
            }
        }
    }
    void FireGrenadeGun()
    {
        playerHealthScript.Damage(grenadeGunCost, true);
        grenadesOut = true;
        GrenadeGun1();
        grenadeGunCoolDown = grenadeGunCoolDownTime;
        /*
        yield return missileFireWait;
        GrenadeGun2();
        grenadeGunCoolDown = grenadeGunCoolDownTime;
        */

    }
    void GrenadeGun1()
    {
        //Play fx
        grenadeGunFx.Play();
        //audio

        //Create shotInstance
        grenadeCopy1 = Instantiate(grenade, grenadeGunStart.transform.position, Quaternion.identity) as GameObject;
        //Grab shotScript
        grenadeScript1 = grenadeCopy1.GetComponent<Grenade>();
        //set teamcount
        grenadeScript1.teamCount = teamCount;
        //set color
        grenadeScript1.mySprite.color = teamColors[teamCount];
        grenadeScript1.myParticles.startColor = teamColors[teamCount];
        //Color
        //set shot color
        //set particle system color
        //Grab and set Rotation
        angleDir = playerRotateScript.MyAngle();
        //Debug.Log("SHOT angleDir: " + angleDir);
        grenadeCopy1.transform.eulerAngles = new Vector3(transform.eulerAngles.x, transform.eulerAngles.y, angleDir);
        //Get Rigidbody, add force.
        rb2d = grenadeCopy1.GetComponent<Rigidbody2D>();
        straight = grenadeGunEnd.transform.position - grenadeGunStart.transform.position;
        rb2d.AddForce(straight * grenadeGunShotSpeed);
        weaponFired = true;
    }
    void GrenadeGun2()
    {
        //Play fx
        grenadeGunFx.Play();
        //audio

        //Create shotInstance
        grenadeCopy2 = Instantiate(grenade, grenadeGunStart.transform.position, Quaternion.identity) as GameObject;
        //Grab shotScript
        grenadeScript2 = grenadeCopy2.GetComponent<Grenade>();
        //set teamcount
        grenadeScript2.teamCount = teamCount;
        //set color
        grenadeScript2.mySprite.color = teamColors[teamCount];
        grenadeScript2.myParticles.startColor = teamColors[teamCount];
        //Color
        //set shot color
        //set particle system color
        //Grab and set Rotation
        angleDir = playerRotateScript.MyAngle();
        //Debug.Log("SHOT angleDir: " + angleDir);
        grenadeCopy2.transform.eulerAngles = new Vector3(transform.eulerAngles.x, transform.eulerAngles.y, angleDir);
        //Get Rigidbody, add force.
        rb2d = grenadeCopy2.GetComponent<Rigidbody2D>();
        straight = grenadeGunEnd.transform.position - grenadeGunStart.transform.position;
        rb2d.AddForce(straight * grenadeGunShotSpeed);
        weaponFired = true;
    }

    void PlasmaThrowerWeapon()
    {
        if (plasmaFireCoolDown > 0)
        {
            plasmaFireCoolDown -= Time.deltaTime;
        }
        else
        {
            plasmaFireCoolDown = 0;
        }
        if (playerInput.GetAxis(shootInput) > 0.1 && plasmaFireCoolDown == 0)
        {
            if (playerHealthScript.shotHealthCount > plasmaThrowerCost || playerHealthScript.overchargeActive)
            {
                StartCoroutine(ThrowPlasma());
            }
        }

        if (playerInput.GetAxis(shootInput) > 0.1 && playerHealthScript.shotHealthCount > plasmaThrowerCost || playerInput.GetAxis(shootInput) > 0.1 && playerHealthScript.overchargeActive)
        {
            if (plasmaEmitting)
            {
                plasmaThrowerParticles.Play();
                plasmaThrowerParticles2.Play();
                plasmaEmitting = false;
            }
        }
        else
        {
            plasmaThrowerParticles.Stop();
            plasmaThrowerParticles2.Stop();
            plasmaEmitting = true;
        }
    }
    IEnumerator ThrowPlasma()
    {
        plasmaFireCoolDown = plasmaFireCoolDownTime;
        playerHealthScript.Damage(plasmaThrowerCost, true);

        PlasmaThrower();
        yield return plasmaFireWait;
        PlasmaThrower();
        yield return plasmaFireWait;
        PlasmaThrower();
        yield return plasmaFireWait;
        PlasmaThrower();
    }
    void PlasmaThrower()
    {
        //audio
        //Create shotInstance
        shotInstance = Instantiate(plasmaChunk, plasmaThrowerStart.transform.position, Quaternion.identity) as GameObject;
        //Grab shotScript
        plasmaScript = shotInstance.GetComponent<Plasma>();
        //set teamcount
        plasmaScript.teamCount = teamCount;
        plasmaScript.mySprite.color = teamColors[teamCount];
        //set particle system color
        //Grab and set Rotation
        angleDir = playerRotateScript.MyAngle();
        //Debug.Log("SHOT angleDir: " + angleDir);
        shotInstance.transform.eulerAngles = new Vector3(transform.eulerAngles.x, transform.eulerAngles.y, angleDir);
        //Get Rigidbody, add force.
        rb2d = shotInstance.GetComponent<Rigidbody2D>();

        //Make it a random circle of points to set the Vector2.
        throwPosition = Random.insideUnitCircle * plasmaSpread;
        finalPlasmaThrowPos = new Vector2(plasmaThrowPos.transform.position.x + throwPosition.x, plasmaThrowPos.transform.position.y + throwPosition.y);
        conversionVector2 = plasmaThrowerStart.transform.position;
        straight = finalPlasmaThrowPos - conversionVector2;
        rb2d.AddForce(straight * Random.Range(minPlasmaShotSpeed, maxPlasmaShotSpeed));
        weaponFired = true;
    }

    //Too Soon, maybe we'll see a hybrid weapon of the focus beam and charge cannon in the future. R.I.P.
    /*
    void ChargeCannonWeapon()
    {
        MonitorChargeCannon();

        if (chargeCannonCoolDown > 0)
        {
            chargeCannonCoolDown -= 1;
        }

        //If we get the button down, start charging the cannon
        if (Input.GetButtonDown(shootInput) && chargeCannonCoolDown < 1 && chargingCannon == false)
        {
            chargingCannon = true;
        }

        //If we get the button back up, stop charging the cannon, release shot.
        if (Input.GetButtonUp(shootInput) && chargingCannon == true)
        {
            chargingCannon = false;
            chargeCannonCoolDown = chargeCannonCoolDownTime;
            Debug.Log("Shot Fired| chargeTime: " + chargeTime);

            if (chargeTime < 0.5f)
            {
                Debug.Log("Fired Small");
                ChargeCannonShot(0);
            }
            else if (chargeTime >= 0.5f && chargeTime < 2.0f)
            {
                Debug.Log("Fired Medium");
                ChargeCannonShot(1);
            }
            else if (chargeTime >= 2.0f)
            {
                Debug.Log("Fired Large");
                ChargeCannonShot(2);
            }
        }
    }
    void ChargeCannonShot(int shotFired)
    {
        GameObject shotInstance;

        //First we have to subtract health amount.
        playerHealthScript.Damage(chargeShotCost[shotFired], true);
        
        //Then create the shot.
        shotInstance = Instantiate(chargeShotPrefab[shotFired], chargeCannonBarrelEnd.transform.position, Quaternion.identity) as GameObject;
        
        //Set teamCount
        chargeShotScript = shotInstance.GetComponent<ChargeCannonShot>();
        chargeShotScript.teamCount = teamCount;
        chargeShotScript.type = shotFired;

        //Color
        chargeShotScript.shotSprite.color = teamColors[teamCount];

        //Grab and set Rotation
        angleDir = playerRotateScript.myAngle();
        //Debug.Log("SHOT angleDir: " + angleDir);
        shotInstance.transform.eulerAngles = new Vector3(transform.eulerAngles.x, transform.eulerAngles.y, angleDir);

        //Get Rigidbody, add force.
        Rigidbody2D rb2d = shotInstance.GetComponent<Rigidbody2D>();
        Vector2 straight = chargeCannonBarrelEnd.transform.position - chargeCannonFirePoint.transform.position;

        //Use speed array to find the correct value
        rb2d.AddForce(straight * chargeShotSpeed[shotFired]);
    }
    void MonitorChargeCannon()
    {
        //If we are charging, keep track of how much time passes.
        if (chargingCannon)
        {
            chargeTime += Time.deltaTime;
            

        }
        else //Otherwise, set the timer to 0.
        {
            chargeTime = 0;
        }
    }
    */

    //-------------------------------------------------------------------------------

    /*
    //Here lies the old FocusBeam code R.I.P.
    void FocusBeamWeapon()
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
        if (Input.GetAxis(shootInput) > 0.1f && focusBeamDamageCoolDown == 0 && playerHealthScript.shotHealthCount > 1)
        {
            focusBeamDamageCoolDown = focusBeamDamageCoolDownTime;
            playerHealthScript.Damage(focusBeamCost, true);
        }
        //If we are using the attack here is the attack.
        //Happens whenever the button is held down, reguardless of cooldown.
        if (Input.GetAxis(shootInput) > 0.1f && playerHealthScript.shotHealthCount > 1)
        {
            Debug.Log("Firing focusBeam");
            //we are using it
            focusBeamInUse = true;
            //this is here to limit the amount of hits to a 'per second' sort of thing. So if we hit..
            if (hitCoolDown == 0)
            {
                hitCoolDown = hitCoolDownTime;
                FocusBeamEffect(true);//Activate focus beam effect
            }
            else //Take from health
            {
                FocusBeamEffect(false);//Don't activate effect
            }
        }
        else //Else we're not using it
        {
            focusBeamInUse = false;
        }

        //ImageShowing
        if (focusBeamInUse == true)
        {
            focusBeamRenderer.enabled = true;
        }
        else
        {
            focusBeamRenderer.enabled = false;
        }
    }
    void FocusBeamEffect(bool hit)
    {
        focusBeamHitSomething = Physics2D.Linecast(focusBeamBarrelEnd.transform.position, focusBeamEnd.transform.position, toBeEffectedLayer);
        if (focusBeamHitSomething)
        {
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
                focusBeamRenderer.SetPosition(0, focusBeamBarrelEnd.transform.position);
                focusBeamRenderer.SetPosition(1, focusBeamHitSomething.point);
                enemyHealthScript = focusBeamHitSomething.transform.GetComponent<PlayerHealth>();

                if (enemyHealthScript.teamCount != teamCount)
                {
                    if (hit == true)
                    {
                        enemyHealthScript.Damage(focusBeamEnemyDamageCurrent, false);
                        Instantiate(focusBeamHitParticles, focusBeamHitSomething.point, Quaternion.identity);
                        Instantiate(focusBeamSmokeParticles, focusBeamHitSomething.point, Quaternion.identity);
                    }
                    else
                    {
                        //no hit
                    }
                }
            }
            else if (focusBeamHitSomething.transform.tag == "Asteroid")
            {
                focusBeamRenderer.SetPosition(0, focusBeamBarrelEnd.transform.position);
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
                focusBeamRenderer.SetPosition(0, focusBeamBarrelEnd.transform.position);
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
            focusBeamRenderer.SetPosition(0, focusBeamBarrelEnd.transform.position);
            focusBeamRenderer.SetPosition(1, focusBeamEnd.transform.position);
        }
    }
    */
}
