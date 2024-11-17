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
        ActualizarPuntuacionUI();
        if (pantallaFinJuego != null)
        {
            pantallaFinJuego.SetActive(false); // Asegurarse de que esté desactivado al iniciar
        }
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
        juegoTerminado = true; // Marcar el juego como terminado
        if (pantallaFinJuego != null)
        {
            pantallaFinJuego.SetActive(true); // Mostrar el panel de fin de juego
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
        SceneManager.LoadScene("MenuInicial");
        // Reiniciar variables de estado
        puntuacion = 0;
        vidas = 1;
        juegoTerminado = false;
    }
}
