using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CardController : MonoBehaviour
{
    private Card cardComponent;
    private GameObject playerField;
    public GameObject target;
    public GameObject card;

    public bool isSpell;
    public bool isArtifact;
    public bool isMonster;
    
    public bool isAttacking;
    public bool isBlocked;
    public bool isOnGameField;
    public bool canBePlayed;
    public bool summoningSickness;
    public bool isFighting;

    public bool isDefender;
    public bool isCleric;
    public bool isFlying;
    public bool isArcher;
    public bool isNecromancer;
    public bool hasPercingAttack;
    public bool isBrawler;

    public bool necromacerUsed;
    public bool brawlerUsed;
    public bool spellUsed;
    public bool monsterEffectUsed;

    
    void Start()
    {
        cardComponent = this.GetComponent<Card>();
        playerField = GameObject.Find("PlayerField");
            
        if (this.gameObject.tag == "EnemyCard")
        {
            this.GetComponent<Card>().isCovered = true;
        }
    }

    void Update()
    {
        if (this.GetComponent<Card>().cardType == Cards.CardType.CARDTYPE_SPELL)
            isSpell = true;

        if(this.GetComponent<Card>().cardType == Cards.CardType.CARDTYPE_ARTIFACT)
            isArtifact = true;

        if (this.GetComponent<Card>().cardType == Cards.CardType.CARDTYPE_MONSTER)
            isMonster = true;

        if (isAttacking)
        {
            this.GetComponent<BoxCollider2D>().enabled = true;
            if (target != null)
            {
                   
            }
        }
        else
            this.GetComponent<BoxCollider2D>().enabled = false;

        if(this.transform.parent.name == "PlayerField" || this.transform.parent.name == "EnemyField" || this.transform.parent.name == "PlayerArtifact" || this.transform.parent.name == "EnemyArtifactField")
        {
            isOnGameField = true;
        }

        if (isSpell && this.transform.parent.name == "PlayedSpellDisplay")
            isOnGameField = true;

        if (isOnGameField)
        {
            this.transform.Find("CardReverse").gameObject.SetActive(false);
            if (isSpell)
            {
                this.GetComponent<SpellBehavior>().enabled = true;
            }

            if (isNecromancer)
            {
                if (!necromacerUsed)
                {
                    Necromacer_Keyword();
                    necromacerUsed = true;
                }
            }

            if (isSpell)
            {
                if (!spellUsed && isOnGameField)
                {
                    this.GetComponent<SpellBehavior>().enabled = true;
                    this.GetComponent<SpellBehavior>().enabled = false;
                }
                    
                if(spellUsed)
                    this.GetComponent<SpellBehavior>().enabled = false;
            }

            if (isMonster)
            {
                if(!monsterEffectUsed && isOnGameField)
                {
                    this.GetComponent<MonsterBehavior>().enabled = true;
                    this.GetComponent<MonsterBehavior>().enabled = false;
                }

                if (monsterEffectUsed)
                {
                    this.GetComponent<MonsterBehavior>().enabled = false;
                }

                if (isBrawler)
                {
                    if(!brawlerUsed && isOnGameField)
                    {
                        this.GetComponent<BrawlerKeywordBehavior>().enabled = true;
                        this.GetComponent<BrawlerKeywordBehavior>().enabled = false;
                    }

                    if(brawlerUsed)
                        this.GetComponent<BrawlerKeywordBehavior>().enabled = false;
                }
            }
        }

        if (this.GetComponent<Card>().key_word1 == Cards.CardKeywords.KEYWORD_DEFENDER || this.GetComponent<Card>().key_word2 == Cards.CardKeywords.KEYWORD_DEFENDER || this.GetComponent<Card>().key_word3 == Cards.CardKeywords.KEYWORD_DEFENDER)
            isDefender = true;

        if(this.GetComponent<Card>().key_word1 == Cards.CardKeywords.KEYWORD_CLERIC || this.GetComponent<Card>().key_word2 == Cards.CardKeywords.KEYWORD_CLERIC || this.GetComponent<Card>().key_word3 == Cards.CardKeywords.KEYWORD_CLERIC)
            isCleric = true;

        if (this.GetComponent<Card>().key_word1 == Cards.CardKeywords.KEYWORD_FLYING || this.GetComponent<Card>().key_word2 == Cards.CardKeywords.KEYWORD_FLYING || this.GetComponent<Card>().key_word3 == Cards.CardKeywords.KEYWORD_FLYING)
            isFlying = true;

        if (this.GetComponent<Card>().key_word1 == Cards.CardKeywords.KEYWORD_ARCHER || this.GetComponent<Card>().key_word2 == Cards.CardKeywords.KEYWORD_ARCHER || this.GetComponent<Card>().key_word3 == Cards.CardKeywords.KEYWORD_ARCHER)
            isArcher = true;

        if (this.GetComponent<Card>().key_word1 == Cards.CardKeywords.KEYWORD_NECROMANCER || this.GetComponent<Card>().key_word2 == Cards.CardKeywords.KEYWORD_NECROMANCER || this.GetComponent<Card>().key_word3 == Cards.CardKeywords.KEYWORD_NECROMANCER)
            isNecromancer = true;

        if (this.GetComponent<Card>().key_word1 == Cards.CardKeywords.KEYWORD_PERCING_ATACK || this.GetComponent<Card>().key_word2 == Cards.CardKeywords.KEYWORD_PERCING_ATACK || this.GetComponent<Card>().key_word3 == Cards.CardKeywords.KEYWORD_PERCING_ATACK)
            hasPercingAttack = true;

        if (this.GetComponent<Card>().key_word1 == Cards.CardKeywords.KEYWORD_BRAWLER || this.GetComponent<Card>().key_word2 == Cards.CardKeywords.KEYWORD_BRAWLER || this.GetComponent<Card>().key_word3 == Cards.CardKeywords.KEYWORD_BRAWLER)
            isBrawler = true;

    }

    public void Cleric_Keyword()
    {
        if (isCleric)
        {
            if (this.gameObject.tag == "PlayerCard")
                GameObject.Find("TurnSystem").GetComponent<TurnSystem>().playerHealth += this.GetComponent<Card>().cardAttack;
            if (this.gameObject.tag == "EnemyCard")
                GameObject.Find("TurnSystem").GetComponent<TurnSystem>().enemyHealth += this.GetComponent<Card>().cardAttack;
        }
    }

    public void Necromacer_Keyword()
    {
        if (this.gameObject.tag == "PlayerCard")
        {
            GameObject graveyard = GameObject.Find("PlayerGraveyard");
            List<CardTemplate> cards = new List<CardTemplate>();
            List<int> indexes = new List<int>();

            foreach (var o in graveyard.GetComponent<Graveyard>().playerGraveyard)
            {
                if (o.cardType == Cards.CardType.CARDTYPE_MONSTER)
                {
                    cards.Add(o);
                    indexes.Add(graveyard.GetComponent<Graveyard>().playerGraveyard.IndexOf(o));
                }
            }

            int index = cards.Count;
            int place = 0;

            if (index > 0)
            {
                int x = Random.Range(0, index - 1);
                place = indexes[x];

                card.GetComponent<Card>().thisCardID = cards[x].cardID - 1;
                GameObject card2 = Instantiate(card, new Vector3(0, 0, 0), Quaternion.identity);
                card2.transform.SetParent(GameObject.Find("PlayerHandZone").transform);
                card2.GetComponent<CardController>().isOnGameField = false;
                card2.transform.localScale = new Vector3(0.9f, 0.9f, 1);

                graveyard.GetComponent<Graveyard>().playerGraveyard.RemoveAt(place);
            }

        }
        if (this.gameObject.tag == "EnemyCard")
        {
            GameObject graveyard = GameObject.Find("EnemyGraveyard");
            List<CardTemplate> cards = new List<CardTemplate>();
            List<int> indexes = new List<int>();

            foreach (var o in graveyard.GetComponent<Graveyard>().enemyGraveyard)
            {
                if (o.cardType == Cards.CardType.CARDTYPE_MONSTER)
                {
                    cards.Add(o);
                    indexes.Add(graveyard.GetComponent<Graveyard>().enemyGraveyard.IndexOf(o));
                }
            }

            int index = cards.Count;
            int place = 0;

            if (index > 0)
            {
                int x = Random.Range(0, index - 1);
                place = indexes[x];

                card.GetComponent<Card>().thisCardID = cards[x].cardID - 1;
                GameObject card2 = Instantiate(card, new Vector3(0, 0, 0), Quaternion.identity);
                card2.transform.SetParent(GameObject.Find("PlayerHandZone").transform);
                card2.GetComponent<CardController>().isOnGameField = false;
                card2.transform.localScale = new Vector3(0.9f, 0.9f, 1);

                graveyard.GetComponent<Graveyard>().enemyGraveyard.RemoveAt(place);
            }
        }
    }

    private void OnDestroy()
    {
        this.GetComponent<ZoomOnCard>().EndZooming();
    }
}
