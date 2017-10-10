using UnityEngine;
using System.Collections;

public class UIFade : MonoBehaviour
{
    //This script will have a configuration for each UI element.
    public int player;
    public GameManager gameManager;
    
    
    void OnTriggerEnter2D(Collider2D other)
    {
        if (player == 1)
        {
            if (other.tag == "Player1(Black)" || other.tag == "Player2(White)" || other.tag == "Player3" || other.tag == "Player4")
            {
                //Debug.Log("P1Transparent");
                gameManager.p1Trans = true;
            }
        }
        else if (player == 2)
        {
            if (other.tag == "Player1(Black)" || other.tag == "Player2(White)" || other.tag == "Player3" || other.tag == "Player4")
            {
                //Debug.Log("P2Transparent");
                gameManager.p2Trans = true;
            }
        }
        else if (player == 3)
        {
            if (other.tag == "Player1(Black)" || other.tag == "Player2(White)" || other.tag == "Player3" || other.tag == "Player4")
            {
                gameManager.p3Trans = true;
            }
        }
        else if (player == 4)
        {
            if (other.tag == "Player1(Black)" || other.tag == "Player2(White)" || other.tag == "Player3" || other.tag == "Player4")
            {
                gameManager.p4Trans = true;
            }
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (player == 1)
        {
            if (other.tag == "Player1(Black)" || other.tag == "Player2(White)" || other.tag == "Player3" || other.tag == "Player4")
            {
                //Debug.Log("P1Transparent");
                gameManager.p1Trans = false;
            }
        }
        else if (player == 2)
        {
            if (other.tag == "Player1(Black)" || other.tag == "Player2(White)" || other.tag == "Player3" || other.tag == "Player4")
            {
                //Debug.Log("P2Transparent");
                gameManager.p2Trans = false;
            }
        }
        else if (player == 3)
        {
            if (other.tag == "Player1(Black)" || other.tag == "Player2(White)" || other.tag == "Player3" || other.tag == "Player4")
            {
                gameManager.p3Trans = false;
            }
        }
        else if (player == 4)
        {
            if (other.tag == "Player1(Black)" || other.tag == "Player2(White)" || other.tag == "Player3" || other.tag == "Player4")
            {
                gameManager.p4Trans = false;
            }
        }
    }

    /*
    void OnCollisionStay2D(Collision2D other)
    {
        if (player == 1)
        {
            if (other.gameObject.tag == "Player1(Black)" || other.gameObject.tag == "Player2(White)" || other.gameObject.tag == "Player3" || other.gameObject.tag == "Player4")
            {
                Debug.Log("P1Transparent");
                gameManager.p1Trans = true;
            }
            else
            { gameManager.p1Trans = false; }
        }
        else if (player == 2)
        {
            if (other.gameObject.tag == "Player1(Black)" || other.gameObject.tag == "Player2(White)" || other.gameObject.tag == "Player3" || other.gameObject.tag == "Player4")
            {
                Debug.Log("P2Transparent");
                gameManager.p2Trans = true;
            }
            else
            { gameManager.p2Trans = false; }
        }
        else if (player == 3)
        {
            if (other.gameObject.tag == "Player1(Black)" || other.gameObject.tag == "Player2(White)" || other.gameObject.tag == "Player3" || other.gameObject.tag == "Player4")
            {
                gameManager.p3Trans = true;
            }
            else
            { gameManager.p3Trans = false; }
        }
        else if (player == 4)
        {
            if (other.gameObject.tag == "Player1(Black)" || other.gameObject.tag == "Player2(White)" || other.gameObject.tag == "Player3" || other.gameObject.tag == "Player4")
            {
                gameManager.p4Trans = true;
            }
            else
            { gameManager.p4Trans = false; }
        }   
    }
    */
}
