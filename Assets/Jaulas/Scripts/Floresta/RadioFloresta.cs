using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RadioFloresta : MonoBehaviour
{
    void OnMouseDown()
    {
        FlorestaController floresta = FindObjectOfType<FlorestaController>();
        floresta.AlternaBtnFloresta(gameObject);
        floresta.numSelect = int.Parse(transform.GetChild(1).GetComponent<TextMesh>().text);
    }
}
