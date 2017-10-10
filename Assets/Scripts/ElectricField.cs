using UnityEngine;
using System.Collections;

public class ElectricField : MonoBehaviour {

    private PlayerHealth healthScript;
    //We also need to push the player back
    private Rigidbody2D hitRB;

    public float shockKnockback;
    public int shockDamage;

	void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player1(Black)" || other.tag == "Player2(White)" || other.tag == "Player3" || other.tag == "Player4")
        {
            healthScript = other.GetComponent<PlayerHealth>();
            hitRB = other.GetComponent<Rigidbody2D>();

            hitRB.velocity *= -1 * shockKnockback;
            healthScript.Damage(shockDamage, false);
        }
    }
}
