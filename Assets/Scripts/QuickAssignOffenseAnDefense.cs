using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class QuickAssignOffenseAnDefense : MonoBehaviour {

    public int offenseDefense;
    public int offenseType;
    public int defenseType;
    public GameManager gameManager;

    public Sprite[] selectTile;
    private SpriteRenderer mySprite;

    public GameObject system1;
    public GameObject system2;
    public GameObject system3;
    public GameObject system4;

    private PlayerShoot shootScript;

    private PlayerHealth healthScript;

    private int weaponSelected;
    private int abilitySelected;

    void PlayerEntered(Collider2D other)
    {
        healthScript = other.GetComponent<PlayerHealth>();
        healthScript.weaponSelected = offenseType;
        healthScript.abilitySelected = defenseType;
        if (offenseDefense == 1)
        {
            healthScript.weaponAbility = true;
        }
        else
        {
            healthScript.weaponAbility = false;
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "Player1(Black)" || other.tag == "Player2(White)" || other.tag == "Player3" || other.tag == "Player4")
        {
            PlayerEntered(other);
        }
    }
}
