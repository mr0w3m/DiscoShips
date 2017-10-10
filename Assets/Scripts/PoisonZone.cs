using UnityEngine;
using System.Collections;

public class PoisonZone : MonoBehaviour
{
    private PlayerHealth healthScript;

	void OnTriggerStay2D(Collider2D other)
    {
        if (other.tag == "Player1(Black)" || other.tag == "Player2(White)" || other.tag == "Player3" || other.tag == "Player4")
        {
            healthScript = other.GetComponent<PlayerHealth>();
            if (!healthScript.poisoned)
            {
                healthScript.poisoned = true;
            }

        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.tag == "Player1(Black)" || other.tag == "Player2(White)" || other.tag == "Player3" || other.tag == "Player4")
        {
            healthScript = other.GetComponent<PlayerHealth>();
            healthScript.poisoned = false;
        }
    }

}
