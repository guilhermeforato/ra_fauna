using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControllerNavio : MonoBehaviour
{
    public MenuContrlGame menu;

    private void OnMouseDown()
    {
        switch (name)
        {
            case "centro":
                string nomeStringCentro = GetComponent<SpriteRenderer>().sprite.name;
                menu.setaNumeroCentro(nomeStringCentro.Substring(0, 1));
                break;
            case "setaEsquerda":
                menu.controlaSetas(gameObject);
                break;
            case "setaDireita":
                menu.controlaSetas(gameObject);
                break;
            case "confirmar":
                StartCoroutine(menu.confirmarSoma(true));
                break;
            case "limpar":
                StartCoroutine(menu.confirmarSoma(limpa: true));
                break;
        }
        // if (name != "centro")
        // {
        //     menu.controlaSetas(gameObject);
        // }
        // else
        // {
        //     string nomeStringCentro = GetComponent<SpriteRenderer>().sprite.name;
        //     menu.setaNumeroCentro(nomeStringCentro.Substring(0, 1));
        // }
    }
}
