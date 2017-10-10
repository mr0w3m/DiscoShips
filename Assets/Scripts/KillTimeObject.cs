using UnityEngine;
using System.Collections;

public class KillTimeObject : MonoBehaviour {

    public float killTime;

    public bool mapScreen;
    private int i = 0;

	void Start()
    {
        Destroy(this.gameObject, killTime);
        i += 1;
    }

    void OnEnable()
    {
        if (mapScreen && i > 0)
        {
            Destroy(this.gameObject);
        }
    }
}
