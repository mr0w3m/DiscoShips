using UnityEngine;
using System.Collections;

public class GrenadeExplosion : MonoBehaviour {

    public int grenadeDamage;
    public SpriteRenderer mySprite;
    public ParticleSystem myParticles;
    public int teamCount;
    public float range;


    private Animator explosionAnim;
    private PlayerHealth playerHealthScript;
    private Asteroid asteroidScript;

    private RemoteLauncherShot rocketScript;
    private LineSniperShot bulletScript;
    private ProtoShotPrefab shotScript;
    private DamageBug damageBugScript;
    private AutoTurret autoTurretScript;
    private TripMine tripMineScript;
    private ShieldMine mineScript;

    private JunkHealth junkScript;

    void Awake()
    {
        mySprite = GetComponentInChildren<SpriteRenderer>();
        myParticles = GetComponent<ParticleSystem>();
        explosionAnim = GetComponentInChildren<Animator>();
    }

    void Start()
    {
        BlowUp();
        explosionAnim.SetTrigger("gravHaultActivate");
    }

    void BlowUp()
    {
        if (this != null)
        {
            Collider2D[] hitThings = Physics2D.OverlapCircleAll(transform.position, range);
            if (hitThings != null)
            {
                for (int i = 0; i < hitThings.Length; i++)
                {
                    if (hitThings[i].tag == "Player1(Black)" || hitThings[i].tag == "Player2(White)" || hitThings[i].tag == "Player3" || hitThings[i].tag == "Player4")
                    {
                        playerHealthScript = hitThings[i].GetComponent<PlayerHealth>();
                        if (playerHealthScript.teamCount != teamCount)
                        {
                            playerHealthScript.Damage(grenadeDamage, false);
                        }
                    }
                    else if (hitThings[i].tag == "Asteroid")
                    {
                        asteroidScript = hitThings[i].GetComponent<Asteroid>();
                        asteroidScript.DamageAsteroid(grenadeDamage);
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
                            damageBugScript.DamageThis(grenadeDamage);
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
                                autoTurretScript.DamageThis(grenadeDamage);
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
                        junkScript.DamageJunk(grenadeDamage);
                    }
                }
            }
            Destroy(this.gameObject, 3f);
        }
    }
}
