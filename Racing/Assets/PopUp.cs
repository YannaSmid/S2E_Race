using UnityEngine;
using System.Collections;

public class PopUp : MonoBehaviour
{
    [Tooltip("The text that will be displayed")]
    [TextArea]
    public string message;

    [Tooltip("Prefab for the message")]
    public PoolObjectDef messagePrefab;

    [Tooltip("Delay before hiding the message")]
    public float displayDuration = 10f;

    private bool displayed;

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && !displayed)
        {
            Display();
        }
    }

    void Display()
    {
        // Assume DisplayMessageManager handles the pooling and positioning of the message prefab
        DisplayMessageManager displayMessageManager = FindObjectOfType<DisplayMessageManager>();

        if (displayMessageManager != null)
        {
            var messageInstance = messagePrefab.getObject(true, displayMessageManager.DisplayMessageRect.transform);
            NotificationToast notification = messageInstance.GetComponent<NotificationToast>();

            if (notification != null)
            {
                notification.Initialize(message);
                displayMessageManager.DisplayMessageRect.UpdateTable(notification.gameObject);
                StartCoroutine(ReturnMessageWithDelay(notification.gameObject, notification.TotalRunTime));
            }
        }

        displayed = true;
    }

    IEnumerator ReturnMessageWithDelay(GameObject messageInstance, float delay)
    {
        yield return new WaitForSeconds(delay);
        messagePrefab.ReturnWithDelay(messageInstance, 0f); // Adjust the return logic as per your pooling system
    }
}
