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
    public Vector3 return_rotation;

    // For displaying the message
    //PickupObject message;
    
    GameObject mainCamera;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("KartClassic_Player2").transform;

        mainCamera = GameObject.Find("Player2Camera");
        hud = GameObject.Find("HUD2").transform;

        pos = this.transform.position;
        
        return_pos = this.transform.Find("return");
        return_rotation = new Vector3(0f, 0f, 0f);

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
                player.rotation = Quaternion.Euler(return_rotation);
                black.color = new Color(0f, 0f, 0f, 0f);
                falling = false;
                player.gameObject.GetComponent<ArcadeKart>().SetCanMove(true);
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
            blackscreen = hud.Find("BlackOut").gameObject;
            player.gameObject.GetComponent<ArcadeKart>().SetCanMove(false);
            blackscreen.SetActive(true);
            black = blackscreen.GetComponent<Image>();
            falling = true;
            
        }

    }
}
