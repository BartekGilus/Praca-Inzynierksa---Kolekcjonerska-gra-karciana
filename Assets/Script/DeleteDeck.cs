using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class DeleteDeck : MonoBehaviour
{
    public void OnClick()
    {
        if (System.IO.File.Exists("deck.dat"))
            File.Delete("deck.dat");
    }
}
