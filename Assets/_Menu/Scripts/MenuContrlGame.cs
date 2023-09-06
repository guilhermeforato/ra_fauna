using System.Collections;
using System.Collections.Generic;
using KetosGames.SceneTransition;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;

public class MenuContrlGame : MonoBehaviour
{
    public bool Dev;
    public GameObject Fx_Click, sfx_Background, panelHelp, parabensPrefab, tenteNovamente, correto;
    [HideInInspector] Sprite spriteQualHelp;
    public Sprite[] spritesToggleSongs, spritesAnimaisCapas;
    [SerializeField] GameObject[] pontuacao;
    [HideInInspector] public int contagemPontos;

    [SerializeField] GameObject[] jogosMacaco, jogosArara, jogosMapa;
    [SerializeField] GameObject[] fundosMacaco, fundosArara, fundosMapa;
    [SerializeField] Sprite[] helpsMacaco, helpsArara, helpsMapa;

    [Header("*** JOGO ARARA #1 ***")]
    [SerializeField] GameObject numeros, loadAnimais;
    [SerializeField] GameObject[] prefabsAnimais;
    public Transform[] posicoes;
    public int[] numAuxs;
    [HideInInspector] public int sumAcerto = 0;
    private bool jogoFogo = false;


    [Header("*** JOGO ARARA #2 ***")]
    [SerializeField] SpriteRenderer animalSelect;
    [HideInInspector]
    public GameObject any;
    [SerializeField] GameObject prefabPuf;
    [SerializeField] Sprite[] sombras, animais;
    public int[] numAuxAnimais;
    [SerializeField] Transform[] posicoes_sombras;
    private int animalSombraContagem = 0;
    private Sprite sombraCerta;

    [Header("*** JOGO ARARA #3 ***")]
    public SpriteRenderer pecaEncaixeEsquerda;
    [HideInInspector] public GameObject selectPeca;
    public GameObject pecaEncaixeDireita;
    [SerializeField] Canvas canvasRandomizaCartas;
    public Transform[] locaisPecas;
    [SerializeField] int[] randomizatrepecas, randomizatrelocais;
    [SerializeField] GameObject[] prefabPecasImages;
    [HideInInspector] public int auxNumFase = 0;
    [HideInInspector] public bool achouPecaIgual = false;
    [HideInInspector] public bool firstRandomizada = true;
    [HideInInspector] public bool randomizando = false;

    [Header("*** JOGO MACACO #1 ***")]
    public GameObject chaveClick;
    [SerializeField] Sprite spriteOtimo;
    public SpriteRenderer numeroJaula, chaveGirando;
    public ParticleSystem particleChave;
    [SerializeField] Animator[] jaulas;
    [SerializeField] Sprite[] numerosJaulas, spritesChaves;
    [HideInInspector] public int quantidadeJaula = 0;
    [HideInInspector] public int auxJaulas = 0;

    [Header("*** JOGO MACACO #2 ***")]
    [HideInInspector] public GameObject anyLixo;
    [SerializeField] GameObject[] itensLixos;
    [SerializeField] Transform[] posicoesLixos;

    [Header("*** JOGO MACACO #3 ***")]
    [HideInInspector] public GameObject clickAntes;
    [SerializeField] Transform[] locaisCartas, locaisCartasAux;
    [SerializeField] Sprite[] cartas, interrogas;
    [HideInInspector] public int contClickCarta, contaAcertosCartas = 0;

    [Header("*** JOGO MAPA #1 ***")]
    public GameObject fumacaPrefab;
    public GameObject fogoPrefab;
    [HideInInspector] public GameObject anyFogo;
    [SerializeField] Transform[] fogoPosicoes;
    [SerializeField] SpriteRenderer[] animaisFogo;
    [HideInInspector] public int quantidadeFogo;

    [Header("*** JOGO MAPA #2 ***")]
    [SerializeField] Transform selectJanela;
    [SerializeField] SpriteRenderer numeroCentro;
    [SerializeField] GameObject menuconfirmar;
    public List<GameObject> listJanelas = new List<GameObject>();
    [SerializeField] GameObject[] fasesJanela, setas;
    [SerializeField] Sprite[] numerosControllerBarco;
    [SerializeField] Sprite[] numerosBarco;
    [HideInInspector] public int auxJanelasNext, auxSetas, auxProximaJanela = 0;

    [Header("*** JOGO MAPA #3 ***")]
    [SerializeField] GameObject regionInfo;
    [HideInInspector] public GameObject anyRegion;
    [SerializeField] BoxCollider2D[] regions;

    void Awake()
    {
        ativaSom();
        contagemPontos = 0;
        resetSpritesPontuacao();
        startGame();
        StartCoroutine(delayStartHelp());
    }

