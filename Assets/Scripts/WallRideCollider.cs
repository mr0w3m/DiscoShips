using UnityEngine;
using System.Collections;

public class WallRideCollider : MonoBehaviour {

    private PlayerAbility playerAbilityScript;

	void Start ()
    {
        playerAbilityScript = GetComponentInParent<PlayerAbility>();
	}

    void OnTriggerEnter2D(Collider2D other)
    {
        //if we hit the map & the collider we hit is not equal to one we already have cached
        if (other.tag == "Map")
        {
            if (other != playerAbilityScript.cachedCollider2D || playerAbilityScript.wallInRange == false)
            {
                //Debug.Log("enteredRange");
                playerAbilityScript.cachedCollider2D = other;
                playerAbilityScript.wallInRange = true;
            }
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        //if we exit the map collider & the collider we exited is == to the current cached collider
        if (other.tag == "Map" && other == playerAbilityScript.cachedCollider2D)
        {
            //Debug.Log("exitedRange");
            playerAbilityScript.wallInRange = false;
        }
    }
}
