using UnityEngine;

public class ShyGuyController : MonoBehaviour
{
    public GameObject banderaIzquierda;
    public GameObject banderaDerecha;
    public bool levantarIzquierda; // Cambiado a public

    void Start()
    {
        InvokeRepeating("CambiarBandera", 2.0f, 2.0f);
    }

    void CambiarBandera()
    {
        levantarIzquierda = !levantarIzquierda;
        banderaIzquierda.SetActive(levantarIzquierda);
        banderaDerecha.SetActive(!levantarIzquierda);
    }
}
