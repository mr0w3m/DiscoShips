using UnityEngine;
using System.Collections;

public class AddToObj : MonoBehaviour {

    private SpaceJunkSystem spaceJunkScript;

    private float killTime;
    public bool kill = true;

    private GameObject obj;

    public void SetScript(SpaceJunkSystem sjsScript)
    {
        spaceJunkScript = sjsScript;
        killTime = 5f;
    }

    public void SetObj()
    {
        obj = transform.parent.gameObject;
    }
    
    void Update()
    {
        if (kill)
        {
            if (killTime > 0)
            {
                killTime -= Time.deltaTime;
            }
            else
            {
                Destroy(obj);
            }
        }
    }
    
    void OnDestroy()
    {
        spaceJunkScript.ReUpJunkCount();
        Debug.Log("killed");
    }
}
