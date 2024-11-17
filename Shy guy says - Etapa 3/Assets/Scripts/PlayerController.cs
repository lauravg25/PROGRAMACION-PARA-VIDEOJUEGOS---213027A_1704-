using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public GameObject banderaRoja;
    public GameObject banderaBlanca;
    public ShyguyController shyguy; // Referencia al script ShyguyController
    private Rigidbody rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        if (GameManager.instance.juegoTerminado) return; // Detener inputs si el juego ha terminado

        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            MostrarBandera(banderaRoja); // Flecha derecha muestra la bandera roja
        }
        else if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            MostrarBandera(banderaBlanca); // Flecha izquierda muestra la bandera blanca
        }
    }




    void MostrarBandera(GameObject bandera)
    {
        banderaRoja.SetActive(false);
        banderaBlanca.SetActive(false);
        bandera.SetActive(true);

        if ((shyguy.banderaRoja.activeSelf && bandera != banderaRoja) ||
            (shyguy.banderaBlanca.activeSelf && bandera != banderaBlanca))
        {
            Debug.Log("Perdiste");
            rb.isKinematic = false;
            rb.useGravity = true;
            rb.AddForce(Vector3.down * 5f, ForceMode.Impulse);

            GameManager.instance.PerderVida(); // Llamar al GameManager para manejar la pérdida de vida
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
