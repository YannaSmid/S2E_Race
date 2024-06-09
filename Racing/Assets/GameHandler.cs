using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameHandler : MonoBehaviour
{
    public bool[] check_points;
    public FinishLine finishline;
    public FinishLineFemale finishline2;

    // Start is called before the first frame update
    void Start()
    {
        check_points = new bool[5];

        for (int i = 0; i < check_points.Length; ++i ) {
            check_points[i] = false;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (MaleAllCheckPointsComplete()){
            MaleReachedFinish();
        }
        if (FemaleAllCheckPointsComplete()){
            FemaleReachedFinish();
        }
        if (finishline.finished && finishline2.finished){
            StartCoroutine(GoToRecording(10));
        }
    }

    private bool MaleAllCheckPointsComplete() {
       
       //Male track checkpoints
        for (int i = 0; i < check_points.Length - 1; ++i ) {
            
            if (check_points[i] == false ) {
                return false;
             }
        }
    return true;
    }

    private bool FemaleAllCheckPointsComplete() {
       
       //female track checkpoints
        if (check_points[4] == false ) {
            return false;
        }
    
    return true;
    }

    void MaleReachedFinish(){
        //Debug.Log("Yippie");
        finishline.canFinish = true;
    }

    void FemaleReachedFinish(){
        //Debug.Log("Yippie");
        finishline2.canFinish = true;
    }

    IEnumerator GoToRecording(float delay)
    {
        yield return new WaitForSeconds(delay);
        SceneManager.LoadScene("RecordingScene");
        
    }
}
