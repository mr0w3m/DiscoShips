using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class StartDanceFloorControl : MonoBehaviour {

    public Sprite[] litFloor;
    public Sprite[] countDownFloor;
    public bool active;
    public GameManager gameManager;

    private bool floorChanged;
    private SpriteRenderer mySprite;
    private WaitForSeconds floorChangeWait = new WaitForSeconds(0.25f);
    private WaitForSeconds aSecond = new WaitForSeconds(1f);
    private bool killSwitch;

    private bool ready;

    private int triggerCount;
    private int countDown;

    void Start()
    {
        mySprite = GetComponent<SpriteRenderer>();
        floorChanged = true;
        ready = true;
        killSwitch = true;
    }

    void Update()
    {
        if (triggerCount > 0)
        {
            active = true;
            if (triggerCount >= gameManager.howManyPlayers)
            {
                Countdown();
            }
            else
            {
                mySprite.sprite = countDownFloor[0];
                countDown = 0;
            }
        }
        else
        {
            active = false;
            countDown = 0;
        }

        if (floorChanged && !active)
        {
            StartCoroutine(ChangeFloor());
            floorChanged = false;
        }
    }

    void Countdown()
    {
        if (ready && killSwitch)
        {
            mySprite.sprite = countDownFloor[countDown];
            countDown++;
            ready = false;
            if (countDown > 5)
            {
                killSwitch = false;
                StartGame();
            }
            StartCoroutine(Clock());
        }
    }

    IEnumerator Clock()
    {
        yield return aSecond;
        ready = true;
    }

    void StartGame()
    {
        gameManager.SelectScreenStart();
    }

    IEnumerator ChangeFloor()
    {
        mySprite.sprite = litFloor[Random.Range(0, 10)];
        yield return floorChangeWait;

        floorChanged = true;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player1(Black)" || other.tag == "Player2(White)" || other.tag == "Player3" || other.tag == "Player4")
        {
            triggerCount++;
            ready = true;
        }
    }
     
    void OnTriggerExit2D(Collider2D other)
    {
        if (other.tag == "Player1(Black)" || other.tag == "Player2(White)" || other.tag == "Player3" || other.tag == "Player4")
        {
            triggerCount--;
        }
    }
}
