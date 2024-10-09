using UnityEngine;

public class PlayerCamera : MonoBehaviour
{
    public Transform target; // El objeto vacío detrás del Shy Guy
    public Transform secondaryTarget; // El jugador frente al Shy Guy
    public Vector3 offset; // Ajusta esto para posicionar la cámara

    void StartFollowing()
    {
        // Posiciona la cámara detrás del objeto vacío
        transform.position = target.position + offset;

        // Calcula el punto medio entre los dos personajes
        Vector3 midpoint = (target.position + secondaryTarget.position) / 2;

        // Asegúrate de que la cámara siempre mire al punto medio
        transform.LookAt(midpoint);
    }
}
