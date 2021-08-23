using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HideDeleteDeckButton : MonoBehaviour
{
    public GameObject button;

    void Update()
    {
        if (System.IO.File.Exists("deck.dat"))
            button.SetActive(true);
        else
            button.SetActive(false);

    }
}
