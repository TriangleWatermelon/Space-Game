

public class Oogloids : PlayerBase
{
    private int vpToWin = 9;

    void Awake()
    {
        engineMin = 6;
    }

    public override void CheckVictory()
    {
        if(vp >= vpToWin)
            ClaimVictory("Oogloids");
    }

    private void PartMaxed(){
        vp += 3;
        CheckVictory();
    }

    public override void SetCapacitorValue(int _value)
    {
        base.SetCapacitorValue(_value);
        if(GetCapacitorValue == capacitorMax)
            PartMaxed();
    }
    public override void SetEngineValue(int _value)
    {
        base.SetEngineValue(_value);
        if(GetEngineValue == engineMax)
            PartMaxed();
    }
    public override void SetHullValue(int _value)
    {
        base.SetHullValue(_value);
        if(GetHullValue == hullMax)
            PartMaxed();
    }
    public override void SetLaserValue(int _value)
    {
        base.SetLaserValue(_value);
        if(GetLaserValue == laserMax)
            PartMaxed();
    }
}
