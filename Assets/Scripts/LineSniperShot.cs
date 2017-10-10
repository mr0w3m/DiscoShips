using UnityEngine;
using System.Collections;

public class LineSniperShot : MonoBehaviour {

    public int teamCount; //We should leave this teamCount thing.
    public int lineSniperShotDamage;
    public GameObject deathParticles;
    public bool notMoving;

    public int pierceCount;

    //Shot scripts
    private RemoteLauncherShot rocketScript;
    private ProtoShotPrefab shotScript;
    private LineSniperShot bulletScript;
    //OtherAbilities
    private ShieldMine mineScript;

    private PlayerHealth enemyHealthScript;
    public PlayerHealth myShotScript;

    private Rigidbody2D rb2D;

    private Asteroid asteroidScript;

    public GameObject particleTrail;

    private ParticleSystem deathParticleSys;
    private GameObject instance;

    public Color color;

    private Vector2 stopVelocity = new Vector2(0, 0);

    //colors
    public SpriteRenderer shotSprite;
    public ParticleSystem particleSys;

    //AI
    private EnemyHealth aiHealthScript;

    void Awake()
    {
        rb2D = GetComponent<Rigidbody2D>();
        shotSprite = GetComponent<SpriteRenderer>();
        particleSys = GetComponentInChildren<ParticleSystem>();
    }

    void Update()
    {
        if (notMoving == true)
        {
            rb2D.velocity = stopVelocity;
            rb2D.angularVelocity = 0;
            //Destroy(particleTrail);
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Boundary" || other.tag == "MineralPickup" || other.tag == "Capsule" || other.tag == "Edge" || other.tag == "BoostHit" || other.tag == "AutoTurret")
        {
            //Debug.Log("hit boundary/mineralpickup/capsule/edge");
            return;
        }
        else if (other.tag == "Player1(Black)" || other.tag == "Player2(White)" || other.tag == "Player3" || other.tag == "Player4")
        {
            //Debug.Log("SniperHit " + other.tag);
            enemyHealthScript = other.GetComponent<PlayerHealth>();
            if (enemyHealthScript.teamCount != teamCount)
            {
                enemyHealthScript.Damage(lineSniperShotDamage, false);
                DestroyThis();
            }
        }
        else if (other.tag == "Enemy")
        {
            aiHealthScript = other.GetComponent<EnemyHealth>();
            aiHealthScript.DamageEnemy(lineSniperShotDamage);
            DestroyThis();
        }
        else if (other.tag == "PlayerPhysicsCollider")
        {
            //Debug.Log("SniperHit " + other.tag);
            enemyHealthScript = other.GetComponentInParent<PlayerHealth>();
            if (enemyHealthScript.teamCount != teamCount)
            {
                enemyHealthScript.Damage(lineSniperShotDamage, false);
                DestroyThis();
            }
        }
        else if (other.tag == "RocketAmmo")
        {
            rocketScript = other.GetComponent<RemoteLauncherShot>();
            if (rocketScript.teamCount != teamCount)
            {
                DestroyThis();
            }
        }
        else if (other.tag == "SniperBullet")
        {
            bulletScript = other.GetComponent<LineSniperShot>();
            if (bulletScript.teamCount != teamCount)
            {
                DestroyThis();
            }
        }
        else if (other.tag == "Shot")
        {
            shotScript = other.GetComponent<ProtoShotPrefab>();
            if (shotScript.teamCount != teamCount)
            {
                DestroyThis();
            }
        }
        else if (other.tag == "ShieldMine")
        {
            mineScript = other.GetComponent<ShieldMine>();
            if (mineScript.teamCount != teamCount)
            {
                DestroyThis();
            }
        }
        else if (other.tag == "Asteroid")
        {
            //Debug.Log("hit asteroid");
            asteroidScript = other.GetComponent<Asteroid>();
            asteroidScript.DamageAsteroid(lineSniperShotDamage);
            pierceCount -= 1;
            instance = Instantiate(deathParticles, transform.position, Quaternion.identity)as GameObject;
            deathParticleSys = instance.GetComponent<ParticleSystem>();
            deathParticleSys.startColor = color;
            if (pierceCount < 1)
            {
                DestroyThis();
            }
        }
        else if (other.tag == "Plasma")
        {

        }
        else if (other.tag == "PhaseBlast")
        {

        }
        else
        {
            //Debug.Log("hit " + other.tag);
            DestroyThis();
        }
    }

    void DestroyThis()
    {
        particleTrail.transform.SetParent(null);
        particleSys.enableEmission = false;
        instance = Instantiate(deathParticles, transform.position, Quaternion.identity)as GameObject;
        deathParticleSys = instance.GetComponent<ParticleSystem>();
        deathParticleSys.startColor = color;
        Destroy(this.gameObject);
    }
}
