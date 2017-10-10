using UnityEngine;
using System.Collections;

public class AutoTurretDamage : MonoBehaviour {

    private ShieldMine mineScript;
    private ProtoShotPrefab shotScript;
    private LineSniperShot bulletScript;
    private EnemySeekerShot seekerShotScript;
    private PhaseBlast phaseBlastScript;
    private Plasma plasmaScript;

    public AutoTurret myTurretScript;

    void Awake()
    {
        myTurretScript = GetComponentInParent<AutoTurret>();
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Missile")
        {
            seekerShotScript = other.GetComponent<EnemySeekerShot>();
            if (seekerShotScript.teamCount != myTurretScript.teamCount)
            {
                //Damage
                myTurretScript.DamageThis(seekerShotScript.seekerDamage);
            }
        }
        else if (other.tag == "ShieldMine")
        {
            mineScript = other.GetComponent<ShieldMine>();
            if (mineScript.teamCount != myTurretScript.teamCount)
            {
                myTurretScript.DamageThis(mineScript.mineDamage);
            }
        }
        else if (other.tag == "SniperBullet")
        {
            bulletScript = other.GetComponent<LineSniperShot>();
            if (bulletScript.teamCount != myTurretScript.teamCount)
            {
                //Damage
                myTurretScript.DamageThis(bulletScript.lineSniperShotDamage);
            }
        }
        else if (other.tag == "Shot")
        {
            shotScript = other.GetComponent<ProtoShotPrefab>();
            if (shotScript.teamCount != myTurretScript.teamCount)
            {
                //Damage
                myTurretScript.DamageThis(shotScript.shotDamage);
            }
        }
        else if (other.tag == "PhaseBlast")
        {
            phaseBlastScript = other.GetComponent<PhaseBlast>();
            if (phaseBlastScript.teamCount != myTurretScript.teamCount)
            {
                myTurretScript.DamageThis(phaseBlastScript.phaseBlastDamage);
            }
        }
        else if (other.tag == "Plasma")
        {
            plasmaScript = other.GetComponent<Plasma>();
            if (plasmaScript.teamCount != myTurretScript.teamCount)
            {
                myTurretScript.DamageThis(plasmaScript.plasmaDamage);
            }
        }
    }
}
