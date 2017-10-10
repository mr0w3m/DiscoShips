using UnityEngine;
using System.Collections;

public class IceEnvironment : MonoBehaviour {
    
    private PlayerMovement moveScript;

    public float freezeTime;

	void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag == "Player1(Black)" || other.gameObject.tag == "Player2(White)" || other.gameObject.tag == "Player3" || other.gameObject.tag == "Player4")
        {
            //if the player runs into ice.
            //Grab script to effect
            moveScript = other.gameObject.GetComponent<PlayerMovement>();
            moveScript.FreezePlayer(freezeTime);
        }
    }
}
