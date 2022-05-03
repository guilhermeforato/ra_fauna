using UnityEngine;

public class BtnLibertar : MonoBehaviour
{
    public JaulaManager jaulaManager;

    void OnMouseDown()
    {
        JaulaDown[] jaulas = FindObjectsOfType<JaulaDown>();

        if (jaulaManager.soma == int.Parse(jaulaManager.numeroCentro.text))
            jaulaManager.ParticlesExplode();

        for (int i = 0; i < jaulas.Length; i++)
            jaulas[i].transform.GetChild(0).GetChild(0).GetChild(1).gameObject.SetActive(false);

        jaulaManager.Randomiza();
        jaulaManager.soma = 0;
    }
}
