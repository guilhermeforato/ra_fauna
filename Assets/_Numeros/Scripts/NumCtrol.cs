using System.Collections.Generic;
using System.Collections;
using UnityEngine;
using DG.Tweening;
using System.Linq;
using UnityEngine.UI;

public class NumCtrol : MonoBehaviour
{
    public Transform positions;
    public Canvas canvas;
    public Sprite spFim;
    public List<int> numeros = new List<int>();
    public List<GameObject> Animais = new List<GameObject>();
    public List<GameObject> BonecosInstaciados = new List<GameObject>();
    public GameObject effctHit;
    public int vezAnimal = 0;
    int numRandom;
    void Start()
    {
        canvas.gameObject.SetActive(false);
        for (var i = 1; i < 11; i++)
            numeros.Add(i);
        numeros = numeros.OrderBy(a => System.Guid.NewGuid()).ToList();
    }
    void OnMouseDown()
    {
        GetComponent<Collider>().enabled = false;
        transform.DOScale(0, .2f).SetEase(Ease.InBack).OnComplete(() => InstanciaObj());
    }
    void resetCoresCanvas()
    {
        foreach (Transform item in canvas.transform.GetChild(0))
            item.GetComponent<Image>().DOColor(Color.white, 0);
    }
    void InstanciaObj()
    {
    volta:
        bool tem = false;
        numRandom = Random.Range(1, 11);
        Debug.Log(numRandom);
        for (var i = 0; i < numeros.Count; i++)
            if (numRandom == numeros[i])
                tem = true;
        if (tem)
        {
            vezAnimal = vezAnimal > 4 ? 0 : vezAnimal;
            for (var i = 0; i < numRandom; i++)
            {
                GameObject game = Instantiate(Animais[vezAnimal], positions.GetChild(i));
                game.transform.localScale = Vector3.zero;
                game.transform.localEulerAngles = new Vector3(0, Random.Range(0, 360), 0);
                game.transform.DOScale(1, .3f).SetEase(Ease.OutBack);
                BonecosInstaciados.Add(game);
            }
            canvas.gameObject.SetActive(true);
        }
        else
            goto volta;
    }
    public void ClearBonecos()
    {
        for (var i = 0; i < BonecosInstaciados.Count; i++)
            EffectInstant(BonecosInstaciados[i].transform);
        for (var i = 0; i < BonecosInstaciados.Count; i++)
            Destroy(BonecosInstaciados[i]);
        BonecosInstaciados.Clear();
        foreach (Transform item in canvas.transform.GetChild(0))
            item.GetComponent<Image>().DOColor(Color.white, 0);
        canvas.gameObject.SetActive(false);
    }
    public void CheckQuantidade(GameObject game)
    {
        StartCoroutine(delay());
        IEnumerator delay()
        {
            int valor = int.Parse(game.name);
            if (valor == numRandom)
            {
                game.GetComponent<Image>().DOColor(Color.green, .2f);
                ClearBonecos();
                numeros.Remove(numRandom);
                vezAnimal++;
                yield return new WaitForSeconds(.3f);
                resetCoresCanvas();
                if (numeros.Count > 0)
                    InstanciaObj();
                else
                    Finish();
            }
            else
                game.GetComponent<Image>().DOColor(Color.red, .2f);
            yield return new WaitForSeconds(.2f);
            if (valor != numRandom)
                resetCoresCanvas();
        }
    }
    void Finish()
    {
        Debug.Log("FIM!");
        GetComponent<SpriteRenderer>().sprite = spFim;
        transform.DOScale(.08f, .1f).SetEase(Ease.OutBack);
    }
    void EffectInstant(Transform transformGame)
    {
        GameObject game = Instantiate(effctHit, transformGame);
        game.transform.localScale = Vector3.one * 1.8f;
        game.transform.localPosition = Vector3.zero;
        game.transform.SetParent(transform.parent);
    }
}