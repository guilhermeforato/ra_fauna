using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JaulaDown : MonoBehaviour
{
    public JaulaManager jaulaManager;

    void Start()
    {
        transform.GetChild(0).GetChild(0).GetChild(1).gameObject.SetActive(false);
    }
    void OnMouseDown()
    {
        if (transform.GetChild(0).GetChild(0).GetChild(1).gameObject.activeSelf)
        {
            jaulaManager.soma--;
            transform.GetChild(0).GetChild(0).GetChild(1).gameObject.SetActive(false);
        }
        else
        {
            jaulaManager.soma++;
            transform.GetChild(0).GetChild(0).GetChild(1).gameObject.SetActive(true);
        }

    }
}
