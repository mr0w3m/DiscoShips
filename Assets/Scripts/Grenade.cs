using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Grenade : MonoBehaviour {

    public int grenadeDamage;
    public int teamCount;
    public float range;
    public float explosionCoolDownTime;
    private float explosionCoolDown;

    public GameObject grenadeExplosion;
    private GameObject grenadeExplosionCopy;

    private GrenadeExplosion explosionScript;
    private PlayerHealth playerHealthScript;

    private RemoteLauncherShot rocketScript;
    private LineSniperShot bulletScript;
    private ProtoShotPrefab shotScript;
    private DamageBug damageBugScript;
    private AutoTurret autoTurretScript;
    private TripMine tripMineScript;
    private ShieldMine mineScript;
    private Plasma plasmaScript;
    private EnemySeekerShot seekerShotScript;

    public SpriteRenderer mySprite;
    public ParticleSystem myParticles;

    public Image coolDownImage;

    void Awake()
    {
        mySprite = GetComponent<SpriteRenderer>();
        myParticles = GetComponent<ParticleSystem>();
        explosionCoolDown = explosionCoolDownTime;
    }

    void Update()
    {
        if (explosionCoolDown > 0)
        {
            explosionCoolDown -= Time.deltaTime;
        }
        else
        {
            explosionCoolDown = 0;
            BlowUp();
        }

        DisplayCoolDown(explosionCoolDown, explosionCoolDownTime);
    }
    
    void DisplayCoolDown(float abilityCoolDown, float abilityCoolDownTime)
    {
        float fillNum; //num for final float fill amount
        fillNum = abilityCoolDown / abilityCoolDownTime; //This is the percentage of where the cooldown is at
        //fillNum = Mathf.Abs(fillNum - 1);
        coolDownImage.fillAmount = fillNum; //Set the fill amount
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player1(Black)" || other.tag == "Player2(White)" || other.tag == "Player3" || other.tag == "Player4")
        {
            //if we hit a player not our team Explode
            playerHealthScript = other.GetComponent<PlayerHealth>();
            if (playerHealthScript.teamCount != teamCount)
            {
                BlowUp();
            }
        }
        else if (other.tag == "Asteroid")
        {
            BlowUp();
        }
        else if (other.tag == "RocketAmmo")
        {
            rocketScript = other.GetComponent<RemoteLauncherShot>();
            if (rocketScript.teamCount != teamCount)
            {
                BlowUp();
            }
        }
        else if (other.tag == "SniperBullet")
        {
            bulletScript = other.GetComponent<LineSniperShot>();
            if (bulletScript.teamCount != teamCount)
            {
                BlowUp();
            }
        }
        else if (other.tag == "Shot")
        {
            shotScript = other.GetComponent<ProtoShotPrefab>();
            if (shotScript.teamCount != teamCount)
            {
                BlowUp();
            }
        }
        else if (other.tag == "ShieldMine")
        {
            mineScript = other.GetComponent<ShieldMine>();
            if (mineScript.teamCount != teamCount)
            {
                BlowUp();
            }
        }/*
        else if (other.tag == "Plasma")
        {
            plasmaScript = other.GetComponent<Plasma>();
            if (plasmaScript.teamCount != teamCount)
            {
                BlowUp();
            }
        }*/
        else if (other.tag == "DamageBug")
        {
            damageBugScript = other.GetComponent<DamageBug>();
            if (damageBugScript.teamCount != teamCount)
            {
                BlowUp();
            }
        }
        else if (other.tag == "Missile")
        {
            seekerShotScript = other.GetComponent<EnemySeekerShot>();
            if (seekerShotScript.teamCount != teamCount)
            {
                BlowUp();
            }
        }
        else if (other.tag == "TripMine")
        {
            tripMineScript = other.GetComponent<TripMine>();
            if (tripMineScript.teamCount != teamCount)
            {
                BlowUp();
            }
        }
        else if (other.tag == "AutoTurretHitBox")
        {
            autoTurretScript = other.GetComponentInParent<AutoTurret>();
            if (autoTurretScript.teamCount != teamCount)
            {
                BlowUp();
            }
        }
        else if (other.tag == "Junk")
        {
            BlowUp();
        }
    }

    void OnCollisionEnter2D(Collision2D coll)
    {
        explosionCoolDown = explosionCoolDown / 2;
    }

    public void BlowUp()
    {
        //We can have this instantiate the explosion
        grenadeExplosionCopy = Instantiate(grenadeExplosion, transform.position, Quaternion.identity) as GameObject;
        explosionScript = grenadeExplosionCopy.GetComponent<GrenadeExplosion>();
        explosionScript.teamCount = teamCount;
        explosionScript.grenadeDamage = grenadeDamage;
        explosionScript.range = range;
        explosionScript.mySprite.color = mySprite.color;
        explosionScript.myParticles.startColor = mySprite.color;
        Destroy(this.gameObject);
    }
}
