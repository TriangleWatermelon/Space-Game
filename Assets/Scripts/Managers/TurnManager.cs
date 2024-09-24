using System.Collections.Generic;
using UnityEngine;

public class TurnManager : MonoBehaviour
{
    public TurnManager instance;
    void Awake(){
        if(instance == null)
            instance = this;
        else
            Destroy(this);
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
    }

    #region Turn Actions
    public void DrawTech(){
        
    }
    #endregion
}
