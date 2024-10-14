

public class Druzhnny : PlayerBase
{
    private int vpToWin = 15;

    void Awake()
    {
        capacitorMin = 2;
    }

    public override void CheckVictory()
    {
        if(vp >= vpToWin)
            ClaimVictory("Druzhnny");
    }

    public void AddOnePoint(){
        vp += 1;
        CheckVictory();
    }
    public void AddTwoPoints(){
        vp += 2;
        CheckVictory();
    }
}
