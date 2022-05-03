using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class LixoController : MonoBehaviour
{
    public GameObject Any = null;
    public GameObject prefabErrorAll;
    public Transform[] posicoes;
    public GameObject[] prefabLixos;
    public List<GameObject> lista = new List<GameObject>();
    public int contLixos = 0;

    void Start()
    {
        StartGame();
    }

    void StartGame()
    {
        int numRandom = Random.Range(4, 9);
        print(numRandom);

        for (int i = 0; i < numRandom; i++)
        {
            lista.Add(InstanciaLixo(i));
            contLixos++;
        }
    }

    public GameObject InstanciaLixo(int position)
    {
        posicoes = posicoes.OrderBy(a => System.Guid.NewGuid()).ToArray();

        GameObject game = Instantiate(prefabLixos[Random.Range(0, prefabLixos.Length)]);
        game.transform.SetParent(posicoes[position]);
        posicoes[position].GetChild(0).transform.localPosition = Vector3.zero;
        posicoes[position].GetChild(0).transform.localScale = new Vector3(1.5f, 1.5f, 1.5f);
        return game;
    }
}
