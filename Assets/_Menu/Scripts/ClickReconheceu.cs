using KetosGames.SceneTransition;
using DG.Tweening;
using UnityEngine;

public class ClickReconheceu : MonoBehaviour
{
    void Start()
    {
        
    }

    private void OnMouseDown() {
        MenuContrl.reconheceuCapa = true;
        SceneLoader.LoadScene("Menu");
        GetComponent<BoxCollider>().enabled = false;
        transform.DOScale(0, .5f).SetEase(Ease.InBounce);
    }

}
