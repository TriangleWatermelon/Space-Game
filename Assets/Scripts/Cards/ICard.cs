using UnityEngine;
using UnityEngine.UI;

public class ICard : MonoBehaviour
{
    public Card activeCard;

    Sprite cardArt;
    string cardName;
    string cardValue;
    string cardDescription;

    public void SetCard(){
        cardArt = activeCard.cardImage;
        cardName = activeCard.cardName;
        cardValue = activeCard.value.ToString();
        cardDescription = activeCard.description;
    }

    void Update(){
        if(Input.GetKeyDown(KeyCode.A))
            SetCard();
    }
}
