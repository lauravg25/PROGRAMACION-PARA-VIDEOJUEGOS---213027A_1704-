using UnityEngine;

public class JugadorController : MonoBehaviour
{
    public GameObject banderaIzquierda;
    public GameObject banderaDerecha;
    public GameObject banderaJugadorIzquierda;
    public GameObject banderaJugadorDerecha;
    public GameObject shyGuy;
    private ShyGuyController shyGuyController;

    void Start()
    {
        shyGuyController = shyGuy.GetComponent<ShyGuyController>();
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
            transform.position = new Vector3(0, -10, -5); // Mueve el cubo del jugador al fondo de la escena
            Debug.Log("¡Perdiste!");
        }
    }
}
