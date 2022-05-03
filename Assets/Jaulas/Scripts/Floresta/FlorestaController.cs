using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class FlorestaController : MonoBehaviour
{
    public GameObject prefabFogo, painel;
    public GameObject[] arvores;
    public TextMesh[] valores;
    [HideInInspector] public int cont, numeroCorreto, numSelect = 0;
    void Start()
    {
        painel.SetActive(false);
        painel.transform.DOScale(0, 0);
        painel.transform.GetChild(1).gameObject.SetActive(false);
        StartCriaFase();
    }

    void RandomValor(int numeroCorreto)
    {
    volta:
        for (int i = 0; i < valores.Length; i++)
        {
            int random = Random.Range(1, 11);
            print("randomizou: " + random);
            if (random == numeroCorreto)
            {
                print("randomizou igual [" + i + "]");
                goto volta;
            }
            else
                valores[i].text = random.ToString();
        }
        valores[Random.Range(0, 3)].text = numeroCorreto.ToString();
    }

    public void AbrePainel()
    {
        painel.SetActive(true);
        painel.transform.DOScale(1, .3f);
    }

    public void AlternaBtnFloresta(GameObject game)
    {
        for (int i = 0; i < painel.transform.GetChild(0).childCount; i++)
            painel.transform.GetChild(0).GetChild(i).GetChild(0).GetChild(0).gameObject.SetActive(false);

        game.transform.GetChild(0).GetChild(0).gameObject.SetActive(true);
        painel.transform.GetChild(1).gameObject.SetActive(true);
    }

    public void restartFase()
    {
        StartCriaFase();
        Balde balde = FindObjectOfType<Balde>();

        for (int i = 0; i < arvores.Length; i++)
            arvores[i].name = "Tree";

        for (int i = 0; i < balde.stonesParent.transform.childCount; i++)
            balde.stonesParent.transform.GetChild(i).DOScale(1, .5f);

        for (int i = 0; i < balde.treesParent.transform.childCount; i++)
            balde.treesParent.transform.GetChild(i).DOScale(1, .5f);

        for (int i = 0; i < painel.transform.GetChild(0).childCount; i++)
            painel.transform.GetChild(0).GetChild(i).GetChild(0).GetChild(0).gameObject.SetActive(false);

        balde.transform.parent.DOScale(.5f, .3f);

        painel.transform.DOScale(0, .3f).OnComplete(() =>
        {
            painel.SetActive(false);
        });
    }

    public void StartCriaFase()
    {
        List<GameObject> arrayFogos = new List<GameObject>();
        List<GameObject> arvoresSelect = new List<GameObject>();

        for (int i = 0; i < arvores.Length; i++)
            arvores[i].name = arvores[i].name + Random.Range(1, 3);

        for (int i = 0; i < arvores.Length; i++)
        {
            if (arvores[i].name.Contains("1"))
            {
                arrayFogos.Add(Instantiate(prefabFogo, Vector3.zero, Quaternion.identity));
                arvores[i].GetComponent<BoxCollider>().enabled = true;
                arvoresSelect.Add(arvores[i]);
            }
        }

        for (int i = 0; i < arvoresSelect.Count; i++)
        {
            int num = Random.Range(1, 3);
            var vector = new Vector3(num == 2 ? 7 : 3, num == 2 ? 7 : 3, num == 2 ? 7 : 3);

            if (num == 1)
                cont++;
            else
                cont = cont + 2;

            arrayFogos[i].transform.SetParent(arvoresSelect[i].transform);
            arrayFogos[i].transform.localScale = vector;
            arrayFogos[i].transform.localPosition = Vector3.zero;
            arrayFogos[i].transform.rotation = Quaternion.Euler(-90, 0, 0);
        }
        numeroCorreto = cont;
        RandomValor(cont);
    }
}
