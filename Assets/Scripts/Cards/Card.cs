using UnityEngine;

[CreateAssetMenu]
public class Card : ScriptableObject
{
    public int value;

    public int numInDeck;

    public string cardName;

    public Sprite cardImage;

    public string description;

    public bool hasEffect = false;

    public enum PlayerRestriction{
        None,
        Caynians,
        Druzhnny,
        Oogloids,
        Rhoz,
    }
    public PlayerRestriction playerRestriction = PlayerRestriction.None;
}
