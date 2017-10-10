using UnityEngine;
using System.Collections;

public class MoveForward : MonoBehaviour {

    private Rigidbody2D rb2d;

	void Awake()
    {
        rb2d = GetComponent<Rigidbody2D>();
    }

	void Update ()
    {
        //rb2d.AddForce(new Vector2(0, 1) * 2);
	}
}
