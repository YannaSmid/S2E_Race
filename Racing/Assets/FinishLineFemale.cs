using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FinishLineFemale : MonoBehaviour
{
    public bool canFinish = false;
    private bool finished = false;

    private Canvas hud2Canvas;
    private DisplayMessageManager hud2MessageManager;
    public Image endcolor; // color of the display message rectangle
    public float fading_rate = 1.5f;


    // For the end message
    private string message;

    [Tooltip("Prefab for the message")]
    public PoolObjectDef messagePrefab;

    public CoinsHandler2 coinshandler;
    int money;
    int moneylost;

    // Start is called before the first frame update
    void Start()
    {
        // Retrieve the correct HUD (HUD2) based on the player
        hud2Canvas = GameObject.Find("HUD2").GetComponent<Canvas>(); // Ensure HUD2 is the targeted Canvas
        hud2MessageManager = hud2Canvas.GetComponent<DisplayMessageManager>();
    }

    // Update is called once per frame
    void Update()
    {
        // if (finished){
        //      if (endcolor.color.a < 1){

        //         endcolor.color += new Color(0f, 0f, 0f, fading_rate * Time.deltaTime);

        //     }
        // }
    }

    private void OnTriggerEnter(Collider other){
        if (canFinish){
           
            Debug.Log("Player reached Finish");
            finished = true;
            money = coinshandler.totalMoney;
            moneylost = coinshandler.totalLost;
            message = $"Well done! Despite the gaps in the road, you managed to reach the finishline. In total, you have been granted {money} coins with your research, which also involves women! But lost a total of {moneylost} coins by the complications it brings to collect data on women.";
            DisplayEndMessage(); // Show popup
            Debug.Log("Display Message");
            //StartCoroutine(DisableKartTemporarily()); // Controls off

        }

    }

    private void DisplayEndMessage(){
         
        if (hud2MessageManager != null)
        {
            var messageInstance = messagePrefab.getObject(true, hud2MessageManager.DisplayMessageRect.transform);
            messageInstance.transform.localPosition = Vector3.zero; // Centers the message
            messageInstance.transform.localRotation = Quaternion.identity; // Resets rotation
            messageInstance.transform.localScale = Vector3.one; // Ensures scale is not altered

            NotificationToast notification = messageInstance.GetComponent<NotificationToast>();
            if (notification != null)
            {
                notification.Initialize(message);
                hud2MessageManager.DisplayMessageRect.UpdateTable(notification.gameObject);
                StartCoroutine(ReturnMessageWithDelay(notification.gameObject, notification.TotalRunTime));
                
            }
        }
    }

     IEnumerator ReturnMessageWithDelay(GameObject messageInstance, float delay)
    {
        yield return new WaitForSeconds(delay);
        messagePrefab.ReturnWithDelay(messageInstance, 0f);
    }
}