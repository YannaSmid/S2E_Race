using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using KartGame.KartSystems;

public class TriggerFall1 : MonoBehaviour
{
    private bool falling = false;


    public Transform player;

    // for blackout
    private Transform hud;
    private GameObject blackscreen;
    private Image black;
    public float fading_rate = 1.5f;

    // for position
    public Vector3 pos = new Vector3(0f, 0f, 0f);
    public Transform return_pos;
    public Quaternion return_rotation;

    CoinsHandler2 coinshandler;
    timebar bar;

    // For displaying the message
    [Tooltip("The text that will be displayed")]
    [TextArea]
    public string message;

    [Tooltip("Prefab for the message")]
    public PoolObjectDef messagePrefab;

    private bool displayed;

    GameObject mainCamera;

    private RecordingPlay recPlayer;

    // Reference to the current message instance
    private GameObject currentMessageInstance;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("KartClassic_Player2").transform;
        mainCamera = GameObject.Find("Player2Camera");
        hud = GameObject.Find("HUD2").transform;
        pos = this.transform.position;
        return_pos = this.transform.Find("return");
        return_rotation = return_pos.transform.rotation;
        coinshandler = GameObject.Find("CoinsHandler2").GetComponent<CoinsHandler2>();
        bar = hud.Find("BarHolder").GetComponent<timebar>();
        recPlayer = GameObject.Find("RecordingPlayer").GetComponent<RecordingPlay>();
    }

    // Update is called once per frame
    void Update()
    {
        if (falling)
        {
            // Handling for blackout
            if (black.color.a < 1)
            {
                black.color += new Color(0f, 0f, 0f, fading_rate * Time.deltaTime);
            }
            else
            {
                falling = false;
            }
        }

        // Check if stopMessage is true and if there is a current message instance
        else if (recPlayer.stopMessage && currentMessageInstance != null)
        {
            recPlayer.stopMessage = false;
            black.color = new Color(0f, 0f, 0f, 0);
            Debug.Log("Screen back to normal");
            StartCoroutine(ForceHideMessage(currentMessageInstance));
            currentMessageInstance = null;  // Reset the reference after handling
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!falling)
        {
            //Debug.Log("I'm FALLING again, I'm FALLING again, I'm FAAAALLLLLIIINGGGGG");
            // Disable the player movement and handle coin loss
            coinshandler.totalMoney -= 100;
            coinshandler.totalLost += 100;
            coinshandler.updateMoney = true;
    
            Display(); // Show popup
            StartCoroutine(DisableKartTemporarily()); // Disable movement temporarily
        }
    }

    void Display()
    {
        falling = true;
        var hud2Canvas = GameObject.Find("HUD2").GetComponent<Canvas>();
        DisplayMessageManager displayMessageManager = hud2Canvas.GetComponent<DisplayMessageManager>();
        black = displayMessageManager.DisplayMessageRect.GetComponent<Image>();
        
        if (displayMessageManager != null)
        {
            var messageInstance = messagePrefab.getObject(true, displayMessageManager.DisplayMessageRect.transform);
            currentMessageInstance = messageInstance;  // Store the instance reference

            messageInstance.transform.localPosition = Vector3.zero;
            messageInstance.transform.localRotation = Quaternion.identity;
            messageInstance.transform.localScale = Vector3.one;

            NotificationToast notification = messageInstance.GetComponent<NotificationToast>();
            if (notification != null)
            {
                notification.Initialize(message);
                displayMessageManager.DisplayMessageRect.UpdateTable(notification.gameObject);
            }
        }
    }

    IEnumerator ForceHideMessage(GameObject messageInstance)
    {
        if (messageInstance != null)
        {
            var canvasGroup = messageInstance.GetComponent<CanvasGroup>();
            if (canvasGroup != null)
            {
                canvasGroup.alpha = 0;  // Forcefully set the transparency to 0
                messageInstance.SetActive(false);  // Deactivate the GameObject
                black.color = new Color(0f, 0f, 0f, 0);
            }
        }
        yield break;  // Immediately exit the coroutine
    }

    IEnumerator DisableKartTemporarily()
    {
        ArcadeKart kart = player.GetComponent<ArcadeKart>();
        if (kart != null)
        {
            Debug.Log("Disabling ArcadeKart script.");
            kart.enabled = false;
            yield return new WaitForSeconds(10);
       
            kart.enabled = true;
            Debug.Log("Enabling ArcadeKart script.");
        }
        else
        {
            Debug.LogError("ArcadeKart script not found on the player object.");
        }
    }
}