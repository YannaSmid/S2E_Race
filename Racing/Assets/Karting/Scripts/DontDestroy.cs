using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DontDestroy : MonoBehaviour
{
   //dontdestroy specific variable
    public static DontDestroy instance;

    public List<GhostTransform> recordedGhostTransforms = new List<GhostTransform>();
    public GhostTransform lastRecordedGhostTransform;

    public List<GhostTransform> recordedGhostTransforms2 = new List<GhostTransform>();
    public GhostTransform lastRecordedGhostTransform2;

    //public List<int> groundPositions = new List<int>();
    public List<bool> groundPositions = new List<bool>();
    
    void Start(){

        // if (SceneManager.GetActiveScene().name == "MainScene"){
        //     recordedGhostTransforms = new List<GhostTransform>();
        //     recordedGhostTransforms2.Clear();
        //     groundPositions.Clear();
        // }
        
        if (instance != null){
            Destroy(gameObject);
        }
        else {
            instance = this;
        }
        
    }

    // Update is called once per frame
    void Awake(){
        
        DontDestroyOnLoad(this.gameObject);
        
    }
}