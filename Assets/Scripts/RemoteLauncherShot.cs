using UnityEngine;
using System.Collections;

public class RemoteLauncherShot : MonoBehaviour {

    public int teamCount;
    public bool notMoving;

    public GameObject remoteShotExplosionParticles;
    private GameObject explosionParticlesCopy;
    private Explosion explodeScript;
    public float blastRadius;
    public int blastDamage;
    public int directHitDamage;

    private bool exploded;

    //OtherShots
    private RemoteLauncherShot rocketScript;
    private LineSniperShot bulletScript;
    private ProtoShotPrefab shotScript;
    private PhaseBlast phaseBlastScript;
    private Grenade grenadeScript;
    private Plasma plasmaScript;
    private DamageBug damageBugScript;
    private AutoTurret autoTurretScript;
    private TripMine tripMineScript;
    private ShieldMine mineScript;
    private JunkHealth junkScript;

    private PlayerHealth enemyHealthScript;
    public PlayerShoot myShotScript;
    private Asteroid asteroidScript;

    public GameObject particleTrail1;
    public GameObject particleTrail2;
    public GameObject particleTrail3;
    public GameObject particleTrail4;

    public SpriteRenderer rocketImage;
    private Rigidbody2D rb2D;
    private PolygonCollider2D myCollider;

    private Vector2 stopVelocity = new Vector2(0,0);

    //New Movement
    public Transform target; //this is the target we always move towards.
    public float moveSpeed;
    public float smoothing;
    
    private float timer;
    //private int counter;

    private int rapidShotHitCount;
    public int totalShotHit;
    private int plasmaHitCount;
    public int totalPlasmaHit;

    //Particles
    public ParticleSystem particleSys;

    //AI
    private EnemyHealth aiHealthScript;
    

    void Awake()
    {
        particleSys = GetComponentInChildren<ParticleSystem>();
        rocketImage = GetComponent<SpriteRenderer>();
        rb2D = GetComponent<Rigidbody2D>();
        myCollider = GetComponent<PolygonCollider2D>();
    }

    void Start()
    {
        notMoving = false;
        myShotScript.rocketActive = true;
    }

    void Update()
    {
        if (notMoving == false && target != null)
        {
            transform.up = Vector2.Lerp(transform.up, target.position - transform.position, smoothing * Time.deltaTime);
            
            timer += Time.deltaTime;
            if (timer > .1)
            {
                //counter++;
                timer = 0;
                //1/10 of a second has passed
                rb2D.AddForce(transform.up * moveSpeed);
            }
        }

        if (notMoving == true)
        {
            rb2D.velocity = stopVelocity;
            rb2D.angularVelocity = 0;
            Destroy(particleTrail1);
            Destroy(particleTrail2);
            Destroy(particleTrail3);
            Destroy(particleTrail4);
        }
    }

    public void ExplosiveTrigger()
    {
        BlowUp();
    }

