using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Planet : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI nameText;
    [SerializeField]
    private IntButton intButton;

    void Start(){
        nameText.text = gameObject.name;

        CheckForPlanets();
    }

    private int maxUnitsOnPlanet = 3;
    private int numUnitsOnPlanet = 0;
    public bool CanAddUnits() => numUnitsOnPlanet < maxUnitsOnPlanet;
    public int GetUnitsOnPlanet() => numUnitsOnPlanet;
    public void AddUnitToPlanet(int _units, PlayerBase _player){
        for(int i = 0; i < _units; i++){
            _player.UseUnit();
            numUnitsOnPlanet ++;
        }
    }
    public void RemoveUnitFromPlanet(){
        numUnitsOnPlanet --;
    }
    private Ship selectedShip;
    public void SetSelectedShip(Ship _ship) => selectedShip = _ship;
    public void StartEmbarkingUnits(){
        intButton.gameObject.SetActive(true);
    }
    public void FinishEmbarkingUnits(){
        TurnManager.instance.Embark(intButton.SetNum(), selectedShip);
    }

    private Station station;
    public Station GetStation() => station;
    public void SetStation(Station _station = null) => station = _station;

    private List<Ship> ships = new List<Ship>();
    public List<Ship> GetShips() => ships;
    public void AddShip(Ship _ship){
        ships.Add(_ship);
    }
    public void RemoveShip(Ship _ship){
        ships.Remove(_ship);
    }
    [SerializeField]
    GameObject shipDockObj;

    private PlayerBase controllingPlayer;
    public PlayerBase GetControllingPlayer() => controllingPlayer;
    public void SetControllingPlayer(PlayerBase _player = null) => controllingPlayer = _player;

    [SerializeField]
    private List<Planet> nearbyPlanets = new List<Planet>();
    public List<Planet> GetNearbyPlanets() => nearbyPlanets;
    private Dictionary<Planet, int> planetDistanceDict = new Dictionary<Planet, int>();
    public Dictionary<Planet, int> GetPanetsWithDistance() => planetDistanceDict;
    private int planetCheckDistance = 400;
    public void CheckForPlanets(){
        nearbyPlanets.Clear();
        Planet p;
        foreach(RaycastHit h in Physics.SphereCastAll(transform.position, planetCheckDistance, Vector3.up)){
            if(p = h.collider.GetComponentInParent<Planet>())
                if(!nearbyPlanets.Contains(p))
                    if(p != this)
                        nearbyPlanets.Add(p);
        }
        lineRenderer.positionCount = nearbyPlanets.Count * 2;
        SetTravelDistances();
    }
    [SerializeField]
    LineRenderer lineRenderer;
    int lineIndex = 0;
    void SetTravelDistances(){
        planetDistanceDict.Clear();
        lineIndex = 0;
        int movementCost = 1;
        foreach(Planet p in nearbyPlanets){
            float distance = Vector3.Distance(transform.position, p.transform.position);
            if(distance < 75)
                movementCost = 1;
            if(distance >= 75 && distance < 125)
                movementCost = 2;
            if(distance >= 125 && distance < 200)
                movementCost = 3;
            if(distance >= 200 && distance < 275)
                movementCost = 4;
            if(distance >= 275 && distance < 350)
                movementCost = 5;
            if(distance >= 350)
                movementCost = 6;
            planetDistanceDict.Add(p, movementCost);
        }
        // for(int i = 0; i < nearbyPlanets.Count; i += 2){
        //     lineRenderer.SetPosition(i, this.transform.position);
        //     lineRenderer.SetPosition(i + 1, nearbyPlanets[i].transform.position);
        // }
        foreach(Planet p in nearbyPlanets){
            lineRenderer.SetPosition(lineIndex, this.transform.position);
            lineRenderer.SetPosition(lineIndex + 1, p.transform.position);
            lineIndex += 2;
        }
    }
}
