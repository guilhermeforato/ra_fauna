using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HelpScript : MonoBehaviour
{
    [SerializeField] GameObject[] animacoes;
    void Start()
    {
        animacoes[int.Parse(MenuContrl.qualCapa)].SetActive(true);
    }

}
