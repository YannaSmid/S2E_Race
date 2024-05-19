using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FinishLine : MonoBehaviour
{
    public bool canFinish = false;
    private bool finished = false;

    private GameObject hud1;
    private Image green;
    public float fading_rate = 1.5f;

    // Start is called before the first frame update
    void Start()
    {
        hud1 = GameObject.Find("HUD1").transform.Find("Finish").gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        if (finished){
             if (green.color.a < 1){

                green.color += new Color(0f, 0f, 0f, fading_rate * Time.deltaTime);

            }
        }
    }

    private void OnTriggerEnter(Collider other){
        if (canFinish){
            // race is starting now
            Debug.Log("Player reached Finish");
            green = hud1.transform.GetComponent<Image>();
            finished = true;

        }

    }
}