    void startGame()
    {
        if (Dev)
        {
            MenuContrl.qualCapa = "1";
            MenuContrl.qualJogo = "1";
        }
        switch (int.Parse(MenuContrl.qualCapa))
        {
            case 0:
                jogosMacaco[int.Parse(MenuContrl.qualJogo)].SetActive(true);
                fundosMacaco[int.Parse(MenuContrl.qualJogo)].SetActive(true);
                fundosMacaco[int.Parse(MenuContrl.qualJogo)].SetActive(true);
                spriteQualHelp = helpsMacaco[int.Parse(MenuContrl.qualJogo)];
                switch (int.Parse(MenuContrl.qualJogo))
                {
                    case 0:
                        startJogo1Macaco();
                        break;
                    case 1:
                        startJogo2Macaco();
                        break;
                    case 2:
                        startJogo3Macaco();
                        break;
                }
                break;
            case 1:
                jogosArara[int.Parse(MenuContrl.qualJogo)].SetActive(true);
                fundosArara[int.Parse(MenuContrl.qualJogo)].SetActive(true);
                spriteQualHelp = helpsArara[int.Parse(MenuContrl.qualJogo)];
                switch (int.Parse(MenuContrl.qualJogo))
                {
                    case 0:
                        startJogo1Arara();
                        break;
                    case 1:
                        startJogo2Arara();
                        break;
                    case 2:
                        startJogo3Arara();
                        break;
                }
                break;
            case 2:
                jogosMapa[int.Parse(MenuContrl.qualJogo)].SetActive(true);
                fundosMapa[int.Parse(MenuContrl.qualJogo)].SetActive(true);
                spriteQualHelp = helpsMapa[int.Parse(MenuContrl.qualJogo)];
                switch (int.Parse(MenuContrl.qualJogo))
                {
                    case 0:
                        startJogo1Mapa();
                        break;
                    case 1:
                        startJogo2Mapa();
                        break;
                    case 2:
                        // nao precisa
                        break;
                }
                break;
        }
    }

    private void startJogo1Arara()
    {
        RandomizeObjects();
        RandomizeIntegers();
        RandomizePositions();
        StartCoroutine(randomizaNovosAnimais());
    }

    private void startJogo2Arara()
    {
        RandomizeAnimais();
        RandomizeSombras();
        setaAnimalCorrespondente();
    }

    public void parabens()
    {
        GameObject canvas = Instantiate(parabensPrefab);
        canvas.GetComponent<Canvas>().sortingOrder = 10;
    }

    public void destroyParabensDaCena()
    {
        GameObject objetoEncontrado = GameObject.FindGameObjectWithTag("parabens");
        if (objetoEncontrado) Destroy(objetoEncontrado);
    }

    IEnumerator delayStartHelp()
    {
        yield return new WaitForSeconds(1f);
        openHelp();
    }

    void resetSpritesPontuacao()
    {
        for (int i = 0; i < pontuacao.Length; i++)
        {
            pontuacao[i].GetComponent<Image>().color = new Color(1f, 1f, 1f, 0f);
            pontuacao[i].SetActive(true);
        }
    }

    public void somaContagePontos(int quantidade) => StartCoroutine(animPontuacao(quantidade));

    public IEnumerator animPontuacao(int quantidade)
    {
        if (contagemPontos < 10 || (contagemPontos == 9 && quantidade < 2))
        {
            for (int i = contagemPontos; i < contagemPontos + quantidade; i++)
            {
                pontuacao[i].GetComponent<Image>().DOFade(1, .2f);
                yield return new WaitForSeconds(.2f);
            }
            contagemPontos += quantidade;
            if (contagemPontos == 10 && !jogoFogo)
            {
                parabens();
            }
        }
    }

    private void openHelp()
    {
        panelHelp.transform.localScale = Vector3.zero;
        panelHelp.SetActive(true);
        panelHelp.transform.DOScale(1, .4f).SetEase(Ease.OutBounce);
        if (spriteQualHelp)
            panelHelp.transform.GetChild(0).GetComponent<Image>().sprite = spriteQualHelp;
    }

    private void ativaSom()
    {
        if (!GameObject.FindGameObjectWithTag("fxClick")) DontDestroyOnLoad(Instantiate(Fx_Click));
        if (!GameObject.FindGameObjectWithTag("sfxBackground")) DontDestroyOnLoad(Instantiate(sfx_Background));
    }

    public static void PlayClick()
    {
        if (GameObject.FindGameObjectWithTag("fxClick")) GameObject.FindGameObjectWithTag("fxClick").GetComponent<AudioSource>().Play();
    }

    public void scaleOneObject(GameObject game)
    {
        game.transform.localScale = Vector3.zero;
        game.SetActive(true);
        game.transform.DOScale(1, .4f).SetEase(Ease.OutBounce);
    }

    public void scaleZeroObject(GameObject game)
    {
        game.transform.DOScale(0, .4f).SetEase(Ease.InBounce).OnComplete(() =>
        {
            game.transform.localScale = Vector3.zero;
            game.SetActive(false);
        });
    }

    public void chageSom(Image toggle)
    {
        toggle.sprite = MenuContrl.som ? spritesToggleSongs[0] : spritesToggleSongs[1];
        changeStatusMusica(Constants.SOM);
        MenuContrl.som = !MenuContrl.som;
    }

    public void changeMusic(Image toggle)
    {
        toggle.sprite = MenuContrl.musica ? spritesToggleSongs[0] : spritesToggleSongs[1];
        changeStatusMusica(Constants.MUSICA);
        MenuContrl.musica = !MenuContrl.musica;
    }

