using UnityEngine;
using System.Collections;

public class TitleScreen : MonoBehaviour {

    //Asteroids
    private GameObject[] asteroidArray = new GameObject[4];
    public GameObject asteroid1;
    public GameObject asteroid2;
    public GameObject asteroid3;
    public GameObject asteroid4;

    private GameObject[] asteroidMineralArray = new GameObject[4];
    public GameObject asteroidMinerals1;
    public GameObject asteroidMinerals2;
    public GameObject asteroidMinerals3;
    public GameObject asteroidMinerals4;

    private GameObject[] ibeamArray = new GameObject[3];
    public GameObject ibeamSmall;
    public GameObject ibeamMedium;
    public GameObject ibeamLarge;
    public float ibeamSpeedMultiplier;

    public GameObject capsule;

    public float spawnRadius;
    public float spaceJunkMoveToRadius;
    public int hazardCount;
    public float spawnWait;
    public float startWait;
    public float waveWait;

    private Asteroid asteroidScript;


    // Use this for initialization
    void Start ()
    {
        asteroidArray[0] = asteroid1;
        asteroidArray[1] = asteroid2;
        asteroidArray[2] = asteroid3;
        asteroidArray[3] = asteroid4;

        asteroidMineralArray[0] = asteroidMinerals1;
        asteroidMineralArray[1] = asteroidMinerals2;
        asteroidMineralArray[2] = asteroidMinerals3;
        asteroidMineralArray[3] = asteroidMinerals4;

        ibeamArray[0] = ibeamSmall;
        ibeamArray[1] = ibeamMedium;
        ibeamArray[2] = ibeamLarge;

        StartCoroutine("SpawnAsteroids");
    }

    IEnumerator SpawnAsteroids()
    {
        yield return new WaitForSeconds(startWait);
        while (true)
        {
            for (int i = 0; i < hazardCount; i++)
            {
                //Create spawnPosition from somewhere on the edge of the circle
                Vector2 spawnPosition = Random.insideUnitCircle.normalized * spawnRadius;
                //Create instance of asteroid and grab Rigidbody2d & the script
                int spaceJunkNum = Random.Range(0, 29);
                GameObject spaceJunkInstance = Instantiate(RandomSpaceJunk(spaceJunkNum), spawnPosition, Quaternion.identity) as GameObject;
                if (spaceJunkNum > 0 && spaceJunkNum < 25)
                {
                    asteroidScript = spaceJunkInstance.GetComponent<Asteroid>();

                    if (spaceJunkNum > 19 && spaceJunkNum < 25)
                    {
                        asteroidScript.asteroidID = 2;
                    }
                }
                Rigidbody2D spaceJunkRB = spaceJunkInstance.GetComponent<Rigidbody2D>();
                //Create random location to move asteroid in
                Vector2 randomLoc = Random.insideUnitCircle * spaceJunkMoveToRadius;
                //Add the subtracted vectors as force to send spaceJunk
                Vector2 randomDir = randomLoc - spawnPosition;
                float randomVelocity = Random.Range(4, 10);
                if (spaceJunkNum > 24 && spaceJunkNum < 28)
                {
                    Vector3 rotationVector = spaceJunkInstance.transform.rotation.eulerAngles;
                    rotationVector.z = Random.Range(0, 360);
                    spaceJunkInstance.transform.rotation = Quaternion.Euler(rotationVector);
                    randomVelocity *= ibeamSpeedMultiplier;
                }
                spaceJunkRB.AddForce(randomDir * randomVelocity);
                yield return new WaitForSeconds(spawnWait);
            }
            yield return new WaitForSeconds(waveWait);
        }
    }

    GameObject RandomSpaceJunk(int randomNumber)
    {
        //Asteroids = 0-19
        if (randomNumber > -1 && randomNumber < 20)
        { return asteroidArray[Random.Range(0, 4)]; }
        //MineralAsteroids = 20-24
        else if (randomNumber > 19 && randomNumber < 25)
        { return asteroidMineralArray[Random.Range(0, 4)]; }
        //Ibeams = 25-27
        else if (randomNumber > 24 && randomNumber < 28)
        { return ibeamArray[Random.Range(0, 3)]; }
        //Capsule = 28
        else if (randomNumber > 27 && randomNumber < 29)
        { return capsule; }

        else
        {
            Debug.Log("Null is: " + randomNumber);
            return null;
        }
    }
}
