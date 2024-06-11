using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;


public class CollectCoinFemale : MonoBehaviour
{
    public bool collected = false;
    CoinsHandler2 coinshandler;

    public float rotation_speed = 0.5f;

    public AudioClip collectSound;
    public float soundVolume = 0.3f;


    // Start is called before the first frame update
    void Start()
    {

        coinshandler = GameObject.Find("CoinsHandler2").GetComponent<CoinsHandler2>();
    }

    // Update is called once per frame
    void Update()
    {
        //this.transform.Rotate(0.0f, 1.0f * rotation_speed, 0.0f, Space.Self);
        this.transform.Rotate(0.0f, 2.0f * rotation_speed * Time.deltaTime * 60, 0.0f, Space.Self);
    }

    void OnTriggerEnter(Collider other){

        if (!collected){
            collected = true;
            coinshandler.totalMoney += 100;
            coinshandler.updateMoney = true;
            //counterText.text = coinshandler.totalMoney.ToString();
            Debug.Log(" Collect the coinzzz");

            GameObject audioObject = new GameObject("CoinSound");
            AudioSource audioSource = audioObject.AddComponent<AudioSource>();
            audioSource.clip = collectSound;
            audioSource.volume = soundVolume;
            audioSource.Play();
            Destroy(audioObject, collectSound.length);

            this.gameObject.SetActive(false);
        }

    }
}
