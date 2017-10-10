using UnityEngine;
using System.Collections;

public class JunkHealth : MonoBehaviour
{
    public float entranceSmoothing;

    public int hitPoints;

    public GameObject junkDestroyObj;

    //Shot scripts
    private RemoteLauncherShot rocketScript;
    private ProtoShotPrefab shotScript;
    private LineSniperShot bulletScript;
    private EnemySeekerShot homingMissileScript;
    private PhaseBlast phaseBlastScript;
    private Plasma plasmaScript;
    private ShieldMine shieldMineScript;

    private Vector3 normalSize = new Vector3(1f, 1f, 1f);
    
    private float decayAmount = 15f;

    /*
    get hit by:
    rapidshot-----
    shield mines-----
    linesniperbullet----
    plasma-----
    homing missiles----
    needlegatling-----
    phaseBlaster-----
    remotelauncher -on remote rocket shot------------
    homing missiles laser -onlaser--------------
    laserRifle  -on laser rifle-----------
    grenade  - on grenade-----------
    grenade blow up on contact - on grenade-------
    */
    
    IEnumerator LerpToSize()
    {
        while(transform.localScale.x < normalSize.x)
        {
            transform.localScale = Vector3.Lerp(transform.localScale, normalSize, entranceSmoothing * Time.deltaTime);
            yield return null;
        }
    }

    void Start()
    {
        transform.localScale = new Vector3(0, 0, 0);
        StartCoroutine(LerpToSize());

    }

    void Update()
    {
        Decay();
    }

    void Decay()
    {
        if (decayAmount > 0)
        {
            decayAmount -= Time.deltaTime;
        }
        else
        {
            DestroyJunk();
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "SniperBullet")
        {
            bulletScript = other.GetComponent<LineSniperShot>();
            DamageJunk(bulletScript.lineSniperShotDamage);
        }
        else if (other.tag == "Missile")
        {
            homingMissileScript = other.GetComponent<EnemySeekerShot>();
            DamageJunk(homingMissileScript.seekerDamage);
        }
        else if (other.tag == "Shot")
        {
            shotScript = other.GetComponent<ProtoShotPrefab>();
            DamageJunk(shotScript.shotDamage);
        }
        else if (other.tag == "PhaseBlast")
        {
            phaseBlastScript = other.GetComponent<PhaseBlast>();
            DamageJunk(phaseBlastScript.phaseBlastDamage);
        }
        else if (other.tag == "Plasma")
        {
            //damage this
            plasmaScript = other.GetComponent<Plasma>();
            DamageJunk(plasmaScript.plasmaDamage);
        }
        else if (other.tag == "ShieldMine")
        {
            shieldMineScript = other.GetComponent<ShieldMine>();
            DamageJunk(hitPoints);
        }
    }

    public void DamageJunk(int amtDamaged)
    {
        hitPoints -= amtDamaged;
        if (hitPoints < 1)
        {
            DestroyJunk();
        }
    }

    void DestroyJunk()
    {
        Instantiate(junkDestroyObj, transform.position, Quaternion.identity);
        Destroy(this.gameObject);
    }
}
