using UnityEngine;

public class FullscreenSetter : MonoBehaviour
{
    void Start()
    {
        // Set the game to fullscreen
        Screen.fullScreen = true;
        // Optionally, set the fullscreen mode
        Screen.fullScreenMode = FullScreenMode.ExclusiveFullScreen; // Use ExclusiveFullScreen or FullScreenWindow
    }
}
