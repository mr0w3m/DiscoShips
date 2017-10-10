using UnityEngine;
using System.Collections;

public class ExitCollider : MonoBehaviour {

    private AddToObj addToObjScript;

	void OnTriggerExit2D(Collider2D other)
    {
        if (other.GetComponentInChildren<AddToObj>() != null)
        {
            addToObjScript = other.GetComponentInChildren<AddToObj>();
            addToObjScript.kill = true;
            Debug.Log("exitedCollider");
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.GetComponentInChildren<AddToObj>() != null)
        {
            addToObjScript = other.GetComponentInChildren<AddToObj>();
            addToObjScript.kill = false;
            Debug.Log("enteredCollider");
        }
    }
}
