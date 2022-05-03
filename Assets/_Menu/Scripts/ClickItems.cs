using UnityEngine;
using UnityEngine.EventSystems;

public class ClickItems : MonoBehaviour, IPointerClickHandler
{
    private MenuContrl menu;

    void Awake() => menu = FindObjectOfType<MenuContrl>();

    public void OnPointerClick(PointerEventData eventData)
    {
        if (gameObject.tag == "capa")
            MenuContrl.qualCapa = gameObject.name;
        else
            MenuContrl.qualJogo = MenuContrl.qualCapa + gameObject.name;

        print(MenuContrl.qualJogo);
    }

}
