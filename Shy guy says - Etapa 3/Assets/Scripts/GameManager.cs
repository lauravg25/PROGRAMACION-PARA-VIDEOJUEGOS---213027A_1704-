using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using System.Collections;
using System.Collections.Generic;

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
    public TMP_Text mensajeFinJuegoText; // Asigna este campo desde el Inspector
    public GameObject mensajeEmergente; // Objeto del mensaje emergente
    public TMP_Text mensajeEmergenteText; // Texto del mensaje emergente

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
            return; // Salir del método para evitar ejecutar el código restante
        }
    }

    private void Start()
    {
        InicializarReferencias();
        ReiniciarEstado();
        StartCoroutine(MostrarCuentaRegresiva());
    }

    private void InicializarReferencias()
    {
        if (pantallaFinJuego == null)
        {
            pantallaFinJuego = GameObject.Find("PantallaFinJuego");
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

        if (mensajeFinJuegoText == null)
        {
            mensajeFinJuegoText = GameObject.Find("MensajeFinJuegoText")?.GetComponent<TMP_Text>();
            if (mensajeFinJuegoText == null)
            {
                Debug.LogError("mensajeFinJuegoText no se encontró en la escena.");
            }
        }

        if (puntuacionFinalText == null)
        {
            puntuacionFinalText = GameObject.Find("PuntuacionFinalText")?.GetComponent<TMP_Text>();
            if (puntuacionFinalText == null)
            {
                Debug.LogError("puntuacionFinalText no se encontró en la escena.");
            }
        }

        if (mensajeEmergente == null)
        {
            mensajeEmergente = GameObject.Find("PanelMensajeEmergente");
            if (mensajeEmergente == null)
            {
                Debug.LogError("mensajeEmergente no se encontró en la escena.");
            }
        }

        if (mensajeEmergenteText == null)
        {
            mensajeEmergenteText = GameObject.Find("TextoMensajeEmergente")?.GetComponent<TMP_Text>();
            if (mensajeEmergenteText == null)
            {
                Debug.LogError("mensajeEmergenteText no se encontró en la escena.");
            }
        }

        // Reasignar jugadores
        jugadores.Clear();
        GameObject[] jugadoresEnEscena = GameObject.FindGameObjectsWithTag("Jugador");
        foreach (var jugador in jugadoresEnEscena)
        {
            jugadores.Add(jugador);
        }
    }

    public void ReiniciarEstado()
    {
        Time.timeScale = 1f;
        puntuacion = 0;
        vidas = 1;
        juegoTerminado = false;

        if (pantallaFinJuego != null)
        {
            pantallaFinJuego.SetActive(false);
        }

        if (mensajeEmergente != null)
        {
            mensajeEmergente.SetActive(false);
        }

        ActualizarPuntuacionUI();
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
        if (juegoTerminado) return;
        juegoTerminado = true;
        Debug.Log("Fin del juego llamado");
        if (pantallaFinJuego != null)
        {
            pantallaFinJuego.SetActive(true);
        }
        else
        {
            Debug.LogError("pantallaFinJuego no está asignado en el GameManager.");
        }
        if (puntuacionFinalText != null)
        {
            puntuacionFinalText.text = "Felicidades quedaste al ultimo tu Puntuación final: " + puntuacion;
        }
        Time.timeScale = 0f;
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
        Time.timeScale = 1f;
        SceneManager.LoadScene("MenuInicial");
    }

    public IEnumerator MostrarCuentaRegresiva()
    {
        if (cuentaRegresivaText == null)
        {
            cuentaRegresivaText = GameObject.Find("CuentaRegresivaText")?.GetComponent<TMP_Text>();
            if (cuentaRegresivaText == null)
            {
                Debug.LogError("cuentaRegresivaText no se encontró en la escena.");
                yield break;
            }
        }
        cuentaRegresivaText.gameObject.SetActive(true);
        for (int i = 3; i > 0; i--)
        {
            cuentaRegresivaText.text = i.ToString();
            yield return new WaitForSeconds(1f);
        }
        cuentaRegresivaText.text = "¡YA!";
        yield return new WaitForSeconds(1f);
        cuentaRegresivaText.gameObject.SetActive(false);
        juegoIniciado = true;
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
            if (jugador.activeSelf)
            {
                jugadoresActivos++;
                ultimoJugador = jugador;
            }
        }
        if (jugadoresActivos == 1)
        {
            juegoTerminado = true;
            StartCoroutine(MostrarMensajeGanador(ultimoJugador.name));
        }
    }

    private IEnumerator MostrarMensajeGanador(string nombreJugador)
    {
        if (mensajeEmergente != null && mensajeEmergenteText != null)
        {
            mensajeEmergenteText.text = $"El jugador ganador es: {nombreJugador}";
            mensajeEmergente.SetActive(true);
            yield return new WaitForSeconds(3f); // Mostrar el mensaje durante 3 segundos
            mensajeEmergente.SetActive(false);
        }
        FinDelJuego(); // Mostrar pantalla de fin tras el mensaje emergente
    }

    public void MostrarMensajeFin(string mensaje)
    {
        if (mensajeFinJuegoText == null)
        {
            mensajeFinJuegoText = GameObject.Find("MensajeFinJuegoText")?.GetComponent<TMP_Text>();
            if (mensajeFinJuegoText == null)
            {
                Debug.LogError("mensajeFinJuegoText no se encontró en la escena.");
                return;
            }
        }
        mensajeFinJuegoText.gameObject.SetActive(true);
        mensajeFinJuegoText.text = mensaje;
    }
}