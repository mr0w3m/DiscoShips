using UnityEngine;
using System.Collections;

public class EnemyHealth : MonoBehaviour {

    public int teamCount;
    public int enemyType;
    public int hitPoints;
    

    void Start()
    {

    }

    public void DamageEnemy(int amt)
    {
        hitPoints -= amt;
        Debug.Log("DamageEnemy");
    }

    void Update()
    {
        if (hitPoints < 1)
        {
            Destroy(this.gameObject);
        }
    }
}
