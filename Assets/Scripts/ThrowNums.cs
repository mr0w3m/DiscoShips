using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ThrowNums : MonoBehaviour {

    public GameObject signGO;
    private SpriteRenderer sign;
    private Text myText;

    public bool numbers;

    public Sprite plus;
    public Sprite minus;

    private Vector2 twoDigitPos = new Vector2(-1.7f, 0.05f);
    private Vector2 threeDigitPos = new Vector2(-2.1f, 0.05f);

    private float killTime;
    private float numberKillTime = 1.5f;
    private float nonNumKillTime = 1f;

    public Color myColor;

    private bool reset;
    private float smoothing = 3f;
    private int myNum;

    private int timesHit;

    void Awake()
    {
        sign = GetComponentInChildren<SpriteRenderer>();
        myText = GetComponentInChildren<Text>();
    }

    void Start()
    {
        if (!numbers)
        {
            killTime = nonNumKillTime;
        }
        else
        {
            //Its numbers and we want the kill time to be a little longer for players
            //to read the amount and add to it.
            killTime = numberKillTime;
        }
    }

    void Update()
    {
        if (!numbers)
        {
            sign.enabled = false;
        }

        if (numbers)
        {
            if (myNum > 9 && myNum < 100)
            {
                signGO.transform.localPosition = twoDigitPos;
            }
            else if (myNum > 99)
            {
                signGO.transform.localPosition = threeDigitPos;
            }
        }

        if (numbers && reset)
        {
            myText.color = Color.white;
        }
        else if (numbers)
        {
            myText.color = Color.Lerp(myText.color, myColor, smoothing * Time.deltaTime);
        }
        reset = false;
        MonitorKill();
    }

    void MonitorKill()
    {
        if (killTime > 0)
        {
            killTime -= Time.deltaTime;
        }
        else
        {
            killTime = 0;
            Destroy(this.gameObject);
        }
    }

    void ResetTime()
    {
        killTime = numberKillTime;
        reset = true;
    }

    public void IncreaseNumBy(int amt)
    {
        ResetTime();
        myNum += amt;
        string i = myNum.ToString();
        myText.text = i;
    }

	public void SetSign(bool posNeg, int num)
    {
        if (posNeg)
        {
            sign.sprite = plus;
            sign.color = Color.green;
        }
        else
        {
            sign.sprite = minus;
            sign.color = Color.red;
        }
    }
}
