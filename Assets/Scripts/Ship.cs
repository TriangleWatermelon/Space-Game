using UnityEngine;

public class Ship : MonoBehaviour
{
    private int maxUnits = 3;
    public void SetMaxUnits(int _value) => maxUnits = _value;
    private int unitsOnShip;
    public int GetUnitsOnShip() => unitsOnShip;
    public void AddUnitToShip(){
        unitsOnShip++;
    }
    public void RemoveUnitsFromShip(){
        unitsOnShip--;
    }

    private PlayerBase controllingPlayer;
    public PlayerBase GetControllingPlayer() => controllingPlayer;
    public void SetControllingPlayer(PlayerBase _player) => controllingPlayer = _player;


    IntButton intButton;
    void Start(){
        intButton = GetComponentInChildren<IntButton>();
    }

    Planet dockedPlanet;
    public void MoveUnitToPlanet(){
        int unitsToSend = intButton.SetNum();
        for(int i = 0; i < unitsToSend; i++){
            dockedPlanet.AddUnitToPlanet(1, controllingPlayer);
            RemoveUnitsFromShip();
        }
    }
    Vector3 shipOffset = new Vector3(75, 0, 75);
    public void MoveToPlanet(Planet _planet){
        transform.position = _planet.transform.position + shipOffset;
        _planet.SetSelectedShip(this);
        dockedPlanet = _planet;
    }
}
