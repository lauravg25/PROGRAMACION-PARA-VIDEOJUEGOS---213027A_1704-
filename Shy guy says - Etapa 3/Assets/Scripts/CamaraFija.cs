using UnityEngine;

public class CamaraFija : MonoBehaviour
{
    private Vector3 posicionInicial;
    private Quaternion rotacionInicial;

    void Start()
    {
        posicionInicial = transform.position;
        rotacionInicial = transform.rotation;
    }

    void LateUpdate()
    {
        transform.position = posicionInicial;
        transform.rotation = rotacionInicial;
    }
}
