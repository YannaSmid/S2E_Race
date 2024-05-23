using UnityEngine;
using System.Collections;

public class PickupObject : TargetObject
{
    [Header("PickupObject")]

    [Tooltip("New Gameobject (a VFX for example) to spawn when you trigger this PickupObject")]
    public GameObject spawnPrefabOnPickup;

    [Tooltip("Destroy the spawned spawnPrefabOnPickup gameobject after this delay time. Time is in seconds.")]
    public float destroySpawnPrefabDelay = 10;

    [Tooltip("Destroy this gameobject after collectDuration seconds")]
    public float collectDuration = 0f;

    [Tooltip("Prefab for the message")]
    public PoolObjectDef messagePrefab;

    [Tooltip("The text that will be displayed")]
    [TextArea]
    public string message;

    private bool displayed;

    void Start()
    {
        Register();
    }

    public void OnCollect()
    {
        // if (CollectSound)
        // {
        //     //AudioUtility.CreateSFX(CollectSound, transform.position, AudioUtility.AudioGroups.Pickup, 0f);
        // }

        if (spawnPrefabOnPickup)
        {
            var vfx = Instantiate(spawnPrefabOnPickup, CollectVFXSpawnPoint.position, Quaternion.identity);
            Destroy(vfx, destroySpawnPrefabDelay);
        }

        //Objective.OnUnregisterPickup(this);

        //TimeManager.OnAdjustTime(TimeGained);

        if (!displayed)
        {
            DisplayMessage();
        }

        Destroy(gameObject, collectDuration);
    }

    void DisplayMessage()
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

    // void OnTriggerEnter(Collider other)
    // {
    //     if ((layerMask.value & 1 << other.gameObject.layer) > 0 && other.gameObject.CompareTag("Player"))
    //     {
    //         OnCollect();
    //     }
    // }
}
