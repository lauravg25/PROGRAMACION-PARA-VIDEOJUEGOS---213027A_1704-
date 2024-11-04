using UnityEngine;

public class ShyguyController : MonoBehaviour
{
    public GameObject banderaRoja;
    public GameObject banderaBlanca;
    public AIController[] aiControllers; // Referencias a los personajes AI

    private void Start()
    {
        InvokeRepeating("MostrarBanderaAleatoria", 2f, 3f);
    }

    void MostrarBanderaAleatoria()
    {
        banderaRoja.SetActive(false);
        banderaBlanca.SetActive(false);

        int bandera = Random.Range(0, 2);

        if (bandera == 0)
        {
            banderaRoja.SetActive(true);
        }
        else
        {
            banderaBlanca.SetActive(true);
        }

        foreach (var ai in aiControllers)
        {
            if (ai != null)
            {
                ai.ReaccionarABandera(); // Cambia esta línea para llamar al método correcto
            }
        }
    }
}
