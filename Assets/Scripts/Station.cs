using Unity.VisualScripting;
using UnityEngine;

public class Station : MonoBehaviour
{
    Material stationMat;
    void Start(){
        Renderer renderer = GetComponent<Renderer>();
        stationMat = new Material(renderer.material);
    }

    [Range(0,3)]
    private int stationLevel;
    public int GetStationLevel() => stationLevel;
    public void IncreaseStationLevel(){
        stationLevel ++;
        switch(stationLevel){
            case 0:
            stationMat.color = Color.clear;
            break;
            case 1:
            stationMat.color = level1Color;
            break;
            case 2:
            stationMat.color = level2Color;
            break;
            case 3:
            stationMat.color = level3Color;
            break;
        }
    }
    public void DecreaseStationLevel(){
        stationLevel --;
        switch(stationLevel){
            case 0:
            stationMat.color = Color.clear;
            break;
            case 1:
            stationMat.color = level1Color;
            break;
            case 2:
            stationMat.color = level2Color;
            break;
            case 3:
            stationMat.color = level3Color;
            break;
        }
    }

    [SerializeField]
    Color level1Color = Color.blue;
    [SerializeField]
    Color level2Color = Color.yellow;
    [SerializeField]
    Color level3Color = Color.red;

    private PlayerBase controllingPlayer;
    public PlayerBase GetControllingPlayer() => controllingPlayer;
    public void SetControllingPlayer(PlayerBase _player = null) => controllingPlayer = _player;
}
