using UnityEngine;
using System.Collections;

public class TripMine : MonoBehaviour {

    //We want it to constantly check by linecasting to a point

    public LineRenderer laser;
    public GameObject laserEnd;
    public GameObject laserStart;
    public int teamCount;
    public SpriteRenderer colorStrip;
    public int tripMineDamage;

    public float finalAngle;

    public GameObject laserHitFx;
    private GameObject laserHitFxCopy;
    private ParticleSystem laserHitParticles;
    public GameObject destroyFx;
    private GameObject destroyFxCopy;
    private ParticleSystem destroyFxParticles;

    private RaycastHit2D laserHit;
    private int toBeEffectedLayer;
    private Vector2 hitAnyVector;
    private Vector2 startPos;
    private Vector2 endPos;

    private ShieldMine mineScript;
    private ProtoShotPrefab shotScript;
    private LineSniperShot bulletScript;
    private EnemySeekerShot seekerShotScript;
    private PhaseBlast phaseBlastScript;
    private Plasma plasmaScript;

    private PlayerHealth otherHealthScript;
    
    void Awake()
    {
        laser = GetComponentInChildren<LineRenderer>();
        toBeEffectedLayer = LayerMask.GetMask("ThingsToBeEffected");
    }

    void Update()
    {
        TripMineActive();
    }

    void TripMineActive()
    {
        startPos = laserStart.transform.position;
        laser.SetPosition(0, startPos);

        //Show Laser

        endPos = laserEnd.transform.position;
        laser.enabled = true;

        //Check for target
        laserHit = Physics2D.Linecast(startPos, endPos, toBeEffectedLayer);

        if (laserHit)
        {
            hitAnyVector = laserHit.point;
            laser.SetPosition(1, hitAnyVector);
            if (laserHit.transform.tag == "Player1(Black)" || laserHit.transform.tag == "Player2(White)" || laserHit.transform.tag == "Player3" || laserHit.transform.tag == "Player4")
            {
                otherHealthScript = laserHit.transform.GetComponent<PlayerHealth>();
                if (otherHealthScript.teamCount != teamCount)
                {
                    //blow up
                    laserHitFxCopy = Instantiate(laserHitFx, transform.position, Quaternion.identity)as GameObject;
                    laserHitFxCopy.transform.SetParent(this.transform.parent);

                    laserHitFxCopy.transform.localEulerAngles = transform.localEulerAngles;

                    laserHitParticles = laserHitFxCopy.GetComponentInChildren<ParticleSystem>();
                    laserHitParticles.startColor = colorStrip.color;
                    BlowUp();
                    //Damage enemy
                    otherHealthScript.Damage(tripMineDamage, false);
                }
            }
        }
        else
        {
            laser.SetPosition(1, endPos);
        }
    }

    public void BlowUp()
    {
        destroyFxCopy = Instantiate(destroyFx, transform.position, Quaternion.identity) as GameObject;
        destroyFxParticles = destroyFxCopy.GetComponent<ParticleSystem>();
        destroyFxParticles.startColor = colorStrip.color;
        Destroy(this.gameObject);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "SniperBullet")
        {
            bulletScript = other.GetComponent<LineSniperShot>();
            if (bulletScript.teamCount != teamCount)
            {
                //Damage
                BlowUp();
            }
        }
        else if (other.tag == "Shot")
        {
            shotScript = other.GetComponent<ProtoShotPrefab>();
            if (shotScript.teamCount != teamCount)
            {
                //Damage
                BlowUp();
            }
        }
        else if (other.tag == "ShieldMine")
        {
            mineScript = other.GetComponent<ShieldMine>();
            if (mineScript.teamCount != teamCount)
            {
                BlowUp();
            }
        }
        else if (other.tag == "Missile")
        {
            seekerShotScript = other.GetComponent<EnemySeekerShot>();
            if (seekerShotScript.teamCount != teamCount)
            {
                //Damage
                BlowUp();
            }
        }
        else if (other.tag == "PhaseBlast")
        {
            phaseBlastScript = other.GetComponent<PhaseBlast>();
            if (phaseBlastScript.teamCount != teamCount)
            {
                //Damage
                BlowUp();
            }
        }
        else if (other.tag == "Plasma")
        {
            plasmaScript = other.GetComponent<Plasma>();
            if (plasmaScript.teamCount != teamCount)
            {
                //Damage
                BlowUp();
            }
        }
    }
}
