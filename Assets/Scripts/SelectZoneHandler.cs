using UnityEngine;
using System.Collections;

public class SelectZoneHandler : MonoBehaviour {

    private PlayerHealth playerHealthScript;

	void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player1(Black)" || other.tag == "Player2(White)" || other.tag == "Player3" || other.tag == "Player4")
        {
            playerHealthScript = other.GetComponent<PlayerHealth>();
            playerHealthScript.insideSelectZone = true;
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.tag == "Player1(Black)" || other.tag == "Player2(White)" || other.tag == "Player3" || other.tag == "Player4")
        {
            playerHealthScript = other.GetComponent<PlayerHealth>();
            playerHealthScript.insideSelectZone = false;
        }
    }
}
