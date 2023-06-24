using DG.Tweening;
using KetosGames.SceneTransition;
using UnityEngine;

public class MenuRA : MonoBehaviour
{
    public GameObject Help;
    public GameObject prefabHelp;
    public GameObject[] jogos;

    public void ChangeScene(string nameScene) => SceneLoader.LoadScene(nameScene);
 
    public void Exit()
    {
        MenuContrl.PlayClick();
        DOVirtual.DelayedCall(.5f, () => Application.Quit());
    }
    void Awake()
    {
        for (int i = 0; i < jogos.Length; i++) 
        if (jogos[i].transform.GetChild(0).GetChild(0).gameObject.name != MenuContrl.qualJogo)
            jogos[i].SetActive(false);
            
        print(MenuContrl.qualJogo);

        if (!Application.HasUserAuthorization(UserAuthorization.WebCam))
        {
            Application.RequestUserAuthorization(UserAuthorization.WebCam);
        }
    }
    private void Start() => ShowHelp();
    public void CloseHelp()
    {
        if (Help)
            Help.SetActive(false);
        MenuContrl.PlayClick();
    }
    public void ShowHelp()
    {
        if (!Help)
            Help = Instantiate(prefabHelp);
        else
            Help.SetActive(true);
        MenuContrl.PlayClick();
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.A))
        {
            Application.RequestUserAuthorization(UserAuthorization.WebCam);
        }
    }
}