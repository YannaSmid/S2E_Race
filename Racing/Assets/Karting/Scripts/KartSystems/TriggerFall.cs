using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using KartGame.KartSystems;

public class TriggerFall : MonoBehaviour
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
    

    // For displaying the message
    //PickupObject message;

    [Tooltip("The text that will be displayed")]
    [TextArea]
    public string message;

    [Tooltip("Prefab for the message")]
    public PoolObjectDef messagePrefab;

    [Tooltip("Delay before hiding the message")]
    public float displayDuration = 10f;

    private bool displayed;

    GameObject mainCamera;

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

    }

    // Update is called once per frame
    void Update()
    {
        if (falling){
            // black screen
            if (black.color.a < 1){
                black.color += new Color(0f, 0f, 0f, fading_rate * Time.deltaTime);

            }

            // still determine the time of the blackout in regards to the popup
            else {
                player.position = return_pos.position;
                player.rotation = return_rotation;
                //
                falling = false;
                //player.gameObject.GetComponent<ArcadeKart>().SetCanMove(true);
            }
            // controls off
            // show popup
            // call function that shows popup
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!falling){
            Debug.Log("I'm FALLING again, I'm FALLING again, I'm FAAAALLLLLIIINGGGGG");
            
            //player.gameObject.GetComponent<ArcadeKart>().SetCanMove(false);
            // Lose money
            coinshandler.totalMoney -= 100;
            coinshandler.totalLost += 100;
            coinshandler.updateMoney = true;
    
            Display(); // Show popup
            StartCoroutine(DisableKartTemporarily()); // Controls off
        }

    }

    void Display()
    {
        falling = true;
        // Retrieve the correct HUD (HUD2) based on the player
        var hud2Canvas = GameObject.Find("HUD2").GetComponent<Canvas>(); // Ensure HUD2 is the targeted Canvas
        DisplayMessageManager displayMessageManager = hud2Canvas.GetComponent<DisplayMessageManager>();
        black = displayMessageManager.DisplayMessageRect.GetComponent<Image>();
        

        if (displayMessageManager != null)
        {
            var messageInstance = messagePrefab.getObject(true, displayMessageManager.DisplayMessageRect.transform);
            messageInstance.transform.localPosition = Vector3.zero; // Centers the message
            messageInstance.transform.localRotation = Quaternion.identity; // Resets rotation
            messageInstance.transform.localScale = Vector3.one; // Ensures scale is not altered

            NotificationToast notification = messageInstance.GetComponent<NotificationToast>();
            if (notification != null)
            {
                notification.Initialize(message);
                displayMessageManager.DisplayMessageRect.UpdateTable(notification.gameObject);
                StartCoroutine(ReturnMessageWithDelay(notification.gameObject, notification.TotalRunTime));
             
            }
        }
    }


    IEnumerator ReturnMessageWithDelay(GameObject messageInstance, float delay)
    {
        yield return new WaitForSeconds(delay);
        messagePrefab.ReturnWithDelay(messageInstance, 0f);
    }

    IEnumerator DisableKartTemporarily()
    {
        ArcadeKart kart = player.GetComponent<ArcadeKart>();
        if (kart != null)
        {
            Debug.Log("Disabling ArcadeKart script.");
            kart.enabled = false;  // Disable the ArcadeKart script --> disable movement
            yield return new WaitForSeconds(10);  // Wait for 10 seconds
       
            black.color = new Color(0f, 0f, 0f, 0f);
            kart.enabled = true;  // Re-enable the ArcadeKart script

            Debug.Log("Enabling ArcadeKart script.");
        }
        else
        {
            Debug.LogError("ArcadeKart script not found on the player object.");
        }
    }
}
