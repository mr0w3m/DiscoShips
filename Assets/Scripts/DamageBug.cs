using UnityEngine;
using System.Collections;

public class DamageBug : MonoBehaviour {

    public int damagePerSecond;
    public int teamCount;
    public int healthCount;
    public SpriteRenderer mySprite;
    public SpriteRenderer healthSprite;
    public Collider2D physicsCollider;
    public GameObject healthCircle;

    public GameObject destroyFx;
    private GameObject destroyFxCopy;
    private ParticleSystem destroyFxParticles;

    private float damageCoolDown;
    private bool attached;

    private PlayerHealth enemyHealthScript;
    private PlayerHealth toDamageHealthScript;
    private ShieldMine mineScript;
    private ProtoShotPrefab shotScript;
    private LineSniperShot bulletScript;
    private EnemySeekerShot seekerShotScript;
    private PhaseBlast phaseBlastScript;
    private Plasma plasmaScript;

    public ParticleSystem moveTrail;
    public ParticleSystem damageSparks;


    public Rigidbody2D rb2D;

    //We need to have the remoteLauncher radius damage this from its' script
    //Also the laser rifle

    void Awake()
    {
        rb2D = GetComponent<Rigidbody2D>();
        mySprite = GetComponent<SpriteRenderer>();
        healthSprite = healthCircle.GetComponent<SpriteRenderer>();
    }

    void Start()
    {
        moveTrail.Play();
    }

    void FixedUpdate()
    {
        if (attached)
        {
            DamagePlayer();
            //Emit particles of damaging
        }
    }

    void Update()
    {
        DisplayHealth();
    }

    public void DamageThis(int amountDamaged)
    {
        healthCount -= amountDamaged;
        if (healthCount < 1)
        {
            DestroyThis();
        }
    }

    void DamagePlayer()
    {
        if (damageCoolDown > 0)
        {
            damageCoolDown -= Time.deltaTime;
        }
        else
        {
            damageCoolDown = 0;
        }

        if (damageCoolDown == 0)
        {
            if (!toDamageHealthScript.dummy)
            {
                damageSparks.startColor = toDamageHealthScript.healthCircle.color;
                damageSparks.Play();
            }
            toDamageHealthScript.Damage(damagePerSecond, false);
            damageCoolDown = 1;
        }
    }

    public void DisplayHealth()
    {
        float fillNum; //num for final float fill amount
        fillNum = (float)healthCount * 0.1f; //multiply by .01 to put the 1/100 value to 0.00/1.00
        healthCircle.transform.localScale = new Vector3(fillNum, fillNum, fillNum);
    }

    void LatchToPlayer(GameObject player, Vector2 normalPoint)
    {
        attached = true;
        //Parent to,
        transform.SetParent(player.transform);
        rb2D.isKinematic = true;
        physicsCollider.enabled = false;
        //Face player
        float angleDir = Mathf.Atan2(normalPoint.y, normalPoint.x) * Mathf.Rad2Deg;
        angleDir -= 270;
        transform.eulerAngles = new Vector3(transform.eulerAngles.x, transform.eulerAngles.y, angleDir);
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag == "Player1(Black)" || other.gameObject.tag == "Player2(White)" || other.gameObject.tag == "Player3" || other.gameObject.tag == "Player4")
        {
            enemyHealthScript = other.gameObject.GetComponent<PlayerHealth>();
            if (enemyHealthScript.teamCount != teamCount)
            {
                toDamageHealthScript = enemyHealthScript;
                Vector2 vector;
                foreach (ContactPoint2D contact in other.contacts)
                {
                    vector = contact.normal;
                    //latchtoPlayer
                    LatchToPlayer(other.gameObject, vector);
                }
            }
        }
        else if (other.gameObject.tag == "PlayerPhysicsCollider")
        {
            enemyHealthScript = other.gameObject.GetComponent<PlayerHealth>();
            if (enemyHealthScript.teamCount != teamCount)
            {
                toDamageHealthScript = enemyHealthScript;
                Vector2 vector;
                foreach (ContactPoint2D contact in other.contacts)
                {
                    vector = contact.normal;
                    //latchtoPlayer
                    LatchToPlayer(other.gameObject, vector);
                }
            }
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Boundary" || other.tag == "MineralPickup" || other.tag == "Capsule" || other.tag == "Edge" || other.tag == "BoostHit")
        {
            return;
        }
        else if (other.tag == "SniperBullet")
        {
            bulletScript = other.GetComponent<LineSniperShot>();
            if (bulletScript.teamCount != teamCount)
            {
                //Damage the bug
                DamageThis(bulletScript.lineSniperShotDamage);
            }
        }
        else if (other.tag == "Shot")
        {
            shotScript = other.GetComponent<ProtoShotPrefab>();
            if (shotScript.teamCount != teamCount)
            {
                //Damage this bug
                DamageThis(shotScript.shotDamage);
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
        else if (other.tag == "Missile")
        {
            seekerShotScript = other.GetComponent<EnemySeekerShot>();
            if (seekerShotScript.teamCount != teamCount)
            {
                //Damage
                DamageThis(seekerShotScript.seekerDamage);
            }
        }
        else if (other.tag == "PhaseBlast")
        {
            phaseBlastScript = other.GetComponent<PhaseBlast>();
            if (phaseBlastScript.teamCount != teamCount)
            {
                DamageThis(phaseBlastScript.phaseBlastDamage);
            }
        }
        else if (other.tag == "Plasma")
        {
            plasmaScript = other.GetComponent<Plasma>();
            if (plasmaScript.teamCount != teamCount)
            {
                DamageThis(plasmaScript.plasmaDamage);
            }
        }
    }

    public void DestroyThis()
    {
        destroyFxCopy = Instantiate(destroyFx, transform.position, Quaternion.identity) as GameObject;
        destroyFxParticles = destroyFxCopy.GetComponent<ParticleSystem>();
        destroyFxParticles.startColor = mySprite.color;
        //destroy this bug
        Destroy(this.gameObject);
        //emit death particles
        Debug.Log("bugdestroyed");
    }
}
