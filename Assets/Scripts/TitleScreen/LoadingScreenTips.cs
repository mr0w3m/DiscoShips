using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class LoadingScreenTips : MonoBehaviour {

    private List<string> tips = new List<string>();

    private Text myText;

    void Awake()
    {
        myText = GetComponent<Text>();

        tips.Add("If your opponent has twice your hit points, don't engage them!");
        tips.Add("Your health is your ammo!");
        tips.Add("You can boost through asteroids to destroy them.");
        tips.Add("If you have a 2:1 hit points advantage, pursue!");
        tips.Add("Your ship will start flashing red at 30 hit points, find some health!");
        tips.Add("If using projectile weapons, don't forget to lead your shots.");
        tips.Add("Each ship has a health circle that expands/contracts relative to your hit points.");
        tips.Add("To view your remaining hit points, look to your corner UI!");
        tips.Add("High burst damage weapons require a good chase strategy!");
        tips.Add("The overcharge buff lets you fire without the cost of health!");
        tips.Add("The speed boost makes you move and boost faster!");
        tips.Add("Trip mines only take one shot to destroy, but cause a lot of damage!");
        tips.Add("All abilities have cooldowns! The ability meter is to the right of your health circle.");
        tips.Add("Use your ability! They cost nothing and have the potential to assist you in many ways.");
        tips.Add("Damage Bugs can be shot off, turn your weapon to blast them!");
        tips.Add("Shield Mines block projectiles but also do a high amount of burst damage on contact!");
        tips.Add("Auto Turrets and Trip Mines can be placed onto asteroids.");
        tips.Add("The Tractor Beam can be a quick way to snag an asteroid as a shield in a pinch.");
        tips.Add("You can Grav Hault almost anything in motion, even other players!");
        tips.Add("Drop Junk offers a great way to cut off a chase at low HP");
        tips.Add("The Line Rifle can fire through up to 2 asteroids, no one is safe!");
        tips.Add("Each time a Phase Blast hits something, it grows in size and increases in damage.");
        tips.Add("When using the Phase Blaster, try firing through asteroids for a larger projectile.");
        tips.Add("You can steer the Remote Launcher shot with the right stick and still maneuver normally.");
        tips.Add("Each time a Grenade Gun shot bounces it reduces the explosion time by half.");
        tips.Add("The larger circles of the Plasma Thrower are what actually do the damage.");
        tips.Add("Get in time with the Laser Rifle's fire cooldown to know when your aim counts!");
        tips.Add("The environmental damage you take can be traced back to poor maneuvers, use your boost wisely!");
        
    }

	void OnEnable()
    {
        //Change text to random saying
        myText.text = tips[Random.Range(0, tips.Count)];
    }


}
