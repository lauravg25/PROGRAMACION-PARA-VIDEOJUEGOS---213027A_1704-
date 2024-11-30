using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using System.Collections; // Necesario para usar IEnumerator
using System.Collections.Generic; // Necesario para List<>



public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public int puntuacion = 0;
    public int vidas = 1; // Solo una vida
    public TMP_Text puntuacionText; // Para mostrar el puntaje durante el juego
    public GameObject pantallaFinJuego; // Panel de fin del juego
    public TMP_Text puntuacionFinalText; // Para mostrar la puntuación final
    public bool juegoTerminado = false; // Cambiar de private a public
    public TMP_Text cuentaRegresivaText; // Texto para la cuenta regresiva
    public bool turnoShyGuy = false; // Controla si es el turno de Shy Guy
    public bool juegoIniciado = false; // Solo se activará tras la cuenta regresiva
    public List<GameObject> jugadores = new List<GameObject>();








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
        StartCoroutine(MostrarCuentaRegresiva()); // Inicia la cuenta regresiva

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

        // Actualizar referencias si son nulas
        if (pantallaFinJuego == null)
        {
            pantallaFinJuego = GameObject.Find("PantallaFinJuego"); // Nombre exacto del objeto en la escena
            if (pantallaFinJuego == null)
            {
                Debug.LogError("pantallaFinJuego no se encontró en la escena.");
            }
        }

        if (cuentaRegresivaText == null)
        {
            cuentaRegresivaText = GameObject.Find("CuentaRegresivaText")?.GetComponent<TMP_Text>();
            if (cuentaRegresivaText == null)
            {
                Debug.LogError("cuentaRegresivaText no se encontró en la escena.");
            }
        }

        if (pantallaFinJuego != null)
        {
            pantallaFinJuego.SetActive(false); // Ocultar la pantalla de fin de juego
        }
    }




    public IEnumerator MostrarCuentaRegresiva()
    {
        cuentaRegresivaText.gameObject.SetActive(true);

        for (int i = 3; i > 0; i--)
        {
            cuentaRegresivaText.text = i.ToString();
            yield return new WaitForSeconds(1f);
        }

        cuentaRegresivaText.text = "¡YA!";
        yield return new WaitForSeconds(1f);

        cuentaRegresivaText.gameObject.SetActive(false);

        // Activa el inicio del juego
        juegoIniciado = true;

        // Inicia el turno de Shy Guy
        ShyguyController shyGuy = FindObjectOfType<ShyguyController>();
        if (shyGuy != null)
        {
            shyGuy.MostrarBanderaAleatoria();
        }
    }



    public void VerificarUltimoJugador()
    {
        int jugadoresActivos = 0;
        GameObject ultimoJugador = null;

        foreach (var jugador in jugadores)
        {
            if (jugador.activeSelf) // Cambiar según tu lógica para determinar si un jugador sigue activo
            {
                jugadoresActivos++;
                ultimoJugador = jugador;
            }
        }

        if (jugadoresActivos == 1)
        {
            juegoTerminado = true;
            MostrarMensajeFin($"Ganó el jugador {ultimoJugador.name}");
            Invoke(nameof(FinDelJuego), 1f); // Mostrar pantalla de fin tras un retraso
        }
    }


    public TMP_Text mensajeFinJuegoText; // Asigna este campo desde el Inspector

    public void MostrarMensajeFin(string mensaje)
    {
        if (mensajeFinJuegoText != null)
        {
            mensajeFinJuegoText.gameObject.SetActive(true); // Activa el texto en la pantalla
            mensajeFinJuegoText.text = mensaje; // Establece el mensaje
        }
        else
        {
            Debug.LogError("mensajeFinJuegoText no está asignado en el GameManager.");
        }
    }







}
