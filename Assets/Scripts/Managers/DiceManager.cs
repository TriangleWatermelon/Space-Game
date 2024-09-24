using UnityEngine;

public class DiceManager : MonoBehaviour
{
    public static DiceManager instance;
    void Awake(){
        if(instance == null)
            instance = this;
        else
            Destroy(this);
    }

    public int RollD4() => Random.Range(0, 4) + 1;

    public int RollD6() => Random.Range(0, 6) + 1;

    public int RollD8() => Random.Range(0, 8) + 1;

    public int RollD12() => Random.Range(0,12) + 1;
}
