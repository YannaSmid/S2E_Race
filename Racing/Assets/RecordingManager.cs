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
    public Transform ghostKart;

    public Transform kart2;
    public Transform ghostKart2;

    public bool recording = true;
    public bool playing = false;

    private List<GhostTransform> recordedGhostTransforms = new List<GhostTransform>();
    private GhostTransform lastRecordedGhostTransform;

    private List<GhostTransform> recordedGhostTransforms2 = new List<GhostTransform>();
    private GhostTransform lastRecordedGhostTransform2;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (recording == true){
            if(kart.position != lastRecordedGhostTransform.position || kart.rotation != lastRecordedGhostTransform.rotation){

                var newGhostTransform = new GhostTransform(kart);
                recordedGhostTransforms.Add(newGhostTransform);

                lastRecordedGhostTransform = newGhostTransform;

            }
            if(kart2.position != lastRecordedGhostTransform2.position || kart2.rotation != lastRecordedGhostTransform2.rotation){

                var newGhostTransform2 = new GhostTransform(kart2);
                recordedGhostTransforms2.Add(newGhostTransform2);

                lastRecordedGhostTransform2 = newGhostTransform2;

            }
        }

        if (playing){
            Play();
        }
    }

    void Play(){

        ghostKart.gameObject.SetActive(true);
        ghostKart2.gameObject.SetActive(true);
        StartCoroutine(StartGhost());
        StartCoroutine(StartGhost2());
        playing = false;
    }

    IEnumerator StartGhost()
    {
        for (int i=0; i<recordedGhostTransforms.Count; i++){
            ghostKart.position = recordedGhostTransforms[i].position;
            ghostKart.rotation = recordedGhostTransforms[i].rotation;

            yield return new WaitForFixedUpdate();
        }


    }

    IEnumerator StartGhost2()
    {
        for (int i=0; i<recordedGhostTransforms2.Count; i++){
            ghostKart2.position = recordedGhostTransforms2[i].position;
            ghostKart2.rotation = recordedGhostTransforms2[i].rotation;

            yield return new WaitForFixedUpdate();
        }


    }
}
