using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DontDestroy : MonoBehaviour
{
   //dontdestroy specific variable
    public static DontDestroy instance;

    public List<GhostTransform> recordedGhostTransforms = new List<GhostTransform>();
    public GhostTransform lastRecordedGhostTransform;

    public List<GhostTransform> recordedGhostTransforms2 = new List<GhostTransform>();
    public GhostTransform lastRecordedGhostTransform2;
    
    void Start(){

        // PlayerPosition = new Vector3(-3.73f, -6.24f, 20.36f);

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