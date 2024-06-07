using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameHandler : MonoBehaviour
{
    public bool[] check_points;
    public FinishLine finishline;

    // Start is called before the first frame update
    void Start()
    {
        check_points = new bool[4];

        for (int i = 0; i < check_points.Length; ++i ) {
            check_points[i] = false;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (AllCheckPointsComplete()){
            ReachedFinish();
        }
    }

    private bool AllCheckPointsComplete() {
       
        for (int i = 0; i < check_points.Length; ++i ) {
            
            if (check_points[i] == false ) {
                return false;
             }
        }
    return true;
    }

    void ReachedFinish(){
        //Debug.Log("Yippie");
        finishline.canFinish = true;
    }
}
