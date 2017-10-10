using Rewired;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour {

    private Player rWPlayer1;
    private Player rWPlayer2;
    private Player rWPlayer3;
    private Player rWPlayer4;

    public GameObject loadingScreen;

    public GameObject loadingScreenObj;

    public SelectScreenCamera selectScreenCameraScript;
    private bool mapScreenActive;

    public GameObject aiPref;
    public Vector2 aiPrefLocation;

    public int howManyPlayers;
    bool killSwitch;

    public Color blueTeamColor;
    public Color orangeTeamColor;
    public Color greenTeamColor;
    public Color pinkTeamColor;

    public int selectScreenInt;
    public int titleScreenInt;

    //EndGame
    public GameObject deadPlayer;

    public GameObject explosiveFinish;
    private GameObject explosiveFinishCopy;
    private ParticleSystem endGameParticles;
    private Text endGameText;
    //PlayerStatus
    public bool player1Dead;
    public bool player2Dead;
    public bool player3Dead;
    public bool player4Dead;

    private int team1PlayerCount;
    private int team2PlayerCount;
    private int team3PlayerCount;
    private int team4PlayerCount;

    //Game Start
    public int selectedMap; //The map selected for the match.

    //Start Space Junk System vars
    //Asteroids
    private GameObject[] asteroidArray = new GameObject[4];
    public GameObject asteroid1;
    public GameObject asteroid2;
    public GameObject asteroid3;
    public GameObject asteroid4;

    private GameObject[] asteroidMineralArray = new GameObject[4];
    public GameObject asteroidMinerals1;
    public GameObject asteroidMinerals2;
    public GameObject asteroidMinerals3;
    public GameObject asteroidMinerals4;

    public GameObject[] asteroidSpeedBuffMineral;
    public GameObject[] asteroidShieldMineral;

    private GameObject[] ibeamArray = new GameObject[3];
    public GameObject ibeamSmall;
    public GameObject ibeamMedium;
    public GameObject ibeamLarge;
    public float ibeamSpeedMultiplier;

    public GameObject capsule;
    public GameObject mineralFragment;

    public float spawnRadius;
    public float spaceJunkMoveToRadius;
    public int hazardCount;
    public float spawnWait;
    public float startWait;
    public float waveWait;
    //End space junk system vars

    //PlayerStuff
    //Player1
    public GameObject player1;
    public GameObject player1Copy;
    private PlayerMovement player1MoveScript;
    private PlayerShoot player1ShootScript;
    public PlayerHealth player1HealthScript;
    private PlayerRotation player1RotateScript;
    private PlayerAbility player1AbilityScript;
    private SpriteManager player1SpriteManager;
    public Vector2 player1StartPos;
    private int p1OffenseSys;
    private int p1DefenseSys;
    private int player1TeamCount;

    //Player2
    public GameObject player2;
    public GameObject player2Copy;
    private PlayerMovement player2MoveScript;
    private PlayerShoot player2ShootScript;
    public PlayerHealth player2HealthScript;
    private PlayerRotation player2RotateScript;
    private PlayerAbility player2AbilityScript;
    private SpriteManager player2SpriteManager;
    public Vector2 player2StartPos;
    private int p2OffenseSys;
    private int p2DefenseSys;
    private int player2TeamCount;

    //Player3
    public GameObject player3;
    public GameObject player3Copy;
    private PlayerMovement player3MoveScript;
    private PlayerShoot player3ShootScript;
    public PlayerHealth player3HealthScript;
    private PlayerRotation player3RotateScript;
    private PlayerAbility player3AbilityScript;
    private SpriteManager player3SpriteManager;
    public Vector2 player3StartPos;
    private int p3OffenseSys;
    private int p3DefenseSys;
    private int player3TeamCount;

    //Player4
    public GameObject player4;
    public GameObject player4Copy;
    private PlayerMovement player4MoveScript;
    private PlayerShoot player4ShootScript;
    public PlayerHealth player4HealthScript;
    private PlayerRotation player4RotateScript;
    private PlayerAbility player4AbilityScript;
    private SpriteManager player4SpriteManager;
    public Vector2 player4StartPos;
    private int p4OffenseSys;
    private int p4DefenseSys;
    private int player4TeamCount;

    private Asteroid asteroidScript;

    public GameObject restartButton;
    public Vector2 restartButtonPos;
    private RestartButton restartScript;
    public bool gameEnd;

    GameObject restartButtonInstance;
    public int killCount = 0;

    //Object for camera follow
    public Camera mainCamera;
    public float cameraSmooth;

    public bool classSelectScreen;

    //private bool gameOver;

    private GameObject winner;

    //SwitchTeamBooleans
    private bool player1SwitchTeam;
    private bool player2SwitchTeam;
    private bool player3SwitchTeam;
    private bool player4SwitchTeam;

    //UI stuff
    public GameObject p1_UI;
    public GameObject p2_UI;
    public GameObject p3_UI;
    public GameObject p4_UI;
    public Transform UICanvas;

    //UI Meter/Number References
    public UIMaster p1_UI_Info;
    public UIMaster p2_UI_Info;
    public UIMaster p3_UI_Info;
    public UIMaster p4_UI_Info;

    //Private copies
    private GameObject p1uiCopy;
    private GameObject p2uiCopy;
    private GameObject p3uiCopy;
    private GameObject p4uiCopy;

    //rect Transforms
    private RectTransform p1Transform;
    private RectTransform p2Transform;
    private RectTransform p3Transform;
    private RectTransform p4Transform;

    //UI CanvasGroups
    private CanvasGroup p1CanvasGroup;
    private CanvasGroup p2CanvasGroup;
    private CanvasGroup p3CanvasGroup;
    private CanvasGroup p4CanvasGroup;


    //Alpha Values for UI
    private float higherAlpha = 0.7f;
    private float lowerAlpha = 0.3f;

    public float smoothing;
    //4 different booleans for each fadable UI segment
    public bool p1Trans;
    public bool p2Trans;
    public bool p3Trans;
    public bool p4Trans;

    private float transparentSmoothing = 5f;

    //AI
    private MobSpawner spawner1;
    private GameObject mobSpawnerCopy1;
    private MobSpawner spawner2;
    private GameObject mobSpawnerCopy2;
    private MobSpawner spawner3;
    private GameObject mobSpawnerCopy3;
    private MobSpawner spawner4;
    private GameObject mobSpawnerCopy4;
    public GameObject mobSpawner;
    public Vector2 mobSpawnPos;

    public Transform[] wayPoints = new Transform[4];


    public bool gamePaused;

    //Environment Effects
    public int fireDPS;
    public int poisonDPS;

    //GameStart
    private WaitForSeconds endGameWait = new WaitForSeconds(4f);
    private WaitForSeconds gameStartWait = new WaitForSeconds(1.5f);
    private WaitForSeconds fourPWait = new WaitForSeconds(1.125f);
    private WaitForSeconds teleportWait = new WaitForSeconds(0.5f);
    public GameObject startGameObj;
    private GameObject startObjCopy;
    private Text startObjText;
    private Shake shakeScript;

    public GameObject teleParticles;
    public GameObject teleParticles2;
    private GameObject teleParticlesCopy;
    private GameObject teleParticles2Copy;
    private ParticleSystem teleActualParticles;
    private ParticleSystem teleActualParticles2;

    private float targetSizePlayer = 0.5f;

    private bool intro;
    private Vector3 targetPosition;
    private int targetSize;
    public int zoomedInCameraSize;

    private Vector3 mainCameraPosition = new Vector3(0, 0, -10f);
    private int zoomedOutCameraSize = 25;

    public SpeakerControl speakerControlScript;
    public SlowSpin mapSpinScript;

    public GameObject tempMusic;
    private AudioSource gameTrack;
    private AudioSource titleTrack;
    private GameObject musicTitleGO;

    public float volumeLevel;

    public StartDanceFloorControl startFloorScript;

    public string[] names;

    public AudioClip ds2PlainMapMusic;
    public AudioClip ds2StartSound;
    public AudioClip ds3IceMapMusic;
    public AudioClip ds3StartSound;
    public AudioClip ds4PoisonMapMusic;
    public AudioClip ds4StartSound;
    public AudioClip ds5StaticMapMusic;
    public AudioClip ds5StartSound;
    public AudioClip ds6FireMapMusic;
    public AudioClip ds6StartSound;

    public AudioSource countdownTrack;

    private AudioClip selectedTrack;
    private AudioClip countdownClip;

    //TitleScreenNumHolder
    public GameObject numPlayerHolder;
    private NumHolder numHolder;

    //PlayerActive
    public bool player1Active;
    public bool player2Active;
    public bool player3Active;
    public bool player4Active;

    //PlayerSpawnPositions
    public GameObject player1SpawnPosition;
    public GameObject player2SpawnPosition;
    public GameObject player3SpawnPosition;
    public GameObject player4SpawnPosition;

    private Vector2 selectScreenP1SpawnPos;
    private Vector2 selectScreenP2SpawnPos;
    private Vector2 selectScreenP3SpawnPos;
    private Vector2 selectScreenP4SpawnPos;

    //slideFollowScript
    public SlideFollow slideFollowScriptP1;
    public SlideFollow slideFollowScriptP2;
    public SlideFollow slideFollowScriptP3;
    public SlideFollow slideFollowScriptP4;

    //DpadSetup For Weapon/Ability/Team
    //Weapon
    public bool p1InWeaponZone;
    public bool p2InWeaponZone;
    public bool p3InWeaponZone;
    public bool p4InWeaponZone;
    //Ability
    public bool p1InAbilityZone;
    public bool p2InAbilityZone;
    public bool p3InAbilityZone;
    public bool p4InAbilityZone;
    //Team
    public bool p1InTeamZone;
    public bool p2InTeamZone;
    public bool p3InTeamZone;
    public bool p4InTeamZone;

    //StartZone
    public bool p1InStartZone;
    public bool p2InStartZone;
    public bool p3InStartZone;
    public bool p4InStartZone;

    //Carousel for Weapons
    public int p1SelectedWeapon;
    public int p2SelectedWeapon;
    public int p3SelectedWeapon;
    public int p4SelectedWeapon;

    //Carousel for Abilities
    public int p1SelectedAbility;
    public int p2SelectedAbility;
    public int p3SelectedAbility;
    public int p4SelectedAbility;

    public GameObject startBanner;
    private GameObject startBannerCopy;
    private bool startBannerInstantiated;
    public GameObject startBannerPosition;

    private bool extraKillSwitch;

    public MapSelectManager mapSelectManager;

    public GameManagerActivate gameManagerActivate;

    //SelectScreenTeamManager
    private int sSTeam0;
    private int sSTeam1;
    private int sSTeam2;
    private int sSTeam3;


    //ExtraFX
    public GameObject screenFlash;

    //ScreenShake
    private bool screenShakeActive;
    private Vector3 shakePos;
    public float shakeAmount;

    private Vector3 zeroPos = new Vector3(0f, 0f, -10f);

    public bool otherThing;

    void Awake()
    {
        //Rewired
        rWPlayer1 = ReInput.players.GetPlayer(0);
        rWPlayer2 = ReInput.players.GetPlayer(1);
        rWPlayer3 = ReInput.players.GetPlayer(2);
        rWPlayer4 = ReInput.players.GetPlayer(3);

        selectScreenP1SpawnPos = player1SpawnPosition.transform.position;
        selectScreenP2SpawnPos = player2SpawnPosition.transform.position;
        selectScreenP3SpawnPos = player3SpawnPosition.transform.position;
        selectScreenP4SpawnPos = player4SpawnPosition.transform.position;

        gameTrack = GetComponent<AudioSource>();

        //startFloorScript.gameManager = this;
        extraKillSwitch = true;

        if (numHolder != null)
        {
            //Set HowManyPlayers
            numPlayerHolder = GameObject.FindGameObjectWithTag("NumHolder");
            numHolder = numPlayerHolder.GetComponent<NumHolder>();
            howManyPlayers = numHolder.numConnectedPlayers;
            Destroy(numPlayerHolder);
        }
    }

    void Start()
    {
        Debug.Log("StartCalled");
        musicTitleGO = GameObject.FindGameObjectWithTag("Music");
        if (musicTitleGO != null)
        {
            titleTrack = musicTitleGO.GetComponent<AudioSource>();
        }
        else //if it is null
        {
            musicTitleGO = Instantiate(tempMusic, transform.position, Quaternion.identity) as GameObject;
            titleTrack = tempMusic.GetComponent<AudioSource>();
        }

        selectedMap = 3;

        DontDestroyOnLoad(transform.gameObject);
        //gameEnd = false;
        killSwitch = true;
        classSelectScreen = true;
        gamePaused = false;

        if (howManyPlayers > 0)
        {
            player1Active = true;

            SpawnPlayer1();
            if (slideFollowScriptP1 != null)
            {
                slideFollowScriptP1.PlayerEntered();
            }
            SetPanelColors(1, player1TeamCount);
        }

        if (howManyPlayers > 1)
        {
            player2Active = true;

            SpawnPlayer2();
            if (slideFollowScriptP2 != null)
            {
                slideFollowScriptP2.PlayerEntered();
            }
            SetPanelColors(2, player2TeamCount);
        }

        if (howManyPlayers > 2)
        {
            player3Active = true;

            SpawnPlayer3();
            if (slideFollowScriptP3 != null)
            {
                slideFollowScriptP3.PlayerEntered();
            }
            SetPanelColors(3, player3TeamCount);
        }

        if (howManyPlayers > 3)
        {
            player4Active = true;

            SpawnPlayer4();
            if (slideFollowScriptP4 != null)
            {
                slideFollowScriptP4.PlayerEntered();
            }
            SetPanelColors(4, player4TeamCount);
        }

        StartCoroutine(ToggleMapScreenActive(false));
        /*
        StartCoroutine(SetUpUI());
        
        //Player1
        //Spawn Players
        player1Copy = Instantiate(player1, selectScreenP1SpawnPos, Quaternion.identity) as GameObject;
        
        player1Active = true;

        //Get Component References
        player1MoveScript = player1Copy.GetComponent<PlayerMovement>();
        player1ShootScript = player1Copy.GetComponent<PlayerShoot>();
        player1AbilityScript = player1Copy.GetComponent<PlayerAbility>();
        player1HealthScript = player1Copy.GetComponent<PlayerHealth>();
        player1SpriteManager = player1Copy.GetComponent<SpriteManager>();

        //Set Component References
        player1TeamCount = 0;

        player1MoveScript.gameManagerScript = this;
        player1MoveScript.classSelect = classSelectScreen;

        player1ShootScript.gameManagerScript = this;

        player1HealthScript.titleScreen = false;
        player1HealthScript.gameManagerScript = this;
        player1HealthScript.PickUp(100);

        player1SpriteManager.numPlayer = 1;
        

        SetTeamCount(player1SpriteManager, player1TeamCount);
        SetOffensiveSystem(0, 1);
        SetDefensiveSystem(1, 1);
        
        //Player2
        player2Copy = Instantiate(player2, selectScreenP2SpawnPos, Quaternion.identity) as GameObject;
        
        player2Active = true;

        player2MoveScript = player2Copy.GetComponent<PlayerMovement>();
        player2TeamCount = 1;
        player2MoveScript.gameManagerScript = this;
        player2ShootScript = player2Copy.GetComponent<PlayerShoot>();
        player2ShootScript.gameManagerScript = this;
        player2MoveScript.classSelect = classSelectScreen;
        player2AbilityScript = player2Copy.GetComponent<PlayerAbility>();
        player2HealthScript = player2Copy.GetComponent<PlayerHealth>();
        player2HealthScript.titleScreen = false;
        player2HealthScript.gameManagerScript = this;
        player2HealthScript.PickUp(100);
        

        player2SpriteManager = player2Copy.GetComponent<SpriteManager>();
        player2SpriteManager.numPlayer = 2;
        

        SetTeamCount(player2SpriteManager, player2TeamCount);
        SetOffensiveSystem(1, 2);
        SetDefensiveSystem(2, 2);
        
        if (howManyPlayers > 2)
        {
            player3Copy = Instantiate(player3, selectScreenP3SpawnPos, Quaternion.identity) as GameObject;
            
            player3Active = true;

            player3MoveScript = player3Copy.GetComponent<PlayerMovement>();
            player3TeamCount = 2;
            player3MoveScript.gameManagerScript = this;
            player3ShootScript = player3Copy.GetComponent<PlayerShoot>();
            player3ShootScript.gameManagerScript = this;
            player3MoveScript.classSelect = classSelectScreen;
            player3AbilityScript = player3Copy.GetComponent<PlayerAbility>();
            
            player3HealthScript = player3Copy.GetComponent<PlayerHealth>();
            player3HealthScript.titleScreen = false;
            player3HealthScript.gameManagerScript = this;
            player3HealthScript.PickUp(100);

            
            player3SpriteManager = player3Copy.GetComponent<SpriteManager>();
            player3SpriteManager.numPlayer = 3;
            

            SetTeamCount(player3SpriteManager, 2);
            SetOffensiveSystem(2, 3);
            SetDefensiveSystem(3, 3);

            if (howManyPlayers > 3)
            {
                player4Copy = Instantiate(player4, selectScreenP4SpawnPos, Quaternion.identity) as GameObject;
                
                player4Active = true;

                player4MoveScript = player4Copy.GetComponent<PlayerMovement>();
                player4TeamCount = 3;
                player4MoveScript.gameManagerScript = this;
                player4ShootScript = player4Copy.GetComponent<PlayerShoot>();
                player4ShootScript.gameManagerScript = this;
                player4MoveScript.classSelect = classSelectScreen;
                player4AbilityScript = player4Copy.GetComponent<PlayerAbility>();
                player4HealthScript = player4Copy.GetComponent<PlayerHealth>();
                player4HealthScript.titleScreen = false;
                player4HealthScript.gameManagerScript = this;
                player4HealthScript.PickUp(100);
                
                player4SpriteManager = player4Copy.GetComponent<SpriteManager>();
                player4SpriteManager.numPlayer = 4;
                

                SetTeamCount(player4SpriteManager, 3);
                SetOffensiveSystem(0, 4);
                SetDefensiveSystem(1, 4);
            }
        }
        */
    }

    void MonitorShakeScreen()
    {
        if (screenShakeActive)
        {
            shakePos = Random.insideUnitCircle * shakeAmount;
            Vector3 newShakePos = new Vector3(mainCamera.transform.position.x + shakePos.x, mainCamera.transform.position.y + shakePos.y, -10f);
            mainCamera.transform.position = Vector3.Lerp(zeroPos, newShakePos, 2f * Time.deltaTime);
        }
        else if (!intro && !classSelectScreen)
        {
            mainCamera.transform.position = zeroPos;
        }
    }

    public void HitShake()
    {
        StartCoroutine(Shake());
    }

    IEnumerator Shake()
    {
        screenShakeActive = true;
        yield return new WaitForSeconds(0.3f);
        screenShakeActive = false;
    }

    public void RestartGame()
    {
        //Debug.Log("restart instantiated");
        killSwitch = true;
        restartButtonInstance = Instantiate(restartButton, restartButtonPos, Quaternion.identity) as GameObject;
        restartScript = restartButtonInstance.GetComponent<RestartButton>();
        restartScript.gameManagerScript = this;
    }

    public void SelectMap(int mapSelected)
    {
        selectedMap = mapSelected;
    }

    public void SelectScreenStart()
    {
        StartCoroutine(PressedStart());
    }

    IEnumerator PressedStart()
    {

        if (selectedMap > 0)
        {
            StartCoroutine(LerpMusicDown(titleTrack));
            switch (howManyPlayers)
            {
                case (2):
                    //Teleport player1
                    StartCoroutine(TeleportPlayer(player1Copy, player1SpriteManager));
                    yield return teleportWait;
                    //Teleport player2
                    StartCoroutine(TeleportPlayer(player2Copy, player2SpriteManager));
                    break;
                case (3):
                    //Teleport player1
                    StartCoroutine(TeleportPlayer(player1Copy, player1SpriteManager));
                    yield return teleportWait;
                    //Teleport player2
                    StartCoroutine(TeleportPlayer(player2Copy, player2SpriteManager));
                    yield return teleportWait;
                    //Teleport player3
                    StartCoroutine(TeleportPlayer(player3Copy, player3SpriteManager));
                    break;
                case (4):
                    //Teleport player1
                    StartCoroutine(TeleportPlayer(player1Copy, player1SpriteManager));
                    yield return teleportWait;
                    //Teleport player2
                    StartCoroutine(TeleportPlayer(player2Copy, player2SpriteManager));
                    yield return teleportWait;
                    //Teleport player3
                    StartCoroutine(TeleportPlayer(player3Copy, player3SpriteManager));
                    yield return teleportWait;
                    //Teleport player4
                    StartCoroutine(TeleportPlayer(player4Copy, player4SpriteManager));
                    break;
            }
            //FadeMusicOut
            yield return gameStartWait;
            Destroy(musicTitleGO);
            SceneManager.LoadScene(selectedMap);
        }
        else
        {
            //if the map isn't selected, don't do anything
            Debug.Log("Map Not Selected.");
        }
    }

    void SpawnPlayer1()
    {
        player1Active = true;

        player1Copy = Instantiate(player1, selectScreenP1SpawnPos, Quaternion.identity) as GameObject;
        //Ref
        player1MoveScript = player1Copy.GetComponent<PlayerMovement>();
        player1ShootScript = player1Copy.GetComponent<PlayerShoot>();
        player1AbilityScript = player1Copy.GetComponent<PlayerAbility>();
        player1HealthScript = player1Copy.GetComponent<PlayerHealth>();
        player1SpriteManager = player1Copy.GetComponent<SpriteManager>();

        //Set Component References
        player1TeamCount = 0;
        player1MoveScript.classSelect = true;
        player1HealthScript.gameManagerScript = this;//Set up
        player1HealthScript.titleScreen = false;
        player1HealthScript.PickUp(100);
        player1SpriteManager.numPlayer = 1;

        SetTeamCount(player1SpriteManager, player1TeamCount);
        SetOffensiveSystem(0, 1);
        SetDefensiveSystem(1, 1);

        p1SelectedWeapon = 0;
        p1SelectedAbility = 1;

        player1SpriteManager.SpriteSet();
        player1SpriteManager.ShowPlayer();
        StartCoroutine(Player1UI());
    }
    IEnumerator Player1UI()
    {
        //Debug.Log("UISetUpRunning");
        //Spawn UI elements
        p1uiCopy = Instantiate(p1_UI, UICanvas.position, Quaternion.identity) as GameObject;

        //Get transform
        p1Transform = p1uiCopy.GetComponent<RectTransform>();

        //Set parent to canvas
        p1uiCopy.transform.SetParent(UICanvas);

        //Set Position
        p1Transform.position = new Vector2(0, 1050);//or 50/479

        //Get Info
        p1_UI_Info = p1uiCopy.GetComponent<UIMaster>();

        //CanvasGroup
        p1CanvasGroup = p1_UI_Info.GetComponentInChildren<CanvasGroup>();

        yield return new WaitForSeconds(0.05f);

        //P1
        //UI references
        //Health
        //player1HealthScript.healthBar = p1_UI_Info.healthBar;
        player1HealthScript.healthNum = p1_UI_Info.healthNum;

        //Shields
        player1HealthScript.overchargeBar = p1_UI_Info.shieldsBar;

        //SpeedBuff
        player1HealthScript.speedBar = p1_UI_Info.speedBar;

        //to Color
        //player1SpriteManager.healthBar = p1_UI_Info.healthBar;
        player1SpriteManager.playerNum = p1_UI_Info.playerNum;
        player1SpriteManager.healthText = p1_UI_Info.healthNum;
        player1SpriteManager.abilityMeter = p1_UI_Info.abilityMeter;
        player1SpriteManager.glow = p1_UI_Info.glow;
        player1SpriteManager.uiSpinner = p1_UI_Info.playerSpinner;

        player1SpriteManager.SpriteSet();

        //AbilityMeter Tracking
        player1AbilityScript.uiAbilityMeter = p1_UI_Info.abilityMeter;

        //yield return new WaitForSeconds(0.05f);

        p1CanvasGroup.alpha = 0;
    }

    void SpawnPlayer2()
    {
        player2Active = true;

        //Player2
        player2Copy = Instantiate(player2, selectScreenP2SpawnPos, Quaternion.identity) as GameObject;
        player2MoveScript = player2Copy.GetComponent<PlayerMovement>();
        player2TeamCount = 1;
        player2ShootScript = player2Copy.GetComponent<PlayerShoot>();
        player2MoveScript.classSelect = true;
        player2AbilityScript = player2Copy.GetComponent<PlayerAbility>();
        player2HealthScript = player2Copy.GetComponent<PlayerHealth>();
        player2HealthScript.gameManagerScript = this;
        player2HealthScript.titleScreen = false;
        player2HealthScript.PickUp(100);


        player2SpriteManager = player2Copy.GetComponent<SpriteManager>();
        player2SpriteManager.numPlayer = 2;
        player2SpriteManager.ShowPlayer();


        SetTeamCount(player2SpriteManager, player2TeamCount);
        SetOffensiveSystem(1, 2);
        SetDefensiveSystem(2, 2);

        p2SelectedWeapon = 1;
        p2SelectedAbility = 2;

        player2SpriteManager.SpriteSet();
        StartCoroutine(Player2UI());
    }
    IEnumerator Player2UI()
    {
        //Spawn UI elements
        p2uiCopy = Instantiate(p2_UI, UICanvas.position, Quaternion.identity) as GameObject;

        //Get transform
        p2Transform = p2uiCopy.GetComponent<RectTransform>();

        //Set parent to canvas
        p2uiCopy.transform.SetParent(UICanvas);

        //Set Position
        p2Transform.position = new Vector2(1680, 1050);// or 736/479 1570

        //Get Info
        p2_UI_Info = p2uiCopy.GetComponent<UIMaster>();

        //CanvasGroup
        p2CanvasGroup = p2_UI_Info.GetComponentInChildren<CanvasGroup>();

        yield return new WaitForSeconds(0.05f);

        //P2
        //UI references
        //Health
        //player2HealthScript.healthBar = p2_UI_Info.healthBar;
        player2HealthScript.healthNum = p2_UI_Info.healthNum;

        //Shields
        player2HealthScript.overchargeBar = p2_UI_Info.shieldsBar;

        //SpeedBuff
        player2HealthScript.speedBar = p2_UI_Info.speedBar;

        //to Color
        //player2SpriteManager.healthBar = p2_UI_Info.healthBar;
        player2SpriteManager.playerNum = p2_UI_Info.playerNum;
        player2SpriteManager.healthText = p2_UI_Info.healthNum;
        player2SpriteManager.abilityMeter = p2_UI_Info.abilityMeter;
        player2SpriteManager.glow = p2_UI_Info.glow;
        player2SpriteManager.uiSpinner = p2_UI_Info.playerSpinner;

        player2SpriteManager.SpriteSet();

        //AbilityMeter Tracking
        player2AbilityScript.uiAbilityMeter = p2_UI_Info.abilityMeter;

        //yield return new WaitForSeconds(0.05f);

        p2CanvasGroup.alpha = 0;
    }

    void SpawnPlayer3()
    {

        player3Copy = Instantiate(player3, selectScreenP3SpawnPos, Quaternion.identity) as GameObject;
        player3MoveScript = player3Copy.GetComponent<PlayerMovement>();
        player3TeamCount = 2;
        player3ShootScript = player3Copy.GetComponent<PlayerShoot>();
        player3MoveScript.classSelect = true;
        player3AbilityScript = player3Copy.GetComponent<PlayerAbility>();

        player3HealthScript = player3Copy.GetComponent<PlayerHealth>();
        player3HealthScript.gameManagerScript = this;
        player3HealthScript.titleScreen = false;
        player3HealthScript.PickUp(100);


        player3SpriteManager = player3Copy.GetComponent<SpriteManager>();
        player3SpriteManager.numPlayer = 3;
        player3SpriteManager.ShowPlayer();


        SetTeamCount(player3SpriteManager, 2);
        SetOffensiveSystem(2, 3);
        SetDefensiveSystem(3, 3);

        p3SelectedWeapon = 2;
        p3SelectedAbility = 3;

        player3SpriteManager.SpriteSet();
        StartCoroutine(Player3UI());
    }
    IEnumerator Player3UI()
    {
        p3uiCopy = Instantiate(p3_UI, UICanvas.position, Quaternion.identity) as GameObject;

        p3Transform = p3uiCopy.GetComponent<RectTransform>();

        p3uiCopy.transform.SetParent(UICanvas);

        p3Transform.position = new Vector2(0, 10);

        p3_UI_Info = p3uiCopy.GetComponent<UIMaster>();

        yield return new WaitForSeconds(0.05f);
        //CanvasGroup
        p3CanvasGroup = p3_UI_Info.GetComponentInChildren<CanvasGroup>();



        //P3
        //UI references
        //Health
        //player3HealthScript.healthBar = p3_UI_Info.healthBar;
        player3HealthScript.healthNum = p3_UI_Info.healthNum;

        //Shields
        player3HealthScript.overchargeBar = p3_UI_Info.shieldsBar;

        //SpeedBuff
        player3HealthScript.speedBar = p3_UI_Info.speedBar;

        //to Color
        //player3SpriteManager.healthBar = p3_UI_Info.healthBar;
        player3SpriteManager.playerNum = p3_UI_Info.playerNum;
        player3SpriteManager.healthText = p3_UI_Info.healthNum;
        player3SpriteManager.abilityMeter = p3_UI_Info.abilityMeter;
        player3SpriteManager.glow = p3_UI_Info.glow;
        player3SpriteManager.uiSpinner = p3_UI_Info.playerSpinner;

        player3SpriteManager.SpriteSet();

        //AbilityMeter Tracking
        player3AbilityScript.uiAbilityMeter = p3_UI_Info.abilityMeter;

        //yield return new WaitForSeconds(0.05f);

        p3CanvasGroup.alpha = 0;
    }

    void SpawnPlayer4()
    {

        player4Copy = Instantiate(player4, selectScreenP4SpawnPos, Quaternion.identity) as GameObject;
        player4MoveScript = player4Copy.GetComponent<PlayerMovement>();
        player4TeamCount = 3;
        player4ShootScript = player4Copy.GetComponent<PlayerShoot>();
        player4MoveScript.classSelect = true;
        player4AbilityScript = player4Copy.GetComponent<PlayerAbility>();
        player4HealthScript = player4Copy.GetComponent<PlayerHealth>();
        player4HealthScript.gameManagerScript = this;
        player4HealthScript.titleScreen = false;
        player4HealthScript.PickUp(100);

        player4SpriteManager = player4Copy.GetComponent<SpriteManager>();
        player4SpriteManager.numPlayer = 4;
        player4SpriteManager.ShowPlayer();


        SetTeamCount(player4SpriteManager, 3);
        SetOffensiveSystem(3, 4);
        SetDefensiveSystem(5, 4);

        p4SelectedWeapon = 3;
        p4SelectedAbility = 5;

        player4SpriteManager.SpriteSet();
        StartCoroutine(Player4UI());
    }
    IEnumerator Player4UI()
    {
        p4uiCopy = Instantiate(p4_UI, UICanvas.position, Quaternion.identity) as GameObject;

        p4Transform = p4uiCopy.GetComponent<RectTransform>();

        p4uiCopy.transform.SetParent(UICanvas);

        p4Transform.position = new Vector2(1680, 10);

        p4_UI_Info = p4uiCopy.GetComponent<UIMaster>();

        yield return new WaitForSeconds(0.05f);
        //CanvasGroup
        p4CanvasGroup = p4_UI_Info.GetComponentInChildren<CanvasGroup>();

        //P4
        //UI references
        //Health
        //player4HealthScript.healthBar = p4_UI_Info.healthBar;
        player4HealthScript.healthNum = p4_UI_Info.healthNum;

        //Shields
        player4HealthScript.overchargeBar = p4_UI_Info.shieldsBar;

        //SpeedBuff
        player4HealthScript.speedBar = p4_UI_Info.speedBar;

        //to Color
        //player4SpriteManager.healthBar = p4_UI_Info.healthBar;
        player4SpriteManager.playerNum = p4_UI_Info.playerNum;
        player4SpriteManager.healthText = p4_UI_Info.healthNum;
        player4SpriteManager.abilityMeter = p4_UI_Info.abilityMeter;
        player4SpriteManager.glow = p4_UI_Info.glow;
        player4SpriteManager.uiSpinner = p4_UI_Info.playerSpinner;

        player4SpriteManager.SpriteSet();

        //AbilityMeter Tracking
        player4AbilityScript.uiAbilityMeter = p4_UI_Info.abilityMeter;

        //yield return new WaitForSeconds(0.05f);

        p4CanvasGroup.alpha = 0;
    }


    IEnumerator TeleportPlayer(GameObject player, SpriteManager playerSpriteManager)
    {
        while (player.transform.localScale.x > targetSizePlayer)
        {
            player.transform.localScale = Vector3.Lerp(player.transform.localScale, Vector3.zero, cameraSmooth * Time.deltaTime);
            yield return null;
        }
        teleParticlesCopy = Instantiate(teleParticles, player.transform.position, Quaternion.identity) as GameObject;
        teleParticles2Copy = Instantiate(teleParticles2, player.transform.position, Quaternion.identity) as GameObject;
        teleActualParticles = teleParticlesCopy.GetComponent<ParticleSystem>();
        teleActualParticles2 = teleParticles2Copy.GetComponent<ParticleSystem>();
        teleActualParticles.startColor = playerSpriteManager.playerColor[playerSpriteManager.teamCount];
        teleActualParticles2.startColor = playerSpriteManager.playerColor[playerSpriteManager.teamCount];
        Destroy(player);
    }

    IEnumerator LerpMusicDown(AudioSource source)
    {
        if (source != null)
        {
            while (source.volume > 0.02f)
            {
                source.volume = Mathf.Lerp(source.volume, 0f, smoothing * Time.fixedDeltaTime);
                yield return null;
            }
            source.Stop();
            source.volume = volumeLevel;
        }
    }

    IEnumerator ToggleMapScreenActive(bool state)
    {
        switch (state)
        {
            case (true):
                yield return new WaitForSeconds(1f);
                mapScreenActive = state;
                mapSelectManager.mapSelectScreenActive = state;
                break;
            case (false):
                
                mapScreenActive = state;
                mapSelectManager.mapSelectScreenActive = state;
                break;
        }
    }

    public void RestartClicked(int sceneSelected)
    {
        StartCoroutine(RestartButtonClicked(sceneSelected));
    }

    IEnumerator RestartButtonClicked(int sceneSelected)
    {
        Destroy(restartButtonInstance);

        GameObject loadingS = Instantiate(loadingScreenObj, transform.position, Quaternion.identity)as GameObject;
        loadingS.SetActive(true);
        yield return new WaitForSeconds(4f);

        if (sceneSelected == selectedMap)
        {
            //Don't destroy gameManager
            SceneManager.LoadScene(selectedMap);
        }
        else if (sceneSelected == 0)
        {
            SceneManager.LoadScene(titleScreenInt);
            Destroy(this.gameObject);
        }
        else if (sceneSelected == 1)
        {
            SceneManager.LoadScene(selectScreenInt);
            Destroy(this.gameObject);
        }
    }

    void MonitorStartGame()
    {
        if (startBannerInstantiated)
        {
            //if someone hits start

            if (rWPlayer1.GetButtonDown("StartButton") || rWPlayer2.GetButtonDown("StartButton") || rWPlayer3.GetButtonDown("StartButton") || rWPlayer4.GetButtonDown("StartButton"))
            {
                GoToMapScreen();
                StartCoroutine(ToggleMapScreenActive(true));
            }
        }

        if (mapScreenActive)
        {
            if (rWPlayer1.GetButtonDown("AButton") && extraKillSwitch || rWPlayer2.GetButtonDown("AButton") && extraKillSwitch || rWPlayer3.GetButtonDown("AButton") && extraKillSwitch || rWPlayer4.GetButtonDown("AButton") && extraKillSwitch)
            {
                extraKillSwitch = false;
                loadingScreen.SetActive(true);
                StartCoroutine(LoadingScreenStartMatch());
            }

            if (rWPlayer1.GetButtonDown("BButton") || rWPlayer2.GetButtonDown("BButton") || rWPlayer3.GetButtonDown("BButton") || rWPlayer4.GetButtonDown("BButton"))
            {
                GoToSelectScreen();
                StartCoroutine(ToggleMapScreenActive(false));
            }
        }
    }

    IEnumerator LoadingScreenStartMatch()
    {
        yield return new WaitForSeconds(4f);
        SelectScreenStart();
    }

    bool AllSameTeam()
    {
        int team0 = 0;
        int team1 = 0;
        int team2 = 0;
        int team3 = 0;

        //Find out if any team is every player
        switch(player1TeamCount)
        {
            case (0):
                team0++;
                break;
            case (1):
                team1++;
                break;
            case (2):
                team2++;
                break;
            case (3):
                team3++;
                break;
        }

        switch (player2TeamCount)
        {
            case (0):
                team0++;
                break;
            case (1):
                team1++;
                break;
            case (2):
                team2++;
                break;
            case (3):
                team3++;
                break;
        }

        if (howManyPlayers > 2)
        {
            switch (player3TeamCount)
            {
                case (0):
                    team0++;
                    break;
                case (1):
                    team1++;
                    break;
                case (2):
                    team2++;
                    break;
                case (3):
                    team3++;
                    break;
            }

            if (howManyPlayers > 3)
            {
                switch (player4TeamCount)
                {
                    case (0):
                        team0++;
                        break;
                    case (1):
                        team1++;
                        break;
                    case (2):
                        team2++;
                        break;
                    case (3):
                        team3++;
                        break;
                }
            }
        }

        //we now know how many players are on each team.
        //We need to now know if all of the active players are part of the same team.
        if (team0 == 0 && team1 == 0 && team2 == 0)
        {
            //Then they're all on team3
            return true;
        }
        else if (team0 == 0 && team1 == 0 && team3 == 0)
        {
            //all team 2
            return true;
        }
        else if (team0 == 0 && team2 == 0 && team3 == 0)
        {
            //all team 1
            return true;
        }
        else if (team1 == 0 && team2 == 0 && team3 == 0)
        {
            //all team 0
            return true;
        }
        else
        {
            return false;
        }
    } 

    void MonitorSpawnBanner()
    {
        if (howManyPlayers <= 2)
        {
            //there's 2 players connected
            if (p1InStartZone && p2InStartZone && !startBannerInstantiated)
            {
                if (!AllSameTeam())
                {
                    //instantiate start button
                    startBannerCopy = Instantiate(startBanner, startBannerPosition.transform.position, Quaternion.identity) as GameObject;
                    startBannerInstantiated = true;
                }
            }
            else if (!p1InStartZone || !p2InStartZone)
            {
                if (startBannerInstantiated)
                {
                    Destroy(startBannerCopy);
                    startBannerInstantiated = false;
                }
            }
        }
        else if (howManyPlayers > 2 && howManyPlayers < 4)
        {
            //there's 3 players connected
            if (p1InStartZone && p2InStartZone && p3InStartZone && !startBannerInstantiated)
            {
                if (!AllSameTeam())
                {
                    //instantiate start button
                    startBannerCopy = Instantiate(startBanner, startBannerPosition.transform.position, Quaternion.identity) as GameObject;
                    startBannerInstantiated = true;
                }
            }
            else if(!p1InStartZone || !p2InStartZone || !p3InStartZone)
            {
                if (startBannerInstantiated)
                {
                    Destroy(startBannerCopy);
                    startBannerInstantiated = false;
                }
            }
        }
        else if (howManyPlayers > 3)
        {
            if (p1InStartZone && p2InStartZone && p3InStartZone && p4InStartZone && !startBannerInstantiated)
            {
                if (!AllSameTeam())
                {
                    //instantiate start button
                    startBannerCopy = Instantiate(startBanner, startBannerPosition.transform.position, Quaternion.identity) as GameObject;
                    startBannerInstantiated = true;
                }
            }
            else if (!p1InStartZone || !p2InStartZone || !p3InStartZone || !p4InStartZone)
            {
                if (startBannerInstantiated)
                {
                    Destroy(startBannerCopy);
                    startBannerInstantiated = false;
                }
            }
        }
    }

    void MonitorSpawnPlayers()
    {
        //Spawn late players
        if (rWPlayer1.GetButtonDown("AButton") && !player1Active && !mapScreenActive)
        {
            player1Active = true;
            howManyPlayers++;

            SpawnPlayer1();
            slideFollowScriptP1.PlayerEntered();
            SetPanelColors(1, player1TeamCount);
        }

        if (rWPlayer2.GetButtonDown("AButton") && !player2Active && !mapScreenActive)
        {
            player2Active = true;
            howManyPlayers++;

            SpawnPlayer2();
            slideFollowScriptP2.PlayerEntered();
            SetPanelColors(2, player2TeamCount);
        }

        if (rWPlayer3.GetButtonDown("AButton") && !player3Active && !mapScreenActive)
        {
            player3Active = true;
            howManyPlayers++;

            SpawnPlayer3();
            slideFollowScriptP3.PlayerEntered();
            SetPanelColors(3, player3TeamCount);
        }

        if (rWPlayer4.GetButtonDown("AButton") && !player4Active && !mapScreenActive)
        {
            player4Active = true;
            howManyPlayers++;

            SpawnPlayer4();
            slideFollowScriptP4.PlayerEntered();
            SetPanelColors(4, player4TeamCount);
        }
    }

    void MonitorDropPlayers()
    {
        //Spawn late players
        if (rWPlayer1.GetButtonDown("BButton") && player1Active && !mapScreenActive)
        {
            player1Active = false;
            howManyPlayers--;

            //Unspawn Player and destroy associated Obj
            Destroy(player1Copy);
            Destroy(p1uiCopy);
            slideFollowScriptP1.PlayerExited();
        }

        if (rWPlayer2.GetButtonDown("BButton") && player2Active && !mapScreenActive)
        {
            player2Active = false;
            howManyPlayers--;

            //Unspawn Player and destroy associated Obj
            Destroy(player2Copy);
            Destroy(p2uiCopy);
            slideFollowScriptP2.PlayerExited();
        }

        if (rWPlayer3.GetButtonDown("BButton") && player3Active && !mapScreenActive)
        {
            player3Active = false;
            howManyPlayers--;

            //Unspawn Player and destroy associated Obj
            Destroy(player3Copy);
            Destroy(p3uiCopy);
            slideFollowScriptP3.PlayerExited();
        }

        if (rWPlayer4.GetButtonDown("BButton") && player4Active && !mapScreenActive)
        {
            player4Active = false;
            howManyPlayers--;

            //Unspawn Player and destroy associated Obj
            Destroy(player4Copy);
            Destroy(p4uiCopy);
            slideFollowScriptP4.PlayerExited();
        }
    }

    void Update()
    {
        if (classSelectScreen)
        {
            //Set Team Comeback to this later, can only set team when the player is in set team zone.
            //We can probably use this same code for switching weapons and abilities
            
            MonitorStartGame();
            if (!mapScreenActive)
            {
                RewiredSetTeam();
                DpadSetWeapon();
                DpadSetAbility();
                MonitorSpawnBanner();
                MonitorSpawnPlayers();
                if (!startBannerInstantiated)
                {
                    MonitorDropPlayers();
                }
            }
            
        }

        if (!classSelectScreen)
        {
            if (Input.GetKeyUp(KeyCode.P) || Input.GetButtonUp("BlackStartButton") || Input.GetButtonUp("WhiteStartButton") || Input.GetButtonUp("Player3StartButton") || Input.GetButtonUp("Player4StartButton"))
            {
                if (gamePaused)
                {
                    //gamePaused = false;
                    //Time.timeScale = 1.0f;
                }
                else
                {
                    //gamePaused = true;
                    //Time.timeScale = 0.0f;
                }
            }
        }

        MonitorShakeScreen();
        MonitorGameOver();
        MonitorUITransparency();
        StartCameraLerp();
    }

    void MonitorUITransparency()
    {
        if (classSelectScreen)
        {
            /*
            p1CanvasGroup.alpha = 0;
            p2CanvasGroup.alpha = 0;
            
            if (howManyPlayers > 2 && p3CanvasGroup != null)
            {
                p3CanvasGroup.alpha = 0;

                if (howManyPlayers > 3 && p4CanvasGroup != null)
                {
                    p4CanvasGroup.alpha = 0;
                }
            }
            */
        }
        else
        {
            //if transparent, lerp to lowerAlpha, if not, lerp to higher alpha
            if (p1Trans)
            {
                p1CanvasGroup.alpha = Mathf.Lerp(p1CanvasGroup.alpha, lowerAlpha, transparentSmoothing * Time.deltaTime);
            }
            else
            {
                p1CanvasGroup.alpha = Mathf.Lerp(p1CanvasGroup.alpha, higherAlpha, transparentSmoothing * Time.deltaTime);
            }

            //if transparent, lerp to lowerAlpha, if not, lerp to higher alpha
            if (p2Trans)
            {
                p2CanvasGroup.alpha = Mathf.Lerp(p2CanvasGroup.alpha, lowerAlpha, transparentSmoothing * Time.deltaTime);
            }
            else
            {
                p2CanvasGroup.alpha = Mathf.Lerp(p2CanvasGroup.alpha, higherAlpha, transparentSmoothing * Time.deltaTime);
            }

            if (howManyPlayers > 2)
            {
                //if transparent, lerp to lowerAlpha, if not, lerp to higher alpha
                if (p3CanvasGroup != null)
                {
                    if (p3Trans)
                    {
                        p3CanvasGroup.alpha = Mathf.Lerp(p3CanvasGroup.alpha, lowerAlpha, transparentSmoothing * Time.deltaTime);
                    }
                    else
                    {
                        p3CanvasGroup.alpha = Mathf.Lerp(p3CanvasGroup.alpha, higherAlpha, transparentSmoothing * Time.deltaTime);
                    }
                }

                if (howManyPlayers > 3)
                {
                    if (p4CanvasGroup != null)
                    {
                        //if transparent, lerp to lowerAlpha, if not, lerp to higher alpha
                        if (p4Trans)
                        {
                            p4CanvasGroup.alpha = Mathf.Lerp(p4CanvasGroup.alpha, lowerAlpha, transparentSmoothing * Time.deltaTime);
                        }
                        else
                        {
                            p4CanvasGroup.alpha = Mathf.Lerp(p4CanvasGroup.alpha, higherAlpha, transparentSmoothing * Time.deltaTime);
                        }
                    }
                }
            }
        }
    }

    //To Set up UI elements
    IEnumerator SetUpUI()
    {
        //Debug.Log("UISetUpRunning");
        //Spawn UI elements
        p1uiCopy = Instantiate(p1_UI, new Vector3(0, 0, 0), Quaternion.identity) as GameObject;
        p2uiCopy = Instantiate(p2_UI, new Vector3(0, 0, 0), Quaternion.identity) as GameObject;
        
        

        //Get transform
        p1Transform = p1uiCopy.GetComponent<RectTransform>();
        p2Transform = p2uiCopy.GetComponent<RectTransform>();
        
        //Set parent to canvas
        p1uiCopy.transform.SetParent(UICanvas);
        p2uiCopy.transform.SetParent(UICanvas);
        

        //Get Info
        p1_UI_Info = p1uiCopy.GetComponent<UIMaster>();
        p2_UI_Info = p2uiCopy.GetComponent<UIMaster>();

        //CanvasGroup
        p1CanvasGroup = p1_UI_Info.GetComponentInChildren<CanvasGroup>();
        p2CanvasGroup = p2_UI_Info.GetComponentInChildren<CanvasGroup>();

        yield return new WaitForSeconds(0.05f);


        //Set Position
        p1Transform.localPosition = new Vector3(0, 50, 0);//or 50/479
        p2Transform.localPosition = new Vector3(80, 50, 0);// or 736/479 1570
        //P1
        //UI references
        //Health
        //player1HealthScript.healthBar = p1_UI_Info.healthBar;
        player1HealthScript.healthNum = p1_UI_Info.healthNum;

        //Shields
        player1HealthScript.overchargeBar = p1_UI_Info.shieldsBar;
        
        //SpeedBuff
        player1HealthScript.speedBar = p1_UI_Info.speedBar;

        //to Color
        //player1SpriteManager.healthBar = p1_UI_Info.healthBar;
        player1SpriteManager.playerNum = p1_UI_Info.playerNum;
        player1SpriteManager.healthText = p1_UI_Info.healthNum;
        player1SpriteManager.abilityMeter = p1_UI_Info.abilityMeter;
        player1SpriteManager.glow = p1_UI_Info.glow;
        player1SpriteManager.uiSpinner = p1_UI_Info.playerSpinner;
    
        player1SpriteManager.SpriteSet();

        //AbilityMeter Tracking
        player1AbilityScript.uiAbilityMeter = p1_UI_Info.abilityMeter;


        //P2
        //UI references
        //Health
        //player2HealthScript.healthBar = p2_UI_Info.healthBar;
        player2HealthScript.healthNum = p2_UI_Info.healthNum;

        //Shields
        player2HealthScript.overchargeBar = p2_UI_Info.shieldsBar;
        
        //SpeedBuff
        player2HealthScript.speedBar = p2_UI_Info.speedBar;

        //to Color
        //player2SpriteManager.healthBar = p2_UI_Info.healthBar;
        player2SpriteManager.playerNum = p2_UI_Info.playerNum;
        player2SpriteManager.healthText = p2_UI_Info.healthNum;
        player2SpriteManager.abilityMeter = p2_UI_Info.abilityMeter;
        player2SpriteManager.glow = p2_UI_Info.glow;
        player2SpriteManager.uiSpinner = p2_UI_Info.playerSpinner;
        
        player2SpriteManager.SpriteSet();

        //AbilityMeter Tracking
        player2AbilityScript.uiAbilityMeter = p2_UI_Info.abilityMeter;

        if (howManyPlayers > 2) //Spawn Player3 UI
        {
            p3uiCopy = Instantiate(p3_UI, new Vector2(0, 0), Quaternion.identity) as GameObject;

            p3Transform = p3uiCopy.GetComponent<RectTransform>();

            p3uiCopy.transform.SetParent(UICanvas);

            p3Transform.localPosition = new Vector2(0, 0);

            p3_UI_Info = p3uiCopy.GetComponent<UIMaster>();

            yield return new WaitForSeconds(0.05f);
            //CanvasGroup
            p3CanvasGroup = p3_UI_Info.GetComponentInChildren<CanvasGroup>();

            

            //P3
            //UI references
            //Health
            //player3HealthScript.healthBar = p3_UI_Info.healthBar;
            player3HealthScript.healthNum = p3_UI_Info.healthNum;

            //Shields
            player3HealthScript.overchargeBar = p3_UI_Info.shieldsBar;
            
            //SpeedBuff
            player3HealthScript.speedBar = p3_UI_Info.speedBar;

            //to Color
            //player3SpriteManager.healthBar = p3_UI_Info.healthBar;
            player3SpriteManager.playerNum = p3_UI_Info.playerNum;
            player3SpriteManager.healthText = p3_UI_Info.healthNum;
            player3SpriteManager.abilityMeter = p3_UI_Info.abilityMeter;
            player3SpriteManager.glow = p3_UI_Info.glow;
            player3SpriteManager.uiSpinner = p3_UI_Info.playerSpinner;

            player3SpriteManager.SpriteSet();

            //AbilityMeter Tracking
            player3AbilityScript.uiAbilityMeter = p3_UI_Info.abilityMeter;


            if (howManyPlayers > 3) //Spawn Player4 UI
            {
                p4uiCopy = Instantiate(p4_UI, new Vector2(0, 0), Quaternion.identity) as GameObject;

                p4Transform = p4uiCopy.GetComponent<RectTransform>();

                p4uiCopy.transform.SetParent(UICanvas);

                p4Transform.localPosition = new Vector2(80, 0);

                p4_UI_Info = p4uiCopy.GetComponent<UIMaster>();

                yield return new WaitForSeconds(0.05f);
                //CanvasGroup
                p4CanvasGroup = p4_UI_Info.GetComponentInChildren<CanvasGroup>();

                //P4
                //UI references
                //Health
                //player4HealthScript.healthBar = p4_UI_Info.healthBar;
                player4HealthScript.healthNum = p4_UI_Info.healthNum;

                //Shields
                player4HealthScript.overchargeBar = p4_UI_Info.shieldsBar;
                
                //SpeedBuff
                player4HealthScript.speedBar = p4_UI_Info.speedBar;

                //to Color
                //player4SpriteManager.healthBar = p4_UI_Info.healthBar;
                player4SpriteManager.playerNum = p4_UI_Info.playerNum;
                player4SpriteManager.healthText = p4_UI_Info.healthNum;
                player4SpriteManager.abilityMeter = p4_UI_Info.abilityMeter;
                player4SpriteManager.glow = p4_UI_Info.glow;
                player4SpriteManager.uiSpinner = p4_UI_Info.playerSpinner;

                player4SpriteManager.SpriteSet();

                //AbilityMeter Tracking
                player4AbilityScript.uiAbilityMeter = p4_UI_Info.abilityMeter;

            }
        }
    }

    void InfiniteHealth(PlayerHealth healthScript)
    {
        if (healthScript != null)
        {
            if (healthScript.shotHealthCount != 100)
            {
                healthScript.shotHealthCount = 100;
            }
        }
    }

    /*
    //Depending how many players there are, victory conditions change.
    void FixedUpdate()
    {
        if (killSwitch == false)
        {
            if (howManyPlayers == 4)
            {
                if (killCount > 2)
                {
                    gameEnd = true;
                    RestartGame();
                }
            }
            else if (howManyPlayers == 3)
            {
                if (killCount > 1)
                {
                    gameEnd = true;
                    RestartGame();
                }
            }
            else if (howManyPlayers == 2)
            {
                if (killCount > 0)
                {
                    gameEnd = true;
                    RestartGame();
                }
            }
            else
            {
                Debug.Log("Somethings Wrong.");
            }
        }


        if (gameEnd == true)
        {
            explosiveFinishCopy = Instantiate(explosiveFinish, transform.position, Quaternion.identity)as GameObject;
            endGameText = explosiveFinishCopy.GetComponentInChildren<Text>();
            endGameParticles = explosiveFinishCopy.GetComponentInChildren<ParticleSystem>();
            if (player1Copy == null)
            {
                if (player2Copy == null)
                {
                    if (player3Copy == null)
                    {
                        if (player4Copy == null)
                        {
                            Debug.Log("It's a draw!");
                            gameEnd = false;
                        }
                        else
                        {
                            //Player4 won
                            //Instantiate(explosiveFinish, player4Copy.transform.position, Quaternion.identity);
                            endGameParticles.startColor = player4SpriteManager.playerColor[player4TeamCount];
                            endGameText.color = player4SpriteManager.playerColor[player4TeamCount];
                            Debug.Log("Player4Won!");
                            gameEnd = false;
                        }
                    }
                    else
                    {
                        //Player3 won
                        //Instantiate(explosiveFinish, player3Copy.transform.position, Quaternion.identity);
                        endGameParticles.startColor = player3SpriteManager.playerColor[player3TeamCount];
                        endGameText.color = player3SpriteManager.playerColor[player3TeamCount];
                        Debug.Log("Player3Won!");
                        gameEnd = false;
                    }
                }
                else
                {
                    //player2 won
                    //Instantiate(explosiveFinish, player2Copy.transform.position, Quaternion.identity);
                    endGameParticles.startColor = player2SpriteManager.playerColor[player2TeamCount];
                    endGameText.color = player2SpriteManager.playerColor[player2TeamCount];
                    Debug.Log("Player2Won!");
                    gameEnd = false;
                }
            }
            else
            {
                //Player1 won
                //Instantiate(explosiveFinish, player1Copy.transform.position, Quaternion.identity);
                endGameParticles.startColor = player1SpriteManager.playerColor[player1TeamCount];
                endGameText.color = player1SpriteManager.playerColor[player1TeamCount];
                Debug.Log("Player1Won!");
                gameEnd = false;
            }
        }
    }
    */
    

    //Here we monitor each team's respective player count
    //Called every frame
    void MonitorGameOver()
    {
        
        if (killSwitch == false)
        {
            //When does the game end? when all but one team remains. so
            if (team2PlayerCount == 0 && team3PlayerCount == 0 && team4PlayerCount == 0)
            {
                //team1 wins Blue
                StartCoroutine(GameOver(1));
                Debug.Log("Team1WINS");
                killSwitch = true;
            }
            else if (team1PlayerCount == 0 && team3PlayerCount == 0 && team4PlayerCount == 0)
            {
                //team2 wins Orange
                StartCoroutine(GameOver(2));
                Debug.Log("Team2WINS");
                killSwitch = true;
            }
            else if (team1PlayerCount == 0 && team2PlayerCount == 0 && team4PlayerCount == 0)
            {
                //team 3 wins Green
                StartCoroutine(GameOver(3));
                Debug.Log("Team3WINS");
                killSwitch = true;
            }
            else if (team1PlayerCount == 0 && team2PlayerCount == 0 && team3PlayerCount == 0)
            {
                //team4 wins Pink
                StartCoroutine(GameOver(4));
                Debug.Log("Team4WINS");
                killSwitch = true;
            }
        }
    }

    void GoToSelectScreen()
    {
        selectScreenCameraScript.PlayerSelect();
        

        if (player1Active)
        {
            player1MoveScript.GameGo();
        }

        if (player2Active)
        {
            player2MoveScript.GameGo();
        }

        if (player3Active)
        {
            player3MoveScript.GameGo();
        }

        if (player4Active)
        {
            player4MoveScript.GameGo();
        }
    }

    void GoToMapScreen()
    {
        //Pan camera downwards
        selectScreenCameraScript.MapSelect();
        

        if (player1Active)
        {
            player1MoveScript.StartingGame();
        }

        if (player2Active)
        {
            player2MoveScript.StartingGame();
        }

        if (player3Active)
        {
            player3MoveScript.StartingGame();
        }

        if (player4Active)
        {
            player4MoveScript.StartingGame();
        }
    }

    IEnumerator GameOver(int winner)
    {
        
        StartCoroutine(LerpMusicDown(gameTrack));
        explosiveFinishCopy = Instantiate(explosiveFinish, transform.position, Quaternion.identity) as GameObject;
        
        endGameText = explosiveFinishCopy.GetComponentInChildren<Text>();
        endGameParticles = explosiveFinishCopy.GetComponentInChildren<ParticleSystem>();
        switch (winner)
        {
            case (1):
                endGameParticles.startColor = blueTeamColor;
                endGameText.color = blueTeamColor;
                break;
            case (2):
                endGameParticles.startColor = orangeTeamColor;
                endGameText.color = orangeTeamColor;
                break;
            case (3):
                endGameParticles.startColor = greenTeamColor;
                endGameText.color = greenTeamColor;
                break;
            case (4):
                endGameParticles.startColor = pinkTeamColor;
                endGameText.color = pinkTeamColor;
                break;
        }
        numPlayerHolder = GameObject.FindGameObjectWithTag("NumHolder");
        numHolder = numPlayerHolder.GetComponent<NumHolder>();
        numHolder.numConnectedPlayers = howManyPlayers;
        yield return endGameWait;
        RestartGame();

        switch (howManyPlayers)
        {
            case (2):
                player1MoveScript.StartingGame();
                player2MoveScript.StartingGame();
                break;
            case (3):
                player1MoveScript.StartingGame();
                player2MoveScript.StartingGame();
                player3MoveScript.StartingGame();
                break;
            case (4):
                player1MoveScript.StartingGame();
                player2MoveScript.StartingGame();
                player3MoveScript.StartingGame();
                player4MoveScript.StartingGame();
                break;
        }
    }

    //When a player dies, they call this function. It tells us who died and what team lost players.
    public void PlayerDied(int playerCount, int teamCount, Vector2 deathPos)
    {
        Instantiate(screenFlash, transform.position, Quaternion.identity);
        Instantiate(deadPlayer, deathPos, Quaternion.identity);
        //Who died so we can weed out who is still alive.
        switch(playerCount)
        {
            case (1):
                player1Dead = true;
                break;
            case (2):
                player2Dead = true;
                break;
            case (3):
                player3Dead = true;
                break;
            case (4):
                player4Dead = true;
                break;
        }

        //What team has lowered a number
        switch(teamCount)
        {
            case (0):
                team1PlayerCount--;
                break;
            case (1):
                team2PlayerCount--;
                break;
            case (2):
                team3PlayerCount--;
                break;
            case (3):
                team4PlayerCount--;
                break;
        }
    }

    //Call at start of game. Should log how many players are in each team.
    void SetUpTeamStruct()
    {
        team1PlayerCount = 0;
        team2PlayerCount = 0;
        team3PlayerCount = 0;
        team4PlayerCount = 0;

        //Through how many players are in the game we set up each team's count
        switch (howManyPlayers)
        {
            case (2):
                SetUpTeams(player1TeamCount);
                SetUpTeams(player2TeamCount);
                break;
            case (3):
                SetUpTeams(player1TeamCount);
                SetUpTeams(player2TeamCount);
                SetUpTeams(player3TeamCount);
                break;
            case (4):
                SetUpTeams(player1TeamCount);
                SetUpTeams(player2TeamCount);
                SetUpTeams(player3TeamCount);
                SetUpTeams(player4TeamCount);
                break;
        }
    }   
    
    void SetUpTeams(int teamCount)
    {
        //Populate player count on each team present.
        switch (teamCount)
        {
            case (0):
                team1PlayerCount++;
                break;
            case (1):
                team2PlayerCount++;
                break;
            case (2):
                team3PlayerCount++;
                break;
            case (3):
                team4PlayerCount++;
                break;
        }
    }


    //Call this from an object in the scene when this gameManager arrives.
    //This happens at the start of the game.
    public void GameStart()
    {
        StartCoroutine(SetUpUI());

        if (selectedMap == 3)
        {
            countdownClip = ds2StartSound;
            selectedTrack = ds2PlainMapMusic;
        }
        else if (selectedMap == 4)
        {
            countdownClip = ds5StartSound;
            selectedTrack = ds5StaticMapMusic;
        }
        else if (selectedMap == 5)
        {
            countdownClip = ds6StartSound;
            selectedTrack = ds6FireMapMusic;
        }
        else if (selectedMap == 6)
        {
            countdownClip = ds4StartSound;
            selectedTrack = ds4PoisonMapMusic;
        }
        else if (selectedMap == 7)
        {
            countdownClip = ds3StartSound;
            selectedTrack = ds3IceMapMusic;
        }
        countdownTrack.clip = countdownClip;
        gameTrack.clip = selectedTrack;

        //We want the players to spawn onto different spots on the map
        player1StartPos = gameManagerActivate.spawnPositions[Random.Range(0, gameManagerActivate.spawnPositions.Length)].transform.position;

        player2StartPos = gameManagerActivate.spawnPositions[Random.Range(0, gameManagerActivate.spawnPositions.Length)].transform.position;
        while (player2StartPos == player1StartPos)
        {
            player2StartPos = gameManagerActivate.spawnPositions[Random.Range(0, gameManagerActivate.spawnPositions.Length)].transform.position;
        }

        if (howManyPlayers > 2)
        {
            player3StartPos = gameManagerActivate.spawnPositions[Random.Range(0, gameManagerActivate.spawnPositions.Length)].transform.position;
            while (player3StartPos == player1StartPos || player3StartPos == player2StartPos)
            {
                player3StartPos = gameManagerActivate.spawnPositions[Random.Range(0, gameManagerActivate.spawnPositions.Length)].transform.position;
            }
            
            if (howManyPlayers > 3)
            {
                player4StartPos = gameManagerActivate.spawnPositions[Random.Range(0, gameManagerActivate.spawnPositions.Length)].transform.position;
                while (player4StartPos == player1StartPos || player4StartPos == player2StartPos || player4StartPos == player3StartPos)
                {
                    player4StartPos = gameManagerActivate.spawnPositions[Random.Range(0, gameManagerActivate.spawnPositions.Length)].transform.position;
                }
            }
        }


        gameEnd = false;
        killSwitch = false;

        //Spawn Player1
        player1Copy = Instantiate(player1, player1StartPos, Quaternion.identity) as GameObject;
        player1MoveScript = player1Copy.GetComponent<PlayerMovement>();
        player1MoveScript.teamCount = player1TeamCount;
        player1MoveScript.gameManagerScript = this;
        player1MoveScript.classSelect = false;
        player1ShootScript = player1Copy.GetComponent<PlayerShoot>();
        player1ShootScript.gameManagerScript = this;
        player1ShootScript.offenseSystem = p1OffenseSys;
        player1AbilityScript = player1Copy.GetComponent<PlayerAbility>();
        player1AbilityScript.defenseSystem = p1DefenseSys;
        player1HealthScript = player1Copy.GetComponent<PlayerHealth>();
        player1HealthScript.gameManagerScript = this;
        player1HealthScript.PickUp(100);
        
        player1SpriteManager = player1Copy.GetComponent<SpriteManager>();
        player1SpriteManager.numPlayer = 1;

        SetWeaponSprite(player1SpriteManager, p1OffenseSys);
        SetAbilitySprite(player1SpriteManager, p1DefenseSys);
        
        SetTeamCount(player1SpriteManager, player1TeamCount);

        //disable player
        player1MoveScript.StartingGame();

        //Instantiate enemy spawner
        mobSpawnerCopy1 = Instantiate(mobSpawner, mobSpawnPos, Quaternion.identity) as GameObject;
        mobSpawnerCopy2 = Instantiate(mobSpawner, mobSpawnPos, Quaternion.identity) as GameObject;
        mobSpawnerCopy3 = Instantiate(mobSpawner, mobSpawnPos, Quaternion.identity) as GameObject;
        mobSpawnerCopy4 = Instantiate(mobSpawner, mobSpawnPos, Quaternion.identity) as GameObject;
        spawner1 = mobSpawnerCopy1.GetComponent<MobSpawner>();
        spawner1.gameManager = this;
        spawner1.transform.position = new Vector2(0, 50);
        spawner2 = mobSpawnerCopy2.GetComponent<MobSpawner>();
        spawner2.gameManager = this;
        spawner2.transform.position = new Vector2(0, -50);
        spawner3 = mobSpawnerCopy3.GetComponent<MobSpawner>();
        spawner3.gameManager = this;
        spawner3.transform.position = new Vector2(50, 0);
        spawner4 = mobSpawnerCopy4.GetComponent<MobSpawner>();
        spawner4.gameManager = this;
        spawner4.transform.position = new Vector2(-50, 0);
        //---------------------------------------

        //Spawn Player2
        player2Copy = Instantiate(player2, player2StartPos, Quaternion.identity) as GameObject;
        player2MoveScript = player2Copy.GetComponent<PlayerMovement>();
        player2MoveScript.teamCount = player2TeamCount;
        player2MoveScript.gameManagerScript = this;
        player2MoveScript.classSelect = false;
        player2ShootScript = player2Copy.GetComponent<PlayerShoot>();
        player2ShootScript.gameManagerScript = this;
        player2ShootScript.offenseSystem = p2OffenseSys;
        player2AbilityScript = player2Copy.GetComponent<PlayerAbility>();
        player2AbilityScript.defenseSystem = p2DefenseSys;
        player2HealthScript = player2Copy.GetComponent<PlayerHealth>();
        player2HealthScript.gameManagerScript = this;
        player2HealthScript.PickUp(100);

        
        player2SpriteManager = player2Copy.GetComponent<SpriteManager>();
        player2SpriteManager.numPlayer = 2;

        SetWeaponSprite(player2SpriteManager, p2OffenseSys);
        SetAbilitySprite(player2SpriteManager, p2DefenseSys);
        
        SetTeamCount(player2SpriteManager, player2TeamCount);

        //disable player
        player2MoveScript.StartingGame();

        if (howManyPlayers > 2)
        {
            Debug.Log("Player3Spawned");
            //Spawn Player3
            player3Copy = Instantiate(player3, player3StartPos, Quaternion.identity) as GameObject;
            player3MoveScript = player3Copy.GetComponent<PlayerMovement>();
            player3MoveScript.teamCount = player3TeamCount;
            player3MoveScript.gameManagerScript = this;
            player3MoveScript.classSelect = false;
            player3ShootScript = player3Copy.GetComponent<PlayerShoot>();
            player3ShootScript.gameManagerScript = this;
            player3ShootScript.offenseSystem = p3OffenseSys;
            player3AbilityScript = player3Copy.GetComponent<PlayerAbility>();
            player3AbilityScript.defenseSystem = p3DefenseSys;
            player3HealthScript = player3Copy.GetComponent<PlayerHealth>();
            player3HealthScript.gameManagerScript = this;
            player3HealthScript.PickUp(100);
            
            player3SpriteManager = player3Copy.GetComponent<SpriteManager>();
            player3SpriteManager.numPlayer = 3;

            SetWeaponSprite(player3SpriteManager, p3OffenseSys);
            SetAbilitySprite(player3SpriteManager, p3DefenseSys);
            SetTeamCount(player3SpriteManager, player3TeamCount);

            //disable player
            player3MoveScript.StartingGame();

            if (howManyPlayers > 3)
            {
                Debug.Log("spawnedPlayer4");
                //Spawn Player4
                player4Copy = Instantiate(player4, player4StartPos, Quaternion.identity) as GameObject;
                player4MoveScript = player4Copy.GetComponent<PlayerMovement>();
                player4MoveScript.teamCount = player4TeamCount;
                player4MoveScript.gameManagerScript = this;
                player4MoveScript.classSelect = false;
                player4ShootScript = player4Copy.GetComponent<PlayerShoot>();
                player4ShootScript.gameManagerScript = this;
                player4ShootScript.offenseSystem = p4OffenseSys;
                player4AbilityScript = player4Copy.GetComponent<PlayerAbility>();
                player4AbilityScript.defenseSystem = p4DefenseSys;
                player4HealthScript = player4Copy.GetComponent<PlayerHealth>();
                player4HealthScript.gameManagerScript = this;
                player4HealthScript.PickUp(100);

                player4SpriteManager = player4Copy.GetComponent<SpriteManager>();
                player4SpriteManager.numPlayer = 4;

                SetWeaponSprite(player4SpriteManager, p4OffenseSys);
                SetAbilitySprite(player4SpriteManager, p4DefenseSys);
                SetTeamCount(player4SpriteManager, player4TeamCount);

                //disable player
                player4MoveScript.StartingGame();
            }
        }
        switch (howManyPlayers)
        {
            case (2):
                player1SpriteManager.HidePlayer();
                player2SpriteManager.HidePlayer();
                break;
            case (3):
                player1SpriteManager.HidePlayer();
                player2SpriteManager.HidePlayer();
                player3SpriteManager.HidePlayer();
                break;
            case (4):
                player1SpriteManager.HidePlayer();
                player2SpriteManager.HidePlayer();
                player3SpriteManager.HidePlayer();
                player4SpriteManager.HidePlayer();
                break;
        }

        SetUpTeamStruct();
        StartCoroutine(CountDown());
    }

    IEnumerator CountDown()
    {
        yield return gameStartWait;

        //3 2 1... GO!
        //Instantiate StartGameText
        intro = true;

        StartCoroutine(StartCameraShift());
        //Debug.Log("spawn");
        startObjCopy = Instantiate(startGameObj, transform.position, Quaternion.identity) as GameObject;
        startObjCopy.transform.SetParent(mainCamera.transform);
        startObjCopy.transform.position = AbovePlayer(mainCamera.transform.position);
        startObjText = startObjCopy.GetComponentInChildren<Text>();
        shakeScript = startObjCopy.GetComponent<Shake>();
        startObjText.text = "3";
        shakeScript.ShakeThis(0.05f, 1f);
        countdownTrack.Play();
        yield return gameStartWait;
        startObjText.text = "2";
        shakeScript.ShakeThis(0.1f, 1f);
        countdownTrack.Play();
        yield return gameStartWait;
        startObjText.text = "1";
        shakeScript.ShakeThis(0.3f, 1f);
        countdownTrack.Play();
        yield return gameStartWait;
        startObjText.text = "FIGHT!";
        startObjCopy.transform.localScale = new Vector3(2f, 2f, 2f);
        shakeScript.ShakeThis(0.5f, 1f);
        gameTrack.volume = volumeLevel;
        gameTrack.Play();
        mapSpinScript.StartMapRotation();
        speakerControlScript.TurnOnSpeakers();
        

        switch (howManyPlayers)
        {
            case (2):
                player1MoveScript.GameGo();
                player2MoveScript.GameGo();
                break;
            case (3):
                player1MoveScript.GameGo();
                player2MoveScript.GameGo();
                player3MoveScript.GameGo();
                break;
            case (4):
                player1MoveScript.GameGo();
                player2MoveScript.GameGo();
                player3MoveScript.GameGo();
                player4MoveScript.GameGo();
                break;
        }

        yield return gameStartWait;
        
        //GameObject aiCopy = Instantiate(aiPref, aiPrefLocation, Quaternion.identity)as GameObject;
        //AiNodeMove aiMoveScript = aiCopy.GetComponent<AiNodeMove>();

        //aiMoveScript.leader = player1Copy;
        

        intro = false;
    }

    IEnumerator StartCameraShift()
    {
        //Okay so we want to lerp between all of the players. 
        //in order to lerp we need to set the target and there's going to be a function that changes.
        
        switch(howManyPlayers)
        {
            case (2):
                //lerp to player1
                targetPosition = new Vector3(player1Copy.transform.position.x, player1Copy.transform.position.y, -10f);
                targetSize = zoomedInCameraSize;
                player1SpriteManager.ShowPlayer();
                InstantiateTele(player1Copy, player1SpriteManager);
                yield return gameStartWait;
                startObjCopy.transform.position = AbovePlayer(player1Copy.transform.position);
                //lerp to player2
                targetPosition = new Vector3(player2Copy.transform.position.x, player2Copy.transform.position.y, -10f);
                player2SpriteManager.ShowPlayer();
                InstantiateTele(player2Copy, player2SpriteManager);
                yield return gameStartWait;
                startObjCopy.transform.position = AbovePlayer(player2Copy.transform.position);
                //lerp to zoomed out
                targetPosition = mainCameraPosition;
                targetSize = zoomedOutCameraSize;
                yield return gameStartWait;
                mainCamera.transform.position = mainCameraPosition;
                mainCamera.orthographicSize = zoomedOutCameraSize;
                break;
            case (3):
                //lerp to player1
                targetPosition = new Vector3(player1Copy.transform.position.x, player1Copy.transform.position.y, -10f);
                targetSize = zoomedInCameraSize;
                player1SpriteManager.ShowPlayer();
                InstantiateTele(player1Copy, player1SpriteManager);
                yield return gameStartWait;
                startObjCopy.transform.position = AbovePlayer(player1Copy.transform.position);
                //lerp to player2
                targetPosition = new Vector3(player2Copy.transform.position.x, player2Copy.transform.position.y, -10f);
                player2SpriteManager.ShowPlayer();
                InstantiateTele(player2Copy, player2SpriteManager);
                yield return gameStartWait;
                startObjCopy.transform.position = AbovePlayer(player2Copy.transform.position);
                //lerp to player3
                targetPosition = new Vector3(player3Copy.transform.position.x, player3Copy.transform.position.y, -10f);
                player3SpriteManager.ShowPlayer();
                InstantiateTele(player3Copy, player3SpriteManager);
                yield return gameStartWait;
                startObjCopy.transform.position = AbovePlayer(player3Copy.transform.position);
                //lerp to zoomed out
                targetPosition = mainCameraPosition;
                targetSize = zoomedOutCameraSize;
                yield return gameStartWait;
                mainCamera.transform.position = mainCameraPosition;
                mainCamera.orthographicSize = zoomedOutCameraSize;
                break;
            case (4):
                //lerp to player1
                targetPosition = new Vector3(player1Copy.transform.position.x, player1Copy.transform.position.y, -10f);
                targetSize = zoomedInCameraSize;
                player1SpriteManager.ShowPlayer();
                InstantiateTele(player1Copy, player1SpriteManager);
                yield return fourPWait;
                startObjCopy.transform.position = AbovePlayer(player1Copy.transform.position);
                //lerp to player2
                targetPosition = new Vector3(player2Copy.transform.position.x, player2Copy.transform.position.y, -10f);
                player2SpriteManager.ShowPlayer();
                InstantiateTele(player2Copy, player2SpriteManager);
                yield return fourPWait;
                startObjCopy.transform.position = AbovePlayer(player2Copy.transform.position);
                //lerp to player3
                targetPosition = new Vector3(player3Copy.transform.position.x, player3Copy.transform.position.y, -10f);
                player3SpriteManager.ShowPlayer();
                InstantiateTele(player3Copy, player3SpriteManager);
                yield return fourPWait;
                startObjCopy.transform.position = AbovePlayer(player3Copy.transform.position);
                //lerp to player4
                targetPosition = new Vector3(player4Copy.transform.position.x, player4Copy.transform.position.y, -10f);
                player4SpriteManager.ShowPlayer();
                InstantiateTele(player4Copy, player4SpriteManager);
                yield return fourPWait;
                startObjCopy.transform.position = AbovePlayer(player4Copy.transform.position);
                //lerp to zoomed out
                targetPosition = mainCameraPosition;
                targetSize = zoomedOutCameraSize;
                yield return gameStartWait;
                mainCamera.transform.position = mainCameraPosition;
                mainCamera.orthographicSize = zoomedOutCameraSize;
                break;
        }
    }

    void InstantiateTele(GameObject player, SpriteManager playerSpriteManager)
    {
        teleParticlesCopy = Instantiate(teleParticles, player.transform.position, Quaternion.identity) as GameObject;
        teleParticles2Copy = Instantiate(teleParticles2, player.transform.position, Quaternion.identity) as GameObject;
        teleActualParticles = teleParticlesCopy.GetComponent<ParticleSystem>();
        teleActualParticles2 = teleParticles2Copy.GetComponent<ParticleSystem>();
        teleActualParticles.startColor = playerSpriteManager.playerColor[playerSpriteManager.teamCount];
        teleActualParticles2.startColor = playerSpriteManager.playerColor[playerSpriteManager.teamCount];
    }

    Vector2 AbovePlayer(Vector2 playerPos)
    {
        float num = playerPos.y + 6f;
        return new Vector2(playerPos.x, num);
    }

    void StartCameraLerp()
    {
        if (intro)
        {
            mainCamera.transform.position = Vector3.Lerp(mainCamera.transform.position, targetPosition, cameraSmooth * Time.deltaTime);
            mainCamera.orthographicSize = Mathf.Lerp(mainCamera.orthographicSize, targetSize, cameraSmooth * Time.deltaTime);
        }
    }

    void SetWeaponSprite(SpriteManager spriteManager, int OSys)
    {
        spriteManager.currentWeapon = OSys;
        spriteManager.SpriteSet();
    }

    void SetAbilitySprite(SpriteManager spriteManager, int DSys)
    {
        spriteManager.currentAbility = DSys;
        spriteManager.SpriteSet();
    }

    void SetTeamCount(SpriteManager spriteManager, int thisTeamCount)
    {
        spriteManager.teamCount = thisTeamCount;
        spriteManager.SpriteSet();


    }

    void SetPanelColors(int player, int teamCount)
    {
        switch(player)
        {
            case (1):
                switch(teamCount)
                {
                    case (0):
                        slideFollowScriptP1.playerText.color = blueTeamColor;
                        slideFollowScriptP1.teamColorBlock.color = blueTeamColor;
                        slideFollowScriptP1.startBox.color = blueTeamColor;
                        slideFollowScriptP1.backPanel.color = blueTeamColor;
                        slideFollowScriptP1.backGradient.color = blueTeamColor;
                        break;
                    case (1):
                        slideFollowScriptP1.playerText.color = orangeTeamColor;
                        slideFollowScriptP1.teamColorBlock.color = orangeTeamColor;
                        slideFollowScriptP1.startBox.color = orangeTeamColor;
                        slideFollowScriptP1.backPanel.color = orangeTeamColor;
                        slideFollowScriptP1.backGradient.color = orangeTeamColor;
                        break;
                    case (2):
                        slideFollowScriptP1.playerText.color = greenTeamColor;
                        slideFollowScriptP1.teamColorBlock.color = greenTeamColor;
                        slideFollowScriptP1.startBox.color = greenTeamColor;
                        slideFollowScriptP1.backPanel.color = greenTeamColor;
                        slideFollowScriptP1.backGradient.color = greenTeamColor;
                        break;
                    case (3):
                        slideFollowScriptP1.playerText.color = pinkTeamColor;
                        slideFollowScriptP1.teamColorBlock.color = pinkTeamColor;
                        slideFollowScriptP1.startBox.color = pinkTeamColor;
                        slideFollowScriptP1.backPanel.color = pinkTeamColor;
                        slideFollowScriptP1.backGradient.color = pinkTeamColor;
                        break;
                }
                break;
            case (2):
                switch (teamCount)
                {
                    case (0):
                        slideFollowScriptP2.playerText.color = blueTeamColor;
                        slideFollowScriptP2.teamColorBlock.color = blueTeamColor;
                        slideFollowScriptP2.startBox.color = blueTeamColor;
                        slideFollowScriptP2.backPanel.color = blueTeamColor;
                        slideFollowScriptP2.backGradient.color = blueTeamColor;
                        break;
                    case (1):
                        slideFollowScriptP2.playerText.color = orangeTeamColor;
                        slideFollowScriptP2.teamColorBlock.color = orangeTeamColor;
                        slideFollowScriptP2.startBox.color = orangeTeamColor;
                        slideFollowScriptP2.backPanel.color = orangeTeamColor;
                        slideFollowScriptP2.backGradient.color = orangeTeamColor;
                        break;
                    case (2):
                        slideFollowScriptP2.playerText.color = greenTeamColor;
                        slideFollowScriptP2.teamColorBlock.color = greenTeamColor;
                        slideFollowScriptP2.startBox.color = greenTeamColor;
                        slideFollowScriptP2.backPanel.color = greenTeamColor;
                        slideFollowScriptP2.backGradient.color = greenTeamColor;
                        break;
                    case (3):
                        slideFollowScriptP2.playerText.color = pinkTeamColor;
                        slideFollowScriptP2.teamColorBlock.color = pinkTeamColor;
                        slideFollowScriptP2.startBox.color = pinkTeamColor;
                        slideFollowScriptP2.backPanel.color = pinkTeamColor;
                        slideFollowScriptP2.backGradient.color = pinkTeamColor;
                        break;
                }
                break;
            case (3):
                switch (teamCount)
                {
                    case (0):
                        slideFollowScriptP3.playerText.color = blueTeamColor;
                        slideFollowScriptP3.teamColorBlock.color = blueTeamColor;
                        slideFollowScriptP3.startBox.color = blueTeamColor;
                        slideFollowScriptP3.backPanel.color = blueTeamColor;
                        slideFollowScriptP3.backGradient.color = blueTeamColor;
                        break;
                    case (1):
                        slideFollowScriptP3.playerText.color = orangeTeamColor;
                        slideFollowScriptP3.teamColorBlock.color = orangeTeamColor;
                        slideFollowScriptP3.startBox.color = orangeTeamColor;
                        slideFollowScriptP3.backPanel.color = orangeTeamColor;
                        slideFollowScriptP3.backGradient.color = orangeTeamColor;
                        break;
                    case (2):
                        slideFollowScriptP3.playerText.color = greenTeamColor;
                        slideFollowScriptP3.teamColorBlock.color = greenTeamColor;
                        slideFollowScriptP3.startBox.color = greenTeamColor;
                        slideFollowScriptP3.backPanel.color = greenTeamColor;
                        slideFollowScriptP3.backGradient.color = greenTeamColor;
                        break;
                    case (3):
                        slideFollowScriptP3.playerText.color = pinkTeamColor;
                        slideFollowScriptP3.teamColorBlock.color = pinkTeamColor;
                        slideFollowScriptP3.startBox.color = pinkTeamColor;
                        slideFollowScriptP3.backPanel.color = pinkTeamColor;
                        slideFollowScriptP3.backGradient.color = pinkTeamColor;
                        break;
                }
                break;
            case (4):
                switch (teamCount)
                {
                    case (0):
                        slideFollowScriptP4.playerText.color = blueTeamColor;
                        slideFollowScriptP4.teamColorBlock.color = blueTeamColor;
                        slideFollowScriptP4.startBox.color = blueTeamColor;
                        slideFollowScriptP4.backPanel.color = blueTeamColor;
                        slideFollowScriptP4.backGradient.color = blueTeamColor;
                        break;
                    case (1):
                        slideFollowScriptP4.playerText.color = orangeTeamColor;
                        slideFollowScriptP4.teamColorBlock.color = orangeTeamColor;
                        slideFollowScriptP4.startBox.color = orangeTeamColor;
                        slideFollowScriptP4.backPanel.color = orangeTeamColor;
                        slideFollowScriptP4.backGradient.color = orangeTeamColor;
                        break;
                    case (2):
                        slideFollowScriptP4.playerText.color = greenTeamColor;
                        slideFollowScriptP4.teamColorBlock.color = greenTeamColor;
                        slideFollowScriptP4.startBox.color = greenTeamColor;
                        slideFollowScriptP4.backPanel.color = greenTeamColor;
                        slideFollowScriptP4.backGradient.color = greenTeamColor;
                        break;
                    case (3):
                        slideFollowScriptP4.playerText.color = pinkTeamColor;
                        slideFollowScriptP4.teamColorBlock.color = pinkTeamColor;
                        slideFollowScriptP4.startBox.color = pinkTeamColor;
                        slideFollowScriptP4.backPanel.color = pinkTeamColor;
                        slideFollowScriptP4.backGradient.color = pinkTeamColor;
                        break;
                }
                break;
        }
    }

    //Setting Team/Weapon/Ability Below

    void RewiredSetTeam()
    {
        //I should use a boolean here so until the player lifts back up the dpad it stays true.

        //Player1TeamCount switch
        if (p1InTeamZone)
        {
            if (rWPlayer1.GetButtonDown("DpadRight") && player1SwitchTeam == false)
            {
                player1SwitchTeam = true;
                player1TeamCount = player1TeamCount + 1;
                if (player1TeamCount > 3)
                { player1TeamCount = 0; }
                SetTeamCount(player1SpriteManager, player1TeamCount);
                SetPanelColors(1, player1TeamCount);
                player1AbilityScript.SwapAbilityTeam(player1TeamCount);
            }
            else if (rWPlayer1.GetButtonDown("DpadLeft") && player1SwitchTeam == false)
            {
                player1SwitchTeam = true;
                player1TeamCount = player1TeamCount - 1;
                if (player1TeamCount < 0)
                { player1TeamCount = 3; }
                SetTeamCount(player1SpriteManager, player1TeamCount);
                SetPanelColors(1, player1TeamCount);
                player1AbilityScript.SwapAbilityTeam(player1TeamCount);
            }
            else if (player1SwitchTeam == true)
            {
                //Debug.Log("Player1 switchreset");
                player1SwitchTeam = false;
            }
        }

        ////Player2TeamCount switch
        if (p2InTeamZone)
        {
            if (rWPlayer2.GetButtonDown("DpadRight") && player2SwitchTeam == false)
            {
                player2SwitchTeam = true;
                player2TeamCount = player2TeamCount + 1;
                if (player2TeamCount > 3)
                { player2TeamCount = 0; }
                SetTeamCount(player2SpriteManager, player2TeamCount);
                SetPanelColors(2, player2TeamCount);
                player2AbilityScript.SwapAbilityTeam(player2TeamCount);
            }
            else if (rWPlayer2.GetButtonDown("DpadLeft") && player2SwitchTeam == false)
            {
                player2SwitchTeam = true;
                player2TeamCount = player2TeamCount - 1;
                if (player2TeamCount < 0)
                { player2TeamCount = 3; }
                SetTeamCount(player2SpriteManager, player2TeamCount);
                SetPanelColors(2, player2TeamCount);
                player2AbilityScript.SwapAbilityTeam(player2TeamCount);
            }
            else if (player2SwitchTeam == true)
            {
                //Debug.Log("Player2 switchreset");
                player2SwitchTeam = false;
            }
        }

        //Player3TeamCount switch
        if (p3InTeamZone)
        {
            if (rWPlayer3.GetButtonDown("DpadRight") && player3SwitchTeam == false)
            {
                player3SwitchTeam = true;
                player3TeamCount = player3TeamCount + 1;
                if (player3TeamCount > 3)
                { player3TeamCount = 0; }
                SetTeamCount(player3SpriteManager, player3TeamCount);
                SetPanelColors(3, player3TeamCount);
                player3AbilityScript.SwapAbilityTeam(player3TeamCount);
            }
            else if (rWPlayer3.GetButtonDown("DpadLeft") && player3SwitchTeam == false)
            {
                player3SwitchTeam = true;
                player3TeamCount = player3TeamCount - 1;
                if (player3TeamCount < 0)
                { player3TeamCount = 3; }
                SetTeamCount(player3SpriteManager, player3TeamCount);
                SetPanelColors(3, player3TeamCount);
                player3AbilityScript.SwapAbilityTeam(player3TeamCount);
            }
            else if (player3SwitchTeam == true)
            {
                //Debug.Log("Player3 switchreset");
                player3SwitchTeam = false;
            }
        }

        //Player4TeamCount switch
        if (p4InTeamZone)
        {
            if (rWPlayer4.GetButtonDown("DpadRight") && player4SwitchTeam == false)
            {
                player4SwitchTeam = true;
                player4TeamCount = player4TeamCount + 1;
                if (player4TeamCount > 3)
                { player4TeamCount = 0; }
                SetTeamCount(player4SpriteManager, player4TeamCount);
                SetPanelColors(4, player4TeamCount);
                player4AbilityScript.SwapAbilityTeam(player4TeamCount);
            }
            else if (rWPlayer4.GetButtonDown("DpadLeft") && player4SwitchTeam == false)
            {
                player4SwitchTeam = true;
                player4TeamCount = player4TeamCount - 1;
                if (player4TeamCount < 0)
                { player4TeamCount = 3; }
                SetTeamCount(player4SpriteManager, player4TeamCount);
                SetPanelColors(4, player4TeamCount);
                player4AbilityScript.SwapAbilityTeam(player4TeamCount);
            }
            else if (player4SwitchTeam == true)
            {
                //Debug.Log("Player4 switchreset");
                player4SwitchTeam = false;
            }
        }
    }

    void DpadSetWeapon()
    {
        //SetWeapon

        if (p1InWeaponZone)
        {
            if (rWPlayer1.GetButtonDown("DpadRight") && player1SwitchTeam == false)
            {
                player1SwitchTeam = true;
                //+1 the p1SelectedWeapon
                p1SelectedWeapon++;
                //if the p1SelectedWeapon exceeds 9 
                if (p1SelectedWeapon > 8)
                { p1SelectedWeapon = 0; }
                //Call set weapon function with the currently selected weapon.
                SetOffensiveSystem(p1SelectedWeapon, 1);
            }
            else if (rWPlayer1.GetButtonDown("DpadLeft") && player1SwitchTeam == false)
            {
                player1SwitchTeam = true;
                //-1 the p1SelectedWeapon
                p1SelectedWeapon--;
                //if the p1SelectedWeapon exceeds 9 
                if (p1SelectedWeapon < 0)
                { p1SelectedWeapon = 8; }
                //Call set weapon function with the currently selected weapon.
                SetOffensiveSystem(p1SelectedWeapon, 1);
            }
            else if (player1SwitchTeam == true)
            {
                //Debug.Log("Player1 switchreset");
                player1SwitchTeam = false;
            }
        }

        if (p2InWeaponZone)
        {
            if (rWPlayer2.GetButtonDown("DpadRight") && player2SwitchTeam == false)
            {
                player2SwitchTeam = true;
                //+1 the p2SelectedWeapon
                p2SelectedWeapon++;
                //if the p2SelectedWeapon exceeds 9 
                if (p2SelectedWeapon > 8)
                { p2SelectedWeapon = 0; }
                //Call set weapon function with the currently selected weapon.
                SetOffensiveSystem(p2SelectedWeapon, 2);
            }
            else if (rWPlayer2.GetButtonDown("DpadLeft") && player2SwitchTeam == false)
            {
                player2SwitchTeam = true;
                //-1 the p2SelectedWeapon
                p2SelectedWeapon--;
                //if the p2SelectedWeapon exceeds 9 
                if (p2SelectedWeapon < 0)
                { p2SelectedWeapon = 8; }
                //Call set weapon function with the currently selected weapon.
                SetOffensiveSystem(p2SelectedWeapon, 2);
            }
            else if (player2SwitchTeam == true)
            {
                //Debug.Log("Player2 switchreset");
                player2SwitchTeam = false;
            }
        }

        if (p3InWeaponZone)
        {
            if (rWPlayer3.GetButtonDown("DpadRight") && player3SwitchTeam == false)
            {
                player3SwitchTeam = true;
                //+1 the p3SelectedWeapon
                p3SelectedWeapon++;
                //if the p3SelectedWeapon exceeds 9 
                if (p3SelectedWeapon > 8)
                { p3SelectedWeapon = 0; }
                //Call set weapon function with the currently selected weapon.
                SetOffensiveSystem(p3SelectedWeapon, 3);
            }
            else if (rWPlayer3.GetButtonDown("DpadLeft") && player3SwitchTeam == false)
            {
                player3SwitchTeam = true;
                //-1 the p3SelectedWeapon
                p3SelectedWeapon--;
                //if the p3SelectedWeapon exceeds 9 
                if (p3SelectedWeapon < 0)
                { p3SelectedWeapon = 8; }
                //Call set weapon function with the currently selected weapon.
                SetOffensiveSystem(p3SelectedWeapon, 3);
            }
            else if (player3SwitchTeam == true)
            {
                //Debug.Log("Player3 switchreset");
                player3SwitchTeam = false;
            }
        }

        if (p4InWeaponZone)
        {
            if (rWPlayer4.GetButtonDown("DpadRight") && player4SwitchTeam == false)
            {
                player4SwitchTeam = true;
                //+1 the p4SelectedWeapon
                p4SelectedWeapon++;
                //if the p4SelectedWeapon exceeds 9 
                if (p4SelectedWeapon > 8)
                { p4SelectedWeapon = 0; }
                //Call set weapon function with the currently selected weapon.
                SetOffensiveSystem(p4SelectedWeapon, 4);
            }
            else if (rWPlayer4.GetButtonDown("DpadLeft") && player4SwitchTeam == false)
            {
                player4SwitchTeam = true;
                //-1 the p4SelectedWeapon
                p4SelectedWeapon--;
                //if the p4SelectedWeapon exceeds 9 
                if (p4SelectedWeapon < 0)
                { p4SelectedWeapon = 8; }
                //Call set weapon function with the currently selected weapon.
                SetOffensiveSystem(p4SelectedWeapon, 4);
            }
            else if (player4SwitchTeam == true)
            {
                //Debug.Log("Player4 switchreset");
                player4SwitchTeam = false;
            }
        }
    }

    void DpadSetAbility()
    {
        //Set Ability

        if (p1InAbilityZone)
        {
            if (rWPlayer1.GetButtonDown("DpadRight") && player1SwitchTeam == false)
            {
                player1SwitchTeam = true;
                
                p1SelectedAbility++;
                
                if (p1SelectedAbility > 7)
                { p1SelectedAbility = 0; }
                
                SetDefensiveSystem(p1SelectedAbility, 1);
            }
            else if (rWPlayer1.GetButtonDown("DpadLeft") && player1SwitchTeam == false)
            {
                player1SwitchTeam = true;

                p1SelectedAbility--;
                
                if (p1SelectedAbility < 0)
                { p1SelectedAbility = 7; }
                
                SetDefensiveSystem(p1SelectedAbility, 1);
            }
            else if (player1SwitchTeam == true)
            {
                //Debug.Log("Player1 switchreset");
                player1SwitchTeam = false;
            }
        }

        if (p2InAbilityZone)
        {
            if (rWPlayer2.GetButtonDown("DpadRight") && player2SwitchTeam == false)
            {
                player2SwitchTeam = true;
                
                p2SelectedAbility++;
                
                if (p2SelectedAbility > 7)
                { p2SelectedAbility = 0; }
                
                SetDefensiveSystem(p2SelectedAbility, 2);
            }
            else if (rWPlayer2.GetButtonDown("DpadLeft") && player2SwitchTeam == false)
            {
                player2SwitchTeam = true;
                
                p2SelectedAbility--;
                
                if (p2SelectedAbility < 0)
                { p2SelectedAbility = 7; }
                
                SetDefensiveSystem(p2SelectedAbility, 2);
            }
            else if (player2SwitchTeam == true)
            {
                //Debug.Log("Player2 switchreset");
                player2SwitchTeam = false;
            }
        }

        if (p3InAbilityZone)
        {
            if (rWPlayer3.GetButtonDown("DpadRight") && player3SwitchTeam == false)
            {
                player3SwitchTeam = true;
                
                p3SelectedAbility++;
                
                if (p3SelectedAbility > 7)
                { p3SelectedAbility = 0; }
                
                SetDefensiveSystem(p3SelectedAbility, 3);
            }
            else if (rWPlayer3.GetButtonDown("DpadLeft") && player3SwitchTeam == false)
            {
                player3SwitchTeam = true;

                p3SelectedAbility--;
                
                if (p3SelectedAbility < 0)
                { p3SelectedAbility = 7; }
                
                SetDefensiveSystem(p3SelectedAbility, 3);
            }
            else if (player3SwitchTeam == true)
            {
                //Debug.Log("Player3 switchreset");
                player3SwitchTeam = false;
            }
        }

        if (p4InAbilityZone)
        {
            if (rWPlayer4.GetButtonDown("DpadRight") && player4SwitchTeam == false)
            {
                player4SwitchTeam = true;
                
                p4SelectedAbility++;
                
                if (p4SelectedAbility > 7)
                { p4SelectedAbility = 0; }
                
                SetDefensiveSystem(p4SelectedAbility, 4);
            }
            else if (rWPlayer4.GetButtonDown("DpadLeft") && player4SwitchTeam == false)
            {
                player4SwitchTeam = true;

                p4SelectedAbility--;
                
                if (p4SelectedAbility < 0)
                { p4SelectedAbility = 7; }
                
                SetDefensiveSystem(p4SelectedAbility, 4);
            }
            else if (player4SwitchTeam == true)
            {
                //Debug.Log("Player4 switchreset");
                player4SwitchTeam = false;
            }
        }
    }


    //Deapreciated--------------------VVV
    void SetTeam()
    {
        //I should use a boolean here so until the player lifts back up the dpad it stays true.
        
        //Player1TeamCount switch
        if (Input.GetAxis("Player1DpadX") > 0.1f && player1SwitchTeam == false)
        {
            player1SwitchTeam = true;
            player1TeamCount = player1TeamCount + 1;
            if(player1TeamCount > 3)
            { player1TeamCount = 0; }
            SetTeamCount(player1SpriteManager, player1TeamCount);
            player1AbilityScript.SwapAbilityTeam(player1TeamCount);
        }
        else if (Input.GetAxis("Player1DpadX") < -0.1f && player1SwitchTeam == false)
        {
            player1SwitchTeam = true;
            player1TeamCount = player1TeamCount - 1;
            if(player1TeamCount < 0)
            { player1TeamCount = 3; }
            SetTeamCount(player1SpriteManager, player1TeamCount);
            player1AbilityScript.SwapAbilityTeam(player1TeamCount);
        }
        else if (Input.GetAxis("Player1DpadX") < 0.1f && Input.GetAxis("Player1DpadX") > -0.1f && player1SwitchTeam == true)
        {
            //Debug.Log("Player1 switchreset");
            player1SwitchTeam = false;
        }

        ////Player2TeamCount switch
        if (Input.GetAxis("Player2DpadX") > 0.1f && player2SwitchTeam == false)
        {
            player2SwitchTeam = true;
            player2TeamCount = player2TeamCount + 1;
            if(player2TeamCount > 3)
            { player2TeamCount = 0; }
            SetTeamCount(player2SpriteManager, player2TeamCount);
            player2AbilityScript.SwapAbilityTeam(player2TeamCount);
        }
        else if (Input.GetAxis("Player2DpadX") < -0.1f && player2SwitchTeam == false)
        {
            player2SwitchTeam = true;
            player2TeamCount = player2TeamCount - 1;
            if(player2TeamCount < 0)
            { player2TeamCount = 3; }
            SetTeamCount(player2SpriteManager, player2TeamCount);
            player2AbilityScript.SwapAbilityTeam(player2TeamCount);
        }
        else if (Input.GetAxis("Player2DpadX") < 0.1f && Input.GetAxis("Player2DpadX") > -0.1f && player2SwitchTeam == true)
        {
            //Debug.Log("Player2 switchreset");
            player2SwitchTeam = false;
        }

        //Player3TeamCount switch
        if (Input.GetAxis("Player3DpadX") > 0.1f && player3SwitchTeam == false)
        {
            player3SwitchTeam = true;
            player3TeamCount = player3TeamCount + 1;
            if(player3TeamCount > 3)
            { player3TeamCount = 0; }
            SetTeamCount(player3SpriteManager, player3TeamCount);
            player3AbilityScript.SwapAbilityTeam(player3TeamCount);
        }
        else if (Input.GetAxis("Player3DpadX") < -0.1f && player3SwitchTeam == false)
        {
            player3SwitchTeam = true;
            player3TeamCount = player3TeamCount - 1;
            if(player3TeamCount < 0)
            { player3TeamCount = 3; }
            SetTeamCount(player3SpriteManager, player3TeamCount);
            player3AbilityScript.SwapAbilityTeam(player3TeamCount);
        }
        else if (Input.GetAxis("Player3DpadX") < 0.1f && Input.GetAxis("Player3DpadX") > -0.1f && player3SwitchTeam == true)
        {
            //Debug.Log("Player3 switchreset");
            player3SwitchTeam = false;
        }

        //Player4TeamCount switch
        if (Input.GetAxis("Player4DpadX") > 0.1f && player4SwitchTeam == false)
        {
            player4SwitchTeam = true;
            player4TeamCount = player4TeamCount + 1;
            if(player4TeamCount > 3)
            { player4TeamCount = 0; }
            SetTeamCount(player4SpriteManager, player4TeamCount);
            player4AbilityScript.SwapAbilityTeam(player4TeamCount);
        }
        else if (Input.GetAxis("Player4DpadX") < -0.1f && player4SwitchTeam == false)
        {
            player4SwitchTeam = true;
            player4TeamCount = player4TeamCount - 1;
            if(player4TeamCount < 0)
            { player4TeamCount = 3; }
            SetTeamCount(player4SpriteManager, player4TeamCount);
            player4AbilityScript.SwapAbilityTeam(player4TeamCount);
        }
        else if (Input.GetAxis("Player4DpadX") < 0.1f && Input.GetAxis("Player4DpadX") > -0.1f && player4SwitchTeam == true)
        {
            //Debug.Log("Player4 switchreset");
            player4SwitchTeam = false;
        }
    }

    public void SetOffensiveSystem(int system, int player)
    {
        //Debug.Log("System:" + system + "|player:" + player);
        if (player == 1)
        {
            SetWeaponSprite(player1SpriteManager, system);
            p1OffenseSys = system;
            player1ShootScript.offenseSystem = system;
        }
        else if (player == 2)
        {
            SetWeaponSprite(player2SpriteManager, system);
            p2OffenseSys = system;
            player2ShootScript.offenseSystem = system;
        }
        else if (player == 3)
        {
            SetWeaponSprite(player3SpriteManager, system);
            p3OffenseSys = system;
            player3ShootScript.offenseSystem = system;
        }
        else //player == 4
        {
            SetWeaponSprite(player4SpriteManager, system);
            p4OffenseSys = system;
            player4ShootScript.offenseSystem = system;
        }
    }

    public void SetDefensiveSystem(int system, int player)
    {
        //Debug.Log("System:" + system + "|player:" + player);
        if (player == 1)
        {
            SetAbilitySprite(player1SpriteManager, system);
            p1DefenseSys = system;
            player1AbilityScript.defenseSystem = system;
            player1AbilityScript.DestroyCurrentAbilites();
        }
        else if (player == 2)
        {
            SetAbilitySprite(player2SpriteManager, system);
            p2DefenseSys = system;
            player2AbilityScript.defenseSystem = system;
            player2AbilityScript.DestroyCurrentAbilites();
        }
        else if (player == 3)
        {
            SetAbilitySprite(player3SpriteManager, system);
            p3DefenseSys = system;
            player3AbilityScript.defenseSystem = system;
            player3AbilityScript.DestroyCurrentAbilites();
        }
        else //player == 4
        {
            SetAbilitySprite(player4SpriteManager, system);
            p4DefenseSys = system;
            player4AbilityScript.defenseSystem = system;
            player4AbilityScript.DestroyCurrentAbilites();
        }
    }

    //This should replace all of the HowManyPlayers code that won't work when player 1 isn't involved.
    /*
    if (player1Active)
            {
                if (player2Active)
                {
                    if (player3Active)
                    {
                        if (player4Active)
                        {
                            //1/2/3/4 in
                        }
                        else
                        {
                            //1/2/3 in
                        }
                    }
                    else if (player4Active)
                    {
                        //1/2/4 in
                    }
                    else
                    {
                        //1/2 in
                    }
                }
                else if (player3Active)
                {
                    if (player4Active)
                    {
                        //1/3/4 in
                    }
                    else
                    {
                        //1/3 in
                    }
                }
                else if (player4Active)
                {
                    //1/4 in
                }
            }

            if (player2Active)
            {
                if (player3Active)
                {
                    if (player4Active)
                    {
                        //2/3/4 in
                    }
                    else
                    {
                        //2/3 in
                    }
                }
                if (player4Active)
                {
                    //2/4 in
                }
            }

            if (player3Active)
            {
                if (player4Active)
                {
                    //3/4 in
                }
            }
    */


    /*
    IEnumerator SpawnAsteroids()
    {
        yield return new WaitForSeconds(startWait);
        while (true)
        {
            for (int i = 0; i < hazardCount; i++)
            {
                //Create spawnPosition from somewhere on the edge of the circle
                Vector2 spawnPosition = Random.insideUnitCircle.normalized * spawnRadius;
                //Create instance of asteroid and grab Rigidbody2d & the script
                int spaceJunkNum = Random.Range(0, 39);
                if (spaceJunkNum > 24 && spaceJunkNum < 28)
                {
                    spaceJunkNum = Random.Range(0, 24);
                }
                GameObject spaceJunkInstance = Instantiate(RandomSpaceJunk(spaceJunkNum), spawnPosition, Quaternion.identity)as GameObject;
                if (spaceJunkNum > 0 && spaceJunkNum < 25)
                {
                    asteroidScript = spaceJunkInstance.GetComponent<Asteroid>();

                    if (spaceJunkNum > 19 && spaceJunkNum < 25)
                    {
                        asteroidScript.asteroidID = 2;
                    }
                }
                Rigidbody2D spaceJunkRB = spaceJunkInstance.GetComponent<Rigidbody2D>();
                //Create random location to move asteroid in
                Vector2 randomLoc = Random.insideUnitCircle * spaceJunkMoveToRadius;
                //Add the subtracted vectors as force to send spaceJunk
                Vector2 randomDir = randomLoc - spawnPosition;
                float randomVelocity = Random.Range(4, 10);
                if (spaceJunkNum > 24 && spaceJunkNum < 28)
                {
                    
                    Vector3 rotationVector = spaceJunkInstance.transform.rotation.eulerAngles;
                    rotationVector.z = Random.Range(0, 360);
                    spaceJunkInstance.transform.rotation = Quaternion.Euler(rotationVector);
                    randomVelocity *= ibeamSpeedMultiplier;
                    
                }
                spaceJunkRB.AddForce(randomDir * randomVelocity);
                yield return new WaitForSeconds(spawnWait);
            }
            yield return new WaitForSeconds(waveWait);
        }
    }

    GameObject RandomSpaceJunk(int randomNumber)
    {
        //Asteroids = 0-19
        if (randomNumber > -1 && randomNumber < 20)
        { return asteroidArray[Random.Range(0, 4)]; }
        //MineralAsteroids = 20-24
        else if (randomNumber > 19 && randomNumber < 25)
        { return asteroidMineralArray[Random.Range(0, 4)]; }
        //Ibeams = 25-27
        else if (randomNumber > 24 && randomNumber < 28)
        { return ibeamArray[Random.Range(0, 3)]; }
        //Capsule = 28
        else if (randomNumber > 27 && randomNumber <29)
        { return capsule; }
        //MineralFragments = 30-34
        else if (randomNumber > 28 && randomNumber < 35)
        {
            return mineralFragment;
        }
        else if(randomNumber > 34 && randomNumber < 37)
        {
            return asteroidShieldMineral[Random.Range(0, 3)];
        }
        else if (randomNumber > 36 && randomNumber < 39)
        {
            return asteroidSpeedBuffMineral[Random.Range(0, 3)];
        }

        else
        {
            Debug.Log("Null is: " + randomNumber);
            return null;
        }
    }
    */
}
