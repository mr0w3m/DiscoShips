using UnityEngine;
using System.Collections;

public class DestroyByBoundary : MonoBehaviour {

	void OnTriggerExit2D(Collider2D other)
    {
        //This is so the trail stays afterward. bullets are set to destroy themselves after 5 seconds.
        if (other.tag == "SniperBullet")
        {
            return;
        }
        
        //Debug.Log("Destroying: " + other.tag);
        Destroy(other.gameObject);
    }
}
