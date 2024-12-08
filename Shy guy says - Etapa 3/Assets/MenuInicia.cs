using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuInicial : MonoBehaviour
{
    public void Jugar()
    {
        SceneManager.LoadScene("Escena final"); // Cambia a la escena del juego
    }

    public void Salir()
    {
        Debug.Log("Salir...");
        Application.Quit();
    }
}