    void changeStatusMusica(string typeSong)
    {
        if (typeSong == Constants.SOM)
        {
            if (MenuContrl.som)
                GameObject.FindGameObjectWithTag("fxClick").GetComponent<AudioSource>().mute = true;
            else
                GameObject.FindGameObjectWithTag("fxClick").GetComponent<AudioSource>().mute = false;
        }
        else
        {
            if (MenuContrl.musica)
                GameObject.FindGameObjectWithTag("sfxBackground").GetComponent<AudioSource>().DOFade(0, .5f);
            else
                GameObject.FindGameObjectWithTag("sfxBackground").GetComponent<AudioSource>().DOFade(.5f, .5f);
        }
    }


    public IEnumerator errorMessage()
    {
        tenteNovamente.transform.localScale = Vector3.zero;
        tenteNovamente.SetActive(true);
        tenteNovamente.transform.DOScale(4, .3f).SetEase(Ease.OutBack);
        yield return new WaitForSeconds(1);
        tenteNovamente.transform.DOScale(0, .3f).SetEase(Ease.InOutQuart);
    }

    public IEnumerator acertoMessage(Vector2 posicao = default(Vector2), Vector2 scala = default(Vector2))
    {
        correto.transform.localPosition = Vector3.zero;
        correto.transform.localScale = Vector3.zero;
        correto.SetActive(true);
        correto.transform.DOScale(scala, .3f).SetEase(Ease.OutBack);
        yield return new WaitForSeconds(1);
        correto.transform.DOScale(0, .3f).SetEase(Ease.OutFlash).OnComplete(() =>
        {
            correto.SetActive(false);
        });
        contaAcertosCartas = 0;
    }

    public void Back() => SceneLoader.LoadScene("Menu");

