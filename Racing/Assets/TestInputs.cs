using UnityEngine;

public class TestInputs : MonoBehaviour
{
    void Update()
    {
        float p1Horizontal = Input.GetAxis("Player1_Horizontal");
        float p1Vertical = Input.GetAxis("Player1_Vertical");
        bool p1Accelerate = Input.GetButton("Player1_Accelerate");
        bool p1Brake = Input.GetButton("Player1_Brake");

        float p2Horizontal = Input.GetAxis("Player2_Horizontal");
        float p2Vertical = Input.GetAxis("Player2_Vertical");
        bool p2Accelerate = Input.GetButton("Player2_Accelerate");
        bool p2Brake = Input.GetButton("Player2_Brake");

        Debug.Log($"P1 Horiz: {p1Horizontal}, Vert: {p1Vertical}, Accel: {p1Accelerate}, Brake: {p1Brake}");
        Debug.Log($"P2 Horiz: {p2Horizontal}, Vert: {p2Vertical}, Accel: {p2Accelerate}, Brake: {p2Brake}");
    }
}
