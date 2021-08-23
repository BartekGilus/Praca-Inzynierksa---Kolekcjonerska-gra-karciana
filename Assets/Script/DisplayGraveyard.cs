using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisplayGraveyard : MonoBehaviour
{
    public GameObject card;
    public List<CardTemplate> graveyard = new List<CardTemplate>();

    public void OnEnable()
    {
        foreach(var o in graveyard)
        {
            card.GetComponent<Card>().thisCardID = o.cardID - 1;
            GameObject card2 = Instantiate(card, new Vector3(0, 0, 0), Quaternion.identity);
            //card2.GetComponent<Card>().isCovered = false;
            card2.transform.SetParent(this.transform);
            card2.transform.localScale = new Vector3(1, 1, 1);
        }

    }

    public void OnDisable()
    {
        for(var i = 0; i < this.transform.childCount; i++)
        {
            Destroy(this.transform.GetChild(i).gameObject);
        }
    }

     void Update()
    {
        for(var i = 0; i < this.transform.childCount; i++)
        {
            this.transform.GetChild(i).gameObject.GetComponent<Card>().isCovered = false;
        }
    }
}
