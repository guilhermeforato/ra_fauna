using KetosGames.SceneTransition;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEngine;
using System.Collections;
using DG.Tweening;

public class MenuContrl : MonoBehaviour
{

    [SerializeField] CanvasGroup warningPais, mainPlaca, uiConfiguracoes;
    [SerializeField] Sprite[] spritesToggleSongs;
    [SerializeField] GameObject configuracoes, capas, jogos;
    [SerializeField] Image toggleSom, toggleMusica, selectGame;
    [SerializeField] InputField inputTerms;
    public GameObject Fx_Click, sfx_Background;
    [SerializeField] Text[] txts;
    [SerializeField] GameObject[] paineis;

    public static string qualCapa, qualJogo = "";

    public static bool aceitou_termos = false;
    public static bool som = true;
    public static bool musica = true;
    private void Start() => deixarConfiguradoStartGame();

    private void deixarConfiguradoStartGame()
    {
        warningPais.gameObject.SetActive(false);
        capas.SetActive(false);
        jogos.SetActive(false);
        configuracoes.SetActive(false);
        ativaSom();
        configuraStartToggleSongs();
        if (!aceitou_termos)
        {
            uiConfiguracoes.DOFade(0, 0);
            uiConfiguracoes.interactable = false;
            uiConfiguracoes.blocksRaycasts = false;
            StartCoroutine(StartWarningPais());
        }
        else
        {
            uiConfiguracoes.interactable = true;
            uiConfiguracoes.blocksRaycasts = true;
            mainPlaca.gameObject.SetActive(true);
        }
    }

    void configuraStartToggleSongs()
    {
        toggleMusica.sprite = musica ? spritesToggleSongs[1] : spritesToggleSongs[0];
        toggleSom.sprite = som ? spritesToggleSongs[1] : spritesToggleSongs[0];
    }

    public void chageSom(Image toggle)
    {
        toggle.sprite = som ? spritesToggleSongs[0] : spritesToggleSongs[1];
        changeStatusMusica(Constants.SOM);
        som = !som;
    }

    public void changeMusic(Image toggle)
    {
        toggle.sprite = musica ? spritesToggleSongs[0] : spritesToggleSongs[1];
        changeStatusMusica(Constants.MUSICA);
        musica = !musica;
    }

    public void selectCapa(Image sprite) {
        qualCapa = sprite.name;
        selectGame.sprite = sprite.sprite;
    }

    void changeStatusMusica(string typeSong)
    {
        if (typeSong == Constants.SOM)
        {
            if (som)
                GameObject.FindGameObjectWithTag("fxClick").GetComponent<AudioSource>().mute = true;
            else
                GameObject.FindGameObjectWithTag("fxClick").GetComponent<AudioSource>().mute = false;
        }
        else
        {
            if (musica)
                GameObject.FindGameObjectWithTag("sfxBackground").GetComponent<AudioSource>().DOFade(0, .5f);
            else
                GameObject.FindGameObjectWithTag("sfxBackground").GetComponent<AudioSource>().DOFade(1, .5f);
        }
    }

    private void ativaSom()
    {
        if (!GameObject.FindGameObjectWithTag("fxClick")) DontDestroyOnLoad(Instantiate(Fx_Click));
        if (!GameObject.FindGameObjectWithTag("sfxBackground")) DontDestroyOnLoad(Instantiate(sfx_Background));
    }

    private IEnumerator StartWarningPais()
    {
        mainPlaca.gameObject.SetActive(false);
        mainPlaca.DOFade(0, 0);
        warningPais.transform.DOScale(0, 0);
        yield return new WaitForSeconds(1f);
        warningPais.gameObject.SetActive(true);
        warningPais.transform.DOScale(1, .5f).SetEase(Ease.OutBounce);
    }

    public static void PlayClick()
    {
        if (GameObject.FindGameObjectWithTag("fxClick")) GameObject.FindGameObjectWithTag("fxClick").GetComponent<AudioSource>().Play();
    }

    public void Play(GameObject game)
    {
        qualJogo = game.name;
        SceneLoader.LoadScene("Game");
    }

    public void QuitGame()
    {
        #if UNITY_EDITOR
                UnityEditor.EditorApplication.isPlaying = false;
        #else
                    Application.Quit();
        #endif
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

    public void setAcceptTerms()
    {
        mainPlaca.gameObject.SetActive(true);
        mainPlaca.DOFade(1, .4f);
        aceitou_termos = true;
        uiConfiguracoes.interactable = true;
        uiConfiguracoes.blocksRaycasts = true;
        warningPais.transform.DOScale(0, .5f).SetEase(Ease.InBounce).OnComplete(() =>
        {
            warningPais.gameObject.SetActive(false);
        });
        uiConfiguracoes.DOFade(1, .4f);
    }

    // _________________________ termos de uso _________________________

    // public void GenerateTerms()
    // {
    //     for (var i = 0; i < txts.Length; i++) txts[i].text = Random.Range(1, 6).ToString();
    // }

    // public void CheckTerms(GameObject game)
    // {
    //     if (inputTerms.text != "")
    //     {
    //         int valortotal = int.Parse(txts[0].text) + int.Parse(txts[1].text);
    //         if (int.Parse(inputTerms.text) == valortotal)
    //         {
    //             game.SetActive(false);
    //             //abrir url
    //             Application.OpenURL("https://drive.google.com/file/d/1-Lgiuo6hlDQ4vBB_3WbRWM67QTL_B2-D/view?usp=sharing");
    //         }
    //         else
    //         {
    //             inputTerms.transform.DOShakeRotation(.5f, 30, 10, 30, true);
    //             inputTerms.text = "";
    //         }
    //     }
    // }
}