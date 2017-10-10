using UnityEngine;
using System.Collections;

public class ExploderFunction : MonoBehaviour {

    private EnemyHealth healthScript;
    private SpriteRenderer sprite;

    private Asteroid asteroidScript;
    private PlayerHealth playerHealthScript;

    public float blastRadius;
    public int blastDamage;

    public GameObject explosionParticles;

    void Awake()
    {
        healthScript = GetComponentInParent<EnemyHealth>();
        sprite = GetComponentInParent<SpriteRenderer>();
    }

    void Start()
    {

    }

	void OnTriggerEnter2D (Collider2D other)
    {
        if (other.tag == "Player1(Black)" || other.tag == "Player2(White)" || other.tag == "Player3" || other.tag == "Player4")
        {
            sprite.enabled = false;
            BlowUp();
            StartCoroutine(DestroyThis());
        }
    }

    void BlowUp()
    {
        if (this != null)
        {
            Collider2D[] hitThings = Physics2D.OverlapCircleAll(transform.position, blastRadius);
            if (hitThings != null)
            {
                for (int i = 0; i < hitThings.Length; i++)
                {
                    if (hitThings[i].tag == "Player1(Black)" || hitThings[i].tag == "Player2(White)" || hitThings[i].tag == "Player3" || hitThings[i].tag == "Player4")
                    {
                        playerHealthScript = hitThings[i].GetComponent<PlayerHealth>();
                        playerHealthScript.Damage(blastDamage, false);
                    }
                    else if (hitThings[i].tag == "Asteroid")
                    {
                        asteroidScript = hitThings[i].GetComponent<Asteroid>();
                        asteroidScript.DamageAsteroid(blastDamage);
                    }
                }
            }
        }
    }

    IEnumerator DestroyThis()
    {
        Instantiate(explosionParticles, transform.position, Quaternion.identity);
        yield return new WaitForSeconds(2f);
        healthScript.DamageEnemy(12);
    }
}
