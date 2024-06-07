using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CoinsHandler2 : MonoBehaviour
{
    public int totalMoney = 0;
    public bool updateMoney = false;

    private Transform coinsinfo;
    public TMP_Text counterText;

    // Start is called before the first frame update
    void Start()
    {
        coinsinfo = GameObject.Find("HUD2").transform.Find("CoinsInfo");
        counterText = coinsinfo.Find("Text").gameObject.GetComponent<TMP_Text>();

    }

    // Update is called once per frame
    void Update()
    {
        if (updateMoney){
            if (totalMoney <= 0){
                counterText.text = "000";
                totalMoney = 0;
            }
            else {
                counterText.text = totalMoney.ToString();
                updateMoney = false;
            }
        }
    }
}
