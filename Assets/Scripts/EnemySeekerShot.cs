using UnityEngine;
using System.Collections;

public class EnemySeekerShot : MonoBehaviour {

    public int teamCount;
    public int seekerDamage;
    public GameObject shotDeathParticles;
    public SpriteRenderer mySprite;
    public ParticleSystem myParticleTrail;

    private PlayerHealth enemyHealthScript;
    private Asteroid asteroidScript;

    //Shot scripts
    private RemoteLauncherShot rocketScript;
    private ProtoShotPrefab shotScript;
    private LineSniperShot bulletScript;
    private EnemySeekerShot missileShotScript;
    //OtherAbilities
    private ShieldMine mineScript;

    private GameObject particlesGO;
    private ParticleSystem explosionParticles;
    
    
    //AI
    private EnemyHealth aiHealthScript;
    
    void Awake()
    {
        mySprite = GetComponentInChildren<SpriteRenderer>();
        myParticleTrail = GetComponentInChildren<ParticleSystem>();
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Boundary" || other.tag == "MineralPickup" || other.tag == "Capsule" || other.tag == "Edge" || other.tag == "AutoTurret")
        {
            //Debug.Log("hit boundary/mineralpickup/capsule/edge");
            return;
        }
        else if (other.tag == "BoostHit")
        {
            enemyHealthScript = other.GetComponentInParent<PlayerHealth>();
            if (enemyHealthScript.teamCount != teamCount)
            {
                enemyHealthScript.Damage(seekerDamage, false);
                DestroyThis();
            }
        }
        else if (other.tag == "Missile")
        {
            missileShotScript = other.GetComponent<EnemySeekerShot>();
            if (missileShotScript.teamCount != teamCount)
            {
                missileShotScript.DestroyThis();
                DestroyThis();
            }
        }
        else if (other.tag == "Player1(Black)" || other.tag == "Player2(White)" || other.tag == "Player3" || other.tag == "Player4")
        {
            //Debug.Log("ShotHit " + other.tag);
            enemyHealthScript = other.GetComponent<PlayerHealth>();
            if (enemyHealthScript.teamCount != teamCount)
            {
                enemyHealthScript.Damage(seekerDamage, false);
                DestroyThis();
            }
        }
        else if (other.tag == "Enemy")
        {
            aiHealthScript = other.GetComponent<EnemyHealth>();
            if (aiHealthScript.teamCount != teamCount)
            {
                aiHealthScript.DamageEnemy(seekerDamage);
                DestroyThis();
            }
        }
        else if (other.tag == "PlayerPhysicsCollider")
        {
            //Debug.Log("ShotHit " + other.tag);
            enemyHealthScript = other.GetComponentInParent<PlayerHealth>();
            if (enemyHealthScript.teamCount != teamCount)
            {
                enemyHealthScript.Damage(seekerDamage, false);
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
            asteroidScript.DamageAsteroid(seekerDamage);
            DestroyThis();
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

    public void DestroyThis()
    {
        particlesGO = Instantiate(shotDeathParticles, transform.position, Quaternion.identity)as GameObject;
        explosionParticles = particlesGO.GetComponent<ParticleSystem>();
        explosionParticles.startColor = mySprite.color;
        Destroy(this.gameObject);
    }
}
