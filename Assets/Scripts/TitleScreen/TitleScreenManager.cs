using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;
using Rewired;

public class TitleScreenManager : MonoBehaviour {

    private int playerCount;

    private Player rWPlayer1;
    private Player rWPlayer2;
    private Player rWPlayer3;
    private Player rWPlayer4;

    private bool player1Active;
    private bool player2Active;
    private bool player3Active;
    private bool player4Active;

    public GameObject loadingScreen;

    //Hold Num players
    public GameObject numPlayerHolder;
    private NumHolder numHolder;

    public GameObject PressStartButton;
    public Vector2 startButtonPos;

    private bool startButtonKillSwitch = false;

    public GameObject p1Text;
    public GameObject p2Text;
    public GameObject p3Text;
    public GameObject p4Text;

    public SpriteRenderer p1Portal;
    public SpriteRenderer p2Portal;
    public SpriteRenderer p3Portal;
    public SpriteRenderer p4Portal;


    public int fireDPS;
    public int poisonDPS;

    //Player1
    public GameObject player1;
    private GameObject player1Copy;
    private PlayerMovement player1MoveScript;
    private PlayerShoot player1ShootScript;
    private PlayerHealth player1HealthScript;
    private PlayerRotation player1RotateScript;
    private PlayerAbility player1AbilityScript;
    private SpriteManager player1SpriteManager;
    public GameObject player1StartPosObj;
    public Vector2 player1StartPos;
    private int p1OffenseSys;
    private int p1DefenseSys;
    private int player1TeamCount;

    //Player2
    public GameObject player2;
    private GameObject player2Copy;
    private PlayerMovement player2MoveScript;
    private PlayerShoot player2ShootScript;
    private PlayerHealth player2HealthScript;
    private PlayerRotation player2RotateScript;
    private PlayerAbility player2AbilityScript;
    private SpriteManager player2SpriteManager;
    public GameObject player2StartPosObj;
    public Vector2 player2StartPos;
    private int p2OffenseSys;
    private int p2DefenseSys;
    private int player2TeamCount;

    //Player3
    public GameObject player3;
    private GameObject player3Copy;
    private PlayerMovement player3MoveScript;
    private PlayerShoot player3ShootScript;
    private PlayerHealth player3HealthScript;
    private PlayerRotation player3RotateScript;
    private PlayerAbility player3AbilityScript;
    private SpriteManager player3SpriteManager;
    public GameObject player3StartPosObj;
    public Vector2 player3StartPos;
    private int p3OffenseSys;
    private int p3DefenseSys;
    private int player3TeamCount;

    //Player4
    public GameObject player4;
    private GameObject player4Copy;
    private PlayerMovement player4MoveScript;
    private PlayerShoot player4ShootScript;
    private PlayerHealth player4HealthScript;
    private PlayerRotation player4RotateScript;
    private PlayerAbility player4AbilityScript;
    private SpriteManager player4SpriteManager;
    public GameObject player4StartPosObj;
    public Vector2 player4StartPos;
    private int p4OffenseSys;
    private int p4DefenseSys;
    private int player4TeamCount;

    public bool classSelectScreen = true;
    public bool titleScreenManager = true;

    
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


    //ScreenShake
    private bool screenShakeActive;
    private Vector3 shakePos;
    public float shakeAmount;

    private Vector3 zeroPos = new Vector3(0f, 0f, -10f);

    public GameObject cameraGO;


    void Awake()
    {
        //Rewired
        rWPlayer1 = ReInput.players.GetPlayer(0);
        rWPlayer2 = ReInput.players.GetPlayer(1);
        rWPlayer3 = ReInput.players.GetPlayer(2);
        rWPlayer4 = ReInput.players.GetPlayer(3);

        //Setting player start pos via obj.
        player1StartPos = player1StartPosObj.transform.position;
        player2StartPos = player2StartPosObj.transform.position;
        player3StartPos = player3StartPosObj.transform.position;
        player4StartPos = player4StartPosObj.transform.position;

        //NumPlayersConnected
        numHolder = numPlayerHolder.GetComponent<NumHolder>();
        
    }

    int RandomNum(bool notWeapon)
    {
        if (notWeapon)
        {
            return Random.Range(0, 8);
        }
        else
        {
            return Random.Range(0, 9);
        }
    }

