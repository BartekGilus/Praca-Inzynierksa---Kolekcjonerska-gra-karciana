using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpellSelect : MonoBehaviour
{
    public void OnClick()
    {
        GameObject[] objects = GameObject.FindGameObjectsWithTag("PlayerCard");
        foreach (var o in objects)
        {
            if (o.GetComponent<CardController>().isOnGameField)
            {
                if (o.GetComponent<CardController>().isSpell)
                {
                    o.GetComponent<CardController>().target = this.transform.parent.gameObject;
                }
            }
        }
    }
}
