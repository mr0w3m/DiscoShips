using UnityEngine;
using System.Collections;

public class GoldGateTrigger : MonoBehaviour {

    public GameObject electricField;
    public float gateActiveTime;
    public SpriteRenderer tempElectricFieldSprite;

    private bool gateActive;

    public bool mapSelectScreen;

    void Start()
    {
        if (mapSelectScreen)
        {
            StartCoroutine(LoopShock());
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player1(Black)" || other.tag == "Player2(White)" || other.tag == "Player3" || other.tag == "Player4" 
            || other.tag == "SniperBullet" || other.tag == "RocketAmmo" || other.tag == "Shot" || other.tag == "Asteroid")
        {
            ActivateGate();
        }
    }

    IEnumerator LoopShock()
    {
        yield return new WaitForSeconds(Random.Range(0.1f, 5.0f));
        while(true)
        {
            electricField.SetActive(true);
            yield return new WaitForSeconds(Random.Range(1.0f, 3.0f));
            electricField.SetActive(false);
            yield return new WaitForSeconds(Random.Range(1.5f, 3.5f));
        }
    }

    void ActivateGate()
    {
        if (!gateActive)
        {
            StartCoroutine(GateActivated());
        }
        else
        {
            //Gate is already active
        }
    }

    IEnumerator GateActivated()
    {
        gateActive = true;
        //tempElectricFieldSprite.enabled = true;
        electricField.SetActive(true);
        yield return new WaitForSeconds(gateActiveTime);
        //tempElectricFieldSprite.enabled = false;
        gateActive = false;
        electricField.SetActive(false);
    }
}
