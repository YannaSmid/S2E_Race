using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class timebar : MonoBehaviour
{

    public GameObject loadingbar;
    public Image bar_line;

    public float time_remainer;
    public float max_time = 10.0f;

    public bool startcount = false;

    void Start()
    {
        bar_line = loadingbar.GetComponent<Image>();

        time_remainer = max_time;
    }


    // Update is called once per frame
    void Update()
    {
        if (startcount){
            loadingbar.SetActive(true);
            if (time_remainer > 0){
                time_remainer -= Time.deltaTime;
                bar_line.fillAmount = time_remainer / max_time;
            }
            else {
                loadingbar.SetActive(false);
                startcount = false;
                time_remainer = max_time;
            }
         
        }
    }
}
