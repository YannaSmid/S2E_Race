using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    public InputActionAsset inputActions;
    public string actionMapName; // Player1Actions or Player2Actions
    private InputAction moveAction;
    private InputAction turnAction;
    private Rigidbody rb;

    private void OnEnable()
    {
        var playerActions = inputActions.FindActionMap(actionMapName);
        moveAction = playerActions.FindAction("Move");
        turnAction = playerActions.FindAction("Turn");

        moveAction.Enable();
        turnAction.Enable();
    }

    private void OnDisable()
    {
        moveAction.Disable();
        turnAction.Disable();
    }

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        float move = moveAction.ReadValue<float>();
        float turn = turnAction.ReadValue<float>();

        Vector3 movement = transform.forward * move * Time.deltaTime;
        transform.Rotate(Vector3.up, turn * Time.deltaTime);
        rb.MovePosition(rb.position + movement);
    }
}
