using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConfereFloresta : MonoBehaviour
{
    void OnMouseDown()
    {
        FlorestaController floresta = FindObjectOfType<FlorestaController>();
        if (floresta.numeroCorreto == floresta.numSelect)
        {
            floresta.restartFase();
        }else
            print("bosta");

    }
}
