using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class SpriteManager : MonoBehaviour {

    public int teamCount; //0 - Blue | 1 - Orange | 2 - Green | 3 - Pink
    public int numPlayer;

    //OtherReferences
    private PlayerHealth healthScript;
    private PlayerShoot shootScript;
    private PlayerAbility abilityScript;
    private PlayerMovement moveScript;


    //The numeric value represents what weapon or ability is assigned to the player.
    public int currentWeapon; //0 -RapidShot | 1 -RemoteLauncher | 2 -LineSniper | 3 -NeedleGatling | 4 -LaserRifle | 5 -HomingMissiles
    public int currentAbility; //0 -TimeShield | 1 -GravHault | 2 -ShieldMines |

    //We'll need to set the positioning later, get a reference to the game objects here to swap sprites on
    //This is because they're all over the prefab, we're consolidating here.
    public GameObject weapon;
    //public GameObject ability;
    public GameObject uiPlate;
    public GameObject spinner;

    //Reference to Sprite renderers of weapon and ability.
    //This is so we can swap out the sprite for these.
    public SpriteRenderer weaponSpriteRenderer;
    //private SpriteRenderer abilitySpriteRenderer;
    private SpriteRenderer uiSpriteRenderer;
    private SpriteRenderer spinnerSpriteRenderer;
    //HealthCircle TO CHANGE COLOR
    public SpriteRenderer uiHealthCircle;
    //AbilityMeter TO CHANGE COLOR
    public Image uiAbilityMeter;
    //UnderGlow
    public SpriteRenderer underGlow;

    //Sprite|Color Arrays
    public Color[] playerColor = new Color[4]; //For use with underglow, healing flash, weapon discharge, ability discharge
    //WeaponSprites
    public Sprite[] blueWeaponSprites = new Sprite[4];
    public Sprite[] orangeWeaponSprites = new Sprite[4];
    public Sprite[] greenWeaponSprites = new Sprite[4];
    public Sprite[] pinkWeaponSprites = new Sprite[4];
    //AbilitySprites
    /*
    public Sprite[] blueAbilitySprites = new Sprite[4];
    public Sprite[] orangeAbilitySprites = new Sprite[4];
    public Sprite[] greenAbilitySprites = new Sprite[4];
    public Sprite[] pinkAbilitySprites = new Sprite[4];
    */

    //Spinner - We should change this to an animation.
    public Sprite[] spinnerColorSprites = new Sprite[4];


    //WeaponPositions
    public Vector2[] weaponPositions = new Vector2[4];


    //AbilityPositions
    public Vector2[] rapidShotAbilityPos = new Vector2[4];
    public Vector2[] remoteLauncherAbilityPos = new Vector2[4];
    public Vector2[] lineSniperAbilityPos = new Vector2[4];
    public Vector2[] focusBeamAbilityPos = new Vector2[4];

    //AbilityRotations
    public Vector3[] rapidShotAbilityRot;
    public Vector3[] remoteLauncherAbilityRot;
    public Vector3[] lineSniperAbilityRot;
    public Vector3[] focusBeamAbilityRot;

    //UI
    public Image playerNum;
    public Text healthText;
    public Image healthBar;
    public Image abilityMeter;
    public Image glow;
    public Image uiSpinner;

    public SpriteRenderer lineRifleLaser;
    public LineRenderer laserRifleLine;
    public LineRenderer homingMissileLaser;

    //OtherFx
    private ParticleSystem moveParticles;
    private ParticleSystem boostParticles;

    //GunFx
    public ParticleSystem RsFlashLeft;
    public ParticleSystem RsFlashRight;
    public ParticleSystem RlFlash;
    public ParticleSystem LrFlash;
    public ParticleSystem laserRifleBottom;
    public ParticleSystem hMLeftSmoke;
    public ParticleSystem hMRightSmoke;


    public SpriteRenderer bottomPlate;
    public SpriteRenderer playerUI;
    public GameObject moveParticlesGO;

    void Awake()
    {
        healthScript = GetComponent<PlayerHealth>();
        shootScript = GetComponent<PlayerShoot>();
        abilityScript = GetComponent<PlayerAbility>();
        moveScript = GetComponent<PlayerMovement>();


        weaponSpriteRenderer = weapon.GetComponent<SpriteRenderer>();
        //abilitySpriteRenderer = ability.GetComponent<SpriteRenderer>();
        uiSpriteRenderer = uiPlate.GetComponent<SpriteRenderer>();
        spinnerSpriteRenderer = spinner.GetComponent<SpriteRenderer>();
    }

    void Start()
    {
        moveParticles = moveScript.moveParticleSys;
        boostParticles = moveScript.burstParticleSys;
    }

    public void HidePlayer()
    {
        moveParticlesGO.SetActive(false); //MovementParticles
        underGlow.enabled = false;
        spinnerSpriteRenderer.enabled = false;
        uiHealthCircle.enabled = false;
        uiAbilityMeter.enabled = false;
        weaponSpriteRenderer.enabled = false;
        playerUI.enabled = false;
        bottomPlate.enabled = false;
        shootScript.ShowingLights(false);
    }

    public void ShowPlayer()
    {
        moveParticlesGO.SetActive(true); //MovementParticles
        underGlow.enabled = true;
        spinnerSpriteRenderer.enabled = true;
        uiHealthCircle.enabled = true;
        uiAbilityMeter.enabled = true;
        weaponSpriteRenderer.enabled = true;
        playerUI.enabled = true;
        bottomPlate.enabled = true;
        shootScript.ShowingLights(true);
    }

    public void SpriteSet()
    {
        healthScript.teamCount = teamCount;
        shootScript.teamCount = teamCount;
        abilityScript.teamCount = teamCount;
        moveScript.teamCount = teamCount;

        SetUpSprites();
        SetUpPositions();
    }

    void SetUpSprites()
    {
        if (playerNum != null)
        {
            //UI PlayerNum
            playerNum.color = playerColor[teamCount];
        }

        if (healthText != null)
        {
            healthText.color = playerColor[teamCount];
        }

        /*
        if (healthBar != null)
        {
            healthBar.color = playerColor[teamCount];
        }
        */

        if (abilityMeter != null)
        {
            abilityMeter.color = playerColor[teamCount];
        }

        if (glow != null)
        {
            glow.color = playerColor[teamCount];
        }

        if (uiSpinner != null)
        {
            uiSpinner.color = playerColor[teamCount];
        }

        //UnderGlow
        underGlow.color = playerColor[teamCount];
        //UI
        uiHealthCircle.color = playerColor[teamCount];
        uiAbilityMeter.color = playerColor[teamCount];
        spinnerSpriteRenderer.color = playerColor[teamCount];

        lineRifleLaser.color = playerColor[teamCount];
        laserRifleLine.SetColors(playerColor[teamCount], playerColor[teamCount]);
        homingMissileLaser.SetColors(playerColor[teamCount], playerColor[teamCount]);

        //RapidShot ShotIndicators
        shootScript.rightRShotInd.color = playerColor[teamCount];
        shootScript.leftRShotInd.color = playerColor[teamCount];
        shootScript.rightRSShotLight.color = playerColor[teamCount];
        shootScript.leftRSShotLight.color = playerColor[teamCount];

        //HomingMissile ShotIndicators
        shootScript.rightMissileIndicator.color = playerColor[teamCount];
        shootScript.leftMissileIndicator.color = playerColor[teamCount];
        shootScript.rightMissileIndBottom.color = playerColor[teamCount];
        shootScript.leftMissileIndBottom.color = playerColor[teamCount];

        SetUpWeaponInd();


        //PlasmaThrower fx
        shootScript.plasmaThrowerParticles.startColor = playerColor[teamCount];

        //GrenadeGun
        shootScript.grenadeGunFx.startColor = playerColor[teamCount];

        //PhaseBlaster
        shootScript.phaseBlasterFx.startColor = playerColor[teamCount];


        //OtherFx
        abilityScript.gravHaultSprite.color = playerColor[teamCount];
        abilityScript.wallRideSprite.color = playerColor[teamCount];

        //DamageBug
        abilityScript.dBugThrowParticles.startColor = playerColor[teamCount];
        abilityScript.dBugThrowRay.color = playerColor[teamCount];

        //TractorBeam
        abilityScript.tractorBeamFxImg.color = playerColor[teamCount];
        abilityScript.tractorBeamParticles.startColor = playerColor[teamCount];

        abilityScript.abilityPlacer.SetColors(playerColor[teamCount], playerColor[teamCount]);
        

        if (moveParticles != null)
        {
            moveParticles.startColor = playerColor[teamCount];
        }

        if (boostParticles != null)
        {
            boostParticles.startColor = playerColor[teamCount];
        }

        //GunFx
        RsFlashLeft.startColor = playerColor[teamCount];
        RsFlashRight.startColor = playerColor[teamCount];
        RlFlash.startColor = playerColor[teamCount];
        LrFlash.startColor = playerColor[teamCount];

        laserRifleBottom.startColor = playerColor[teamCount];
        hMLeftSmoke.startColor = playerColor[teamCount];
        hMRightSmoke.startColor = playerColor[teamCount];


        //Weapons and abilities depend on team color, then the current equipped.
        if (teamCount == 0) //Blue
        {
            weaponSpriteRenderer.sprite = blueWeaponSprites[currentWeapon];
            //abilitySpriteRenderer.sprite = blueAbilitySprites[currentAbility];
        }
        else if (teamCount == 1) //Orange
        {
            weaponSpriteRenderer.sprite = orangeWeaponSprites[currentWeapon];
            //abilitySpriteRenderer.sprite = orangeAbilitySprites[currentAbility];
        }
        else if (teamCount == 2) //Green
        {
            weaponSpriteRenderer.sprite = greenWeaponSprites[currentWeapon];
            //abilitySpriteRenderer.sprite = greenAbilitySprites[currentAbility];
        }
        else if (teamCount == 3) //Pink
        {
            weaponSpriteRenderer.sprite = pinkWeaponSprites[currentWeapon];
            //abilitySpriteRenderer.sprite = pinkAbilitySprites[currentAbility];
        }
        else
        {
            //Debug.Log("teamCount not set");
        }

        if (currentWeapon > 2)
        {
            weaponSpriteRenderer.flipY = false;
        }
        else { weaponSpriteRenderer.flipY = true; }

        StartCoroutine(SecondarySetUpSprites());
    }

    void SetUpWeaponInd()
    {
        shootScript.launcherFiredGlow.color = playerColor[teamCount];
        shootScript.launcherFiredLight.color = playerColor[teamCount];
        shootScript.launcherActiveGlow.color = playerColor[teamCount];
        shootScript.launcherActiveLight.color = playerColor[teamCount];

        shootScript.lRLightOn.color = playerColor[teamCount];
        shootScript.lRGlow.color = playerColor[teamCount];

        shootScript.laserRGlow.color = playerColor[teamCount];
        shootScript.laserRLightOn.color = playerColor[teamCount];

        shootScript.hMFired.color = playerColor[teamCount];
        shootScript.hMFiredLight.color = playerColor[teamCount];
        shootScript.hMLaserActive.color = playerColor[teamCount];
        shootScript.hMLeftMissiles.color = playerColor[teamCount];
        shootScript.hMRightMissiles.color = playerColor[teamCount];

        shootScript.nGatlingLight.color = playerColor[teamCount];
        shootScript.nGatlingGlowA.color = playerColor[teamCount];
        shootScript.nGatlingGlowB.color = playerColor[teamCount];


        shootScript.pBFiredLight.color = playerColor[teamCount];
        shootScript.pBLightOn.color = playerColor[teamCount];
        shootScript.pBBackGlow.color = playerColor[teamCount];
        shootScript.pBFrontGlow.color = playerColor[teamCount];

        shootScript.gGBackSquareOn.color = playerColor[teamCount];
        shootScript.gGBarrelLight.color = playerColor[teamCount];
        shootScript.gGBarrelGlow.color = playerColor[teamCount];
        shootScript.gGBackGlow.color = playerColor[teamCount];

        shootScript.pTBackPackColor.color = playerColor[teamCount];
        shootScript.pTBackPackGlow.color = playerColor[teamCount];
        shootScript.pTNozzleGlow.color = playerColor[teamCount];
        shootScript.pTNozzleOn.color = playerColor[teamCount];
    }

    IEnumerator SecondarySetUpSprites()
    {
        yield return new WaitForSeconds(.5f);
        //UI PlayerNum
        playerNum.color = playerColor[teamCount];
    }

    void SetUpPositions()
    {
        //Weapons positions set
        //Debug.Log("CurrentWeapon:" + currentWeapon);
        weapon.transform.localPosition = weaponPositions[currentWeapon];

        /*
        //Abilities positions set depending on what weapon we have.
        if (currentWeapon == 0)
        {
            ability.transform.localPosition = rapidShotAbilityPos[currentAbility];
            ability.transform.localRotation = Quaternion.EulerAngles(rapidShotAbilityRot[currentAbility]);
        }
        else if (currentWeapon == 1)
        {
            ability.transform.localPosition = remoteLauncherAbilityPos[currentAbility];
            ability.transform.localRotation = Quaternion.EulerAngles(remoteLauncherAbilityRot[currentAbility]);
        }
        else if (currentWeapon == 2)
        {
            ability.transform.localPosition = lineSniperAbilityPos[currentAbility];
            ability.transform.localRotation = Quaternion.EulerAngles(lineSniperAbilityRot[currentAbility]);
        }
        else if (currentWeapon == 3)
        {
            ability.transform.localPosition = focusBeamAbilityPos[currentAbility];
            ability.transform.localRotation = Quaternion.EulerAngles(focusBeamAbilityRot[currentAbility]);
        }
        */
    }
}
