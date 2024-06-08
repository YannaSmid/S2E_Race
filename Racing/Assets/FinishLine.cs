using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using KartGame.KartSystems;

public class FinishLine : MonoBehaviour
{
    public bool canFinish = false;
    private bool finished = false;

    private GameObject hud1Finish;
    private Canvas hud1Canvas;
    private DisplayMessageManager hud1MessageManager;
    public Image green; // green color of the display message rectangle
    public float fading_rate = 1.5f;
    public Transform player;


    // For the end message
    private string message;

    [Tooltip("Prefab for the message")]
    public PoolObjectDef messagePrefab;

    public CoinsHandler coinshandler;
    int money;

    // Start is called before the first frame update
    void Start()
    {
        hud1Finish = GameObject.Find("HUD1").transform.Find("Finish").gameObject;
        // Retrieve the correct HUD (HUD1) based on the player
        hud1Canvas = GameObject.Find("HUD1").GetComponent<Canvas>(); // Ensure HUD1 is the targeted Canvas
        hud1MessageManager = hud1Canvas.GetComponent<DisplayMessageManager>();
        player = GameObject.Find("KartClassic_Player").transform;
    }

    // Update is called once per frame
    void Update()
    {
        if (finished)
        {
            if (green.color.a < 1)
            {

                green.color += new Color(0f, 0f, 0f, fading_rate * Time.deltaTime);

            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        ArcadeKart kart = player.GetComponent<ArcadeKart>();
        if (canFinish && other.CompareTag("Player")) // Assuming the player has a tag "Player"
        {
            Debug.Log("Player reached Finish");
            finished = true;
            Rigidbody playerRigidbody = player.GetComponent<Rigidbody>();
            if (playerRigidbody != null)
            {
                StartCoroutine(FreezeDelay(playerRigidbody));  // Start the coroutine
            }
            money = coinshandler.totalMoney;
            message = $"Congratulations! You have made it to the finishline and granted a total of {money} coins by doing your research!";
            DisplayEndMessage(); // Show popup
            Debug.Log("Display Message");
            // Additional code for disabling control can go here

            if (kart != null)
            {
                Debug.Log("Disabling ArcadeKart script.");
                kart.enabled = false;
            }
            else
            {
                Debug.LogError("ArcadeKart script not found on the player object.");
            }
        }
    }


    private void DisplayEndMessage()
    {

        if (hud1MessageManager != null)
        {
            var messageInstance = messagePrefab.getObject(true, hud1MessageManager.DisplayMessageRect.transform);
            messageInstance.transform.localPosition = Vector3.zero; // Centers the message
            messageInstance.transform.localRotation = Quaternion.identity; // Resets rotation
            messageInstance.transform.localScale = Vector3.one; // Ensures scale is not altered

            NotificationToast notification = messageInstance.GetComponent<NotificationToast>();
            if (notification != null)
            {
                notification.Initialize(message);
                hud1MessageManager.DisplayMessageRect.UpdateTable(notification.gameObject);
                StartCoroutine(ReturnMessageWithDelay(notification.gameObject, notification.TotalRunTime));
            }
        }
    }

    IEnumerator ReturnMessageWithDelay(GameObject messageInstance, float delay)
    {
        yield return new WaitForSeconds(delay);
        messagePrefab.ReturnWithDelay(messageInstance, 0f);
    }

    IEnumerator FreezeDelay(Rigidbody playerRigidbody)
    {
        yield return new WaitForSeconds(0.47f);  // Wait
        playerRigidbody.constraints = RigidbodyConstraints.FreezePosition;  // Apply the freeze constraint
    }
}
