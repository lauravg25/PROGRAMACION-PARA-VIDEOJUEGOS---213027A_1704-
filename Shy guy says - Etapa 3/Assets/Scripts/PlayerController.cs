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
        // Invertir la lógica de las teclas
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            MostrarBandera(banderaBlanca); // Ahora la flecha izquierda muestra la bandera blanca
        }
        else if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            MostrarBandera(banderaRoja); // Ahora la flecha derecha muestra la bandera roja
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

            // Activar la gravedad y desactivar isKinematic para que el personaje caiga
            rb.isKinematic = false;
            rb.useGravity = true;
            rb.AddForce(Vector3.down * 5f, ForceMode.Impulse); // Agregar fuerza para iniciar la caída

            // Reiniciar el juego después de 2 segundos
            Invoke("ReiniciarJuego", 2f);
        }
    }


    void ReiniciarJuego()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(
            UnityEngine.SceneManagement.SceneManager.GetActiveScene().name);
    }
}
