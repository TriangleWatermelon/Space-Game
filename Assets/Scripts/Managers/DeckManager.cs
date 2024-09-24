using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class DeckManager : MonoBehaviour
{
    public DeckManager instance;
    void Awake(){
        if(instance == null)
            instance = this;
        else
            Destroy(this);
    }

    private List<Card> cardsList = new List<Card>();
    private int deckSize;
    public List<Card> GetCards() => cardsList;

    [SerializeField] Card[] cardArray;

    void Start(){
        FillDeck();
    }

    void FillDeck(){
        foreach(Card c in cardArray){
            for(int i = 0; i < c.numInDeck; i++)
                cardsList.Add(c);
        }
        deckSize = cardsList.Count;
    }

    public void ShuffleDeck(){
        for(int i = 0; i < deckSize; i++){
            Card card = cardsList[i];
            int randomIndex = Random.Range(i, deckSize);
            cardsList[i] = cardsList[randomIndex];
            cardsList[randomIndex] = card;
        }
    }
}