    void BlowUp()
    {
        if (!exploded)
        {
            exploded = true;
            if (this != null)
            {
                Collider2D[] hitThings = Physics2D.OverlapCircleAll(transform.position, blastRadius);
                if (hitThings != null)
                {
                    for (int i = 0; i < hitThings.Length; i++)
                    {
                        if (hitThings[i].tag == "Player1(Black)" || hitThings[i].tag == "Player2(White)" || hitThings[i].tag == "Player3" || hitThings[i].tag == "Player4")
                        {
                            enemyHealthScript = hitThings[i].GetComponent<PlayerHealth>();
                            if (enemyHealthScript.teamCount != teamCount)
                            {
                                enemyHealthScript.Damage(blastDamage, false);
                            }
                        }
                        else if (hitThings[i].tag == "Enemy")
                        {
                            aiHealthScript = hitThings[i].GetComponent<EnemyHealth>();
                            aiHealthScript.DamageEnemy(blastDamage);
                        }
                        else if (hitThings[i].tag == "Asteroid")
                        {
                            asteroidScript = hitThings[i].GetComponent<Asteroid>();
                            asteroidScript.DamageAsteroid(blastDamage);
                        }
                        else if (hitThings[i].tag == "RocketAmmo")
                        {
                            rocketScript = hitThings[i].GetComponent<RemoteLauncherShot>();
                            if (rocketScript.teamCount != teamCount)
                            {
                                rocketScript.DestroyThis();
                            }
                        }
                        else if (hitThings[i].tag == "DamageBug")
                        {
                            damageBugScript = hitThings[i].transform.GetComponent<DamageBug>();
                            if (damageBugScript.teamCount != teamCount)
                            {
                                //damage the bug
                                damageBugScript.DamageThis(blastDamage);
                            }
                        }
                        else if (hitThings[i].tag == "AutoTurretHitBox")
                        {
                            autoTurretScript = hitThings[i].transform.GetComponentInParent<AutoTurret>();
                            if (autoTurretScript != null)
                            {
                                if (autoTurretScript.teamCount != teamCount)
                                {
                                    //damage the Turret
                                    autoTurretScript.DamageThis(blastDamage);
                                }
                            }
                        }
                        else if (hitThings[i].tag == "TripMine")
                        {
                            tripMineScript = hitThings[i].transform.GetComponent<TripMine>();
                            if (tripMineScript.teamCount != teamCount)
                            {
                                //Blow up tripMine
                                tripMineScript.BlowUp();
                            }
                        }
                        else if (hitThings[i].tag == "Junk")
                        {
                            junkScript = hitThings[i].GetComponent<JunkHealth>();
                            junkScript.DamageJunk(blastDamage);
                        }
                    }
                    DestroyThis();
                }
                else
                {
                    //Debug.Log("hit nothin");
                }
                DestroyThis();
            }
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Boundary" || other.tag == "MineralPickup" || other.tag == "Capsule" || other.tag == "Edge" || other.tag == "BoostHit" || other.tag == "AutoTurret")
        {
            return;
        }
        else if (other.tag == "Player1(Black)" || other.tag == "Player2(White)" || other.tag == "Player3" || other.tag == "Player4")
        {
            enemyHealthScript = other.GetComponent<PlayerHealth>();
            if (enemyHealthScript.teamCount != teamCount)
            {
                BlowUp();
                DestroyThis();
            }
        }
        else if (other.tag == "PlayerPhysicsCollider")
        {
            enemyHealthScript = other.GetComponentInParent<PlayerHealth>();
            if (enemyHealthScript.teamCount != teamCount)
            {
                BlowUp();
                DestroyThis();
            }
        }
        else if (other.tag == "RocketAmmo")
        {
            rocketScript = other.GetComponent<RemoteLauncherShot>();
            if (rocketScript.teamCount != teamCount)
            {
                BlowUp();
                DestroyThis();
            }
        }
        else if (other.tag == "SniperBullet")
        {
            bulletScript = other.GetComponent<LineSniperShot>();
            if (bulletScript.teamCount != teamCount)
            {
                BlowUp();
                DestroyThis();
            }
        }
        else if (other.tag == "Shot")
        {
            shotScript = other.GetComponent<ProtoShotPrefab>();
            if (shotScript.teamCount != teamCount)
            {
                rapidShotHitCount++;
                if (rapidShotHitCount > totalShotHit)
                {
                    BlowUp();
                    DestroyThis();
                }
                else
                {
                    return;
                }
            }
        }
        else if (other.tag == "ShieldMine")
        {
            mineScript = other.GetComponent<ShieldMine>();
            if (mineScript.teamCount != teamCount)
            {
                BlowUp();
                DestroyThis();
            }
        }
        else if (other.tag == "Plasma")
        {
            /*
            plasmaScript = other.GetComponent<Plasma>();
            if (plasmaScript.teamCount != teamCount)
            {
                plasmaHitCount++;
                if (plasmaHitCount > totalPlasmaHit)
                {
                    BlowUp();
                    DestroyThis();
                }
                else
                {
                    return;
                }
            }
            */
        }
        else if (other.tag == "PhaseBlast")
        {
            phaseBlastScript = other.GetComponent<PhaseBlast>();
            if (phaseBlastScript.teamCount != teamCount)
            {
                BlowUp();
                DestroyThis();
            }
        }
        else if (other.tag == "Grenade")
        {
            grenadeScript = other.GetComponent<Grenade>();
            if (grenadeScript.teamCount != teamCount)
            {
                BlowUp();
                DestroyThis();
            }
        }
        else if (other.tag == "Asteroid")
        {
            BlowUp();
            DestroyThis();
        }
        else
        {
            BlowUp();
            DestroyThis();
        }
        /*
        myShotScript.newRemoteLauncherCoolDown = myShotScript.newRemoteLauncherCoolDownTime;
        myShotScript.rocketFired = false;
        */
    }

    public void DestroyThis()
    {
        explosionParticlesCopy = Instantiate(remoteShotExplosionParticles, transform.position, Quaternion.identity)as GameObject;
        explodeScript = explosionParticlesCopy.GetComponent<Explosion>();
        explodeScript.system1.startColor = rocketImage.color;
        //explodeScript.system2.startColor = rocketImage.color;
        rb2D.velocity = stopVelocity;
        rb2D.angularVelocity = 0;
        notMoving = true;
        rocketImage.enabled = false;
        myCollider.enabled = false;
        myShotScript.rocketActive = false;
        Destroy(this.gameObject, 3f);
    }
}
