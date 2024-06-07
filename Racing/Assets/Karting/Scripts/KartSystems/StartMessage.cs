using System.Collections;
using UnityEngine;

public class StartMessage : MonoBehaviour
{
    [Tooltip("The text that will be displayed")]
    [TextArea]
    public string message;

    [Tooltip("Prefab for the message")]
    public PoolObjectDef messagePrefab;

    // Start is called before the first frame update
    void Start()
    {
        Display();
    }

    void Display()
    {
        var hudCanvas = GameObject.Find("HUD").GetComponent<Canvas>();
        DisplayMessageManager displayMessageManager = hudCanvas.GetComponent<DisplayMessageManager>();

        if (displayMessageManager != null)
        {
            var messageInstance = messagePrefab.getObject(true, displayMessageManager.DisplayMessageRect.transform);
            NotificationToast notification = messageInstance.GetComponent<NotificationToast>();
            if (notification != null)
            {
                notification.Initialize(message);  // Initialization with default times from NotificationToast
                displayMessageManager.DisplayMessageRect.UpdateTable(notification.gameObject);
                StartCoroutine(ForceHideMessage(notification.gameObject));
            }
        }
    }

    IEnumerator ForceHideMessage(GameObject messageInstance)
    {
        // Wait for 3 seconds total display time
        yield return new WaitForSeconds(3);
        if (messageInstance != null)
        {
            var canvasGroup = messageInstance.GetComponent<CanvasGroup>();
            if (canvasGroup != null)
            {
                canvasGroup.alpha = 0; // Forcefully set the transparency to 0
                Destroy(messageInstance); // Destroy if not using pooling
            }
        }
    }
}
