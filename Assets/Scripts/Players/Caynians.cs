

public class Caynians : PlayerBase
{
    private int vpToWin = 16;

    void Awake()
    {
        hullMin = 3;
    }

    public void StationFull(){
        vp += 2;
        CheckVictory();
    }

    public override void CheckVictory()
    {
        if(vp >= vpToWin)
            ClaimVictory("Caynians");
    }
}
