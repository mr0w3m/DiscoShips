using UnityEngine;
using System.Collections;

public class MobSpawner : MonoBehaviour {

    public GameObject enemy1;
    public GameObject enemy2;
    public GameObject enemy3;
    public Transform target;
    public Transform[] wayPoints = new Transform[4];
    public GameManager gameManager;

    private EnemyFollow aiFollowScript;
    private EnemyShoot aiShootScript;
    private GameObject aiCopy;
    private EnemyHealth aiHealth;

    void Update()
    {
        if (Input.GetKeyUp(KeyCode.A))
        {
            SpawnEnemy(enemy1);
        }
        
        if (Input.GetKeyUp(KeyCode.S))
        {
            SpawnEnemy(enemy2);
        }

        if (Input.GetKeyUp(KeyCode.D))
        {
            SpawnEnemy(enemy3);
        }
    }

    void Start()
    {
        StartCoroutine(SetUp());
    }

    IEnumerator SetUp()
    {
        yield return new WaitForSeconds(0.5f);
        //Debug.Log("SetUP");
        wayPoints[0] = gameManager.wayPoints[0];
        wayPoints[1] = gameManager.wayPoints[1];
        wayPoints[2] = gameManager.wayPoints[2];
        wayPoints[3] = gameManager.wayPoints[3];
    }

	void SpawnEnemy(GameObject enemyType)
    {
        //Debug.Log("enemy spawned");
        aiCopy = Instantiate(enemyType, transform.position, Quaternion.identity)as GameObject;
        aiHealth = aiCopy.GetComponent<EnemyHealth>();
        
        if (aiHealth.enemyType == 0)
        {
            aiFollowScript = aiCopy.GetComponent<EnemyFollow>();
            aiFollowScript.target = wayPoints[Random.Range(0, 4)];
            //aiFollowScript.spawner = this;
        }
        else if (aiHealth.enemyType == 1)
        {
            aiFollowScript = aiCopy.GetComponent<EnemyFollow>();
            aiFollowScript.target = wayPoints[Random.Range(0, 4)];
            //aiFollowScript.spawner = this;
        }
        else if (aiHealth.enemyType == 2)
        {
            aiFollowScript = aiCopy.GetComponent<EnemyFollow>();
            aiFollowScript.target = wayPoints[Random.Range(0, 4)];
        }
    }
}
