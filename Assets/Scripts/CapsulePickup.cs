using UnityEngine;
using System.Collections;

public class CapsulePickup : MonoBehaviour {

    public int pickUpAmount;
    public GameObject pickUpParticles;

    private PlayerHealth hitHealthScript;

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player1(Black)" || other.tag == "Player2(White)" || other.tag == "Player3" || other.tag == "Player4")
        {
            Debug.Log("PlayerHitCApsule");
            Instantiate(pickUpParticles, transform.position, Quaternion.identity);
            hitHealthScript = other.GetComponent<PlayerHealth>();
            hitHealthScript.PickUp(pickUpAmount);
            Destroy(this.gameObject);
        }
        else
        {
            return;
        }
    }
}
