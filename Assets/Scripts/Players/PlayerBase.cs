using UnityEngine;

public class PlayerBase : MonoBehaviour
{
    protected int engine;
    protected int engineMin = 4;
    protected int engineMax = 12;
    public int GetEngineValue => engine;
    public virtual void SetEngineValue(int _value) => engine = _value;
    
    protected int capacitor;
    protected int capacitorMin = 1;
    protected int capacitorMax = 4;
    public int GetCapacitorValue => capacitor;
    public virtual void SetCapacitorValue(int _value) => capacitor = _value;

    protected int laser;
    protected int laserMin = 0;
    protected int laserMax = 3;
    public int GetLaserValue => laser;
    public virtual void SetLaserValue(int _value) => laser = _value;

    protected int hull;
    protected int hullMin = 2;
    protected int hullMax = 5;
    public int GetHullValue => hull;
    public virtual void SetHullValue(int _value) => hull = _value;

    private int unitsAvailable = 20;
    public int GetUnitsAvailable() => unitsAvailable;
    public void UseUnit() => unitsAvailable--;
    public void ReturnUnits() => unitsAvailable++;

    private bool isTurn;
    public void SetIsTurn(bool _value) => isTurn = _value;

    public virtual void CheckVictory(){}

    protected void ClaimVictory(string _Victor){

    }

    protected int vp = 0;

    void Start(){
        engine = engineMin;
        capacitor = capacitorMin;
        laser = laserMin;
        hull = hullMin;
        isTurn = false;
    }

    public HandController hand;
}
