using UnityEngine;
using System.Collections;

public class ChargeCannonShot : MonoBehaviour {

    public int teamCount;

    //shot type
    public int type;

    //Shot Damage
    public int[] shotDamage;

    public int pierceCount;

    public GameObject shotDeathParticles;

    private PlayerHealth enemyHealthScript;
    private Asteroid asteroidScript;

    //Color
    public SpriteRenderer shotSprite;


    void Awake()
    {
        shotSprite = GetComponent<SpriteRenderer>();
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Boundary" || other.tag == "MineralPickup" || other.tag == "Capsule" || other.tag == "Edge")
        {
            return;
        }
        else if (other.tag == "Player1(Black)" || other.tag == "Player2(White)" || other.tag == "Player3" || other.tag == "Player4")
        {
            enemyHealthScript = other.GetComponent<PlayerHealth>();
            if (enemyHealthScript.teamCount != teamCount)
            {
                enemyHealthScript.Damage(shotDamage[type], false);
                DestroyThis();
            }
        }
        else if (other.tag == "PlayerPhysicsCollider")
        {
            enemyHealthScript = other.GetComponentInParent<PlayerHealth>();
            if (enemyHealthScript.teamCount != teamCount)
            {
                enemyHealthScript.Damage(shotDamage[type], false);
                DestroyThis();
            }
        }
        else if (other.tag == "Asteroid")
        {
            if (type == 2)
            {
                asteroidScript = other.GetComponent<Asteroid>();
                asteroidScript.DamageAsteroid(shotDamage[type]);
                pierceCount -= 1;
                if (pierceCount < 1)
                {
                    DestroyThis();
                }
            }
            else
            {
                asteroidScript = other.GetComponent<Asteroid>();
                asteroidScript.DamageAsteroid(shotDamage[type]);
                DestroyThis();
            }
        }
        else
        {
            DestroyThis();
        }
    }

    void DestroyThis()
    {
        //Instantiate(shotDeathParticles, transform.position, Quaternion.identity);
        Destroy(this.gameObject);
    }
}
