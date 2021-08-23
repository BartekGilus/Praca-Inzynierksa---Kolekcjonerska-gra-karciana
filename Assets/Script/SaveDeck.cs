using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

public class SaveDeck : MonoBehaviour
{ 
    public void OnClick()
    {
        if(GameObject.Find("DeckListContent").GetComponent<DeckBuilderDeckList>().cardsInDeck.Count == 40)
        {
            FileStream fs = new FileStream("deck.dat", FileMode.Create);
            BinaryFormatter bf = new BinaryFormatter();

            bf.Serialize(fs, GameObject.Find("DeckListContent").GetComponent<DeckBuilderDeckList>().cardsInDeck);
            fs.Close();
        }
        else
        {
            Debug.Log("Za mało kart");
        }
       
    }
}
