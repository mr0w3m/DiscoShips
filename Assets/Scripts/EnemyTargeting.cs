using UnityEngine;
using System.Collections;

public class EnemyTargeting : MonoBehaviour {

    //So it's just a matter of having the game manager reference those at the start of their lives.
    private EnemyFollow followScript;

    void Awake()
    {
        followScript = GetComponentInParent<EnemyFollow>();
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player1(Black)" || other.tag == "Player2(White)" || other.tag == "Player3" || other.tag == "Player4")
        {
            SetTarget(other.transform);
        }
    }

    void SetTarget(Transform target)
    {
        followScript.target = target;
    }
}
