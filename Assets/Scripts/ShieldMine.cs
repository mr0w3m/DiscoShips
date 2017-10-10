using UnityEngine;
using System.Collections;

public class ShieldMine : MonoBehaviour {

    public int teamCount;
    public Vector2 goToPosition;

    public float smoothValue;
    public int mineDamage;
    public Color myColor;
    public float entranceSmoothing;

    public int hitCount;

    public GameObject deathParticles;
    private GameObject deathParticlesCopy;
    private ParticleSystem deathParticleSystem;

    //Shot scripts
    private RemoteLauncherShot rocketScript;
    private ProtoShotPrefab shotScript;
    private LineSniperShot bulletScript;
    private EnemySeekerShot homingMissileScript;

    private AutoTurretDamage autoTurretDamageScript;

    private PlayerHealth enemyHealthScript;
    private Asteroid asteroidScript;
    private SpriteRenderer mySprite;
    private bool here;
    private Plasma plasmaScript;

    void Awake()
    {
        mySprite = GetComponent<SpriteRenderer>();
    }

    void Start()
    {
        here = true;
        hitCount = 0;
    }

    void Update()
    {
        if (here)
        {
            //Debug.Log("lerpingColor...");
            mySprite.color = Color.Lerp(mySprite.color, myColor, entranceSmoothing * Time.deltaTime);
            if (mySprite.color == myColor)
            {
                here = false;
                //Debug.Log("colorfinishedlerping");
            }
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Boundary" || other.tag == "MineralPickup" || other.tag == "Capsule" || other.tag == "Edge" || other.tag == "Map" || other.tag == "Ibeam")
        {
            return;
        }
        else if (other.tag == "RocketAmmo")
        {
            rocketScript = other.GetComponent<RemoteLauncherShot>();
            if (rocketScript.teamCount != teamCount)
            {
                //Debug.Log(teamCount + " |other: " + rocketScript.teamCount);
                if (hitCount > 1)
                {
                    Destroy(this.gameObject);
                }
                else
                {
                    hitCount++;
                }
            }
        }
        else if (other.tag == "SniperBullet")
        {
            bulletScript = other.GetComponent<LineSniperShot>();
            if (bulletScript.teamCount != teamCount)
            {
                //Debug.Log(teamCount + " |other: " + bulletScript.teamCount);
                if (hitCount > 1)
                {
                    Destroy(this.gameObject);
                }
                else
                {
                    hitCount++;
                }
            }
        }
        else if (other.tag == "Missile")
        {
            homingMissileScript = other.GetComponent<EnemySeekerShot>();
            if (homingMissileScript.teamCount != teamCount)
            {
                if (hitCount > 1)
                {
                    Destroy(this.gameObject);
                }
                else
                {
                    hitCount++;
                }
            }
        }
        else if (other.tag == "Shot")
        {
            shotScript = other.GetComponent<ProtoShotPrefab>();
            if (shotScript.teamCount != teamCount)
            {
                //Debug.Log(teamCount + " |other: " + shotScript.teamCount);
                if (hitCount > 1)
                {
                    Destroy(this.gameObject);
                }
                else
                {
                    hitCount++;
                }
            }
        }
        else if (other.tag == "Player1(Black)" || other.tag == "Player2(White)" || other.tag == "Player3" || other.tag == "Player4")
        {
            enemyHealthScript = other.GetComponent<PlayerHealth>();
            if (enemyHealthScript.teamCount != teamCount)
            {
                enemyHealthScript.Damage(mineDamage, false);
                Destroy(this.gameObject);
            }
        }
        else if (other.tag == "PlayerPhysicsCollider")
        {
            enemyHealthScript = other.GetComponentInParent<PlayerHealth>();
            if (enemyHealthScript.teamCount != teamCount)
            {
                enemyHealthScript.Damage(mineDamage, false);
                Destroy(this.gameObject);
            }
        }
        else if (other.tag == "Asteroid")
        {
            asteroidScript = other.GetComponent<Asteroid>();
            asteroidScript.DamageAsteroid(mineDamage);
            Destroy(this.gameObject);
        }
        else if (other.tag == "Plasma")
        {
            plasmaScript = other.GetComponent<Plasma>();
            if (plasmaScript.teamCount != teamCount)
            {
                if (hitCount > 1)
                {
                    Destroy(this.gameObject);
                }
                else
                {
                    hitCount++;
                }
            }
        }
    }

    void DestroyThis()
    {
        deathParticlesCopy = Instantiate(deathParticles, transform.position, Quaternion.identity) as GameObject;
        deathParticleSystem = deathParticlesCopy.GetComponent<ParticleSystem>();
        deathParticleSystem.startColor = mySprite.color;

        Destroy(this.gameObject);
    }
}
