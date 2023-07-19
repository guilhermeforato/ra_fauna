using UnityEngine;
using DG.Tweening;
using System.Collections;

public class PrefabPeca : MonoBehaviour
{
    public MenuContrlGame menu;
    public Sprite imgEsquerda, encaixe;
    public bool bico = false;
    private bool isDragging = false;
    private Vector3 initialLocalPosition;
    private Vector3 initialMousePosition;
    private Vector3 offset;
    public bool newPrefab = false;

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
        if (menu.selectPeca != null)
        {
            if (!bico)
                menu.pecaEncaixeDireita.transform.localPosition = new Vector2(2.15f, 0);
            else
                menu.pecaEncaixeDireita.transform.localPosition = new Vector2(1.358758f, 0);
            menu.selectPeca.GetComponent<SpriteRenderer>().sprite = encaixe;

            menu.pecaEncaixeDireita.transform.localScale = Vector3.zero;
            menu.pecaEncaixeDireita.transform.DOScale(0.5115618f, .4f).SetEase(Ease.OutBack);

            if (menu.confereFase3Arara()) StartCoroutine(nextStep());
            else
            {
                resetPosition();
                StartCoroutine(menu.errorMessage());
                menu.pecaEncaixeDireita.GetComponent<SpriteRenderer>().DOFade(.2f, .2f);
                gameObject.SetActive(true);
            }
        }
        else
        {
            transform.DOLocalMove(initialLocalPosition, .5f).SetEase(Ease.InOutBack);
        }
        isDragging = false;
    }

    IEnumerator nextStep()
    {
        menu.pecaEncaixeDireita.GetComponent<SpriteRenderer>().DOFade(1, .2f);
        GetComponent<SpriteRenderer>().sortingOrder = 5;
        for (int i = 0; i < menu.locaisPecas.Length; i++)
            if (menu.locaisPecas[i].childCount > 0)
            {
                if (menu.locaisPecas[i].GetChild(0).gameObject.name != name)
                {
                    menu.locaisPecas[i].GetChild(0).gameObject.SetActive(false);
                }
            }

        menu.somaContagePontos(2);
        menu.ativaDesativaBoxPecas(false);
        yield return new WaitForSeconds(.5f);
        StartCoroutine(menu.acertoMessage(new Vector2(50, -35), new Vector2(1.5f, 1.5f)));
        yield return new WaitForSeconds(2);
        if (menu.contagemPontos < 10)
        {
            menu.pecaEncaixeDireita.transform.DOScale(0, .3f).SetEase(Ease.InOutBack).OnComplete(() =>
            {
                menu.pecaEncaixeDireita.GetComponent<SpriteRenderer>().sprite = null;
                menu.pecaEncaixeDireita.transform.localScale = new Vector2(0.5115618f, 0.5115618f);
                menu.ativaDesativaBoxPecas(true);
            });
            StartCoroutine(menu.acertoMessage());
            menu.destroyPrefabs();
            if (menu.auxNumFase < 4)
            {
                menu.firstRandomizada = false;
                menu.auxNumFase++;
                menu.achouPecaIgual = false;
                menu.RandomizePecas();
            }
            else menu.pecaEncaixeEsquerda.gameObject.SetActive(false);
        }
    }

    private Vector3 GetMouseLocalPosition()
    {
        Vector3 mousePosition = Input.mousePosition;
        mousePosition.z = -Camera.main.transform.position.z;
        return Camera.main.ScreenToWorldPoint(mousePosition);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.name == "encaixe") menu.selectPeca = other.gameObject;
    }
    private void OnTriggerExit2D(Collider2D other)
    {
        menu.selectPeca = null;
    }
}
