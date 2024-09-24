using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ICard : MonoBehaviour
{
    private Card activeCard;
    public Card GetActiveCard() => activeCard;
    public void SetActiveCard(Card _card){
        activeCard = _card;
        SetCard();
    }

    [SerializeField]
    Image cardArt;
    string cardName;
    [SerializeField]
    TextMeshProUGUI cardNameText;
    string cardValue;
    [SerializeField]
    TextMeshProUGUI cardValueText;
    string cardDescription;
    [SerializeField]
    TextMeshProUGUI cardDescriptionText;

    void SetCard(){
        cardArt.sprite = activeCard.cardImage;
        cardName = activeCard.cardName;
        cardValue = activeCard.value.ToString();
        cardDescription = activeCard.description;
        gameObject.SetActive(true);
    }

    void Update(){
        if(Input.GetKeyDown(KeyCode.A))
            SetCard();
    }
}
