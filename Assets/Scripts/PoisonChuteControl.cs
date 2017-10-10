using UnityEngine;
using System.Collections;

public class PoisonChuteControl : MonoBehaviour {

    public float lowerWait;
    public float upperWait;
    public float flowTime;

    public GameObject poisonChute;
    public ParticleSystem underSmog;
    public ParticleSystem overSmog;


    void Start()
    {
        StartCoroutine(MapChange());
    }

    IEnumerator MapChange()
    {
        while (true)
        {
            
            //Debug.Log("poison chute wait.");
            yield return new WaitForSeconds(Random.Range(lowerWait, upperWait));
            //Debug.Log("poison chute started.");

            //Activate gameobject
            poisonChute.SetActive(true);
            underSmog.Play();
            overSmog.Play();

            //How long does it stay active?
            yield return new WaitForSeconds(flowTime);

            //deactivate gameobject
            poisonChute.SetActive(false);

            //Debug.Log("poison chute cycle complete.");
        }

    }
}
