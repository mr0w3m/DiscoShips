using UnityEngine;
using System.Collections;

public class AITurner : MonoBehaviour {

    public Transform[] wayPoints;

    public GameManager gameManager;

    void Start()
    {
        gameManager.wayPoints[0] = wayPoints[0];
        gameManager.wayPoints[1] = wayPoints[1];
        gameManager.wayPoints[2] = wayPoints[2];
        gameManager.wayPoints[3] = wayPoints[3];
    }
}
