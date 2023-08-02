using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;

public class Parabens : MonoBehaviour
{
    MenuContrlGame menu;

    private void Awake()
    {
        transform.localScale = Vector3.zero;
        GetComponent<Canvas>().worldCamera = FindObjectOfType<Camera>();
        GetComponent<Canvas>().sortingOrder = 3;
        transform.GetChild(0).DOScale(1, 1f).SetEase(Ease.OutBack);
        menu = GameObject.FindWithTag("menuGame").GetComponent<MenuContrlGame>();

    }

    public void setaCapaParabens() => GameObject.FindWithTag("parabens").transform.GetChild(0).GetChild(2).GetComponent<Image>().sprite = menu.spritesAnimaisCapas[int.Parse(MenuContrl.qualCapa)];
}
