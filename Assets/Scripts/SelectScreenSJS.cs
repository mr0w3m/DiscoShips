using UnityEngine;
using System.Collections;

[System.Serializable]
public class SpaceJunkOther
{
    public GameObject junkType;
    public float weight;
}

public class SelectScreenSJS : MonoBehaviour
{
    public SpaceJunkOther[] spaceJunk;

    //Randomizer Vars
    private float totalWeight;
    private float randomNumber;
    private float index;

    public float upperRandomVelocity;
    public float lowerRandomVelocity;

    //SpaceJunkGenerator Vars
    public float startWait;
    public float spawnWait;
    public float waveWait;
    public int junkCountPerWave;

    public float spawnRadius;
    public float junkThrowRadius;

    public GameObject spawnPosGO;
    public GameObject goToPosGO;
    private Vector2 startLoc;
    private Vector2 randomLoc;
    private Vector2 randomDir;
    private Vector3 rotVector;
    private float randomVelocity;
    private GameObject junkInstance;
    private Rigidbody2D junkRB;

    private bool produceJunk = true;

    public bool devVersion;

    void Start()
    {
        SetUpSpaceJunkSystem();
        
    }

    void Update()
    {
        if (devVersion)
        {
            if (produceJunk && Input.GetKey(KeyCode.Z))
            {
                produceJunk = false;
            }

            if (!produceJunk && Input.GetKey(KeyCode.X))
            {
                produceJunk = true;
                StartCoroutine(SpaceJunkController());
            }
        }
    }

    //Set up the weighted total.
    void SetUpSpaceJunkSystem()
    {
        totalWeight = 0;

        for (int i = 0; i < spaceJunk.Length; i++)
        {
            totalWeight += spaceJunk[i].weight;
        }

        StartCoroutine(SpaceJunkController());
    }
    


    IEnumerator SpaceJunkController()
    {
        yield return new WaitForSeconds(startWait);
        while (produceJunk)
        {
            for (int i = 0; i < junkCountPerWave; i++)
            {
                Vector2 firstPos = Random.insideUnitCircle * spawnRadius;
                firstPos.x += spawnPosGO.transform.position.x;
                firstPos.y += spawnPosGO.transform.position.y;
                startLoc = firstPos;

                //Create instance of spaceJunk
                junkInstance = Instantiate(GenerateRandomSpaceJunk(), startLoc, Quaternion.identity) as GameObject;

                //Get Rigidbody2D
                junkRB = junkInstance.GetComponent<Rigidbody2D>();

                //Create random location to move spacejunk in
                Vector2 newPos = Random.insideUnitCircle * junkThrowRadius;
                newPos.x += goToPosGO.transform.position.x;
                newPos.y += goToPosGO.transform.position.y;
                randomLoc = newPos;

                //Subtract randomLoc from the spawn position to throw junk in that direction.
                randomDir = randomLoc - startLoc;

                //Create a random velocity to throw obj at
                randomVelocity = Random.Range(lowerRandomVelocity, upperRandomVelocity);

                //Adjust its rotation
                rotVector = junkInstance.transform.rotation.eulerAngles;
                rotVector.z = Random.Range(0, 360);
                junkInstance.transform.rotation = Quaternion.Euler(rotVector);

                //Finally add force to the object
                junkRB.AddForce(randomDir * randomVelocity);

                yield return new WaitForSeconds(spawnWait);
            }
            yield return new WaitForSeconds(waveWait);
        }
    }

    //To generate a random piece of space junk.
    GameObject GenerateRandomSpaceJunk()
    {
        //pick a random number in the range of total weight
        randomNumber = Random.Range(0.0f, totalWeight);
        index = 0;
        //loop through the spaceJunk array checking to see if the number was less than cumulative weight
        for (int i = 0; i < spaceJunk.Length; i++)
        {
            index += spaceJunk[i].weight;
            //if the random number is less than the index than i = to corresponding junk
            if (randomNumber < index)
            {
                return spaceJunk[i].junkType;
            }
        }
        return spaceJunk[Random.Range(0, spaceJunk.Length)].junkType;
    }
}

