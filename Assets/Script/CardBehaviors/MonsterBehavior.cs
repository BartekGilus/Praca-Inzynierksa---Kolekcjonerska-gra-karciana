using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterBehavior : MonoBehaviour
{
    private bool enumeratorDone;

    void OnEnable()
    {

        switch (this.gameObject.GetComponent<Card>().cardName)
        {
            case "Mag królewskiej straży":
                Mag_krolewskiej_strazy();
                break;
            case "Bard":
                Bard();
                break;
            case "Leśny druid":
                Lesny_Druid();
                break;
            case "Niedoświadczona czarodziejka":
                Niedoświadczona_Czarodziejka(2);
                break;
            case "Poszukiwacz wiedzy":
                Poszukiwacz_Wiedzy();
                break;
            case "Poszukiwacz szkarbów":
                Poszukiwacz_Wiedzy();
                break;
            case "Poszukiwaczka artefaktów":
                Poszukiwaczka_Artefaktów();
                break;
            case "Zbiegły mag":
                Zbiegly_Mag();
                break;
            case "Władczyni many":
                Wladczyni_Many();
                break;
            case "Wędrowny bard":
                Wedrowny_Bard();
                break;
            case "Elfia kapłanka lasu":
                Elfia_Kaplanka_Lasu(5);
                break;
            case "Tajemnicza czarodzejka":
                Tajemnicza_Czarodzejka();
                break;
            case "Zdrajca komanda":
                Zdrajca_Komanda();
                break;
            case "Elficki druid":
                Elfia_Kaplanka_Lasu(3);
                break;
            case "Elficki mag bojowy":
                Niedoświadczona_Czarodziejka(3);
                break;
            case "Adeptka magii":
                Poszukiwacz_Wiedzy();
                break;
            case "Zagubiony elf":
                Poszukiwacz_Wiedzy();
                break;
            case "Elfi mędrzec":
                Poszukiwaczka_Artefaktów();
                break;
            case "Łowca ludzi":
                Tajemnicza_Czarodzejka();
                break;
            default:
                DefaultAction();
                break;
        }
    }

    void OnDisable()
    {
        
    }

    private void DefaultAction()
    {
        this.GetComponent<CardController>().monsterEffectUsed = true;
    }

    private void Mag_krolewskiej_strazy()
    {
        if (this.gameObject.tag == "PlayerCard")
        {
            GameObject[] enemyCards = GameObject.FindGameObjectsWithTag("EnemyCard");
            foreach (var o in enemyCards)
            {
                if (o.GetComponent<CardController>().isOnGameField)
                {
                    if (o.GetComponent<CardController>().isMonster)
                    {
                        o.GetComponent<Card>().cardHealth -= 2;
                        if (o.GetComponent<Card>().cardHealth <= 0)
                        {
                            o.GetComponent<Card>().RemoveFromTheField();
                            Destroy(o.gameObject);
                        }
                    }
                    
                    this.GetComponent<CardController>().monsterEffectUsed = true;
                }
            }
        }

        if (this.gameObject.tag == "EnemyCard")
        {
            GameObject[] playerCards= GameObject.FindGameObjectsWithTag("PlayerCard");
            foreach (var o in playerCards)
            {
                if (o.GetComponent<CardController>().isOnGameField)
                {
                    if (o.GetComponent<CardController>().isMonster)
                    {
                        o.GetComponent<Card>().cardHealth -= 2;
                        if (o.GetComponent<Card>().cardHealth <= 0)
                        {
                            o.GetComponent<Card>().RemoveFromTheField();
                            Destroy(o.gameObject);
                        }
                    }
                   
                    this.GetComponent<CardController>().monsterEffectUsed = true;
                }
            }
        }
    }

    private void Bard()
    {
        if (this.gameObject.tag == "PlayerCard")
        {
            GameObject[] playerCards = GameObject.FindGameObjectsWithTag("PlayerCard");
            foreach (var o in playerCards)
            {
                if (o.GetComponent<CardController>().isOnGameField)
                {
                    if(o.GetComponent<Card>().cardClass == Cards.CardClass.CARDCLASS_HUMAN)
                    {
                        o.GetComponent<Card>().cardAttack += 1;
                        o.GetComponent<Card>().cardHealth += 1;
                    }
                    this.GetComponent<CardController>().monsterEffectUsed = true;
                }
            }
        }

        if (this.gameObject.tag == "EnemyCard")
        {
            GameObject[] enemyCards = GameObject.FindGameObjectsWithTag("EnemyCard");
            foreach (var o in enemyCards)
            {
                if (o.GetComponent<CardController>().isOnGameField)
                {
                    if (o.GetComponent<Card>().cardClass == Cards.CardClass.CARDCLASS_HUMAN)
                    {
                        o.GetComponent<Card>().cardAttack += 1;
                        o.GetComponent<Card>().cardHealth += 1;
                    }
                    this.GetComponent<CardController>().monsterEffectUsed = true;
                }
            }
        }

    }

    private void Lesny_Druid()
    {
        if (this.gameObject.tag == "PlayerCard")
        {
            GameObject[] playerCards = GameObject.FindGameObjectsWithTag("PlayerCard");
            foreach (var o in playerCards)
            {
                if (o.GetComponent<CardController>().isOnGameField)
                {
                    o.GetComponent<Card>().cardHealth = o.GetComponent<Card>().cardMaxHealth;
                    this.GetComponent<CardController>().monsterEffectUsed = true;
                }
            }
        }

        if (this.gameObject.tag == "EnemyCard")
        {
            GameObject[] enemyCards = GameObject.FindGameObjectsWithTag("EnemyCard");
            foreach (var o in enemyCards)
            {
                if (o.GetComponent<CardController>().isOnGameField)
                {
                    o.GetComponent<Card>().cardHealth = o.GetComponent<Card>().cardMaxHealth;
                    this.GetComponent<CardController>().monsterEffectUsed = true;
                }
            }
        }
    }

    private void Niedoświadczona_Czarodziejka(int damage)
    {
        if (this.gameObject.tag == "PlayerCard")
        {
            GameObject[] enemyCards = GameObject.FindGameObjectsWithTag("EnemyCard");
            List<GameObject> list = new List<GameObject>();
            foreach (var o in enemyCards)
            {
                if (o.GetComponent<CardController>().isOnGameField)
                {
                    if (o.GetComponent<Card>().cardType == Cards.CardType.CARDTYPE_MONSTER)
                        list.Add(o.gameObject);
                }
            }

            if (list.Count > 0)
            {
                int x = Random.Range(0, list.Count - 1);
                list[x].GetComponent<Card>().cardHealth -= damage;
                if (list[x].GetComponent<Card>().cardHealth <= 0)
                {
                    list[x].GetComponent<Card>().RemoveFromTheField();
                    Destroy(list[x].gameObject);
                }
            }

            this.GetComponent<CardController>().monsterEffectUsed = true;
        }

        if (this.gameObject.tag == "EnemyCard")
        {
            GameObject[] playerCards = GameObject.FindGameObjectsWithTag("PlayerCard");
            List<GameObject> list = new List<GameObject>();
            foreach (var o in playerCards)
            {
                if (o.GetComponent<CardController>().isOnGameField)
                {
                    if (o.GetComponent<Card>().cardType == Cards.CardType.CARDTYPE_MONSTER)
                        list.Add(o.gameObject);
                }
            }

            if(list.Count > 0)
            {
                int x = Random.Range(0, list.Count - 1);
                list[x].GetComponent<Card>().cardHealth -= damage;
                if (list[x].GetComponent<Card>().cardHealth <= 0)
                {
                    list[x].GetComponent<Card>().RemoveFromTheField();
                    Destroy(list[x].gameObject);
                }
            }

            this.GetComponent<CardController>().monsterEffectUsed = true;
        }
    }

    private void Poszukiwacz_Wiedzy()
    {
        if (this.gameObject.tag == "PlayerCard")
        {
            if (this.GetComponent<CardController>().isOnGameField)
                GameObject.Find("PlayerDeck").GetComponent<DrawCard>().Draw();

            this.GetComponent<CardController>().monsterEffectUsed = true;
        }

        if (this.gameObject.tag == "EnemyCard")
        {
            if (this.GetComponent<CardController>().isOnGameField)
                GameObject.Find("EnemyDeck").GetComponent<DrawCard>().Draw();

            this.GetComponent<CardController>().monsterEffectUsed = true;
        }
    }

    private void Poszukiwaczka_Artefaktów()
    {
        if (this.gameObject.tag == "PlayerCard")
        {
            GameObject[] enemyCards = GameObject.FindGameObjectsWithTag("EnemyCard");
            foreach (var o in enemyCards)
            {
                if (o.GetComponent<CardController>().isOnGameField)
                {
                    if (o.GetComponent<CardController>().isArtifact)
                    {
                        o.GetComponent<Card>().RemoveFromTheField();
                        Destroy(o.gameObject);
                    }
                    this.GetComponent<CardController>().monsterEffectUsed = true;
                }
            }
        }

        if (this.gameObject.tag == "EnemyCard")
        {
            GameObject[] playerCards = GameObject.FindGameObjectsWithTag("PlayerCard");
            foreach (var o in playerCards)
            {
                if (o.GetComponent<CardController>().isOnGameField)
                {
                    if (o.GetComponent<CardController>().isArtifact)
                    {
                        o.GetComponent<Card>().RemoveFromTheField();
                        Destroy(o.gameObject);
                    }
                    this.GetComponent<CardController>().monsterEffectUsed = true;
                }
            }
        }
    }

    private void Zbiegly_Mag()
    {
        if (this.gameObject.tag == "PlayerCard")
        {
            GameObject[] playerCards = GameObject.FindGameObjectsWithTag("PlayerCard");
            List<GameObject> list = new List<GameObject>();
            foreach (var o in playerCards)
            {
                if (o.GetComponent<CardController>().isOnGameField)
                {
                    if(o.GetComponent<Card>().cardType == Cards.CardType.CARDTYPE_MONSTER && !o.gameObject.Equals(this.gameObject))
                        list.Add(o.gameObject);
                }
            }
            
            if (list.Count > 1)
            {
                int x = Random.Range(0, list.Count - 1);
                GameObject.Find("TurnSystem").GetComponent<TurnSystem>().playerHealth -= list[x].GetComponent<Card>().cardAttack;
                list[x].GetComponent<Card>().RemoveFromTheField();
                Destroy(list[x].gameObject);
            }

            this.GetComponent<CardController>().monsterEffectUsed = true;
        }

        if (this.gameObject.tag == "EnemyCard")
        {
            GameObject[] enemyCards = GameObject.FindGameObjectsWithTag("EnemyCard");
            List<GameObject> list = new List<GameObject>();
            foreach (var o in enemyCards)
            {
                if (o.GetComponent<CardController>().isOnGameField)
                {
                    if (o.GetComponent<Card>().cardType == Cards.CardType.CARDTYPE_MONSTER && !o.gameObject.Equals(this.gameObject))
                        list.Add(o.gameObject);
                }
            }

            if(list.Count > 1)
            {
                int x = Random.Range(0, list.Count - 1);
                GameObject.Find("TurnSystem").GetComponent<TurnSystem>().enemyHealth -= list[x].GetComponent<Card>().cardAttack;
                list[x].GetComponent<Card>().RemoveFromTheField();
                Destroy(list[x].gameObject);
            }
            this.GetComponent<CardController>().monsterEffectUsed = true;
        }
    }

    private void Wladczyni_Many()
    {
        if (this.gameObject.tag == "PlayerCard")
        {
            GameObject.Find("TurnSystem").GetComponent<TurnSystem>().playerMana += 1;
            this.GetComponent<CardController>().monsterEffectUsed = true;
        }

        if (this.gameObject.tag == "EnemyCard")    
        {
            GameObject.Find("Turnsystem").GetComponent<TurnSystem>().enemyMana += 1;
            this.GetComponent<CardController>().monsterEffectUsed = true;
        }

    }

    private void Wedrowny_Bard()
    {
        if (this.gameObject.tag == "PlayerCard")
        {
            GameObject[] playerCards = GameObject.FindGameObjectsWithTag("PlayerCard");
            foreach (var o in playerCards)
            {
                if (o.GetComponent<CardController>().isOnGameField)
                {
                    if (o.GetComponent<Card>().cardClass == Cards.CardClass.CARDCLASS_ELF)
                    {
                        o.GetComponent<Card>().cardAttack += 1;
                        o.GetComponent<Card>().cardHealth += 1;
                    }
                    this.GetComponent<CardController>().monsterEffectUsed = true;
                }
            }
        }

        if (this.gameObject.tag == "EnemyCard")
        {
            GameObject[] enemyCards = GameObject.FindGameObjectsWithTag("EnemyCard");
            foreach (var o in enemyCards)
            {
                if (o.GetComponent<CardController>().isOnGameField)
                {
                    if (o.GetComponent<Card>().cardClass == Cards.CardClass.CARDCLASS_ELF)
                    {
                        o.GetComponent<Card>().cardAttack += 1;
                        o.GetComponent<Card>().cardHealth += 1;
                    }
                    this.GetComponent<CardController>().monsterEffectUsed = true;
                }
            }
        }
    }

    private void Elfia_Kaplanka_Lasu(int heal)
    {
        if (this.gameObject.tag == "PlayerCard")
        {
            if (this.gameObject.GetComponent<CardController>().isOnGameField)
                GameObject.Find("TurnSystem").GetComponent<TurnSystem>().playerHealth += heal;
            this.GetComponent<CardController>().monsterEffectUsed = true;
        }

        if (this.gameObject.tag == "EnemyCard")
        {
            if (this.gameObject.GetComponent<CardController>().isOnGameField)
                GameObject.Find("TurnSystem").GetComponent<TurnSystem>().enemyHealth += heal;
            this.GetComponent<CardController>().monsterEffectUsed = true;
        }
    }

    private void Tajemnicza_Czarodzejka()
    {
        if (this.gameObject.tag == "PlayerCard")
        {
            GameObject[] enemyCards = GameObject.FindGameObjectsWithTag("EnemyCard");
            List<GameObject> list = new List<GameObject>();
            foreach (var o in enemyCards)
            {
                if (o.GetComponent<CardController>().isOnGameField)
                {
                    if (o.GetComponent<Card>().cardType == Cards.CardType.CARDTYPE_MONSTER)
                        list.Add(o.gameObject);
                }
            }

            if (list.Count > 0)
            {
                int x = Random.Range(0, list.Count - 1);
                list[x].GetComponent<Card>().RemoveFromTheField();
                Destroy(list[x].gameObject);
            }

            this.GetComponent<CardController>().monsterEffectUsed = true;
        }

        if (this.gameObject.tag == "EnemyCard")
        {
            GameObject[] playerCards = GameObject.FindGameObjectsWithTag("PlayerCard");
            List<GameObject> list = new List<GameObject>();
            foreach (var o in playerCards)
            {
                if (o.GetComponent<CardController>().isOnGameField)
                {
                    if (o.GetComponent<Card>().cardType == Cards.CardType.CARDTYPE_MONSTER)
                        list.Add(o.gameObject);
                }
            }

            if (list.Count > 0)
            {
                int x = Random.Range(0, list.Count - 1);
                list[x].GetComponent<Card>().RemoveFromTheField();
                Destroy(list[x].gameObject);
            }
            this.GetComponent<CardController>().monsterEffectUsed = true;
        }
    }

    private void Zdrajca_Komanda()
    {
        if (this.gameObject.tag == "PlayerCard")
        {
            GameObject[] playerCards = GameObject.FindGameObjectsWithTag("PlayerCard");
            List<GameObject> list = new List<GameObject>();
            foreach (var o in playerCards)
            {
                if (o.GetComponent<CardController>().isOnGameField)
                {
                    if (o.GetComponent<Card>().cardType == Cards.CardType.CARDTYPE_MONSTER)
                        list.Add(o.gameObject);
                }
            }

            if (list.Count > 1)
            {
                int x = Random.Range(0, list.Count - 1);
                GameObject.Find("TurnSystem").GetComponent<TurnSystem>().enemyHealth += list[x].GetComponent<Card>().cardAttack;
                list[x].GetComponent<Card>().RemoveFromTheField();
                Destroy(list[x].gameObject);
            }

            this.GetComponent<CardController>().monsterEffectUsed = true;
        }

        if (this.gameObject.tag == "EnemyCard")
        {
            GameObject[] enemyCards = GameObject.FindGameObjectsWithTag("EnemyCard");
            List<GameObject> list = new List<GameObject>();
            foreach (var o in enemyCards)
            {
                if (o.GetComponent<CardController>().isOnGameField)
                {
                    if (o.GetComponent<Card>().cardType == Cards.CardType.CARDTYPE_MONSTER)
                        list.Add(o.gameObject);
                }
            }

            if (list.Count > 1)
            {
                int x = Random.Range(0, list.Count - 1);
                GameObject.Find("TurnSystem").GetComponent<TurnSystem>().playerHealth += list[x].GetComponent<Card>().cardAttack;
                list[x].GetComponent<Card>().RemoveFromTheField();
                Destroy(list[x].gameObject);
            }
            this.GetComponent<CardController>().monsterEffectUsed = true;
        }
    }

    IEnumerator WaitAfterPlayed()
    {
        yield return new WaitForSecondsRealtime(2);
        enumeratorDone = true;
    }
}
