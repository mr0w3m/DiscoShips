using UnityEngine;
using System.Collections;

public class SeekerTargeting : MonoBehaviour {
    
    private EnemyShoot shootScript;

    void Awake()
    {
        shootScript = GetComponentInParent<EnemyShoot>();
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
        shootScript.seekerTarget = target;
    }
}
