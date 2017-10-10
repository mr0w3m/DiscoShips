using UnityEngine;
using System.Collections;

public class Ibeam : MonoBehaviour {

	void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Shot")
        {
            Destroy(other.gameObject);
        }
    }
}
