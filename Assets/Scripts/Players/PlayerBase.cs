using UnityEngine;

public class PlayerBase : MonoBehaviour
{
    protected int engine;
    protected int engineMin = 4;
    private int engineMax = 12;
    public int GetEngineValue => engine;
    public void SetEngineValue(int _value) => engine = _value;
    
    protected int capacitor;
    protected int capacitorMin = 1;
    private int capacitorMax = 4;
    public int GetCapacitorValue => capacitor;
    public void SetCapacitorValue(int _value) => capacitor = _value;

    protected int laser;
    protected int laserMin = 0;
    private int laserMax = 3;
    public int GetLaserValue => laser;
    public void SetLaserValue(int _value) => laser = _value;

    protected int hull;
    protected int hullMin = 2;
    private int hullMax = 5;
    public int GetHullValue => hull;
    public void SetHullValue(int _value) => hull = _value;

    private bool isTurn;
    public void SetIsTurn(bool _value) => isTurn = _value;

    void Start(){
        engine = engineMin;
        capacitor = capacitorMin;
        laser = laserMin;
        hull = hullMin;
        isTurn = false;
    }
}
