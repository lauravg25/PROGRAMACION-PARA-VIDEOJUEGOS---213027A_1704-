using UnityEngine;

public class ShyguyController : MonoBehaviour
{
    public GameObject banderaRoja;
    public GameObject banderaBlanca;
    public AIController[] aiControllers; // Referencias a los personajes AI

    private void Start()
    {
        // Mostrar una bandera al inicio del juego

        MostrarBanderaAleatoria();
    }

    public void MostrarBanderaAleatoria() // Asegúrate de que sea public
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

        // Notificar a los AI para que reaccionen
        foreach (var ai in aiControllers)
        {
            if (ai != null)
            {
                ai.ReaccionarABandera();
            }
        }

        GameManager.instance.turnoShyGuy = true; // Activar el turno de los jugadores

    }
}
