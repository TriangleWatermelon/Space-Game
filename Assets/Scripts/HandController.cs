using System.Collections.Generic;
using UnityEngine;

public class HandController : MonoBehaviour
{
    public static HandController instance;
    void Awake(){
        if(instance == null)
            instance = this;
        else
            Destroy(this);
    }
    [SerializeField]
    GameObject hand;
    [SerializeField]
    GameObject research;
    private List<ICard> researchCardObjects = new List<ICard>();
    [SerializeField]
    ICard cardPrefab;
    private List<ICard> handCardObjects = new List<ICard>();

    void Start(){
        for(int i = 0; i < 15; i++)
            handCardObjects.Add(Instantiate(cardPrefab, Vector3.zero, Quaternion.identity, hand.transform));
        foreach(ICard c in handCardObjects)
            c.gameObject.SetActive(false);
        for(int i = 0; i < 3; i++)
            researchCardObjects.Add(Instantiate(cardPrefab, Vector3.zero, Quaternion.identity, hand.transform));
        foreach(ICard c in researchCardObjects)
            c.gameObject.SetActive(false);
    }

    [Range(3, 7)]
    private int handLimit = 3;
    public void IncreaseHandLimit(){
        if(handLimit < 7)
            handLimit += 2;
    }
    public void DecreaseHandLimit(){
        if(handLimit > 3)
            handLimit -= 2;
    }
    private bool atLimit = false;
    public bool canAddToHand() => !atLimit;

    private List<Card> cards = new List<Card>();
    public void AddToHand(Card _card){
        cards.Add(_card);
        ICard card = handCardObjects[cards.Count -1];
        card.SetActiveCard(_card);
        _card.SetInHand(true);
        if(cards.Count > handLimit)
            atLimit = true;
    }
    public void RemoveFromHand(Card _card){
        cards.Remove(_card);
        _card.SetInHand(false);
        if(cards.Count < handLimit)
            atLimit = false;
    }

    public void PlayCard(Card _card){
        if(_card.hasEffect)
            _card.CardEvent.Invoke();
        RemoveFromHand(_card);
    }

    public void AddResearchHand(Card[] _cards){
        for(int i = 0; i < _cards.Length; i++)
            researchCardObjects[i].SetActiveCard(_cards[i]);
    }

    ICard selectedCard;
    public void ChooseResearchCard(){
        AddToHand(selectedCard.GetActiveCard());
        foreach(var c in researchCardObjects)
            c.gameObject.SetActive(false);
    }
}
