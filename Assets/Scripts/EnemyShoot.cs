using UnityEngine;
using System.Collections;

public class EnemyShoot : MonoBehaviour {

    public int enemyType;

    //Fodder
    public GameObject shotPrefab;

    public float shotSpeed;
    public int shotCoolDownTime;
    public GameObject barrelEnd;
    public GameObject firePoint;

    private GameObject shotInstance;

    //Seeker
    public GameObject seekerShotPrefab;
    public GameObject seekerFirePointA;
    public GameObject seekerFirePointB;
    public GameObject seekerBarrelEndA;
    public GameObject seekerBarrelEndB;

    private GameObject shotInstanceA;
    private GameObject shotInstanceB;
    private EnemyFollow seekerShotFollow;
    public Transform seekerTarget;
    
    private int shotCoolDown;
    
    private Rigidbody2D rb2D;    

    void Update()
    {
        if (shotCoolDown > 0)
        {
            shotCoolDown -= 1;
        }
        else
        {
            FireShot();
            shotCoolDown = shotCoolDownTime;
        }
    }

    void FireShot()
    {
        //Create shotInstance
        
        if (enemyType == 0)
        {
            shotInstance = Instantiate(shotPrefab, firePoint.transform.position, Quaternion.identity) as GameObject;
            //Grab and set Rotation
            shotInstance.transform.eulerAngles = new Vector3(transform.eulerAngles.x, transform.eulerAngles.y, transform.rotation.z);

            rb2D = shotInstance.GetComponent<Rigidbody2D>();
            Vector2 straight = barrelEnd.transform.position - firePoint.transform.position;
            rb2D.AddForce(straight * shotSpeed);
        }
        else if (enemyType == 1 && seekerTarget != null)
        {
            shotInstanceA = Instantiate(seekerShotPrefab, seekerFirePointA.transform.position, Quaternion.identity) as GameObject;
            shotInstanceB = Instantiate(seekerShotPrefab, seekerFirePointB.transform.position, Quaternion.identity) as GameObject;

            shotInstanceA.transform.eulerAngles = new Vector3(transform.eulerAngles.x, transform.eulerAngles.y, transform.rotation.z);
            shotInstanceB.transform.eulerAngles = new Vector3(transform.eulerAngles.x, transform.eulerAngles.y, transform.rotation.z);

            seekerShotFollow = shotInstanceA.GetComponent<EnemyFollow>();
            seekerShotFollow.target = seekerTarget;

            seekerShotFollow = shotInstanceB.GetComponent<EnemyFollow>();
            seekerShotFollow.target = seekerTarget;

            rb2D = shotInstanceA.GetComponent<Rigidbody2D>();
            Vector2 straightA = seekerBarrelEndA.transform.position - seekerFirePointA.transform.position;
            rb2D.AddForce(straightA * shotSpeed);
            rb2D = shotInstanceB.GetComponent<Rigidbody2D>();
            Vector2 straightB = seekerBarrelEndB.transform.position - seekerFirePointB.transform.position;
            rb2D.AddForce(straightB * shotSpeed);
        }
        
    }

}
