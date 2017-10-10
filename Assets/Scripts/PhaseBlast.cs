using UnityEngine;
using System.Collections;

public class PhaseBlast : MonoBehaviour {

    private PlayerHealth playerHealthScript;
    public PlayerShoot shootScript;
    private Asteroid asteroidScript;

    public int teamCount;
    public int phaseBlastDamage;
    public float expandRate;
    public SpriteRenderer mySprite;
    //public SpriteRenderer orbitSprite1;
    //public SpriteRenderer orbitSprite2;

    public GameObject phaseBlastFizzleOutFx;
    private GameObject phaseBlastFizzleOutFxCopy;
    private ParticleSystem deathParticles;

    public SpriteRenderer phaseBlastWhite;


    public ParticleSystem phaseBlastStopParticles;
    public ParticleSystem trail;
    public ParticleSystem innerTrail;

    private int damageBoost;
    private float timer;
    private Rigidbody2D rb2D;

    private float smoothing = 2f;
    private bool flash;
    private bool stop;
    public float moveSpeed;

    public void AddDrag()
    {
        shootScript.rocketActive = false;
        stop = true;
        flash = true;
        phaseBlastStopParticles.Play();
    }

    void Start()
    {
        damageBoost = 0;
        rb2D = GetComponent<Rigidbody2D>();
        phaseBlastStopParticles.startColor = mySprite.color;
        trail.startColor = mySprite.color;
        innerTrail.startColor = mySprite.color;
    }

	void Update()
    {
        //if gravHaulted it should stop and fizzle out
        if (!stop)
        {
            //transform.up = Vector2.Lerp(transform.up, target.position - transform.position, smoothing * Time.deltaTime);

            timer += Time.deltaTime;
            if (timer > .1)
            {
                //counter++;
                timer = 0;
                //1/10 of a second has passed
                rb2D.AddForce(transform.up * moveSpeed);
            }
        }

        if (flash)
        {
            phaseBlastWhite.color = Color.white;
        }
        else
        {
            phaseBlastWhite.color = Color.Lerp(phaseBlastWhite.color, Color.clear, smoothing * Time.deltaTime);
        }
        flash = false;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player1(Black)" || other.tag == "Player2(White)" || other.tag == "Player3" || other.tag == "Player4")
        {
            playerHealthScript = other.GetComponent<PlayerHealth>();
            if (playerHealthScript.teamCount != teamCount)
            {
                playerHealthScript.Damage(phaseBlastDamage + damageBoost, false);
                transform.localScale += new Vector3(expandRate, expandRate, expandRate);
                damageBoost += 2;
                flash = true;
            }
        }
        else if (other.tag == "Asteroid")
        {
            asteroidScript = other.GetComponent<Asteroid>();
            asteroidScript.DamageAsteroid(phaseBlastDamage + damageBoost);
            transform.localScale += new Vector3(expandRate, expandRate, expandRate);
            damageBoost += 2;
            flash = true;
        }
    }

    void OnDestroy()
    {
        phaseBlastFizzleOutFxCopy = Instantiate(phaseBlastFizzleOutFx, transform.position, Quaternion.identity) as GameObject;
        deathParticles = phaseBlastFizzleOutFxCopy.GetComponent<ParticleSystem>();
        deathParticles.startColor = mySprite.color;
    }
}
