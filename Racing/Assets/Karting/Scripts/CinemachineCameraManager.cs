using UnityEngine;
using Cinemachine;

public class CinemachineCameraManager : MonoBehaviour
{
    public CinemachineVirtualCamera player1VirtualCamera;
    public CinemachineVirtualCamera player2VirtualCamera;
    public Camera player1Camera;
    public Camera player2Camera;

    void Start()
    {
        // Ensure both virtual cameras are initially inactive
        player1VirtualCamera.Priority = 0;
        player2VirtualCamera.Priority = 0;

        // Activate the correct virtual camera for each player
        ActivatePlayer1Camera();
        ActivatePlayer2Camera();
    }

    void ActivatePlayer1Camera()
    {
        player1VirtualCamera.Priority = 11; // Higher than default priority
        player1Camera.enabled = true;
        Debug.Log("Player 1 Camera Activated: " + player1VirtualCamera.name);
    }

    void ActivatePlayer2Camera()
    {
        player2VirtualCamera.Priority = 11; // Higher than default priority
        player2Camera.enabled = true;
        Debug.Log("Player 2 Camera Activated: " + player2VirtualCamera.name);
    }
}
