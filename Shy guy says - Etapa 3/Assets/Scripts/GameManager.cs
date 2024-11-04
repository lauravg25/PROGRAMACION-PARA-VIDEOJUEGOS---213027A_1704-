using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public int puntuacion = 0;
    public int vidas = 2;
    public Text puntuacionText;
    public GameObject pantallaFinJuego; // Panel para mostrar al perder
    public Text puntuacionFinalText;

    private void Awake()
    {
        // Asegurarse de que solo hay un GameManager
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        // Actualizar la UI al iniciar el juego
        ActualizarPuntuacionUI();
        pantallaFinJuego.SetActive(false);
    }

    public void IncrementarPuntuacion(int puntos)
    {
        puntuacion += puntos;
        ActualizarPuntuacionUI();
    }

    public void PerderVida()
    {
        vidas--;

        if (vidas < 0)
        {
            FinDelJuego();
        }
    }

    private void ActualizarPuntuacionUI()
    {
        puntuacionText.text = "Puntuación: " + puntuacion;
    }

    private void FinDelJuego()
    {
        // Mostrar la pantalla de fin de juego
        pantallaFinJuego.SetActive(true);
        puntuacionFinalText.text = "Puntuación final: " + puntuacion;
        Time.timeScale = 0f; // Pausar el juego
    }

    public void VolverAlMenuPrincipal()
    {
        Time.timeScale = 1f; // Reanudar el tiempo al volver al menú
        SceneManager.LoadScene("menu principal");
    }
}
