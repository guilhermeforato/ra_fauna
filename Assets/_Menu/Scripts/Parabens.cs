using UnityEngine;
using DG.Tweening;

public class Parabens : MonoBehaviour
{
    private void Awake()
    {
        transform.localScale = Vector3.zero;
        GetComponent<Canvas>().worldCamera = FindObjectOfType<Camera>();
        GetComponent<Canvas>().sortingOrder = 3;
        transform.GetChild(0).DOScale(1, 1f).SetEase(Ease.OutBack);

    }
}
