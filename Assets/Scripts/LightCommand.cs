using UnityEngine;
using System.Collections;

public class LightCommand : MonoBehaviour
{

    public Color[] usableColors;
    public Color selectedColor;
    private int num;
    private WaitForSeconds waitTime = new WaitForSeconds(60.0f / 104.0f);

    void Start()
    {
        StartCoroutine(SelectColor());
    }

    IEnumerator SelectColor()
    {
        while(true)
        {
            num = Random.Range(0, 4);
            selectedColor = usableColors[num];
            yield return waitTime;
        }
    }
}
