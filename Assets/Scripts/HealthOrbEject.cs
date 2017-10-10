using UnityEngine;
using System.Collections;

public class HealthOrbEject : MonoBehaviour {

    public GameObject orbGameObject;

    private Rigidbody2D orbRB;
    private GameObject orb;
    private Vector2 randomDirection;
    public float upperForce;
    public float lowerForce;
    private float force;

    public bool health;

    void Start()
    {
        //Eject orbs
        switch (health)
        {
            case (true):
                StartCoroutine(EjectOrbs());
                break;
            case (false):
                EjectBuff();
                break;
        }
    }

    IEnumerator EjectOrbs()
    {
        SendOrb();
        yield return new WaitForSeconds(0.1f);
        SendOrb();
        yield return new WaitForSeconds(0.1f);
        SendOrb();
    }

    void EjectBuff()
    {
        SendOrb();
    }

    void SendOrb()
    {
        orb = Instantiate(orbGameObject, transform.position, Quaternion.identity)as GameObject;
        orbRB = orb.GetComponent<Rigidbody2D>();
        randomDirection = Random.insideUnitCircle.normalized;
        force = Random.Range(lowerForce, upperForce);
        orbRB.AddForce(randomDirection * force);
    }
}
