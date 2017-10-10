using UnityEngine;
using System.Collections;

public class BoostDestroyAsteroids : MonoBehaviour {

    private Asteroid asteroidScript;


    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Asteroid")
        {
            //Debug.Log("hit asteroid");
            asteroidScript = other.GetComponent<Asteroid>();
            asteroidScript.DamageAsteroid(6);
        }
    }
}
