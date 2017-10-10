using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class WinSeparateScript : MonoBehaviour {

    public Text winText;
    public float targetSize;

    private float smoothing;
    
    private string[] winStrings = new string[17];

    void Awake()
    {
        //List of sayings at the finish
        winStrings[0] = "YOU\nWIN!";
        winStrings[1] = "SWEET\nSTUFF!";
        winStrings[2] = "OAK\nNUGGINS!";
        winStrings[3] = "DIP ME IN YA\nMONDAY MILK!";
        winStrings[4] = "AWWW\nYEAH!";
        winStrings[5] = "GROOVY\nBABY!";
        winStrings[6] = "ITS ALL GRAVY IN\nTHE NAVY BABY!";
        winStrings[7] = "GET\nFUNKY!";
        winStrings[8] = "ZAM\nZADDY!";
        winStrings[9] = "DREAM\nON!";
        winStrings[10] = "FAR\nOUT!";
        winStrings[11] = "IN YA\nFACE!";
        winStrings[12] = "GET\nDOWN!";
        winStrings[13] = "YANK TRAIN\nBABY!";
        winStrings[14] = "PUT A BOOGIE\nDOLLAR DOWN!";
        winStrings[15] = "JIVE\nTURKEY!";
        winStrings[16] = "FEEL THE\nFUNK";
    }
    

    void Start()
    {
        winText = GetComponent<Text>();
        winText.text = ActualRandomText();
        smoothing = 0.3f;
    }

    void Update()
    {
        winText.lineSpacing = Mathf.Lerp(winText.lineSpacing, targetSize, smoothing);
    }

    string ActualRandomText()
    {
        int num = Random.Range(0, winStrings.Length);
        return winStrings[num];
    }
    
    string RandomText()
    {
        int num = Random.Range(0, 16);
        if (num == 0)
        {
            return ("YOU\nWIN!");
        }
        else if (num == 1)
        {
            return "ROCK\nON!";
        }
        else if (num == 2)
        {
            return "FRIGGEN'\nAWESOME!";
        }
        else if (num == 3)
        {
            return "AWWW\nYEAH!";
        }
        else if (num == 4)
        {
            return "GROOVY\nBABY";
        }
        else if (num == 5)
        {
            return "GET\nREKKED";
        }
        else
        {
            return "GET\nWRECKED!";
        }
    }
}
