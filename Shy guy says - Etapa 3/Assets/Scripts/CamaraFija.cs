using UnityEngine;

public class MainCamera : MonoBehaviour
{
    private Vector3 posicionInicial;
    private Quaternion rotacionInicial;

    void Initialize()
    {
        posicionInicial = transform.position;
        rotacionInicial = transform.rotation;
    }

    void UpdateCamera()
    {
        transform.position = posicionInicial;
        transform.rotation = rotacionInicial;
    }
}
