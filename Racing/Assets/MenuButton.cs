using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuButtonHandler : MonoBehaviour
{
    void Update()
    {
        if (Input.GetButtonDown("GameMenu"))
        {
            LoadMenuScene();
        }
    }

    void LoadMenuScene()
    {
        SceneManager.LoadScene("Menu"); // Make sure "MenuScene" matches the exact name of your scene
    }
}
