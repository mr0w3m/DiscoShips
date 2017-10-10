using UnityEngine;
using System.Collections;

public class SpeedBuffORShieldsPickUp : MonoBehaviour
{

    public int type; //type 0 = Shields || type 1 = SpeedBuff
    private PlayerHealth hitHealthScript;

    public int shieldIncrease;
    public int speedBuffIncrease;
    
    

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player1(Black)" || other.tag == "Player2(White)" || other.tag == "Player3" || other.tag == "Player4")
        {
            hitHealthScript = other.GetComponent<PlayerHealth>();
            if (type == 0)
            {
                hitHealthScript.PickUpOvercharge();
            }
            else //size == 1
            {
                hitHealthScript.PickUpSpeedBuff();
            }
            Destroy(this.gameObject);
        }
        else
        {
            return;
        }
    }
}
