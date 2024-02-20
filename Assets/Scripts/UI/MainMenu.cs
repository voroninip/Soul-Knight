using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
  public void PlayGame()
  {
    PlayerPrefs.SetInt("health", 0); // пример значения
    PlayerPrefs.SetInt("damage", 0); // пример значения
    PlayerPrefs.SetInt("ups", 0); // пример значения
    PlayerPrefs.SetInt("energy", 0); // пример значения
    SceneManager.LoadSceneAsync(1);
  }

  public void QuitGame()
  {
    Application.Quit();
  }
}
