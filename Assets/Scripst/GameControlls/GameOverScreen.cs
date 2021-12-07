using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class GameOverScreen : MonoBehaviour
{
    public void ResturtButton()
    {
        SceneManager.LoadScene("SampleScene");
    }

    public void MenuButton()
    {
        SceneManager.LoadScene("MainMenu");
    }
}
