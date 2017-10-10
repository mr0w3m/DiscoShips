using UnityEngine;
using System.Collections;

public class ParticleColorTest : MonoBehaviour {

    private ParticleSystem system;

	void Start ()
    {
        system = GetComponent<ParticleSystem>();
        
        system.startColor = Color.red;
	}
}
