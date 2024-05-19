using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinishPoints : MonoBehaviour
{
    public GameHandler gameHandler;
    public int point_nr;
    // Start is called before the first frame update
    void Start()
    {
        gameHandler = GameObject.Find("GameManager").GetComponent<GameHandler>();
        point_nr = int.Parse(this.gameObject.name);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other){
        gameHandler.check_points[point_nr] = true;
    }
}
