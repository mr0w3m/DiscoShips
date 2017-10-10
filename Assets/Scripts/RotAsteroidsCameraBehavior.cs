using UnityEngine;
using System.Collections;

public class RotAsteroidsCameraBehavior : MonoBehaviour {


    private Camera thisCamera;

    public bool zoomOut;

    private int player1Track;
    private int player2Track;
    private int player3Track;
    private int player4Track;

    public float smoothing;

    void Start()
    {
        thisCamera = GetComponent<Camera>();
        thisCamera.orthographicSize = 15f;

        player1Track = 0;
        player2Track = 0;
        player3Track = 0;
        player4Track = 0;
    }

    void FixedUpdate()
    {
        if (AllInCenter())
        {
            zoomOut = false;
            thisCamera.orthographicSize = Mathf.Lerp(thisCamera.orthographicSize, 15f, smoothing);
        }
        else
        {
            zoomOut = true;
            thisCamera.orthographicSize = Mathf.Lerp(thisCamera.orthographicSize, 25f, smoothing);
        }
    }

    bool AllInCenter()
    {
        int num;
        num = player1Track + player2Track + player3Track + player4Track;
        if (num == 0)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player1(Black)")
        {
            player1Track = 0;
        }
        else if (other.tag == "Player2(White)")
        {
            player2Track = 0;
        }
        else if (other.tag == "Player3")
        {
            player3Track = 0;
        }
        else if (other.tag == "Player4")
        {
            player4Track = 0;
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.tag == "Player1(Black)")
        {
            player1Track = 1;
        }
        else if (other.tag == "Player2(White)")
        {
            player2Track = 1;
        }
        else if(other.tag == "Player3")
        {
            player3Track = 1;
        }
        else if (other.tag == "Player4")
        {
            player4Track = 1;
        }
    }
}
