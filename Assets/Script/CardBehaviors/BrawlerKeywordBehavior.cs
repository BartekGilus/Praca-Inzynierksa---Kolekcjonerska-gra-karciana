using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BrawlerKeywordBehavior : MonoBehaviour
{
    public GameObject turnSystem;

    void OnEnable()
    {
        if (this.gameObject.tag == "PlayerCard")
        {
            GameObject[] enemyCards = GameObject.FindGameObjectsWithTag("EnemyCard");
            if (!this.GetComponent<CardController>().brawlerUsed)
            {
                foreach(var o in enemyCards)
                {
                    if (o.GetComponent<CardController>().isOnGameField)
                    {
                        if(o.GetComponent<CardController>().isMonster)
                            o.transform.GetChild(3).gameObject.SetActive(true);
                    } 
                }

                if(this.GetComponent<CardController>().target != null)
                {
                    this.GetComponent<Card>().cardHealth -= this.GetComponent<CardController>().target.GetComponent<Card>().cardAttack;
                    this.GetComponent<CardController>().target.GetComponent<Card>().cardHealth -= this.GetComponent<Card>().cardAttack;

                    if(this.GetComponent<CardController>().target.GetComponent<Card>().cardHealth <= 0)
                    {
                        this.GetComponent<CardController>().target.GetComponent<Card>().RemoveFromTheField();
                        Destroy(this.GetComponent<CardController>().target);
                    }

                    if (this.GetComponent<Card>().cardHealth <= 0)
                    {
                        this.GetComponent<Card>().RemoveFromTheField();
                        Destroy(this.gameObject);
                    }
                    this.GetComponent<CardController>().brawlerUsed = true;
                }
            }
        }
    }

    void OnDisable()
    {
        if (this.GetComponent<CardController>().brawlerUsed)
        {
            GameObject[] enemyCards = GameObject.FindGameObjectsWithTag("EnemyCard");
            foreach (var o in enemyCards)
            {
                    o.transform.GetChild(3).gameObject.SetActive(false);
                    Debug.Log(o);
            }

            if (this.GetComponent<CardController>().target != null)
                this.GetComponent<BrawlerKeywordBehavior>().enabled = false;

            this.GetComponent<CardController>().target = null;
            this.GetComponent<ZoomOnCard>().EndZooming();
        }
    }
}
