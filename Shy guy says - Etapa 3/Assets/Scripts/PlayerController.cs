using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public GameObject banderaRoja;
    public GameObject banderaBlanca;
    public ShyguyController shyguy; // Referencia al script ShyguyController
    private Rigidbody rb;
    public string alias = " Tu Jugador"; // Alias del jugador


    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        if (rb == null)
        {
            Debug.LogError("Rigidbody no encontrado en el objeto PlayerController.");
        }
        if (shyguy == null)
        {
            Debug.LogError("Referencia a ShyguyController no asignada en PlayerController.");
        }
        if (banderaRoja == null)
        {
            Debug.LogError("Referencia a banderaRoja no asignada en PlayerController.");
        }
        if (banderaBlanca == null)
        {
            Debug.LogError("Referencia a banderaBlanca no asignada en PlayerController.");
        }
    }

    private void Update()
    {
        if (GameManager.instance == null)
        {
            Debug.LogError("GameManager.instance es nulo en PlayerController.");
            return;
        }

        if (!GameManager.instance.juegoIniciado) return; // Bloquear lógica hasta que el juego inicie
        if (GameManager.instance.juegoTerminado || !GameManager.instance.turnoShyGuy) return;

        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            MostrarBandera(banderaRoja);
        }
        else if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            MostrarBandera(banderaBlanca);
        }
    }

    void MostrarBandera(GameObject bandera)
    {
        if (banderaRoja == null || banderaBlanca == null || shyguy == null)
        {
            Debug.LogError("Una o más referencias no están asignadas en PlayerController.");
            return;
        }
        banderaRoja.SetActive(false);
        banderaBlanca.SetActive(false);
        bandera.SetActive(true);
        if ((shyguy.banderaRoja.activeSelf && bandera != banderaRoja) || (shyguy.banderaBlanca.activeSelf && bandera != banderaBlanca))
        {
            Debug.Log("Perdiste");
            rb.isKinematic = false;
            rb.useGravity = true;
            rb.AddForce(Vector3.down * 5f, ForceMode.Impulse);
            GameManager.instance.PerderVida(); // Llamar al GameManager para manejar la pérdida de vida
            GameManager.instance.jugadoresPerdieron.Add(gameObject); // Agregar a la lista de jugadores que han perdido
            GameManager.instance.VerificarUltimoJugador(); // Verificar si es el último jugador
        }
        else
        {
            Debug.Log("Sigue en juego.");
            GameManager.instance.IncrementarPuntuacion(10); // Aumentar la puntuación si acierta
            shyguy.MostrarBanderaAleatoria();
        }
    }


    void ReiniciarJuego()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(
            UnityEngine.SceneManagement.SceneManager.GetActiveScene().name);
    }
}

