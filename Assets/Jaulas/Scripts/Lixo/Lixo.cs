using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class Lixo : MonoBehaviour
{
    public LixoController lixoController = null;
    public string destino;
    Vector3 startPosition;
    void Start()
    {
        lixoController = FindObjectOfType<LixoController>();
        startPosition = transform.localPosition;
    }

    void OnMouseDrag()
    {
        float distance_to_screen = Camera.main.WorldToScreenPoint(gameObject.transform.position).z;
        Vector3 pos_move = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, distance_to_screen));
        transform.position = new Vector3(pos_move.x, .2f, pos_move.z);
    }

    void OnMouseUp()
    {
        transform.localPosition = startPosition;
        if (lixoController.Any != null)
        {
            lixoController.Any.transform.GetChild(0).gameObject.SetActive(false);
            if (destino == lixoController.Any.name)
            {
                //particle acerto
                lixoController.lista.Remove(gameObject);
                Destroy(this.gameObject);
                lixoController.contLixos--;
            }
            else
            {
                //particle error
                lixoController.contLixos++;
                if (lixoController.contLixos < lixoController.posicoes.Length)
                {
                    lixoController.lista.Add(lixoController.InstanciaLixo(lixoController.contLixos));

                    for (int i = 0; i < lixoController.lista.Count; i++)
                    {
                        lixoController.lista[i].transform.SetParent(lixoController.posicoes[i]);
                        lixoController.lista[i].transform.localScale = new Vector3(1.5f, 1.5f, 1.5f);
                    }

                    for (int i = 0; i < lixoController.lista.Count; i++)
                    {
                        lixoController.lista[i].transform.localPosition = Vector3.zero;
                        Instantiate(lixoController.prefabErrorAll).transform.SetParent(lixoController.lista[i].transform);
                        lixoController.lista[i].transform.GetChild(0).localPosition = Vector3.zero;
                        lixoController.lista[i].transform.GetChild(0).localScale = Vector3.one;
                    }

                }
                else
                {
                    //game over
                    print("game over");
                }
            }

            lixoController.Any = null;
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.transform.parent.name == "Sensores")
        {
            other.transform.GetChild(0).gameObject.SetActive(true);
            lixoController.Any = other.gameObject;
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.transform.parent.name == "Sensores")
        {
            other.transform.GetChild(0).gameObject.SetActive(false);
            lixoController.Any = null;
        }
    }
}
