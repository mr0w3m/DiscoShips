using UnityEngine;
using System.Collections;

public class Asteroid : MonoBehaviour {

    //1 is standard 2 is minerals
    public int asteroidID;

    public int asteroidHealth;
    public int asteroidDamage;
    public GameObject deathParticles;
    public Color asteroidDamagedColor;
    public float flashSpeed;

    private float killTime = 60f;
    private bool damaged;
    private Rigidbody2D rb;
    private SpriteRenderer rend;

    private PlayerHealth healthScript;
    private PlayerMovement moveScript;

    //DiffMinerals
    public GameObject ammoHealthPickup;
    private AmmoHealthPickup ammoHealthPickupScript;
    public GameObject speedBuffPickup;
    private SpeedBuffORShieldsPickUp shieldsORSpeedBuffScript;
    public GameObject shieldPickup;

    //Color
    public SpriteRenderer mineralRenderer;

    public Color healthColor;
    public Color shieldsColor;
    public Color speedBuffColor;

    private SpaceJunkSystem sjsScript;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        if (asteroidID < 10)
        {
            rend = GetComponent<SpriteRenderer>();
        }
    }
    

    void Update()
    {
        if (asteroidID < 10)
        {
            if (damaged == true)
            {
                rend.color = asteroidDamagedColor;
            }
            else
            {
                rend.color = Color.Lerp(rend.color, Color.white, flashSpeed * Time.deltaTime);
            }
            damaged = false;
        }
    }

    public void DamageAsteroid(int amountDamaged)
    {
        asteroidHealth -= amountDamaged;
        if (asteroidHealth < 1)
        {
            AsteroidDestroyed();
            Destroy(this.gameObject);
        }
        damaged = true;
    }

    void AsteroidDestroyed()
    {
        if (asteroidID == 0)
        {
            //Debug.Log("asteroid ID is not set (0)"); 
        }
        if (asteroidID == 2)
        {
            Instantiate(ammoHealthPickup, transform.position, Quaternion.identity);
            //Drop Energy
        }
        else if (asteroidID == 3)
        {
            Instantiate(shieldPickup, transform.position, Quaternion.identity);
        }
        else if (asteroidID == 4)
        {
            Instantiate(speedBuffPickup, transform.position, Quaternion.identity);
        }
        Instantiate(deathParticles, transform.position, Quaternion.identity);
        Destroy(this.gameObject);
    }
    /*
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Boundary" || other.tag == "Capsule")
        {
            return;
        }
        if (other.tag == "Player1(Black)" || other.tag == "Player2(White)" || other.tag == "Player3" || other.tag == "Player4")
        {
            if (asteroidDamage > 0)
            {
                healthScript = other.GetComponent<PlayerHealth>();
                healthScript.Damage(asteroidDamage, false);
            }

            if (asteroidID < 10)
            {
                Destroy(this.gameObject);
                Instantiate(deathParticles, transform.position, Quaternion.identity);
            }
        }
    }
    */
}
