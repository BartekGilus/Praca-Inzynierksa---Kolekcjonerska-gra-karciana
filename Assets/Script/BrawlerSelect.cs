using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BrawlerSelect : MonoBehaviour
{
    public void OnClick()
    {
        GameObject[] objects = GameObject.FindGameObjectsWithTag("PlayerCard");
        foreach(var o in objects)
        {
            if (o.GetComponent<CardController>().isOnGameField)
            {
                if (!o.GetComponent<CardController>().brawlerUsed)
                {
                    o.GetComponent<CardController>().target = this.transform.parent.gameObject;

                }
            }
        }
    }
}
