using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class WarpField : MonoBehaviour {

    private bool state = true; //true = Slow / false = SpeedUp

    private CircleCollider2D collider;

    private Rigidbody2D grabRB;
    private Vector2 newVelocity;
    private float x;
    private float y;
    private float newx;
    private float newy;

    public GameObject slowFieldGO;
    public GameObject speedFieldGO;

    public ParticleSystem slowField;
    public ParticleSystem speedField;
    public Image image;

    void Awake()
    {
        collider = GetComponent<CircleCollider2D>();
    }

    void Start()
    {

    }

    public void SetState(bool setState)
    {
        state = setState;
        collider.enabled = true;
    }

    public void FlipState()
    {
        switch(state)
        {
            case (true):
                state = false;
                break;
            case (false):
                state = true;
                break;
        }
    }

    public void SetColors(Color setColor)
    {
        slowField.startColor = setColor;
        speedField.startColor = setColor;
        image.color = setColor;
    }

    void Update()
    {
        if (state)
        {
            slowFieldGO.SetActive(true);
            speedFieldGO.SetActive(false);
        }
        else
        {
            slowFieldGO.SetActive(false);
            speedFieldGO.SetActive(true);
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        //if they have a rigidbody and are not the player
        if (other.GetComponent<Rigidbody2D>() != null && other.GetComponent<PlayerShoot>() == null)
        {
            grabRB = other.GetComponent<Rigidbody2D>();
            switch(state)
            {
                case(true):
                    //Slow
                    x = grabRB.velocity.x;
                    y = grabRB.velocity.y;

                    newx = x / 1.5f;
                    newy = y / 1.5f;
                    grabRB.velocity = new Vector2(newx, newy);

                    break;
                case(false):
                    //Speed
                    x = grabRB.velocity.x;
                    y = grabRB.velocity.y;

                    newx = x * 1.5f;
                    newy = y * 1.5f;
                    grabRB.velocity = new Vector2(newx, newy);
                    break;
            }
        }
    }
}
