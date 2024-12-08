using System.Collections;
using UnityEngine;

public class AIController : MonoBehaviour
{
    public GameObject banderaRoja;
    public GameObject banderaBlanca;
    public ShyguyController shyguy;
    private Rigidbody rb;
    private bool haPerdido = false; // Variable para controlar si el AI ha perdido
    private bool juegoIniciado = false; // Variable para controlar si el juego ha comenzado
    private bool estaCayendo = false; // Variable para controlar si el AI está cayendo
    public string alias = "AI"; // Alias del AI

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.isKinematic = true; // Inicialmente estático
        MostrarAmbasBanderas(); // Mostrar ambas banderas al inicio
        StartCoroutine(EsperarInicioDelJuego()); // Iniciar la corrutina para esperar el inicio del juego
    }

    private void Update()
    {
        // Si el AI ha perdido, moverlo hacia abajo continuamente
        if (haPerdido)
        {
            transform.position += Vector3.down * Time.deltaTime; // Mueve el AI hacia abajo lentamente
        }
    }

    public void MostrarAmbasBanderas()
    {
        banderaRoja.SetActive(true);
        banderaBlanca.SetActive(true);
    }

    private IEnumerator EsperarInicioDelJuego()
    {
        yield return new WaitForSeconds(4f); // Esperar 4 segundos
        juegoIniciado = true; // Marcar que el juego ha comenzado
        ReaccionarABandera(); // Iniciar la primera reacción
    }

    public void ReaccionarABandera()
    {
        if (!juegoIniciado || estaCayendo) return; // No reaccionar si el juego no ha comenzado o si está cayendo

        // Decisión aleatoria del AI
        int banderaElegida = Random.Range(0, 2); // 0 para roja, 1 para blanca
        banderaRoja.SetActive(false);
        banderaBlanca.SetActive(false);
        if (banderaElegida == 0)
        {
            banderaRoja.SetActive(true);
        }
        else
        {
            banderaBlanca.SetActive(true);
        }
        // Comparar la decisión del AI con la bandera de Shyguy
        if ((shyguy.banderaRoja.activeSelf && banderaElegida != 0) || (shyguy.banderaBlanca.activeSelf && banderaElegida != 1))
        {
            Debug.Log(gameObject.name + " cayó");
            // Desactivar el Rigidbody y empezar a hundir al personaje
            rb.isKinematic = true;
            rb.useGravity = false;
            haPerdido = true; // Activa el hundimiento
            estaCayendo = true; // Marcar que está cayendo
            GameManager.instance.jugadoresPerdieron.Add(gameObject); // Agregar a la lista de jugadores que han perdido
            GameManager.instance.VerificarUltimoJugador(); // Verificar si es el último jugador
        }
        else
        {
            Debug.Log(gameObject.name + " sigue en juego.");
        }
    }
}