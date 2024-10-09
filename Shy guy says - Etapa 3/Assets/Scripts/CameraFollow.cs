using UnityEngine;

public class PlayerCamera : MonoBehaviour
{
    public Transform target; // El objeto vac�o detr�s del Shy Guy
    public Transform secondaryTarget; // El jugador frente al Shy Guy
    public Vector3 offset; // Ajusta esto para posicionar la c�mara

    void StartFollowing()
    {
        // Posiciona la c�mara detr�s del objeto vac�o
        transform.position = target.position + offset;

        // Calcula el punto medio entre los dos personajes
        Vector3 midpoint = (target.position + secondaryTarget.position) / 2;

        // Aseg�rate de que la c�mara siempre mire al punto medio
        transform.LookAt(midpoint);
    }
}
