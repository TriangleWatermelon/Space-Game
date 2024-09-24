using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu]
public class Card : ScriptableObject
{
    public int value;

    public int numInDeck;

    public string cardName;

    public Sprite cardImage;

    public string description;

    public bool hasEffect = false;
    public UnityEvent CardEvent;

    public enum PlayerRestriction{
        None,
        Caynians,
        Druzhnny,
        Oogloids,
        Rhoz,
    }
    public PlayerRestriction playerRestriction = PlayerRestriction.None;

    private bool inHand = false;
    public bool GetInHand() => inHand;
    public void SetInHand(bool _isInHand) => inHand = _isInHand; 
}
