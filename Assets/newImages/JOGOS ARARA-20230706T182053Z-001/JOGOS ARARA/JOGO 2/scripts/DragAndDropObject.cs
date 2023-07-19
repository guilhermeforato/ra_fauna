using UnityEngine;
using DG.Tweening;

public class DragAndDropObject : MonoBehaviour
{
    public MenuContrlGame menu;
    private Vector3 initialLocalPosition;
    private Vector3 initialMousePosition;
    private Transform parentPosition;
    private Vector3 offset;
    private bool isDragging = false;

    private void Start()
    {
        initialLocalPosition = transform.localPosition;
        parentPosition = transform.parent;
    }

    private void OnMouseDown()
    {
        isDragging = true;
        initialMousePosition = GetMouseLocalPosition();
        offset = initialMousePosition - transform.localPosition;
    }

    public void resetPosition()
    {
        transform.SetParent(parentPosition);
        transform.localPosition = initialLocalPosition;
    }

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
        if (menu.any != null)
        {
            if (menu.any.GetComponent<SpriteRenderer>().sprite.name == GetComponent<SpriteRenderer>().sprite.name)
            {
                transform.SetParent(menu.any.transform);
                transform.localPosition = Vector3.zero;
                menu.acertouSombra(gameObject);
            }
            else
                transform.DOLocalMove(initialLocalPosition, .5f).SetEase(Ease.InOutBack).OnComplete(() => StartCoroutine(menu.errorMessage()));
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
        if (other.name == "sombras")
        {
            menu.any = other.gameObject;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        menu.any = null;
    }
}
