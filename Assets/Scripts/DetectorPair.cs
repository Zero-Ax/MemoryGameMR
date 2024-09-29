using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetectorPair : MonoBehaviour
{
   
    public List<GameObject> cards = new List<GameObject>();
    private string ResultFrlip;
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Card"))
        {
            cards.Add(other.gameObject);
            Debug.Log($"Objeto añadido: {other.gameObject.name}");
            other.GetComponent<Animator>().SetBool("Turn",true);
            other.GetComponent<Collider>().enabled = false;
            if (cards.Count > 1)
            {
                if (cards[cards.Count - 1].name == cards[cards.Count - 2].name)
                {
                    Debug.Log("Los últimos dos objetos tienen el mismo nombre: " + cards[cards.Count - 1].name);
                    ResultFrlip = "Correct";
                    Invoke("TurnDownAllCards", 1);
                }
                else
                {
                    Debug.Log("Los últimos dos objetos NO tienen el mismo nombre.");
                    ResultFrlip = "Incorrect";
                    Invoke("TurnDownAllCards", 1);
                }
               
            }
           
        }
    }

    public void TurnDownAllCards()
    {
        for (int i = 0; i < cards.Count; i++)
        {
            cards[i].GetComponent<Animator>().SetBool("Return", true);
            cards[i].GetComponent<Animator>().SetBool("Turn", false);
        }
        Invoke("ClearAnimator", 0.25f);
    }

    public void ClearAnimator()
    {
        if (ResultFrlip != "Correct")
        {
            for (int i = 0; i < cards.Count; i++)
            {
                cards[i].GetComponent<Animator>().SetBool("Return", false);
                cards[i].GetComponent<Collider>().enabled = true;
            }
        }
        else if (ResultFrlip != "Incorrect")
        {
            DestroyPairAndClean();
        }
        ClearCards();
    }

    public void ClearCards()
    {
        cards.Clear();
        ResultFrlip = "";
        Debug.Log("Todos los objetos han sido eliminados de la lista.");
    }

    public void DestroyPairAndClean()
    {
        for (int i = 0; i < cards.Count; i++)
        {
            Destroy(cards[i]);
        }
        ClearCards();
    }
}
