using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class regiaoMove : MonoBehaviour
{
    public MenuContrlGame menu;
    private bool isDragging = false;
    private Vector3 initialLocalPosition;
    private Vector3 initialMousePosition;
    private Vector3 offset, sizeBox;
    private Vector3 scaleRegion;
    private int orderRender;
    public Sprite spriteInfo;

    private void Start()
    {
        scaleRegion = transform.localScale;
        sizeBox = GetComponent<BoxCollider2D>().size;
        orderRender = GetComponent<SpriteRenderer>().sortingOrder;
        initialLocalPosition = transform.localPosition;
    }
    private void OnMouseDown()
    {
        isDragging = true;
        GetComponent<SpriteRenderer>().sortingOrder = 5;
        GetComponent<BoxCollider2D>().size = new Vector3(3, 3, 3);
        initialMousePosition = GetMouseLocalPosition();
        offset = initialMousePosition - transform.localPosition;
    }

    private void OnMouseDrag()
    {
        if (isDragging)
        {
            Vector3 mousePosition = GetMouseLocalPosition();
            transform.localPosition = mousePosition - offset;
        }
    }

    void resetPositionRegiao()
    {
        GetComponent<BoxCollider2D>().enabled = false;
        GetComponent<BoxCollider2D>().size = sizeBox;
        transform.DOLocalMove(initialLocalPosition, .5f).SetEase(Ease.InOutBack).OnComplete(() =>
        {
            GetComponent<SpriteRenderer>().sortingOrder = orderRender;
            GetComponent<BoxCollider2D>().enabled = true;
        });
    }

    private void OnMouseUp()
    {
        if (menu.anyRegion != null)
        {
            if (menu.anyRegion.name == name)
            {
                menu.anyRegion.GetComponent<SpriteRenderer>().DOColor(Color.white, .4f);
                menu.anyRegion.GetComponent<SpriteRenderer>().DOFade(1, .4f);
                menu.anyRegion.GetComponent<SpriteRenderer>().sortingOrder = 4;
                menu.anyRegion.name = "ok";
                transform.localScale = Vector3.zero;
                StartCoroutine(menu.abreInfoRegions(true, spriteInfo));
            }
            else
            {
                resetPositionRegiao();
                StartCoroutine(menu.errorMessage());
            }
        }
        else
            resetPositionRegiao();

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
        if (other.transform.parent.name == "boxRegions")
        {
            menu.anyRegion = other.gameObject;
            if (other.transform.parent.name == "boxRegions" && other.name != "ok")
                other.GetComponent<SpriteRenderer>().DOFade(.5f, .3f);
        }
    }
    private void OnTriggerExit2D(Collider2D other)
    {
        menu.anyRegion = null;
        if (other.transform.parent.name == "boxRegions" && other.name != "ok")
            other.GetComponent<SpriteRenderer>().DOFade(0, .3f);
    }
}
