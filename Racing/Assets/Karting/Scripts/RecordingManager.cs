using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public struct GhostTransform
{
    public Vector3 position;
    public Quaternion rotation;

    public GhostTransform(Transform transform){
        this.position = transform.position;
        this.rotation = transform.rotation;
    }
}

public class RecordingManager : MonoBehaviour
{
    public Transform kart;
    //public Transform ghostKart;

    public Transform kart2;
    //public Transform ghostKart2;

    public bool recording = true;
    public bool stopFem = false;
    public bool safeInd= false;




    //public bool playing = false;

    // private List<GhostTransform> recordedGhostTransforms = new List<GhostTransform>();
    // private GhostTransform lastRecordedGhostTransform;

    // private List<GhostTransform> recordedGhostTransforms2 = new List<GhostTransform>();
    // private GhostTransform lastRecordedGhostTransform2;

    public DontDestroy dontdestroy;
    // Start is called before the first frame update
    void Start()
    {
        dontdestroy = GameObject.Find("DontDestroy").GetComponent<DontDestroy>();
        
    }

    // Update is called once per frame
    void Update()
    {
        if (recording == true){
            if(kart.position != dontdestroy.lastRecordedGhostTransform.position || kart.rotation != dontdestroy.lastRecordedGhostTransform.rotation){

                var newGhostTransform = new GhostTransform(kart);
                dontdestroy.recordedGhostTransforms.Add(newGhostTransform);

                dontdestroy.lastRecordedGhostTransform = newGhostTransform;

            }
         
            if(kart2.position != dontdestroy.lastRecordedGhostTransform2.position || kart2.rotation != dontdestroy.lastRecordedGhostTransform2.rotation){
             
                var newGhostTransform2 = new GhostTransform(kart2);
                dontdestroy.recordedGhostTransforms2.Add(newGhostTransform2);

                dontdestroy.lastRecordedGhostTransform2 = newGhostTransform2;
                dontdestroy.groundPositions.Add(safeInd);
                if (safeInd){
                    safeInd = false;
                    //dontdestroy.groundPositions.Add(dontdestroy.recordedGhostTransforms.Count);
                }

            }
            
        }

        // if (playing){
        //     Play();
        // }
    }

    // public void addIndexToRecording(int index){
    //     dontdestroy.groundPositions.Add(index);
    // }

    // void Play(){

    //     ghostKart.gameObject.SetActive(true);
    //     ghostKart2.gameObject.SetActive(true);
    //     StartCoroutine(StartGhost());
    //     StartCoroutine(StartGhost2());
    //     playing = false;
    // }

    // IEnumerator StartGhost()
    // {
    //     for (int i=0; i<dontdestroy.recordedGhostTransforms.Count; i++){
    //         ghostKart.position = dontdestroy.recordedGhostTransforms[i].position;
    //         ghostKart.rotation = dontdestroy.recordedGhostTransforms[i].rotation;

    //         yield return new WaitForFixedUpdate();
    //     }


    // }

    // IEnumerator StartGhost2()
    // {
    //     for (int i=0; i<dontdestroy.recordedGhostTransforms2.Count; i++){
    //         ghostKart2.position = dontdestroy.recordedGhostTransforms2[i].position;
    //         ghostKart2.rotation = dontdestroy.recordedGhostTransforms2[i].rotation;

    //         yield return new WaitForFixedUpdate();
    //     }


    // }
}
