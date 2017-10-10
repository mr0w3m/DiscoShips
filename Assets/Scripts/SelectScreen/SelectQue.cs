using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class SelectQue : MonoBehaviour {

    public string[] names;
    public Sprite[] images;

    public bool weaponOrAbility;

    public Text currentText;
    public SpriteRenderer symbol;

    public GameManager gameManagerScript;
    public int representedPlayer;

    void Start()
    {
        currentText = GetComponentInChildren<Text>();
    }

    void Update()
    {
        switch(representedPlayer)
        {
            case (1):
                if (weaponOrAbility)
                {
                    //true = weapon
                    currentText.text = names[gameManagerScript.p1SelectedWeapon];
                    //symbol.sprite = images[gameManagerScript.p1SelectedWeapon];
                }
                else
                {
                    //false = ability
                    currentText.text = names[gameManagerScript.p1SelectedAbility];
                    //symbol.sprite = images[gameManagerScript.p1SelectedAbility];
                }
                break;
            case (2):
                if (weaponOrAbility)
                {
                    //true = weapon
                    currentText.text = names[gameManagerScript.p2SelectedWeapon];
                    //symbol.sprite = images[gameManagerScript.p2SelectedWeapon];
                }
                else
                {
                    //false = ability
                    currentText.text = names[gameManagerScript.p2SelectedAbility];
                    //symbol.sprite = images[gameManagerScript.p2SelectedAbility];
                }
                break;
            case (3):
                if (weaponOrAbility)
                {
                    //true = weapon
                    currentText.text = names[gameManagerScript.p3SelectedWeapon];
                    //symbol.sprite = images[gameManagerScript.p3SelectedWeapon];
                }
                else
                {
                    //false = ability
                    currentText.text = names[gameManagerScript.p3SelectedAbility];
                    //symbol.sprite = images[gameManagerScript.p3SelectedAbility];
                }
                break;
            case (4):
                if (weaponOrAbility)
                {
                    //true = weapon
                    currentText.text = names[gameManagerScript.p4SelectedWeapon];
                    //symbol.sprite = images[gameManagerScript.p4SelectedWeapon];
                }
                else
                {
                    //false = ability
                    currentText.text = names[gameManagerScript.p4SelectedAbility];
                    //symbol.sprite = images[gameManagerScript.p4SelectedAbility];
                }
                break;
        }
    }

}
