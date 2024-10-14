

public class Rhoz : PlayerBase
{
    private int vpToWin = 18;
    
    void Awake()
    {
        laserMin = 1;
    }

    public override void CheckVictory()
    {
        if(vp >= vpToWin)
            ClaimVictory("Rhoz");
    }

    public void KillSettler(){
        vp++;
        CheckVictory();
    }
}