    public void QuitGame()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
                        Application.Quit();
#endif
    }

    // JOGO 1 ARARA ------------------------------------------------------

    public void confereJogo1Arara(GameObject game)
    {
        int numButton = int.Parse(game.name);
        if (numButton == numAuxs[sumAcerto])
        {
            somaContagePontos(1);
            for (int i = 0; i < posicoes.Length; i++)
            {
                if (posicoes[i].childCount > 0) Destroy(posicoes[i].GetChild(0).gameObject);
            }
            sumAcerto++;

            RandomizeObjects();
            RandomizePositions();
            if (sumAcerto < 10)
                StartCoroutine(randomizaNovosAnimais());
            else
            {
                numeros.SetActive(false);
            }
        }
    }

    public IEnumerator randomizaNovosAnimais()
    {
    volta:

        for (int i = 0; i < posicoes.Length; i++) if (posicoes[i].childCount > 0) Destroy(posicoes[i].GetChild(0).gameObject);

        RandomizeObjects();

        for (int i = 0; i < numAuxs[sumAcerto]; i++)
        {
            GameObject game = Instantiate(prefabsAnimais[i]);
            game.transform.SetParent(posicoes[i]);
            game.transform.localPosition = Vector3.zero;
            game.GetComponent<SpriteRenderer>().color = new Color(255, 255, 255, 0);
            game.GetComponent<SpriteRenderer>().sortingOrder = game.transform.parent.GetComponent<SpriteRenderer>().sortingOrder;
        }
        yield return new WaitForSeconds(.1f);

        if (!conferePosicaoMacaco())
        {
            loadAnimais.SetActive(true);
            goto volta;
        }
        else
        {
            yield return new WaitForSeconds(.5f);
            for (int i = 0; i < posicoes.Length; i++) if (posicoes[i].childCount > 0) posicoes[i].GetChild(0).GetComponent<SpriteRenderer>().DOFade(1, .3f);
            loadAnimais.SetActive(false);
        }

    }

    bool conferePosicaoMacaco()
    {
        bool valid = true;
        for (int i = 0; i < posicoes.Length; i++)
        {
            if (posicoes[i].childCount > 0)
            {
                valid = posicoes[i].GetChild(0).tag == posicoes[i].tag;
                if (!valid) break;
            }
        }

        return valid;
    }

    private void RandomizeObjects()
    {
        int arrayLength = prefabsAnimais.Length;
        for (int i = 0; i < arrayLength - 1; i++)
        {
            int randomIndex = Random.Range(i, arrayLength);
            GameObject temp = prefabsAnimais[randomIndex];
            prefabsAnimais[randomIndex] = prefabsAnimais[i];
            prefabsAnimais[i] = temp;
        }
    }

    private void RandomizePositions()
    {
        int arrayLength = posicoes.Length;
        for (int i = 0; i < arrayLength - 1; i++)
        {
            int randomIndex = Random.Range(i, arrayLength);
            Transform temp = posicoes[randomIndex];
            posicoes[randomIndex] = posicoes[i];
            posicoes[i] = temp;
        }
    }

    private void RandomizeIntegers()
    {
        int arrayLength = numAuxs.Length;
        for (int i = 0; i < arrayLength - 1; i++)
        {
            int randomIndex = Random.Range(i, arrayLength);
            int temp = numAuxs[randomIndex];
            numAuxs[randomIndex] = numAuxs[i];
            numAuxs[i] = temp;
        }
    }

    // FIM JOGO 1

    // JOGO 2 ARARA ------------------------------------------------------

    private void RandomizeSombras()
    {
    achouSombraIgual:
        int arraySombrasLength = sombras.Length;
        for (int i = 0; i < arraySombrasLength - 1; i++)
        {
            int randomIndex = Random.Range(i, arraySombrasLength);
            Sprite temp = sombras[randomIndex];
            sombras[randomIndex] = sombras[i];
            sombras[i] = temp;
        }

        RandomizeArrayValues();

        print(animalSelect.sprite.name);

        for (int i = 0; i < posicoes_sombras.Length; i++)
        {
            posicoes_sombras[i].GetComponent<SpriteRenderer>().sprite = sombras[numAuxAnimais[i]];

            if (animalSelect.sprite.name.Equals(posicoes_sombras[i].GetComponent<SpriteRenderer>().sprite.name)) goto achouSombraIgual;
        }
    }

    private void RandomizeAnimais()
    {
        int arrayAnimaisLength = animais.Length;
        for (int i = 0; i < arrayAnimaisLength - 1; i++)
        {
            int randomIndex = Random.Range(i, arrayAnimaisLength);
            Sprite temp = animais[randomIndex];
            animais[randomIndex] = animais[i];
            animais[i] = temp;
        }
        animalSelect.sprite = animais[animalSombraContagem];
    }

    private void RandomizeArrayValues()
    {
        int[] availableValues = new int[10];

        for (int i = 0; i < availableValues.Length; i++) availableValues[i] = i + 1;

        for (int i = 0; i < numAuxAnimais.Length; i++)
        {
            int randomIndex = Random.Range(0, availableValues.Length - i);
            numAuxAnimais[i] = availableValues[randomIndex];
            availableValues[randomIndex] = availableValues[availableValues.Length - i - 1];
        }
    }

    void setaAnimalCorrespondente()
    {
        int posiRandom = Random.Range(0, 4);

        for (int i = 0; i < sombras.Length; i++)
        {
            if (animalSelect.sprite.name.Equals(sombras[i].name))
            {
                sombraCerta = sombras[i];
                break;
            }
        }

        posicoes_sombras[posiRandom].GetComponent<SpriteRenderer>().sprite = sombraCerta;
    }

    public void instatiatePuf()
    {
        GameObject puf = Instantiate(prefabPuf);
        puf.transform.SetParent(posicoes_sombras[0].transform.parent.parent);
        puf.transform.localPosition = Vector3.zero;
        puf.transform.localScale = Vector3.one;
    }

    public void acertouSombra(GameObject game)
    {
        game.GetComponent<BoxCollider2D>().enabled = false;
        somaContagePontos(1);
        instatiatePuf();
        for (int i = 0; i < posicoes_sombras.Length; i++)
        {
            posicoes_sombras[i].DOScale(Vector3.zero, .5f).SetEase(Ease.InOutBack);
        }
        game.transform.DOScale(Vector3.zero, .5f).SetEase(Ease.InOutBack).OnComplete(() =>
        {
            if (animalSombraContagem < 9)
            {
                stepToNext();
                game.GetComponent<DragAndDropObject>().resetPosition();
                game.transform.DOScale(4, .5f).SetEase(Ease.OutBack).OnComplete(() =>
                {
                    for (int i = 0; i < posicoes_sombras.Length; i++) posicoes_sombras[i].DOScale(.6f, .5f).SetEase(Ease.OutBack);
                    game.GetComponent<BoxCollider2D>().enabled = true;
                });
            }
        });
    }

    void stepToNext()
    {
        animalSombraContagem++;
        animalSelect.sprite = animais[animalSombraContagem];
        RandomizeSombras();
        setaAnimalCorrespondente();
    }

    // FIM JOGO 2

    // JOGO 3 ARARA ------------------------------------------------------

    void startJogo3Arara()
    {
        RandomizePecas();
    }

    public void RandomizePecas()
    {
        int arrayPecasLength = prefabPecasImages.Length;
        int randPeca = Random.Range(0, 3);

        int[] availableValues = new int[5];
        for (int i = 0; i < availableValues.Length; i++) availableValues[i] = i + 1;

        for (int i = 0; i < randomizatrepecas.Length; i++)
        {
            int randomIndex = Random.Range(0, availableValues.Length - i);
            randomizatrepecas[i] = availableValues[randomIndex];
            availableValues[randomIndex] = availableValues[availableValues.Length - i - 1];
        }

        int[] availableValuesLocais = new int[3];
        for (int i = 0; i < availableValuesLocais.Length; i++) availableValuesLocais[i] = i + 1;

        for (int i = 0; i < locaisPecas.Length; i++)
        {
            int randomIndex = Random.Range(0, availableValuesLocais.Length - i);
            randomizatrelocais[i] = availableValuesLocais[randomIndex];
            availableValuesLocais[randomIndex] = availableValuesLocais[availableValuesLocais.Length - i - 1];
        }
        if (firstRandomizada)
        {
            for (int i = 0; i < arrayPecasLength - 1; i++)
            {
                int randomIndex = Random.Range(i, arrayPecasLength);
                GameObject temp = prefabPecasImages[randomIndex];
                prefabPecasImages[randomIndex] = prefabPecasImages[i];
                prefabPecasImages[i] = temp;
            }
        }

        StartCoroutine(criaAsPecas());
    }

    public void chamaCriarPecas()
    {
        StartCoroutine(criaAsPecas());
    }

    public IEnumerator criaAsPecas()
    {
        embaralhandoCartas(1);
        randomizando = false;
        yield return new WaitForSeconds(.5f);

    retry:

        if (achouPecaIgual)
        {
            achouPecaIgual = false;
        }

        for (int i = 0; i < locaisPecas.Length; i++)
        {
            GameObject prefab = Instantiate(prefabPecasImages[randomizatrepecas[i] - 1]);
            prefab.GetComponent<PrefabPeca>().menu = this;
            prefab.transform.SetParent(locaisPecas[randomizatrelocais[i] - 1]);
            prefab.transform.localPosition = Vector3.zero;
            prefab.transform.localScale = Vector3.zero;
        }

        print("instanciou as pecas");


        int sorteioLugar = Random.Range(0, 3);
        Destroy(locaisPecas[sorteioLugar].GetChild(0).gameObject);
        print("sorteioLugar: " + sorteioLugar);
        GameObject prefabDaVez = Instantiate(prefabPecasImages[auxNumFase]);
        prefabDaVez.GetComponent<PrefabPeca>().newPrefab = true;
        prefabDaVez.GetComponent<PrefabPeca>().menu = this;
        prefabDaVez.transform.SetParent(locaisPecas[sorteioLugar]);
        prefabDaVez.transform.localPosition = Vector3.zero;
        prefabDaVez.transform.localScale = Vector3.zero;

        yield return StartCoroutine(achouIgual(prefabDaVez));

        if (achouPecaIgual)
        {
            randomizando = true;
            yield return new WaitForSeconds(.2f);

            for (int i = 0; i < locaisPecas.Length; i++)
            {
                Destroy(locaisPecas[i].GetChild(0).gameObject);
            }
            print("destruiu");

            yield return new WaitForSeconds(.2f);

            goto retry;
        }
        else
        {
            randomizando = false;
            embaralhandoCartas(0);
            pecaEncaixeEsquerda.sprite = prefabDaVez.GetComponent<PrefabPeca>().imgEsquerda;
            if (!prefabDaVez.GetComponent<PrefabPeca>().bico)
                pecaEncaixeEsquerda.transform.localPosition = new Vector2(-1.387f, 0);
            else
                pecaEncaixeEsquerda.transform.localPosition = new Vector2(-2.18f, 0);
        }

    }

    IEnumerator achouIgual(GameObject prefabDavez)
    {
        yield return new WaitForSeconds(.2f);
        for (int i = 0; i < locaisPecas.Length; i++)
        {
            if (prefabDavez.name == locaisPecas[i].GetChild(0).gameObject.name && !locaisPecas[i].GetChild(0).GetComponent<PrefabPeca>().newPrefab)
            {
                print("achou igual");
                achouPecaIgual = true;
                break;
            }
        }
    }

    public void ativaDesativaBoxPecas(bool ativa)
    {
        for (int i = 0; i < locaisPecas.Length; i++)
            if (locaisPecas[i].childCount > 0)
                locaisPecas[i].GetChild(0).GetComponent<BoxCollider2D>().enabled = ativa;
    }

    public void destroyPrefabs()
    {
        for (int i = 0; i < locaisPecas.Length; i++)
        {
            if (locaisPecas[i].childCount > 0)
            {
                print("destruiuPrefabs");
                Destroy(locaisPecas[i].GetChild(0).gameObject);
            }
        }
    }

    public void fadeRandomizaCartas(int fade) => canvasRandomizaCartas.GetComponent<CanvasGroup>().DOFade(fade, .3f);


    public void embaralhandoCartas(int num)
    {
        fadeRandomizaCartas(num);
        switch (num)
        {
            case 0:
                pecaEncaixeEsquerda.transform.DOScale(0.5115618f, .3f).SetEase(Ease.InOutBack);
                for (int i = 0; i < locaisPecas.Length; i++)
                {
                    if (locaisPecas[0].childCount > 0)
                        locaisPecas[i].GetChild(0).transform.DOScale(2.5f, .3f).SetEase(Ease.InOutBack);
                }
                break;

            case 1:

                pecaEncaixeEsquerda.transform.DOScale(0, .3f).SetEase(Ease.InOutBack);
                for (int i = 0; i < locaisPecas.Length; i++)
                {
                    if (locaisPecas[0].childCount > 0)
                        locaisPecas[i].GetChild(0).transform.DOScale(0, .3f).SetEase(Ease.InOutBack);
                }
                break;

            default:
                break;
        }
    }

    public bool confereFase3Arara() => pecaEncaixeEsquerda.sprite.name.Substring(0, 1) == pecaEncaixeDireita.GetComponent<SpriteRenderer>().sprite.name.Substring(0, 1);

    // FIM JOGO 3 ARARA

    // JOGO 1 MACACO ------------------------------------------------------

    void startJogo1Macaco()
    {
        randomizaNumerosJaula();
        numeroJaula.sprite = numerosJaulas[auxJaulas];
    }

    void randomizaNumerosJaula()
    {
        int arrayAnimaisLength = numerosJaulas.Length;
        chaveClick.GetComponent<BoxCollider2D>().enabled = false;

        for (int i = 0; i < arrayAnimaisLength - 1; i++)
        {
            int randomIndex = Random.Range(i, arrayAnimaisLength);
            Sprite temp = numerosJaulas[randomIndex];
            numerosJaulas[randomIndex] = numerosJaulas[i];
            numerosJaulas[i] = temp;
        }
    }

    void fechaTodasJaulas()
    {
        for (int i = 0; i < jaulas.Length; i++) jaulas[i].SetBool("open", false);
    }

    public IEnumerator confereFase1Macaco()
    {
        for (int i = 0; i < jaulas.Length; i++) jaulas[i].GetComponent<BoxCollider2D>().enabled = false;
        tenteNovamente.transform.localPosition = Vector3.zero;
        if (quantidadeJaula == int.Parse(numerosJaulas[auxJaulas].name))
        {
            auxJaulas++;
            numeroJaula.transform.DOScale(0, .3f).SetEase(Ease.InOutBack).OnComplete(() =>
            {
                somaContagePontos(2);
                numeroJaula.sprite = numerosJaulas[auxJaulas];
            });
            if (auxJaulas < 5)
            {
                Vector2 vetorz = Vector2.zero;
                Vector2 vecScale = new Vector2(2, 2);
                StartCoroutine(acertoMessage(vetorz, vecScale));
            }
        }
        else StartCoroutine(errorMessage());

        quantidadeJaula = 0;
        yield return new WaitForSeconds(1);
        StartCoroutine(acertoMessage());
        tenteNovamente.transform.DOScale(0, .4f).SetEase(Ease.InOutBack);
        numeroJaula.transform.DOScale(2, .3f).SetEase(Ease.InOutBack);
        fechaTodasJaulas();
        for (int i = 0; i < jaulas.Length; i++) jaulas[i].GetComponent<JaulaClick>().stateJaula = true;
        yield return new WaitForSeconds(.5f);
        if (auxJaulas < 5)
        {
            for (int i = 0; i < jaulas.Length; i++) jaulas[i].GetComponent<BoxCollider2D>().enabled = true;
            chaveGirando.transform.parent.GetComponent<Animator>().SetBool("gira", false);
            particleChave.Stop();
        }
    }

    void ativaChave(bool ativa)
    {
        chaveGirando.transform.DOScale(0, .3f).SetEase(Ease.OutBack).OnComplete(() =>
        {
            chaveGirando.sprite = ativa ? spritesChaves[0] : spritesChaves[1];
            chaveGirando.transform.DOScale(0, .3f).SetEase(Ease.InOutBack);
        });
    }

    // FIM JOGO 1 MACACO

    // JOGO 2 MACACO ------------------------------------------------------

    void startJogo2Macaco()
    {
        randomizaItensLixo();
    }

    void randomizaItensLixo()
    {
        int arrayItensLixoLength = itensLixos.Length;

        for (int i = 0; i < arrayItensLixoLength - 1; i++)
        {
            int randomIndex = Random.Range(i, arrayItensLixoLength);
            GameObject temp = itensLixos[randomIndex];
            itensLixos[randomIndex] = itensLixos[i];
            itensLixos[i] = temp;
        }

        for (int i = 0; i < posicoesLixos.Length; i++)
        {
            itensLixos[i].transform.SetParent(posicoesLixos[i]);
            itensLixos[i].transform.localPosition = Vector3.zero;
        }
    }

    void setBoxItensLixos(bool stats)
    {
        for (int i = 0; i < itensLixos.Length; i++) itensLixos[i].GetComponent<BoxCollider2D>().enabled = stats;
    }

    public void confereFase2Macaco(GameObject game)
    {
        GameObject currentAny = anyLixo;
        setBoxItensLixos(false);

        if (game.name == currentAny.name)
        {
            int orderOld = currentAny.transform.GetChild(0).GetComponent<SpriteRenderer>().sortingOrder;
            game.transform.SetParent(currentAny.transform);
            game.transform.DOScale(.75f, .4f);
            game.transform.DOLocalMove(new Vector2(0, 10), .3f).SetEase(Ease.InFlash).OnComplete(() =>
            {
                somaContagePontos(1);
                currentAny.transform.GetChild(0).GetComponent<SpriteRenderer>().sortingOrder = 4;
                game.transform.DOLocalMove(Vector3.zero, .4f).SetEase(Ease.InOutBack).OnComplete(() =>
                {
                    game.SetActive(false);
                    setBoxItensLixos(true);
                    currentAny.transform.GetChild(0).GetComponent<SpriteRenderer>().sortingOrder = orderOld;
                });
            });
        }
        else
        {
            game.GetComponent<moveLixo>().resetPosition();
            StartCoroutine(delayErrorLixos());
        }
    }

    IEnumerator delayErrorLixos()
    {
        StartCoroutine(errorMessage());
        yield return new WaitForSeconds(1.6f);
        setBoxItensLixos(true);
    }

    // FIM JOGO 2 MACACO

    // JOGO 3 MACACO ------------------------------------------------------

    void startJogo3Macaco()
    {
        randomizaCartas();
    }

    void randomizaCartas()
    {
        int arrayCartasLength = cartas.Length;

        for (int i = 0; i < arrayCartasLength - 1; i++)
        {
            int randomIndex = Random.Range(i, arrayCartasLength);
            Sprite temp = cartas[randomIndex];
            cartas[randomIndex] = cartas[i];
            cartas[i] = temp;
        }

        int arrayInterrogasLength = interrogas.Length;

        for (int i = 0; i < arrayInterrogasLength - 1; i++)
        {
            int randomIndex = Random.Range(i, arrayInterrogasLength);
            Sprite temp = interrogas[randomIndex];
            interrogas[randomIndex] = interrogas[i];
            interrogas[i] = temp;
        }

        int arrayPosicoesCartasAuxLength = locaisCartasAux.Length;

        for (int i = 0; i < arrayPosicoesCartasAuxLength - 1; i++)
        {
            int randomIndex = Random.Range(i, arrayPosicoesCartasAuxLength);
            Transform temp = locaisCartasAux[randomIndex];
            locaisCartasAux[randomIndex] = locaisCartasAux[i];
            locaisCartasAux[i] = temp;
        }

        for (int i = 0; i < locaisCartas.Length; i++) locaisCartas[i].localPosition = locaisCartasAux[i].localPosition;

        setImagesCartas();
    }
    void setImagesCartas()
    {
        for (int i = 0; i < 4; i++) locaisCartas[i].GetComponent<CartaMemoria>().mySprite = cartas[i];
        for (int i = 4, auxiliar = 0; i < locaisCartas.Length; i++, auxiliar++) locaisCartas[i].GetComponent<CartaMemoria>().mySprite = cartas[auxiliar];
        for (int i = 0; i < locaisCartas.Length; i++)
        {
            locaisCartas[i].GetComponent<SpriteRenderer>().sprite = interrogas[i];
            locaisCartas[i].GetComponent<CartaMemoria>().interrogaSprite = interrogas[i];
        }
    }

    public void confereFase3Macaco(GameObject game)
    {
        string nomeAntes = clickAntes.GetComponent<CartaMemoria>().mySprite.name;
        string nomeDepois = game.GetComponent<CartaMemoria>().mySprite.name;

        for (int i = 0; i < locaisCartas.Length; i++) locaisCartas[i].GetComponent<BoxCollider2D>().enabled = false;

        if (nomeAntes.Equals(nomeDepois))
        {
            contaAcertosCartas++;
            game.GetComponent<CartaMemoria>().acertou = true;
            clickAntes.GetComponent<CartaMemoria>().acertou = true;
            contClickCarta = 0;
            clickAntes = null;
            ativaBoxCartas();
            if (contaAcertosCartas == 4) StartCoroutine(resetCartas());
        }
        else
            StartCoroutine(delaydesgira(game));
    }

    IEnumerator delaydesgira(GameObject game)
    {
        yield return new WaitForSeconds(1);
        StartCoroutine(errorMessage());
        game.GetComponent<Animator>().SetBool("gira", false);
        clickAntes.GetComponent<Animator>().SetBool("gira", false);
        yield return new WaitForSeconds(1);
        ativaBoxCartas();
    }
    IEnumerator resetCartas()
    {
        yield return new WaitForSeconds(1);
        StartCoroutine(acertoMessage(new Vector2(0, 0), new Vector2(4, 4)));
        somaContagePontos(2);
        for (int i = 0; i < locaisCartas.Length; i++)
        {
            locaisCartas[i].GetComponent<Animator>().SetBool("gira", false);
            locaisCartas[i].GetComponent<CartaMemoria>().acertou = false;
        }
        yield return new WaitForSeconds(1);
        randomizaCartas();
        yield return new WaitForSeconds(.5f);
        if (contagemPontos < 10)
        {
            ativaBoxCartas();
        }
    }
    void ativaBoxCartas()
    {
        for (int i = 0; i < locaisCartas.Length; i++)
        {
            if (!locaisCartas[i].GetComponent<CartaMemoria>().acertou)
                locaisCartas[i].GetComponent<BoxCollider2D>().enabled = true;
        }
    }

    // FIM JOGO 3 MACACO

    // JOGO 1 MAPA ------------------------------------------------------

    void startJogo1Mapa()
    {
        jogoFogo = true;
        randomLocaisFogo();
    }

    void randomLocaisFogo()
    {
        quantidadeFogo = 10;
        regionInfo.transform.GetChild(0).transform.localScale = Vector3.zero;
        regionInfo.transform.GetChild(1).transform.localScale = Vector3.zero;
        for (int i = 0; i < fogoPosicoes.Length; i++)
        {
            GameObject fogo = Instantiate(fogoPrefab);
            fogo.transform.SetParent(fogoPosicoes[i]);
            fogo.transform.localPosition = Vector3.zero;
        }
    }

    public void fadeAnimaisFogo()
    {
        for (int i = 0; i < animaisFogo.Length; i++)
            animaisFogo[i].DOFade(1, .5f);
    }

    // FIM JOGO 1 MAPA

    // JOGO 2 MAPA ------------------------------------------------------

    void startJogo2Mapa()
    {
        randomFasesJanelas();
        nextStepJanelas();
    }

    void randomFasesJanelas()
    {
        setas[0].SetActive(false);
        int arrayFasesJanelasLength = fasesJanela.Length;

        for (int i = 0; i < arrayFasesJanelasLength - 1; i++)
        {
            int randomIndex = Random.Range(i, arrayFasesJanelasLength);
            GameObject temp = fasesJanela[randomIndex];
            fasesJanela[randomIndex] = fasesJanela[i];
            fasesJanela[i] = temp;
        }
    }

    void nextStepJanelas()
    {
        for (int i = 0; i < fasesJanela.Length; i++) fasesJanela[i].SetActive(false);
        if (auxJanelasNext < fasesJanela.Length)
        {
            fasesJanela[auxJanelasNext].SetActive(true);
            for (int i = 0; i < fasesJanela[auxJanelasNext].transform.childCount; i++)
            {
                GameObject filho = fasesJanela[auxJanelasNext].transform.GetChild(i).gameObject;
                if (filho.name == "janela")
                {
                    listJanelas.Add(filho);
                }
            }
            selectJanela.transform.SetParent(listJanelas[0].transform);
            selectJanela.localPosition = Vector3.zero;
            selectJanela.gameObject.SetActive(true);
            menuconfirmar.transform.DOScale(0, .3f);
            numeroCentro.transform.parent.DOScale(3, .3f);
        }
        else
            menuconfirmar.transform.DOScale(0, .3f);

    }

    public void controlaSetas(GameObject game)
    {
        if (game.name == "setaEsquerda")
        {
            if (auxSetas > 0)
            {
                auxSetas--;
                setas[1].SetActive(true);
            }
            if (auxSetas == 0) game.SetActive(false);
        }
        else
        {
            if (auxSetas < 3)
            {
                auxSetas++;
                setas[0].SetActive(true);
            }
            if (auxSetas == 3) game.SetActive(false);
        }

        numeroCentro.sprite = numerosControllerBarco[auxSetas];
    }

    public void setaNumeroCentro(string name)
    {
        if (auxProximaJanela < listJanelas.Count)
        {
            for (int i = 0; i < numerosControllerBarco.Length; i++)
            {
                if (numerosControllerBarco[i].name == name)
                {
                    listJanelas[auxProximaJanela].transform.GetChild(0).GetComponent<SpriteRenderer>().sprite = numerosBarco[i];
                    break;
                }
            }
            listJanelas[auxProximaJanela].transform.GetChild(0).transform.localScale = Vector3.zero;
            listJanelas[auxProximaJanela].transform.GetChild(0).gameObject.SetActive(true);
            listJanelas[auxProximaJanela].transform.GetChild(0).DOScale(Vector3.one, .3f).SetEase(Ease.InOutBack);
            auxProximaJanela++;
            if (auxProximaJanela < listJanelas.Count)
            {
                selectJanela.SetParent(listJanelas[auxProximaJanela].transform);
                selectJanela.localPosition = Vector3.zero;
            }
        }

        if (auxProximaJanela == listJanelas.Count)
        {
            selectJanela.gameObject.SetActive(false);
            numeroCentro.transform.parent.DOScale(0, .3f);
            menuconfirmar.transform.DOScale(2.5f, .3f);
        }
    }

    public IEnumerator confirmarSoma(bool cond = false, bool limpa = false)
    {
        int valorSomado = 0;
        for (int i = 0; i < listJanelas.Count; i++) valorSomado += int.Parse(listJanelas[i].transform.GetChild(0).GetComponent<SpriteRenderer>().sprite.name.Substring(0, 1));

        if (cond) cond = valorSomado == 5;

        if (!cond)
        {
            menuconfirmar.transform.DOScale(0, .3f);
            if (!limpa)
            {
                StartCoroutine(errorMessage());
                yield return new WaitForSeconds(1f);
            }

            numeroCentro.transform.parent.DOScale(3, .3f);
            for (int i = 0; i < listJanelas.Count; i++) listJanelas[i].transform.GetChild(0).gameObject.SetActive(false);
            selectJanela.transform.SetParent(listJanelas[0].transform);
            selectJanela.transform.localPosition = Vector3.zero;
            selectJanela.gameObject.SetActive(true);
        }
        else
        {
            somaContagePontos(2);
            StartCoroutine(acertoMessage(new Vector2(0, 0), new Vector2(2, 2)));
            auxJanelasNext++;
            listJanelas.Clear();
            menuconfirmar.transform.DOScale(0, .3f);
            yield return new WaitForSeconds(1);
            nextStepJanelas();
        }
        auxProximaJanela = 0;
    }
    // FIM JOGO 2 MAPA

    // JOGO 3 MAPA ------------------------------------------------------

    public void ativaBoxsRegions(bool stats)
    {
        for (int i = 0; i < regions.Length; i++)
            regions[i].enabled = stats;
    }

    public IEnumerator abreInfoRegions(bool stats, Sprite sprite = null)
    {
        GameObject quadroInfo = regionInfo.transform.GetChild(0).gameObject;
        GameObject botao = regionInfo.transform.GetChild(1).gameObject;
        quadroInfo.GetComponent<SpriteRenderer>().sprite = sprite;
        quadroInfo.transform.DOScale(stats ? 1 : 0, .5f).SetEase(Ease.InOutBack);
        ativaBoxsRegions(!stats);
        if (!stats)
        {
            botao.transform.DOScale(0, .1f);
            somaContagePontos(2);
        }
        yield return new WaitForSeconds(.5f);
        botao.transform.DOScale(stats ? .3f : 0, .3f).SetEase(Ease.InOutBack);
    }

    // FIM JOGO 3 MAPA

}
