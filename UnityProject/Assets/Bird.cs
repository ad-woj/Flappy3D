using UnityEngine;
using System.Collections;



public class Bird : MonoBehaviour {

    private const float bounceTime = 0.8f;
    private float speed;
    private Vector3 dir;
    private GameObject handleUpDir;
    private GameObject handleDownDir;
    private float lastBounceTime;
    Vector3 jumpForce = new Vector3(0.0f, 7.3f, 0.0f);


    // Use this for initialization
    void Start () {
        speed = 2f;
        lastBounceTime = 0;
        handleUpDir = GameObject.Find("Up");
        handleDownDir = GameObject.Find("Down");
        dir = handleDownDir.transform.position - transform.localPosition; // starting direction to bottom handle
    }
	
	// Update is called once per frame
	void Update () {
        float rotationModifier;
        float currentFrameStep = speed * Time.deltaTime;
        float timeFromLastBounce;
        bool goingUp = false;

        if (Input.GetKeyDown(KeyCode.Space)){
            lastBounceTime = bounceTime;
            goingUp = true;
        };

        if (lastBounceTime > 0){ // Bird is going up after bounce
            dir = handleUpDir.transform.position - transform.localPosition;
            rotationModifier = 2.5f;
            currentFrameStep *= 1.4f; 
            lastBounceTime -= Time.deltaTime;
        }
        else { // Bird is falling down
            goingUp = false;
            rotationModifier = 1f;
            dir = handleDownDir.transform.position - transform.localPosition;
        }

        transform.Translate(dir.normalized * currentFrameStep, Space.World);
        Quaternion targetRotation = Quaternion.LookRotation(dir);


        
            
        //targetRotation = new Quaternion(targetRotation.x-0.1f, targetRotation.y, targetRotation.z, targetRotation.w);

        transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, 0.02f * rotationModifier);

        
        if (goingUp){
            Vector3 newRotationAngles = transform.rotation.eulerAngles;

            newRotationAngles.x -= 5;

            transform.rotation = Quaternion.Euler(newRotationAngles);
        }
        
        float xAngle = transform.eulerAngles.x;
        Debug.Log(xAngle);

        /*
        if (zAngle > 20 && zAngle <= 180)
        {
            zAngle = 20;
        }
        else if (zAngle < 340 && zAngle > 180)
        {
            zAngle = 340;
        } */
        
        

        updateDirectionObjectsPositions(currentFrameStep);
        //Debug.Log(lastBounceTime);
    }


    private void updateDirectionObjectsPositions(float currentFrameStep)
    {
        handleUpDir.transform.Translate(0,0,dir.normalized.z * currentFrameStep);
        handleDownDir.transform.Translate(0,0, dir.normalized.z * currentFrameStep);
    }
}
