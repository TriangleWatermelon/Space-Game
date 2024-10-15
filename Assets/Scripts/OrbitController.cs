using System.Collections;
using UnityEngine;

public class OrbitController : MonoBehaviour
{
    Druzhnny druzhnny;
    void Awake(){
        if(instance == null)
            instance = this;
        else
            Destroy(this);
        druzhnny = FindObjectOfType<Druzhnny>();

        planets = FindObjectsOfType<Planet>();
    }
    public static OrbitController instance;

    void Update(){
        if(Input.GetKeyDown(KeyCode.O))
            StartOrbit();
    }

    public OrbitRing[] rings;
    public void CheckPlayerControl(){
        int druzhnnyControlledPlanets = 0;
        for(int i = 0; i < rings.Length; i++){
            rings[i].CheckControllingPlayer();
            if(rings[i].GetHasDruzhnnyPlanet())
                druzhnnyControlledPlanets++;
        }
        if(druzhnnyControlledPlanets == 3)
            druzhnny.AddOnePoint();
    }

    bool isOrbit = false;
    public void StartOrbit(){
        if(isOrbit)
            return;

        if(FlipCoin())
            orbitDir = 10;
        else
            orbitDir = -10;

        orbitTime = 0;
        orbit = StartCoroutine(Orbit());
    }

    int orbitDir = 1;
    int orbitIndex = 0;
    float orbitTime = 0;
    Coroutine orbit;
    IEnumerator Orbit(){
        while(orbitTime < 3){
            yield return new WaitForEndOfFrame();
            rings[orbitIndex].transform.RotateAround(Vector3.zero, Vector3.up, orbitDir * Time.deltaTime);
            orbitTime += Time.deltaTime;
        }
        orbitIndex++;
        if(orbitIndex >= rings.Length)
            orbitIndex = 0;
            
        ReconnectPlanets();
        StopCoroutine(orbit);
    }

    private Planet[] planets;
    void ReconnectPlanets(){
        foreach(Planet p in planets)
            p.CheckForPlanets();
    }

    bool FlipCoin() => Random.Range(0, 2) > 0;
}
