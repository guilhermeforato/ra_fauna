using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CartaMemoria : MonoBehaviour
{
    public MenuContrlGame menu;
    // [HideInInspector] 
    public Sprite mySprite, interrogaSprite;
    public bool acertou = false;

    private void Start()
    {
        interrogaSprite = GetComponent<SpriteRenderer>().sprite;
    }
    public void setSpriteAnimal() => GetComponent<SpriteRenderer>().sprite = mySprite;
    public void setSpriteInterroga() => GetComponent<SpriteRenderer>().sprite = interrogaSprite;

    private void OnMouseDown()
    {
        menu.contClickCarta++;
        GetComponent<BoxCollider2D>().enabled = false;
        GetComponent<Animator>().SetBool("gira", true);
        if (menu.contClickCarta == 1) menu.clickAntes = gameObject;
        if (menu.contClickCarta == 2) menu.confereFase3Macaco(gameObject);
    }

    public void resetCont() {
        menu.contClickCarta = 0;
        menu.clickAntes = null;
    }
}