    void MonitorPlayerCount()
    {
        if (playerCount > 1 && !startButtonKillSwitch)
        {
            Instantiate(PressStartButton, startButtonPos, Quaternion.identity);
            startButtonKillSwitch = true;
        }

        //if the killswitch is true then we know there are enough players active to start the game so
        //start listening for a start button to initiate the game.
        if (startButtonKillSwitch)
        {
            if (rWPlayer1.GetButtonDown("StartButton") || rWPlayer2.GetButtonDown("StartButton") || rWPlayer3.GetButtonDown("StartButton") || rWPlayer4.GetButtonDown("StartButton"))
            {
                loadingScreen.SetActive(true);
                StartCoroutine(LoadSelectScreen());
            }
        }
    }

    IEnumerator LoadSelectScreen()
    {
        yield return new WaitForSeconds(4f);
        Application.LoadLevel(2);
    }

    void MonitorDropPlayer()
    {
        if (rWPlayer1.GetButtonDown("BButton") && player1Active)
        {
            playerCount--;
            numHolder.numConnectedPlayers--;

            player1Active = false;
            p1Text.SetActive(true);
            p1Portal.enabled = false;

            Destroy(player1Copy);
            Destroy(p1uiCopy);
        }

        if (rWPlayer2.GetButtonDown("BButton") && player2Active)
        {
            playerCount--;
            numHolder.numConnectedPlayers--;

            player2Active = false;
            p2Text.SetActive(true);
            p2Portal.enabled = false;

            Destroy(player2Copy);
            Destroy(p2uiCopy);
        }

        if (rWPlayer3.GetButtonDown("BButton") && player3Active)
        {
            playerCount--;
            numHolder.numConnectedPlayers--;

            player3Active = false;
            p3Text.SetActive(true);
            p3Portal.enabled = false;

            Destroy(player3Copy);
            Destroy(p3uiCopy);
        }

        if (rWPlayer4.GetButtonDown("BButton") && player4Active)
        {
            playerCount--;
            numHolder.numConnectedPlayers--;

            player4Active = false;
            p4Text.SetActive(true);
            p4Portal.enabled = false;

            Destroy(player4Copy);
            Destroy(p4uiCopy);
        }
    }

