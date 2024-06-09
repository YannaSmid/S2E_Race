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
        hud1Canvas = GameObject.Find("HUD1").GetComponent<Canvas>();
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
        if (canFinish && other.CompareTag("Player"))
        {
            Debug.Log("Player reached Finish");
            finished = true;
            Rigidbody playerRigidbody = player.GetComponent<Rigidbody>();
            if (playerRigidbody != null)
            {
                StartCoroutine(FreezeDelay(playerRigidbody));
            }
            money = coinshandler.totalMoney;
            message = $"Congratulations! You have made it to the finish line and earned a total of {money} coins by doing your research!";
            DisplayEndMessage();
            Debug.Log("Display Message");

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
            messageInstance.transform.localPosition = Vector3.zero;
            messageInstance.transform.localRotation = Quaternion.identity;
            messageInstance.transform.localScale = Vector3.one;

            NotificationToast notification = messageInstance.GetComponent<NotificationToast>();
            if (notification != null)
            {
                notification.Initialize(message);
                hud1MessageManager.DisplayMessageRect.UpdateTable(notification.gameObject);
                StartCoroutine(DisplayMesLoop(notification, messageInstance));
            }
        }
    }

    IEnumerator DisplayMesLoop(NotificationToast notification, GameObject messageInstance)
    {
        while (true)
        {
            yield return new WaitForSeconds(notification.TotalRunTime - 0.1f); // Slightly before it fades out
            notification.Initialize(message); // Re-initialize to reset fade timers
        }
    }

    IEnumerator ReturnMessageWithDelay(GameObject messageInstance, float delay)
    {
        yield return new WaitForSeconds(delay);
        messagePrefab.ReturnWithDelay(messageInstance, 0f);
    }

    IEnumerator FreezeDelay(Rigidbody playerRigidbody)
    {
        yield return new WaitForSeconds(0.47f);
        playerRigidbody.constraints = RigidbodyConstraints.FreezePosition;
    }
}