using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class BaldeMove : MonoBehaviour
{
    public MenuContrlGame menu;
    private bool isDragging = false;
    private Vector3 initialLocalPosition;
    private Vector3 initialMousePosition;
    private Vector3 offset;
    private Vector3 scaleBalde;
    [SerializeField] SpriteRenderer mySombra;

    private void Start()
    {
        scaleBalde = transform.localScale;
        initialLocalPosition = transform.localPosition;
    }
    private void OnMouseDown()
    {
        isDragging = true;
        initialMousePosition = GetMouseLocalPosition();
        offset = initialMousePosition - transform.localPosition;
        mySombra.DOFade(0, .5f);
    }

    private void OnMouseDrag()
    {
        if (isDragging)
        {
            transform.DOScale(Vector3.one, .3f);
            transform.DORotate(new Vector3(0, 0, 40), .3f);
            Vector3 mousePosition = GetMouseLocalPosition();
            transform.localPosition = mousePosition - offset;
        }
    }

    void resetPositionBalde()
    {
        GetComponent<BoxCollider2D>().enabled = false;
        transform.DOLocalMove(initialLocalPosition, .5f).SetEase(Ease.InOutBack).OnComplete(() =>
            {
                transform.DOScale(scaleBalde, .3f);
                mySombra.DOFade(1, .7f);
                if (menu.quantidadeFogo != 0)
                    GetComponent<BoxCollider2D>().enabled = true;
                else
                    menu.fadeAnimaisFogo();
            });
    }

    private void OnMouseUp()
    {
        transform.DORotate(new Vector3(0, 0, 0), .3f);
        if (menu.anyFogo != null)
        {
            menu.quantidadeFogo--;
            GameObject fogo = menu.anyFogo;
            GameObject fumaca = Instantiate(menu.fumacaPrefab);
            fumaca.transform.SetParent(fogo.transform);
            fumaca.transform.localPosition = Vector3.zero;
            fumaca.transform.localScale = new Vector3(3, 3, 3);
            fumaca.transform.SetParent(null);
            Destroy(fogo.transform.parent.gameObject);
            menu.somaContagePontos(1);
        }
        resetPositionBalde();

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
        if (other.name == "boxFogo") menu.anyFogo = other.gameObject;
    }
    private void OnTriggerExit2D(Collider2D other)
    {
        menu.anyFogo = null;
    }
}
