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
    }
    public static OrbitController instance;

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
}
