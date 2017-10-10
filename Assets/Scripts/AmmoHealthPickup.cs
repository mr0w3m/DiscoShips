using UnityEngine;
using System.Collections;

public class AmmoHealthPickup : MonoBehaviour {

    public int size;
    private PlayerHealth hitHealthScript;

    public int fragmentIncrease;
    public int asteroidIncrease;

    public GameObject pickupFx;

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player1(Black)" || other.tag == "Player2(White)" || other.tag == "Player3" || other.tag == "Player4")
        {
            hitHealthScript = other.GetComponent<PlayerHealth>();
            if (size == 1)
            {
                hitHealthScript.PickUp(asteroidIncrease);
            }
            else //size == 2
            {
                hitHealthScript.PickUp(fragmentIncrease);
            }
            Instantiate(pickupFx, transform.position, Quaternion.identity);
            Destroy(this.gameObject);
        }
        else
        {
            return;
        }
    }
}
