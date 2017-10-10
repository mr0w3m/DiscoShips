using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class SelectTileFunction : MonoBehaviour {

    private SpriteRenderer mySprite;

    public Sprite[] selectTile;

    void Start()
    {
        mySprite = GetComponent<SpriteRenderer>();
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player1(Black)" || other.tag == "Player2(White)" || other.tag == "Player3" || other.tag == "Player4")
        {
            mySprite.sprite = selectTile[1];
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.tag == "Player1(Black)" || other.tag == "Player2(White)" || other.tag == "Player3" || other.tag == "Player4")
        {
            mySprite.sprite = selectTile[0];
        }
    }
}
