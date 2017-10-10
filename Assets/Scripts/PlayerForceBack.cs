using UnityEngine;
using System.Collections;

public class PlayerForceBack : MonoBehaviour {

    private Vector3 midPoint = new Vector3(0, 0, 0);
    private Vector2 backward;
    private Rigidbody2D playerRb;
    public float speed;

	void OnTriggerExit2D(Collider2D other)
    {
        if (other.tag == "Player1(Black)" || other.tag == "Player2(White)" || other.tag == "Player3" || other.tag == "Player4")
        {
            //Debug.Log("Happened");
            backward = (midPoint - other.transform.position);
            playerRb = other.GetComponent<Rigidbody2D>();
            playerRb.AddForce(backward * speed);
        }
        else
        {
            return;
        }
    }
}
