using System.Collections.Generic;
using UnityEngine;

public class TurnManager : MonoBehaviour
{
    public static TurnManager instance;
    void Awake(){
        if(instance == null)
            instance = this;
        else
            Destroy(this);
    }

    void Start(){
        Station[] stations = FindObjectsOfType<Station>();
    }

    private int turnCounter = 0;

    private int turnIndex = 0;
    private int playerCount = 0;
    public void SetPlayerCount(int _value) => playerCount = _value;

    public List<PlayerBase> PlayerList { get; private set; }
    public void SetPlayerList(List<PlayerBase> _playerList) => PlayerList = _playerList;
    public void AddPlayer(PlayerBase _player){
        PlayerList.Add(_player);
        playerCount = PlayerList.Count;
    }
    public void RemovePlayer(PlayerBase _player){
        PlayerList.Remove(_player);
        playerCount = PlayerList.Count;
    }

    public void NextTurn(){
        turnCounter ++;
        PlayerList[turnIndex].SetIsTurn(false);
        turnIndex ++;
        if(turnIndex >= playerCount)
            turnIndex = 0;
        PlayerList[turnIndex].SetIsTurn(true);
        currentPlayer = PlayerList[turnIndex];
    }

    #region Turn Actions
    PlayerBase currentPlayer;
    public void DrawTech(){
        Card newCard = DeckManager.instance.GiveNextCard();
        HandController.instance.AddToHand(newCard);
    }

    private Planet selectedPlanet;
    public void SetSelectedPlanet(Planet _selectedPlanet) => selectedPlanet = _selectedPlanet;
    public void CheckEmbark(){
        int availableUnits = 0;
        for(int i = 0; i < selectedPlanet.GetUnitsOnPlanet(); i ++){
                availableUnits++;
        }
        if(availableUnits > 0)
            SetUnitsToEmbark();
    }
    private int numUnitsToEmbark;
    void SetUnitsToEmbark(){
        selectedPlanet.StartEmbarkingUnits();
    }
    public void Embark(int _numUnits, Ship _ship){
        List<Ship> ships = selectedPlanet.GetShips();
        for(int i = 0; i < _numUnits; i++){
            _ship.AddUnitToShip();
            selectedPlanet.RemoveUnitFromPlanet();
        }
    }

    int spacesToMove;
    Ship selectedShip;
    public void SetSelectedShip(Ship _ship) => selectedShip = _ship;
    public void Launch(){
        spacesToMove = RollEngineValue();
        ChooseFlightDestination();
    }
    int distanceBetweenPlanets; //I will return to figure out how to set this value.
    public void ChooseFlightDestination(){
        spacesToMove -= distanceBetweenPlanets;
    }
    
    public void Fire(Planet _planet, Ship _ship = null, Station _station = null){
        int defense = 0;
        wallsDestroyed = 0;
        defense += _planet.GetUnitsOnPlanet();
        if(_ship != null){
            defense += _ship.GetUnitsOnShip();
        }
        if(_station != null){
            defense += _station.GetStationLevel();
        }
        int attackPower = RollEngineValue();
        if(attackPower > defense){
            attackPower -= defense;
            if(_ship != null)
                for(int i = 0; i < attackPower; i++){
                    _ship.RemoveUnitsFromShip();
                    attackPower--;
                    }
            for(int i = 0; i < attackPower; i++){
                _planet.RemoveUnitFromPlanet();
                attackPower--;
            }
            if(_station != null)
                for(int i = 0; i < attackPower; i++){
                    _station.DecreaseStationLevel();
                    attackPower--;
                    wallsDestroyed++;
                }
        }
    }

    int wallsDestroyed = 0;
    public void Conquer(){
        for(int i = 0; i < wallsDestroyed; i++)
            selectedPlanet.GetStation().IncreaseStationLevel();
    }

    Station[] stations;
    public void SetStationActions(){
        foreach(var s in stations)
            if(s.GetControllingPlayer() == currentPlayer)
                stationActions++;
    }
    int stationActions = 0;
    public void Establish(){
        stationActions--;
        selectedPlanet.GetStation().IncreaseStationLevel();
    }
    public void Fortify(){
        Establish();
    }

    public void Research(){
        int cardsToResearch = 0;
        if(selectedPlanet.GetStation() != null)
            cardsToResearch = selectedPlanet.GetUnitsOnPlanet();
        HandController.instance.AddResearchHand(DeckManager.instance.DrawCards(cardsToResearch).ToArray());
    }

    public void Populate(){
        if(currentPlayer.GetUnitsAvailable() > 0)
            if(selectedPlanet.GetStation() != null)
                selectedPlanet.AddUnitToPlanet(1, currentPlayer);
    }

    public void Retaliate(){
        //Rule unclear, will figure out later
    }

    public void Upgrade(){
        
    }
    #endregion

    int RollEngineValue(){
        int enginePower = currentPlayer.GetEngineValue;
        int rollVal = 0;
        switch(enginePower){
            case 4:
            rollVal = DiceManager.instance.RollD4();
            break;
            case 6:
            rollVal = DiceManager.instance.RollD6();
            break;
            case 8:
            rollVal = DiceManager.instance.RollD8();
            break;
            case 12:
            rollVal = DiceManager.instance.RollD12();
            break;
        }
        return rollVal;
    }
}
