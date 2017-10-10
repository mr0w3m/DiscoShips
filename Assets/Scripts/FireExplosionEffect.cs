using UnityEngine;
using System.Collections;

public class FireExplosionEffect : MonoBehaviour {

    public float blastRadius;
    public float fireLength;

    private PlayerHealth enemyHealthScript;

	void Start()
    {
        BlowUp();
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
                        enemyHealthScript = hitThings[i].GetComponent<PlayerHealth>();
                        enemyHealthScript.TouchedLava(fireLength);
                    }
                }
            }
            Destroy(this.gameObject, 3f);
        }
    }
}
