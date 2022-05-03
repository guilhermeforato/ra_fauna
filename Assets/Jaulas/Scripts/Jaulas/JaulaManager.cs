using UnityEngine;

public class JaulaManager : MonoBehaviour
{
    public int soma;
    public TextMesh numeroCentro;
    public ParticleSystem[] particles;

    void Start()
    {
        soma = 0;
        Randomiza();
    }

    public void Randomiza()
    {
        numeroCentro.text = Random.Range(1, 6).ToString();
    }

    public void ParticlesExplode()
    {
        for (int i = 0; i < particles.Length; i++)
            particles[i].Play();
    }
}
