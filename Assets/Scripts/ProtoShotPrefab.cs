using UnityEngine;
using System.Collections;

public class ProtoShotPrefab : MonoBehaviour
{
    public int teamCount;
    public int shotDamage;
    public Color color;

    public int shotType; //0 - rapidShot | 1 - needleGatling

    public GameObject needleDeathParticles;
    public GameObject shotDeathParticles;
    private ParticleSystem deathParticleSys;
    private GameObject instance;

    private PlayerHealth enemyHealthScript;
    private Asteroid asteroidScript;

    //Shot scripts
    private RemoteLauncherShot rocketScript;
    private ProtoShotPrefab shotScript;
    private LineSniperShot bulletScript;
    //OtherAbilities
    private ShieldMine mineScript;

    //Color
    public ParticleSystem particleSys;
    public SpriteRenderer shotSprite;

    //AI
    private EnemyHealth aiHealthScript;

    void Awake()
    {
        shotSprite = GetComponent<SpriteRenderer>();
        particleSys = GetComponent<ParticleSystem>();
        
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
            //Debug.Log("ShotHit " + other.tag);
            enemyHealthScript = other.GetComponent<PlayerHealth>();
            if (enemyHealthScript.teamCount != teamCount)
            {
                enemyHealthScript.Damage(shotDamage, false);
                DestroyThis();
            }
        }
        else if (other.tag == "Enemy")
        {
            aiHealthScript = other.GetComponent<EnemyHealth>();
            if (aiHealthScript.teamCount != teamCount)
            {
                aiHealthScript.DamageEnemy(shotDamage);
                DestroyThis();
            }
        }
        else if (other.tag == "PlayerPhysicsCollider")
        {
            //Debug.Log("ShotHit " + other.tag);
            enemyHealthScript = other.GetComponentInParent<PlayerHealth>();
            if (enemyHealthScript.teamCount != teamCount)
            {
                enemyHealthScript.Damage(shotDamage, false);
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
            asteroidScript.DamageAsteroid(shotDamage);
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
            //Debug.Log("hit " +other.tag);
            DestroyThis();
        }
    }

    void DestroyThis()
    {
        if (shotType == 0)
        {
            instance = Instantiate(shotDeathParticles, transform.position, Quaternion.identity) as GameObject;
            deathParticleSys = instance.GetComponent<ParticleSystem>();
            deathParticleSys.startColor = color;
        }
        else if (shotType == 1)
        {
            instance = Instantiate(needleDeathParticles, transform.position, Quaternion.identity) as GameObject;
            deathParticleSys = instance.GetComponent<ParticleSystem>();
            deathParticleSys.startColor = color;
        }

        Destroy(this.gameObject);
    }
}
