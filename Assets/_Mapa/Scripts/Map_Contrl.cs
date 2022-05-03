using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Map_Contrl : MonoBehaviour
{
    public GameObject  m_regions;
    public TextMesh tx_Estado, tx_Capital, tx_Região;
    public bool Estado = true;
    public Color[] colors;


    private void Start()
    {
        tx_Estado.text = "ESTADO:";
        tx_Região.text = "REGIÃO:";
        tx_Capital.text = "CAPITAL:";
    }
}
