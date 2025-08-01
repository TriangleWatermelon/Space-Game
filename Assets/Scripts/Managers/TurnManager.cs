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
        if(turnIndex >= playerCount - 1)
            turnIndex = 0;
        PlayerList[turnIndex].SetIsTurn(true);
        currentPlayer = PlayerList[turnIndex];
        if(turnIndex/playerCount - 1 == 1)
            OrbitController.instance.StartOrbit();
    }

    #region Turn Actions
    PlayerBase currentPlayer;
    public PlayerBase GetCurrentPlayer() => currentPlayer;
    public void DrawTech(){
        Card newCard = DeckManager.instance.GiveNextCard();
        currentPlayer.hand.AddToHand(newCard);
    }
    int shipActionsAvailable = 0;
    int shipActionReset = 0;
    public void CalculateShipActions(){
        foreach(Ship s in currentPlayer.GetShips())
            if(s.GetActionStatus())
                shipActionsAvailable++;

        shipActionReset = shipActionsAvailable;
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
        shipActionsAvailable--;
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
            if(_station != null)
                for(int i = 0; i < attackPower; i++){
                    _station.DecreaseStationLevel();
                    attackPower--;
                    wallsDestroyed++;
                }
            if(_ship != null)
                for(int i = 0; i < attackPower; i++){
                    _ship.RemoveUnitsFromShip();
                    currentPlayer.GetComponent<Rhoz>()?.KillSettler();
                    attackPower--;
                    }
            for(int i = 0; i < attackPower; i++){
                _planet.RemoveUnitFromPlanet();
                currentPlayer.GetComponent<Rhoz>()?.KillSettler();
                attackPower--;
            }
        }
        shipActionsAvailable--;
    }

    int wallsDestroyed = 0;
    public void Conquer(){
        for(int i = 0; i < wallsDestroyed; i++)
            selectedPlanet.GetStation().SetControllingPlayer(currentPlayer);
            selectedPlanet.GetStation().IncreaseStationLevel();
        shipActionsAvailable--;
    }

    bool firstCheck = true;
    int recursionAmount = 0;
    public void CheckRecur(){
        if(firstCheck)
            recursionAmount = currentPlayer.GetCapacitorValue - 1;

        if(recursionAmount > 0){
            shipActionsAvailable = shipActionReset;
            recursionAmount --;
        }
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
        currentPlayer.hand.AddResearchHand(DeckManager.instance.DrawCards(cardsToResearch).ToArray());
    }

    public void Populate(){
        if(currentPlayer.GetUnitsAvailable() > 0)
            if(selectedPlanet.GetStation() != null)
                selectedPlanet.AddUnitToPlanet(1, currentPlayer);
    }

    public void Retaliate(){
        //If an enemy ship is on your planet, you may attack with station. Costs one action
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

    void EndTurn(){
        OrbitController.instance.CheckPlayerControl();
    }

    //At the end of the turn, broadcast your deck to the other players to synchronize.
    //Create a Player class that holds each reference that is currently a static instance.
}
