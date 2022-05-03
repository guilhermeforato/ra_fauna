using KetosGames.SceneTransition;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using DG.Tweening;
using UnityEngine;

public class MenuContrl : MonoBehaviour
{

    [SerializeField] CanvasGroup canvasGroup, canvasPais;
    [SerializeField] InputField inputTerms;
    public GameObject Fx_Click, sfx_Background;
    [SerializeField] Text[] txts;
    [SerializeField] GameObject[] paineis;

    public static string qualCapa, qualJogo = "";

    public static bool aceitou_termos = false;

    private void Start()
    {
        canvasGroup.interactable = false;
        canvasGroup.blocksRaycasts = false;
        canvasPais.gameObject.SetActive(false);
        
        canvasGroup.DOFade(0, 0);
        for (int i = 0; i < paineis.Length; i++) paineis[i].SetActive(false);
        if (!GameObject.FindGameObjectWithTag("fxClick")) DontDestroyOnLoad(Instantiate(Fx_Click));
        if (!GameObject.FindGameObjectWithTag("sfxBackground")) DontDestroyOnLoad(Instantiate(sfx_Background));
        if (!aceitou_termos) paineis[0].SetActive(true);
    }

    public static void PlayClick()
    {
        if (GameObject.FindGameObjectWithTag("fxClick")) GameObject.FindGameObjectWithTag("fxClick").GetComponent<AudioSource>().Play();
    }
    public void test(){
        print(gameObject.name);
    }
    public void CheckPaisTerms(GameObject painelPais){ if (aceitou_termos) Play(); else painelPais.SetActive(true); }

    public void Play()
    {
        SceneLoader.LoadScene("Game");
        GameObject.FindObjectOfType<MenuContrl>().GetComponent<AudioSource>().Play();
    }

    public void setAcceptTerms() => aceitou_termos = true;

    public void PanelQR(int num)
    {
        canvasGroup.interactable = num == 0 ? false : true;
        canvasGroup.blocksRaycasts = num == 0 ? false : true;
        canvasGroup.DOFade(num, .2f);
        PlayClick();
    }

    // _________________________ termos de uso _________________________

    public void GenerateTerms()
    {
        for (var i = 0; i < txts.Length; i++) txts[i].text = Random.Range(1, 6).ToString();
    }

    public void CheckTerms(GameObject game)
    {
        if (inputTerms.text != "")
        {
            int valortotal = int.Parse(txts[0].text) + int.Parse(txts[1].text);
            if (int.Parse(inputTerms.text) == valortotal)
            {
                game.SetActive(false);
                //abrir url
                Application.OpenURL("https://drive.google.com/file/d/1-Lgiuo6hlDQ4vBB_3WbRWM67QTL_B2-D/view?usp=sharing");
            }
            else
            {
                inputTerms.transform.DOShakeRotation(.5f, 30, 10, 30, true);
                inputTerms.text = "";
            }
        }
    }
}