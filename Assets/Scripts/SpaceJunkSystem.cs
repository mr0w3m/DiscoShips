using UnityEngine;
using System.Collections;

[System.Serializable]
public class SpaceJunk
{
    public GameObject junkType;
    public float weight;
}

public class SpaceJunkSystem : MonoBehaviour
{
    public SpaceJunk[] spaceJunk;

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
    private int totalJunkCount = 15;

    public float spawnRadius;
    public float junkThrowRadius;

    private Vector2 spawnPos;
    private Vector2 randomLoc;
    private Vector2 randomDir;
    private Vector3 rotVector;
    private float randomVelocity;
    private GameObject junkInstance;
    private Rigidbody2D junkRB;

    private bool stop = false;
    public bool devMode = false;

    public GameObject addToObj;
    private GameObject addToObjCopy;
    private AddToObj addToObjScript;

    public bool mapSelectScreen;

    void Start()
    {
        SetUpSpaceJunkSystem();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Z))
        {
            stop = true;
        }

        if (Input.GetKeyDown(KeyCode.X))
        {
            stop = false;
            StartCoroutine(SpaceJunkController());
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

    public void ReUpJunkCount()
    {
        totalJunkCount++;
    }

    
    IEnumerator SpaceJunkController()
    {
        yield return new WaitForSeconds(startWait);
        while (true)
        {
            for (int i = 0; i < totalJunkCount; i++)
            {
                
                totalJunkCount--;
                Debug.Log(totalJunkCount);

                //Create spawnPosition somewhere on the edge of the circle
                Vector2 spawnPos = Random.insideUnitCircle.normalized * spawnRadius;

                //Create instance of spaceJunk
                junkInstance = Instantiate(GenerateRandomSpaceJunk(), spawnPos, Quaternion.identity) as GameObject;
                
                addToObjCopy = Instantiate(addToObj, junkInstance.transform.position, Quaternion.identity) as GameObject;
                addToObjCopy.transform.SetParent(junkInstance.transform);
                addToObjScript = addToObjCopy.GetComponent<AddToObj>();
                addToObjScript.SetScript(this);
                addToObjScript.SetObj();
                

                //Get Rigidbody2D
                junkRB = junkInstance.GetComponent<Rigidbody2D>();

                //Create random location to move spacejunk in
                randomLoc = Random.insideUnitCircle * junkThrowRadius;

                //Subtract randomLoc from the spawn position to throw junk in that direction.
                randomDir = randomLoc - spawnPos;

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
            Debug.Log("Working");
            if (stop && devMode)
            {
                Debug.Log("SJS OFF");
                yield break;
            }
        }
    }

    //To generate a random piece of space junk.
    GameObject GenerateRandomSpaceJunk()
    {
        //pick a random number in the range of total weight
        randomNumber = Random.Range(0.0f, totalWeight);
        index = 0;
        //loop through the spaceJunk array checking to see if the number was less than cumulative weight
        for (int i = 0; i <spaceJunk.Length; i ++)
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
