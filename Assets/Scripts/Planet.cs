using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Planet : MonoBehaviour
{
    private TextMeshProUGUI nameText;
    private IntButton intButton;

    void Start(){
        nameText = GetComponentInChildren<TextMeshProUGUI>();
        nameText.text = gameObject.name;

        intButton = GetComponentInChildren<IntButton>();

        CheckForPlanets();
    }

    private int maxUnitsOnPlanet = 3;
    private int numUnitsOnPlanet = 0;
    public bool CanAddUnits() => numUnitsOnPlanet < maxUnitsOnPlanet;
    // private List<PlayerUnit> unitsOnPlanet = new List<PlayerUnit>();
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

    private int planetCheckDistance = 300;
    private List<Planet> nearbyPlanets = new List<Planet>();
    private Dictionary<Planet, int> planetDistanceDict = new Dictionary<Planet, int>();
    public void CheckForPlanets(){
        Planet p;
        foreach(RaycastHit h in Physics.SphereCastAll(transform.position, planetCheckDistance, Vector3.up)){
            if(p = h.collider.GetComponentInParent<Planet>())
                if(!nearbyPlanets.Contains(p))
                    if(p != this)
                        nearbyPlanets.Add(p);
        }
        SetTravelDistances();
    }
    void SetTravelDistances(){
        int movementCost = 1;
        foreach(Planet p in nearbyPlanets){
            float distance = Vector3.Distance(transform.position, p.transform.position);
            if(distance < 100)
                movementCost = 1;
            if(distance >= 100 && distance < 150)
                movementCost = 2;
            if(distance >= 150 && distance < 200)
                movementCost = 3;
            if(distance >= 200 && distance < 250)
                movementCost = 4;
            if(distance >= 250 && distance < 300)
                movementCost = 5;
            if(distance >= 300)
                movementCost = 6;
            planetDistanceDict.Add(p, movementCost);
        }
        foreach(Planet p in planetDistanceDict.Keys){
            Debug.Log($"{p.gameObject.name} is {planetDistanceDict[p]} away!");
            LineRenderer lineRenderer = ObjectPool.instance.GiveLineRenderer().GetComponent<LineRenderer>();
            lineRenderer.transform.parent = transform;
            lineRenderer.SetPosition(0, this.transform.position);
            lineRenderer.SetPosition(1, p.transform.position);
        }
    }
}
