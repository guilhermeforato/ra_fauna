using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class moveLixo : MonoBehaviour
{
    public MenuContrlGame menu;
    private bool isDragging = false;
    private Vector3 initialLocalPosition;
    private Vector3 initialMousePosition;
    private Vector3 offset;

    private void OnMouseDown()
    {
        isDragging = true;
        initialMousePosition = GetMouseLocalPosition();
        offset = initialMousePosition - transform.localPosition;
    }

    public void resetPosition() => transform.localPosition = initialLocalPosition;

    private void OnMouseDrag()
    {
        if (isDragging)
        {
            Vector3 mousePosition = GetMouseLocalPosition();
            transform.localPosition = mousePosition - offset;
        }
    }

    private void OnMouseUp()
    {
        if (menu.anyLixo != null)
        {
            menu.confereFase2Macaco(gameObject);
        }
        else
        {
            transform.DOLocalMove(initialLocalPosition, .5f).SetEase(Ease.InOutBack);
        }
        isDragging = false;
    }


    private Vector3 GetMouseLocalPosition()
    {
        Vector3 mousePosition = Input.mousePosition;
        mousePosition.z = -Camera.main.transform.position.z;
        return Camera.main.ScreenToWorldPoint(mousePosition);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.transform.parent.name == "Lixos") menu.anyLixo = other.gameObject;
    }
    private void OnTriggerExit2D(Collider2D other)
    {
        menu.anyLixo = null;
    }

}
