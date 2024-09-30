using UnityEngine;
using System.Collections;

public class ControladorBanderas : MonoBehaviour
{
    public GameObject[] banderas; // Array de banderas
    public GameObject[] banderasShyGuy; // Array de banderas de Shy Guy
    private int indiceActual = 0;
    private bool haPerdido = false;
    private bool primeraInteraccion = true;

    void Start()
    {
        MostrarBanderaInicial();
    }

    void Update()
    {
        if (!haPerdido)
        {
            if (primeraInteraccion)
            {
                MostrarBanderaInicial();
                primeraInteraccion = false;
            }
            else
            {
                MostrarBanderaAleatoria();
            }
        }
    }

    void MostrarBanderaInicial()
    {
        // Desactivar todas las banderas
        foreach (GameObject bandera in banderas)
        {
            bandera.SetActive(false);
        }

        // Activar una bandera de Shy Guy
        if (banderasShyGuy.Length > 0)
        {
            banderasShyGuy[0].SetActive(true);
        }
    }

    void MostrarBanderaAleatoria()
    {
        // Desactivar todas las banderas
        foreach (GameObject bandera in banderas)
        {
            bandera.SetActive(false);
        }

        // Verificar que el array no esté vacío
        if (banderas.Length > 0)
        {
            // Seleccionar una bandera aleatoria y activarla
            indiceActual = Random.Range(0, banderas.Length);
            banderas[indiceActual].SetActive(true);

            // Verificar si la bandera es distinta a Shy Guy
            if (!EsBanderaShyGuy(banderas[indiceActual]))
            {
                HundirseEnElMar();
            }
        }
        else
        {
            Debug.LogError("El array de banderas está vacío.");
        }
    }


    bool EsBanderaShyGuy(GameObject bandera)
    {
        foreach (GameObject shyGuyBandera in banderasShyGuy)
        {
            if (bandera == shyGuyBandera)
            {
                return true;
            }
        }
        return false;
    }

    void HundirseEnElMar()
    {
        haPerdido = true;
        // Mover al jugador hacia abajo (al fondo del mar)
        StartCoroutine(Hundir());
    }

    IEnumerator Hundir()
    {
        while (transform.position.y > -10)
        {
            transform.position += Vector3.down * Time.deltaTime * 2; // Ajusta la velocidad según sea necesario
            yield return null;
        }
    }
}