    void Update()
    {
        MonitorShakeScreen();
        MonitorPlayerCount();
        MonitorDropPlayer();

        if (rWPlayer1.GetButtonDown("AButton") && !player1Active)
        {
            playerCount++;
            numHolder.numConnectedPlayers++;

            player1Active = true;
            p1Text.SetActive(false);
            p1Portal.enabled = true;

            player1Copy = Instantiate(player1, player1StartPos, Quaternion.identity) as GameObject;
            //Ref
            player1MoveScript = player1Copy.GetComponent<PlayerMovement>();
            player1ShootScript = player1Copy.GetComponent<PlayerShoot>();
            player1AbilityScript = player1Copy.GetComponent<PlayerAbility>();
            player1HealthScript = player1Copy.GetComponent<PlayerHealth>();
            player1SpriteManager = player1Copy.GetComponent<SpriteManager>();

            //Set Component References
            player1TeamCount = 0;
            player1MoveScript.classSelect = true;
            player1HealthScript.titleScreenManager = this;//Set up
            player1HealthScript.titleScreen = true;
            player1HealthScript.PickUp(100);
            player1SpriteManager.numPlayer = 1;
            player1SpriteManager.ShowPlayer();

            SetTeamCount(player1SpriteManager, player1TeamCount);
            SetOffensiveSystem(RandomNum(false), 1);
            SetDefensiveSystem(RandomNum(true), 1);

            StartCoroutine(Player1UI());
        }

        if (rWPlayer2.GetButtonDown("AButton") && !player2Active)
        {
            playerCount++;
            numHolder.numConnectedPlayers++;

            player2Active = true;
            p2Text.SetActive(false);
            p2Portal.enabled = true;

            //Player2
            player2Copy = Instantiate(player2, player2StartPos, Quaternion.identity) as GameObject;
            player2MoveScript = player2Copy.GetComponent<PlayerMovement>();
            player2TeamCount = 1;
            player2ShootScript = player2Copy.GetComponent<PlayerShoot>();
            player2MoveScript.classSelect = true;
            player2AbilityScript = player2Copy.GetComponent<PlayerAbility>();
            player2HealthScript = player2Copy.GetComponent<PlayerHealth>();
            player2HealthScript.titleScreenManager = this;
            player2HealthScript.titleScreen = true;
            player2HealthScript.PickUp(100);


            player2SpriteManager = player2Copy.GetComponent<SpriteManager>();
            player2SpriteManager.numPlayer = 2;
            player2SpriteManager.ShowPlayer();


            SetTeamCount(player2SpriteManager, player2TeamCount);
            SetOffensiveSystem(RandomNum(false), 2);
            SetDefensiveSystem(RandomNum(true), 2);

            StartCoroutine(Player2UI());
        }

        if (rWPlayer3.GetButtonDown("AButton") && !player3Active)
        {
            playerCount++;
            numHolder.numConnectedPlayers++;

            player3Active = true;
            p3Text.SetActive(false);
            p3Portal.enabled = true;

            player3Copy = Instantiate(player3, player3StartPos, Quaternion.identity) as GameObject;
            player3MoveScript = player3Copy.GetComponent<PlayerMovement>();
            player3TeamCount = 2;
            player3ShootScript = player3Copy.GetComponent<PlayerShoot>();
            player3MoveScript.classSelect = true;
            player3AbilityScript = player3Copy.GetComponent<PlayerAbility>();

            player3HealthScript = player3Copy.GetComponent<PlayerHealth>();
            player3HealthScript.titleScreenManager = this;
            player3HealthScript.titleScreen = true;
            player3HealthScript.PickUp(100);


            player3SpriteManager = player3Copy.GetComponent<SpriteManager>();
            player3SpriteManager.numPlayer = 3;
            player3SpriteManager.ShowPlayer();


            SetTeamCount(player3SpriteManager, 2);
            SetOffensiveSystem(RandomNum(false), 3);
            SetDefensiveSystem(RandomNum(true), 3);

            StartCoroutine(Player3UI());
        }

        if (rWPlayer4.GetButtonDown("AButton") && !player4Active)
        {
            playerCount++;
            numHolder.numConnectedPlayers++;

            player4Active = true;
            p4Text.SetActive(false);
            p4Portal.enabled = true;

            player4Copy = Instantiate(player4, player4StartPos, Quaternion.identity) as GameObject;
            player4MoveScript = player4Copy.GetComponent<PlayerMovement>();
            player4TeamCount = 3;
            player4ShootScript = player4Copy.GetComponent<PlayerShoot>();
            player4MoveScript.classSelect = true;
            player4AbilityScript = player4Copy.GetComponent<PlayerAbility>();
            player4HealthScript = player4Copy.GetComponent<PlayerHealth>();
            player4HealthScript.titleScreenManager = this;
            player4HealthScript.titleScreen = true;
            player4HealthScript.PickUp(100);

            player4SpriteManager = player4Copy.GetComponent<SpriteManager>();
            player4SpriteManager.numPlayer = 4;
            player4SpriteManager.ShowPlayer();


            SetTeamCount(player4SpriteManager, 3);
            SetOffensiveSystem(RandomNum(false), 4);
            SetDefensiveSystem(RandomNum(true), 4);

            StartCoroutine(Player4UI());
        }
    }

    void MonitorShakeScreen()
    {
        if (screenShakeActive)
        {
            shakePos = Random.insideUnitCircle * shakeAmount;
            Vector3 newShakePos = new Vector3(cameraGO.transform.position.x + shakePos.x, cameraGO.transform.position.y + shakePos.y, -10f);
            cameraGO.transform.position = Vector3.Lerp(zeroPos, newShakePos, 2f * Time.deltaTime);
        }
        else
        {
            cameraGO.transform.position = zeroPos;
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

    void SetTeamCount(SpriteManager spriteManager, int thisTeamCount)
    {
        spriteManager.teamCount = thisTeamCount;
        spriteManager.SpriteSet();
    }

    public void SetOffensiveSystem(int system, int player)
    {
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
        if (player == 1)
        {
            SetAbilitySprite(player1SpriteManager, system);
            p1DefenseSys = system;
            player1AbilityScript.defenseSystem = system;
        }
        else if (player == 2)
        {
            SetAbilitySprite(player2SpriteManager, system);
            p2DefenseSys = system;
            player2AbilityScript.defenseSystem = system;
        }
        else if (player == 3)
        {
            SetAbilitySprite(player3SpriteManager, system);
            p3DefenseSys = system;
            player3AbilityScript.defenseSystem = system;
        }
        else //player == 4
        {
            SetAbilitySprite(player4SpriteManager, system);
            p4DefenseSys = system;
            player4AbilityScript.defenseSystem = system;
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
}
