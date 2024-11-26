using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuInicial : MonoBehaviour
{
    public void Jugar()
    {
        // Reiniciar el estado del GameManager si existe
        if (GameManager.instance != null)
        {
            GameManager.instance.ReiniciarEstado(); // Reiniciar el estado antes de comenzar
        }

        // Cargar la escena del juego
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }


    public void Salir()
    {
        Debug.Log("Salir...");
        Application.Quit();
    }
}
