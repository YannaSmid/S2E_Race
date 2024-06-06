using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuButton : MonoBehaviour
{
    void Update()
    {
        if (Input.GetButtonDown("MenuButton"))
        {
            LoadMenuScene();
        }
    }

    void LoadMenuScene()
    {
        SceneManager.LoadScene("MenuScene"); // Make sure "MenuScene" matches the exact name of your scene
    }
}
