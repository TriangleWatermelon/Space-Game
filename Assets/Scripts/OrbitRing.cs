using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrbitRing : MonoBehaviour
{
    public Planet[] planets;
    Druzhnny druzhnny;

    void Awake(){
        druzhnny = FindObjectOfType<Druzhnny>();
        foreach(var p in planets)
            p.transform.parent = this.transform;
    }

    bool hasDruzhnnyPlanet = false;
    public bool GetHasDruzhnnyPlanet() => hasDruzhnnyPlanet;
    public void CheckControllingPlayer(){
        int druzhnnyControlledPlanets = 0;
        foreach (Planet p in planets){
            if(p.GetControllingPlayer() == druzhnny){
                druzhnnyControlledPlanets++;
            }
        }
        if(druzhnnyControlledPlanets > 0)
            hasDruzhnnyPlanet = true;
        else
            hasDruzhnnyPlanet = false;
        if(planets.Length == druzhnnyControlledPlanets)
            druzhnny.AddTwoPoints();
    }
}
