using UnityEngine;
using System.Collections;

public class LavaEnvironment : MonoBehaviour
{
    private PlayerHealth healthScript;

    public float fireLength;

    public float lowerActiveTime;
    public float upperActiveTime;
    public float timeBetweenActivity;

    private PolygonCollider2D lavaCollider;

    public SpriteRenderer activeCraters;
    public SpriteRenderer activeVeins;
    public SpriteRenderer activeLava;
    
    public float lerpTime;
    private bool active;

    public bool mapScreen;

    private bool screenShakeActive;
    private Vector3 shakePos;
    public float shakeAmount;
    public GameObject camera;

    private Vector3 zeroPos = new Vector3(0f, 0f, -10f);

    
    void Awake()
    {
        lavaCollider = GetComponent<PolygonCollider2D>();
        active = false;
        screenShakeActive = false;
    }

    void Start()
    {
        StartCoroutine(MapChange());

        activeLava.enabled = true;
        activeCraters.enabled = true;
        activeVeins.enabled = true;
    }

    void OnEnable()
    {
        StartCoroutine(MapChange());
    }

    void Update()
    {
        if (active)
        {
            //Lerp between red and white!
            activeCraters.color = Color.Lerp(Color.white, Color.red, Mathf.PingPong(Time.time, lerpTime));
            activeLava.color = Color.Lerp(Color.white, Color.red, Mathf.PingPong(Time.time, lerpTime));
            activeVeins.color = Color.Lerp(Color.white, Color.red, Mathf.PingPong(Time.time, lerpTime));
        }
        else
        {
            activeCraters.color = Color.Lerp(activeCraters.color, Color.clear, lerpTime * Time.deltaTime);
            activeLava.color = Color.Lerp(activeLava.color, Color.clear, lerpTime * Time.deltaTime);
            activeVeins.color = Color.Lerp(activeVeins.color, Color.clear, lerpTime * Time.deltaTime);
        }

        if (!mapScreen)
        {
            if (screenShakeActive)
            {
                shakePos = Random.insideUnitCircle * shakeAmount;
                Vector3 newShakePos = new Vector3(camera.transform.position.x + shakePos.x, camera.transform.position.y + shakePos.y, -10f);
                camera.transform.position = Vector3.Lerp(zeroPos, newShakePos, 2f * Time.deltaTime);
            }
        }
    }

    IEnumerator MapChange()
    {
        while(true)
        {
            //Start wait constant amount of time that flow won't happen
            //Debug.Log("Lava flow wait.");
            yield return new WaitForSeconds(timeBetweenActivity);
            //Display warning
            screenShakeActive = true;
            
            yield return new WaitForSeconds(2f);
            //Debug.Log("Lava flow started.");

            active = true;

            //enable polygon collider
            lavaCollider.enabled = true;

            //enable sprite renderers for Activity

            //How long does it stay active?
            yield return new WaitForSeconds(Random.Range(lowerActiveTime, upperActiveTime));
            //Debug.Log("Lava flow stopped.");
            active = false;
            screenShakeActive = false;
            if (camera != null)
            {
                camera.transform.position = zeroPos;
            }
            //disable polygon collider
            lavaCollider.enabled = false;

            //disable sprite renderers for activity

            //Debug.Log("Lava flow cycle complete.");
        }

    }

	void OnTriggerStay2D(Collider2D other)
    {
        if (other.tag == "Player1(Black)" || other.tag == "Player2(White)" || other.tag == "Player3" || other.tag == "Player4")
        {
            //When a player hits the fire.
            //Get player health
            healthScript = other.GetComponent<PlayerHealth>();

            //Call on fire function on player
            healthScript.TouchedLava(fireLength);
        }
    }
}
