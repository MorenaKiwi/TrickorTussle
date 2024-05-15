using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneController : MonoBehaviour
{
    // Method to load the main game scene
    public void StartGame()
    {
        SceneManager.LoadScene("MainCityScene"); // Replace "MainScene" with the actual name of your main game scene
    }

    // Method to show instructions or help (you can replace this with the actual scene name)
    public void ShowInstructions()
    {
        SceneManager.LoadScene("Help"); // Replace "InstructionsScene" with the actual name of your instructions scene
    }

    // Method to restart the game
    public void RestartGame()
    {
        SceneManager.LoadScene("Menu"); // Replace "MainScene" with the actual name of your main game scene
    }
}
