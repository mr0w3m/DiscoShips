using UnityEngine;
using System.Collections.Generic;
using System.Collections;

public class PotentialTarget
{
    public GameObject obj;
    public bool visible;
    public int teamCount;
    public float distanceAway;

    public PotentialTarget (GameObject objCon, bool visibleCon, int teamCountCon)
    {
        obj = objCon;
        visible = visibleCon;
        teamCount = teamCountCon;
    }

    public void PrintMe()
    {
        Debug.Log("objtag: " + obj.tag + " visible: " + visible + " teamCount: "+teamCount);
    }
}

public class AutoTurret : MonoBehaviour
{

	public int teamCount;
    public LineRenderer laser;
    public SpriteRenderer colorCircle;
    public GameObject healthCircle;
    public GameObject laserStart;
    public GameObject laserEnd;
    private int toBeEffectedLayer;
    public GameObject turretHead;
    public float smoothing;

    public GameObject destroyFx;
    private GameObject destroyFxCopy;
    private ParticleSystem destroyFxParticles;

    public float maxDistance;

    private GameObject currentTarget; //Target while inside our zone.
    private PlayerHealth currentTargetHealthScript;

    //private float turretRotReset = 0; //Set rotation of turret back to normal.
    
    private float damageTimer; //To find when 1 second is up.
    public int damageAmount; //Amount of damage done per second.
    public ParticleSystem shotParticles; //This goes onto the turret head to it always faces the right direction.
    public ParticleSystem rangeIndicator;

    private RaycastHit2D linecastHit;
    private PlayerHealth healthScript;

    private List<PotentialTarget> targets = new List<PotentialTarget>();
    private float[] distances = new float[4];

    private float closestDistance;

    private float lowestNum;
    private int closestIndex;

    //Healthstuff
    public int hitPoints;


    void Awake()
    {
        closestDistance = 13;
        toBeEffectedLayer = LayerMask.GetMask("ThingsToBeEffected");
        rangeIndicator = GetComponent<ParticleSystem>();
    }


    void Update()
    {
        DisplayHealth();
        if (targets.Count > 0)
        {
            CheckForTargets();
        }

        //The ultimate decider
        if (targets.Count > 0 && targets[closestIndex].distanceAway < maxDistance && targets[closestIndex].visible)
        {
            if (targets[closestIndex].obj != null)
            {
                laser.enabled = true;
                //Fire at target.
                laser.SetPosition(0, laserStart.transform.position);
                laser.SetPosition(1, targets[closestIndex].obj.transform.position);
                turretHead.transform.up = Vector2.Lerp(turretHead.transform.up, targets[closestIndex].obj.transform.position - turretHead.transform.position, smoothing * Time.deltaTime);
                DamageEnemyPlayer();
            }
        }
        else
        {
            laser.enabled = false;
            //lerp turrethead back to center.
            turretHead.transform.localEulerAngles = new Vector3(turretHead.transform.eulerAngles.x, turretHead.transform.eulerAngles.y, 0f);
        }
        
        if (targets.Count > 0)
        {
            //Loop through all of the targets and find which is the closest
            lowestNum = maxDistance;
            for (int i = 0; i < targets.Count; i++)
            {
                distances[i] = Vector2.Distance(targets[i].obj.transform.position, transform.position);

                targets[i].distanceAway = distances[i];
                if (distances[i] < maxDistance)//if they're in range
                {
                    if (targets[i].visible && targets[i].teamCount != teamCount)//if they're visible
                    {
                        if (distances[i] < lowestNum)//if they're the closest
                        {
                            lowestNum = distances[i];
                            closestIndex = i;
                        }
                    }
                }
            }
            //Here we have the closest player.
        }
    }

    public void DamageThis(int amountDamaged)
    {
        hitPoints -= amountDamaged;
        if (hitPoints < 1)
        {
            DestroyThis();
        }
    }

    public void DisplayHealth()
    {
        float fillNum; //num for final float fill amount
        fillNum = (float)hitPoints * 0.1f; //multiply by .01 to put the 1/100 value to 0.00/1.00
        healthCircle.transform.localScale = new Vector3(fillNum, fillNum, fillNum);
    }

    void CheckForTargets()
    {
        if (targets.Count > 0)
        {
            //loop through list and find out which ones are visible.
            for (int i = 0; i < targets.Count; i++)
            {
                if (targets[i].obj != null)
                {
                    linecastHit = Physics2D.Linecast(laserStart.transform.position, targets[i].obj.transform.position, toBeEffectedLayer);
                    if (linecastHit) //if it hits
                    {
                        if (linecastHit.transform.tag == "Player1(Black)" || linecastHit.transform.tag == "Player2(White)" || linecastHit.transform.tag == "Player3" || linecastHit.transform.tag == "Player4") //if it hits the target we want
                        {
                            healthScript = linecastHit.transform.GetComponent<PlayerHealth>();
                            targets[i].teamCount = healthScript.teamCount;
                            targets[i].visible = true;
                        }
                        else //We didn't hit the target we wanted to
                        {
                            targets[i].visible = false;
                        }
                    }
                    else //if it doesn't hit anything
                    {

                    }
                }
            }
        }
    }
    
    void UpdateList(bool add, PotentialTarget target)
    {
        if (add)
        {
            targets.Add(target);
        }
        else //take away
        {
            targets.Remove(target);
        }
    }

    void DamageEnemyPlayer()
    {
        if (damageTimer > 0)
        {
            damageTimer -= Time.deltaTime;
        }
        else
        {
            damageTimer = 1;
            currentTargetHealthScript = targets[closestIndex].obj.GetComponent<PlayerHealth>();
            currentTargetHealthScript.Damage(damageAmount, false);
            shotParticles.Play();
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player1(Black)" || other.tag == "Player2(White)" || other.tag == "Player3" || other.tag == "Player4")
        {
            PlayerHealth tempHealthScript = other.GetComponent<PlayerHealth>();
            if (tempHealthScript.teamCount != teamCount)
            {
                PotentialTarget newTarget = new PotentialTarget(other.gameObject, false, tempHealthScript.teamCount);
                UpdateList(true, newTarget);
            }
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.tag == "Player1(Black)" || other.tag == "Player2(White)" || other.tag == "Player3" || other.tag == "Player4")
        {
            PlayerHealth tempHealthScript = other.GetComponent<PlayerHealth>();

            if (tempHealthScript.teamCount != teamCount)
            {
                for (int i = 0; i < targets.Count; i++)
                {
                    if (targets[i].teamCount == tempHealthScript.teamCount)
                    {
                        UpdateList(false, targets[i]);
                    }
                }
            }
        }
    }

    public void DestroyThis()
    {
        destroyFxCopy = Instantiate(destroyFx, transform.position, Quaternion.identity) as GameObject;
        destroyFxParticles = destroyFxCopy.GetComponent<ParticleSystem>();
        destroyFxParticles.startColor = colorCircle.color;
        //Instantiate death particles
        Destroy(this.gameObject);
    }
}
