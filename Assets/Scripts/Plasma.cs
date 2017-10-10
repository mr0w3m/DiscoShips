using UnityEngine;
using System.Collections;

public class Plasma : MonoBehaviour {

    public int teamCount;
    public int plasmaDamage;

    public SpriteRenderer mySprite; 

    private PlayerHealth playerHealthScript;
    private Asteroid asteroidScript;
    private ShieldMine mineScript;

    void Awake()
    {
        mySprite = GetComponent<SpriteRenderer>();
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player1(Black)" || other.tag == "Player2(White)" || other.tag == "Player3" || other.tag == "Player4")
        {
            playerHealthScript = other.GetComponent<PlayerHealth>();
            if (playerHealthScript.teamCount != teamCount)
            {
                playerHealthScript.Damage(plasmaDamage, false);
            }
        }
        else if (other.tag == "Asteroid")
        {
            asteroidScript = other.GetComponent<Asteroid>();
            asteroidScript.DamageAsteroid(plasmaDamage);
        }
        else if (other.tag == "ShieldMine")
        {
            mineScript = other.GetComponent<ShieldMine>();
            if (mineScript.teamCount != teamCount)
            {
                Destroy(this.gameObject);
            }
        }
    }
}
