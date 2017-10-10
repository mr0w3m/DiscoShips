using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class IntroSequenceControl : MonoBehaviour {

    public float waitTime;
    private WaitForSeconds wait;

    void Start()
    {
        wait = new WaitForSeconds(waitTime);
        StartCoroutine(Intro());
    }

	IEnumerator Intro()
    {
        yield return wait;
        SceneManager.LoadScene(1);
    }
}
