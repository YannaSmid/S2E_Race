using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RecordingPlay : MonoBehaviour
{

    //public Transform kart;
    public Transform ghostKart;

    //public Transform kart2;
    public Transform ghostKart2;

    //public bool recording = true;
    public bool playing;

    public bool stopMessage = false;

    public DontDestroy dontdestroy;

    // Start is called before the first frame update
    void Start()
    {
        dontdestroy = GameObject.Find("DontDestroy").GetComponent<DontDestroy>();
    }

    // Update is called once per frame
    void Update()
    {
        if (playing){
            Play();
        }
    }

     void Play(){

        // ghostKart.gameObject.SetActive(true);
        // ghostKart2.gameObject.SetActive(true);
        StartCoroutine(StartGhost());
        StartCoroutine(StartGhost2());
        playing = false;
    }

    IEnumerator StartGhost()
    {
        for (int i=0; i<dontdestroy.recordedGhostTransforms.Count; i++){
            ghostKart.position = dontdestroy.recordedGhostTransforms[i].position;
            ghostKart.rotation = dontdestroy.recordedGhostTransforms[i].rotation;

            yield return new WaitForFixedUpdate();
        }


    }

    IEnumerator StartGhost2()
    {
        for (int i=0; i<dontdestroy.recordedGhostTransforms2.Count; i++){

            if (dontdestroy.groundPositions[i]){
                Debug.Log("Back on the ground");
            
                yield return new WaitForSeconds(5);
                stopMessage = true;
            }

            ghostKart2.position = dontdestroy.recordedGhostTransforms2[i].position;
            ghostKart2.rotation = dontdestroy.recordedGhostTransforms2[i].rotation;

            yield return new WaitForFixedUpdate();
        }


    }
}
