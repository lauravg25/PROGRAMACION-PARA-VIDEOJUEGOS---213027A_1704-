using UnityEngine;

public class AIController : MonoBehaviour
{
    public GameObject banderaRoja;
    public GameObject banderaBlanca;
    public ShyguyController shyguy;
    private Rigidbody rb;
    private bool haPerdido = false; // Variable para controlar si el AI ha perdido

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.isKinematic = true; // Inicialmente estático
    }

    private void Update()
    {
        
        // Si el AI ha perdido, moverlo hacia abajo continuamente
        if (haPerdido)
        {
            transform.position += Vector3.down * Time.deltaTime; // Mueve el AI hacia abajo lentamente
        }
    }

    public void ReaccionarABandera()
    {
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
        if ((shyguy.banderaRoja.activeSelf && banderaElegida != 0) ||
            (shyguy.banderaBlanca.activeSelf && banderaElegida != 1))
        {
            Debug.Log(gameObject.name + " cayó");

            // Desactivar el Rigidbody y empezar a hundir al personaje
            rb.isKinematic = true;
            rb.useGravity = false;
            haPerdido = true; // Activa el hundimiento
            GameManager.instance.VerificarUltimoJugador();
        }
        else
        {
            Debug.Log(gameObject.name + " sigue en juego.");
        }
    }
}
