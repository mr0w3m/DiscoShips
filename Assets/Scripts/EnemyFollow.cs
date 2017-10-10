using UnityEngine;
using System.Collections;

public class EnemyFollow : MonoBehaviour {

    private Rigidbody2D rb2D;
    private float timer;

    public float moveSpeed;
    public float smoothing;
    public Transform target;

    private bool inPursuit;
    private ParticleSystem particles;

    void Awake()
    {
        rb2D = GetComponent<Rigidbody2D>();
        inPursuit = true;
        particles = GetComponent<ParticleSystem>();
    }

    public void KillFollow()
    {
        inPursuit = false;
        Destroy(this.gameObject, 3f);
        particles.Pause();
    }
	
    void Update()
    {
        timer += Time.deltaTime;
        if (target != null)
        {
            transform.up = Vector2.Lerp(transform.up, target.position - transform.position, smoothing * Time.deltaTime);
        }
        else
        {
            
        }
    }

    void FixedUpdate()
    {
        if (inPursuit)
        {
            if (timer > .1)
            {
                timer = 0;
                //1/10 of a second has passed
                rb2D.AddForce(transform.up * moveSpeed);
            }
        }
    }
}
