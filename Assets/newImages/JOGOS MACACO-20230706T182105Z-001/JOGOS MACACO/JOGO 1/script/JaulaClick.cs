using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JaulaClick : MonoBehaviour
{
    public MenuContrlGame menu;
     public bool stateJaula = true;

    private void OnMouseDown()
    {
        if (GetComponent<Animator>())
        {
            menu.chaveClick.GetComponent<BoxCollider2D>().enabled = true;
            menu.chaveGirando.transform.parent.GetComponent<Animator>().SetBool("gira", true);
            menu.particleChave.Play();
            GetComponent<Animator>().SetBool("open", stateJaula);

            if (stateJaula) menu.quantidadeJaula++;
            else menu.quantidadeJaula--;

            stateJaula = !stateJaula;
        }
        else
        {
            GetComponent<BoxCollider2D>().enabled = false;
            StartCoroutine(menu.confereFase1Macaco());
        }

    }
}
