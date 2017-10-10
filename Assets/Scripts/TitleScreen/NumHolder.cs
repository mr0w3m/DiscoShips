using UnityEngine;
using System.Collections;

public class NumHolder : MonoBehaviour {

    public int numConnectedPlayers;

    void Awake()
    {
        DontDestroyOnLoad(this.gameObject);
    }

    void OnDestroy()
    {
        Debug.Log("numHolder discarded | playersConnected: " + numConnectedPlayers);
    }
}
