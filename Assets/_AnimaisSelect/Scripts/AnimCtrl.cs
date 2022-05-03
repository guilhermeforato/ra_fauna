using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class AnimCtrl : MonoBehaviour
{
    public GameObject effect;
    public GameObject prefabEffect;
    public void InstantiateAnimal(GameObject animal)
    {
        InstatiateEffect();
        foreach (Transform item in transform)
            item.gameObject.SetActive(false);
        animal.SetActive(true);
        animal.transform.localScale = Vector3.zero;
        animal.transform.DOScale(animal.GetComponent<AnimItem>().m_scale, .2f).SetEase(Ease.OutBack, 3);
    }
    void InstatiateEffect() => Instantiate(prefabEffect, effect.transform);
}
