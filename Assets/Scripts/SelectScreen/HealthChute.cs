using UnityEngine;
using System.Collections;

public class HealthChute : MonoBehaviour {

    public int playerNum;
    public GameManager gameManagerScript;
    public GameObject minAsteroidDrop;
    public GameObject healthSpawnPoint;

    public float healthSpawnCoolDownTime;
    private float healthSpawnTime;

    public SpriteRenderer dimLights;
    private bool dispense;
    private Vector2 expanded = new Vector2(1.2f, 1.2f);
    private Vector2 normal = new Vector2(1f, 1f);
    private float smoothing = 1.5f;

    void Update()
    {
        switch(playerNum)
        {
            case (1):
                if (gameManagerScript.player1HealthScript != null)
                {
                    CheckHP(gameManagerScript.player1HealthScript);
                }
                break;
            case (2):
                if (gameManagerScript.player2HealthScript != null)
                {
                    CheckHP(gameManagerScript.player2HealthScript);
                }
                break;
            case (3):
                if (gameManagerScript.player3HealthScript != null)
                {
                    CheckHP(gameManagerScript.player3HealthScript);
                }
                break;
            case (4):
                if (gameManagerScript.player4HealthScript != null)
                {
                    CheckHP(gameManagerScript.player4HealthScript);
                }
                break;
        }

        if (healthSpawnTime > 0)
        {
            healthSpawnTime -= Time.deltaTime;
        }
        else
        {
            healthSpawnTime = 0;
        }

        if (dispense)
        {
            transform.localScale = expanded;
            dimLights.color = Color.white;
        }
        else
        {
            transform.localScale = Vector2.Lerp(transform.localScale, normal, smoothing * Time.deltaTime);
            dimLights.color = Color.Lerp(dimLights.color, Color.clear, smoothing * Time.deltaTime);
        }
        dispense = false;
    }

    void CheckHP(PlayerHealth healthScript)
    {
        if (healthScript.shotHealthCount < 100)
        {
            if (healthSpawnTime == 0)
            {
                ReleaseHealth();
                healthSpawnTime = healthSpawnCoolDownTime;
            }
        }
    }

    void ReleaseHealth()
    {
        Instantiate(minAsteroidDrop, healthSpawnPoint.transform.position, Quaternion.identity);
        //Animate Box expanding
        dispense = true;
        //Flash plus to a larger size
    }
}
