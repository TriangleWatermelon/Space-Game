using UnityEngine;

public class IntButton : MonoBehaviour
{
    private int number;
    // public int GetNumber() => number;
    public void AddToNum() => number++;
    public void SubtractFromNum() => number--;
    public int SetNum(){
        this.gameObject.SetActive(false);
        return number;
    }
}
