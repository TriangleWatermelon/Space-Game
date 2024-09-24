using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Planet : MonoBehaviour
{
    // public string planetName;
    private TextMeshProUGUI nameText;
    private IntButton intButton;

    void Start(){
        nameText = GetComponentInChildren<TextMeshProUGUI>();
        nameText.text = gameObject.name;

        intButton = GetComponentInChildren<IntButton>();
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

    private PlayerBase controllingPlayer;
    public PlayerBase GetControllingPlayer() => controllingPlayer;
    public void SetControllingPlayer(PlayerBase _player = null) => controllingPlayer = _player;
}
