using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement; // Necesario para reiniciar la escena

public class JugadorController : MonoBehaviour
{
    public GameObject banderaIzquierda;
    public GameObject banderaDerecha;
    public GameObject banderaJugadorIzquierda;
    public GameObject banderaJugadorDerecha;
    public GameObject shyGuy;
    private ShyGuyController shyGuyController;
    private Rigidbody rb;

    void Start()
    {
        shyGuyController = shyGuy.GetComponent<ShyGuyController>();
        rb = GetComponent<Rigidbody>();

        if (rb == null)
        {
            Debug.LogError("No se encontró un componente Rigidbody en el objeto Jugador.");
            return;
        }

        rb.useGravity = false; // Desactivar la gravedad al inicio
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            banderaJugadorIzquierda.SetActive(true);
            banderaJugadorDerecha.SetActive(false);
            VerificarRespuesta(true);
        }
        else if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            banderaJugadorIzquierda.SetActive(false);
            banderaJugadorDerecha.SetActive(true);
            VerificarRespuesta(false);
        }
    }

    void VerificarRespuesta(bool esIzquierda)
    {
        if (esIzquierda != shyGuyController.levantarIzquierda)
        {
            // El jugador pierde
            banderaJugadorIzquierda.transform.parent = this.transform;
            banderaJugadorDerecha.transform.parent = this.transform;
            StartCoroutine(CaerLentamente());
            Debug.Log("¡Perdiste!");
        }
    }

    IEnumerator CaerLentamente()
    {
        rb.useGravity = true; // Activar la gravedad cuando el jugador pierde
        float tiempoCaida = 0f;
        while (tiempoCaida < 2f)
        {
            transform.position += Vector3.down * Time.deltaTime * 0.5f; // Ajusta la velocidad según sea necesario
            tiempoCaida += Time.deltaTime;
            yield return null;
        }
        ReiniciarJuego();
    }

    void ReiniciarJuego()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name); // Reinicia la escena actual
    }
}
