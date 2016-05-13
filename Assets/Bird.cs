using UnityEngine;
using System.Collections;



public class Bird : MonoBehaviour {

    private const float bounceTime = 60f;
    private float speed;
    private Vector3 dir;
    private GameObject handleUpDir;
    private GameObject handleDownDir;
    private float lastBounceTime;
  

    // Use this for initialization
    void Start () {
        speed = 1.5f;
        lastBounceTime = 0;
        handleUpDir = GameObject.Find("Up");
        handleDownDir = GameObject.Find("Down");
        dir = handleDownDir.transform.position - transform.localPosition; // starting direction to bottom handle
    }
	
	// Update is called once per frame
	void Update () {
        float rotationModifier;
        float currentFrameStep = speed * Time.deltaTime;

        if (Input.GetKeyDown(KeyCode.Space)){
            lastBounceTime = bounceTime * Time.deltaTime;
        };

        if (lastBounceTime > 0){ // Bird is going up after bounce
            dir = handleUpDir.transform.position - transform.localPosition;
            rotationModifier = 1f + lastBounceTime/bounceTime;
            currentFrameStep += (lastBounceTime / bounceTime) * Time.deltaTime; 
            lastBounceTime -= Time.deltaTime;
        }
        else { // Bird is falling down
            rotationModifier = 1f;
            dir = handleDownDir.transform.position - transform.localPosition;
        }

        transform.Translate(dir.normalized * currentFrameStep, Space.World);
        Quaternion targetRotation = Quaternion.LookRotation(dir);
        transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, Time.deltaTime/100);

        updateDirectionObjectsPositions(currentFrameStep);
    }


    private void updateDirectionObjectsPositions(float currentFrameStep)
    {
        handleUpDir.transform.Translate(0,0,dir.normalized.z * currentFrameStep);
        handleDownDir.transform.Translate(0,0, dir.normalized.z * currentFrameStep);
    }
}
