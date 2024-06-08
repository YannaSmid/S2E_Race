using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;


public class CollectCoinFemale : MonoBehaviour
{
    public bool collected = false;
    CoinsHandler2 coinshandler;

    public float rotation_speed = 0.5f;


    // Start is called before the first frame update
    void Start()
    {

        coinshandler = GameObject.Find("CoinsHandler2").GetComponent<CoinsHandler2>();
    }

    // Update is called once per frame
    void Update()
    {
        this.transform.Rotate(0.0f, 1.0f * rotation_speed, 0.0f, Space.Self);
    }

    void OnTriggerEnter(Collider other){

        if (!collected){
            collected = true;
            coinshandler.totalMoney += 100;
            coinshandler.updateMoney = true;
            //counterText.text = coinshandler.totalMoney.ToString();
            Debug.Log(" Collect the coinzzz");
            this.gameObject.SetActive(false);
        }

    }
}
