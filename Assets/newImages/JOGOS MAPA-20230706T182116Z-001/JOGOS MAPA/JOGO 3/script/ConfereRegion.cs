using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class ConfereRegion : MonoBehaviour
{
    public MenuContrlGame menu;

    void Start()
    {
        transform.localScale = Vector3.zero;
    }

    private void OnMouseDown() => StartCoroutine(menu.abreInfoRegions(false));
    
}
