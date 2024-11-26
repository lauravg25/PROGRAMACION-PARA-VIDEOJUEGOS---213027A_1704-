using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public int puntuacion = 0;
    public int vidas = 1; // Solo una vida
    public TMP_Text puntuacionText; // Para mostrar el puntaje durante el juego
    public GameObject pantallaFinJuego; // Panel de fin del juego
    public TMP_Text puntuacionFinalText; // Para mostrar la puntuación final
    public bool juegoTerminado = false; // Cambiar de private a public


    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject); // Mantener el GameManager al recargar la escena
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        // Reasignar pantallaFinJuego si es necesario
        if (pantallaFinJuego == null)
        {
            pantallaFinJuego = GameObject.Find("PantallaFinJuego");
        }

        ReiniciarEstado(); // Asegurarse de que el estado se reinicie
    }


    public void IncrementarPuntuacion(int puntos)
    {
        if (!juegoTerminado) // Solo incrementar si el juego no ha terminado
        {
            puntuacion += puntos;
            ActualizarPuntuacionUI();
        }
    }

    public void PerderVida()
    {
        if (juegoTerminado) return; // Evita múltiples llamadas

        Debug.Log("Vida perdida. Vidas restantes: " + vidas);
        vidas--;

        if (vidas <= 0)
        {
            FinDelJuego(); // Llamar al método para mostrar la pantalla de fin
        }
    }


    private void FinDelJuego()
    {
        if (juegoTerminado) return; // Evitar múltiples llamadas

        juegoTerminado = true; // Marcar el juego como terminado
        Debug.Log("Fin del juego llamado");

        if (pantallaFinJuego != null)
        {
            pantallaFinJuego.SetActive(true); // Mostrar el panel de fin de juego
        }
        else
        {
            Debug.LogError("pantallaFinJuego no está asignado en el GameManager.");
        }

        if (puntuacionFinalText != null)
        {
            puntuacionFinalText.text = "Puntuación final: " + puntuacion;
        }

        Time.timeScale = 0f; // Pausar el juego
    }


    private void ActualizarPuntuacionUI()
    {
        if (puntuacionText != null)
        {
            puntuacionText.text = "Puntuación: " + puntuacion;
        }
    }

    public void VolverAlMenuPrincipal()
    {
        Time.timeScale = 1f; // Reanudar el tiempo al volver al menú
        ReiniciarEstado(); // Reiniciar el estado del juego
        SceneManager.LoadScene("MenuInicial"); // Cambiar a la escena del menú principal
    }


    public void ReiniciarEstado()
    {
        Time.timeScale = 1f; // Reanudar el tiempo si estaba pausado
        puntuacion = 0;
        vidas = 1;
        juegoTerminado = false;

        if (pantallaFinJuego != null)
        {
            pantallaFinJuego.SetActive(false); // Ocultar la pantalla de fin de juego
        }
    }


}
