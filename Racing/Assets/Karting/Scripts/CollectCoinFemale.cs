using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;


public class CollectCoinFemale : MonoBehaviour
{
    public bool collected = false;
    CoinsHandler2 coinshandler;

    // private Transform coinsinfo;
    // public TMP_Text counterText;

    // Start is called before the first frame update
    void Start()
    {
        // coinsinfo = GameObject.Find("HUD2").transform.Find("CoinsInfo");
        // counterText = coinsinfo.Find("Text").gameObject.GetComponent<TMP_Text>();

        coinshandler = GameObject.Find("CoinsHandler2").GetComponent<CoinsHandler2>();
    }

    // Update is called once per frame
    void Update()
    {
        
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
