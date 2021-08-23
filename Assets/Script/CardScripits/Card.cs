using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Cards;

public class Card : MonoBehaviour
{
    public List<CardTemplate> card = new List<CardTemplate>();
    public int thisCardID;

    //Current Card SetUp
    public int cardID;
    public string cardName;
    public string cardDescription;
    public int cardAttack;
    public int cardHealth;
    public int cardCost;
    public CardType cardType;
    public CardClass cardClass;
    public CardKeywords key_word1;
    public CardKeywords key_word2;
    public CardKeywords key_word3;

    public int cardMaxHealth;
    public int cardMaxAttack;

    public Text nameText;
    public Text costText;
    public Text cardTypeText;
    public Text statsText;
    public Text descriptionText;
    public Text cardKeywords;

    public Sprite cardSprite;
    public Image cardImage;
    public Image cardBack;
    public GameObject statsBorder;
    public GameObject cardBackground;

    public GameObject targetFrame;
    public bool isCovered;


    void Start()
    {
        card[0] = CardsDatabase.cardList[thisCardID];
        targetFrame.SetActive(false);
        this.cardID = card[0].cardID;
        this.cardName = card[0].cardName;
        this.cardDescription = card[0].cardDescription;
        this.cardAttack = card[0].cardAttack;
        this.cardHealth = card[0].cardHealth;
        this.cardCost = card[0].cardCost;
        this.cardType = card[0].cardType;
        this.cardClass = card[0].cardClass;
        this.key_word1 = card[0].key_word1;
        this.key_word2 = card[0].key_word2;
        this.key_word3 = card[0].key_word3;
        this.cardSprite = card[0].cardSprite;

        if (cardType == CardType.CARDTYPE_ARTIFACT || cardType == CardType.CARDTYPE_SPELL)
            statsBorder.SetActive(false);

        cardMaxAttack = cardAttack;
        cardMaxHealth = cardHealth;
    }

    void Update()
    {
        this.nameText.text = "" + cardName;
        this.costText.text = "" + cardCost;
        this.descriptionText.text = "" + cardDescription;

        cardImage.sprite = cardSprite;

        switch (card[0].cardType)
        {
            case CardType.CARDTYPE_MONSTER:
                this.statsText.text = "" + cardAttack + "/" + cardHealth;
                this.cardTypeText.text = "Karta Potwora";
                break;
            case CardType.CARDTYPE_SPELL:
                this.statsText.text = "";
                this.cardTypeText.text = "Karta Czaru";
                break;
            case CardType.CARDTYPE_ARTIFACT:
                this.statsText.text = "";
                this.cardTypeText.text = "Artefakt";
                break;
        }

        switch (card[0].cardClass)
        {
            case CardClass.CARDCLASS_HUMAN:
                cardBackground.GetComponent<Image>().color = new Color32(255, 255, 180, 255);
                break;
            case CardClass.CARDCLASS_ELF:
                cardBackground.GetComponent<Image>().color = new Color32(75, 150, 100, 255);
                break;
            case CardClass.CARDCLASS_NEUTRAL:
                cardBackground.GetComponent<Image>().color = new Color32(150, 150, 150, 255);
                break;
        }

        if(card[0].cardType == CardType.CARDTYPE_SPELL)
            cardBackground.GetComponent<Image>().color = new Color32(50, 100, 150, 255);

        this.cardKeywords.text = "";
        if (this.key_word1 == CardKeywords.KEYWORD_CLERIC || this.key_word2 == CardKeywords.KEYWORD_CLERIC || this.key_word3 == CardKeywords.KEYWORD_CLERIC)
            this.cardKeywords.text += "Kleryk ";
        if (this.key_word1 == CardKeywords.KEYWORD_DEFENDER || this.key_word2 == CardKeywords.KEYWORD_DEFENDER || this.key_word3 == CardKeywords.KEYWORD_DEFENDER)
            this.cardKeywords.text += "Obrońca ";
        if (this.key_word1 == CardKeywords.KEYWORD_ARCHER || this.key_word2 == CardKeywords.KEYWORD_ARCHER || this.key_word3 == CardKeywords.KEYWORD_ARCHER)
            this.cardKeywords.text += "Strzelec ";
        if (this.key_word1 == CardKeywords.KEYWORD_FLYING || this.key_word2 == CardKeywords.KEYWORD_FLYING || this.key_word3 == CardKeywords.KEYWORD_FLYING)
            this.cardKeywords.text += "Jednostka latająca ";
        if (this.key_word1 == CardKeywords.KEYWORD_NECROMANCER || this.key_word2 == CardKeywords.KEYWORD_NECROMANCER || this.key_word3 == CardKeywords.KEYWORD_NECROMANCER)
            this.cardKeywords.text += "Nekromanta ";
        if (this.key_word1 == CardKeywords.KEYWORD_PERCING_ATACK || this.key_word2 == CardKeywords.KEYWORD_PERCING_ATACK || this.key_word3 == CardKeywords.KEYWORD_PERCING_ATACK)
            this.cardKeywords.text += "Przeszywający Atak ";
        if (this.key_word1 == CardKeywords.KEYWORD_BRAWLER || this.key_word2 == CardKeywords.KEYWORD_BRAWLER || this.key_word3 == CardKeywords.KEYWORD_BRAWLER)
            this.cardKeywords.text += "Buntownik ";

        if (isCovered)
        {
            cardBack.gameObject.SetActive(true);
        }
        else
        {
            cardBack.gameObject.SetActive(false);
        }

    }

    public void RemoveFromTheField()
    {
        if(this.gameObject.tag == "PlayerCard")
            GameObject.Find("PlayerGraveyard").GetComponent<Graveyard>().playerGraveyard.Add(CardsDatabase.cardList[this.GetComponent<Card>().thisCardID]);
        if (this.gameObject.tag == "EnemyCard")
            GameObject.Find("EnemyGraveyard").GetComponent<Graveyard>().enemyGraveyard.Add(CardsDatabase.cardList[this.GetComponent<Card>().thisCardID]);
    }

}