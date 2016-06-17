using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UI;


public class Bird : MonoBehaviour {

    public Text countText;
    public Text gameOverText;

    private const float bounceTime = 1.9f;
    private const float speed = 15f;
    private GameObject bird;

    private Vector3 dir;
    private GameObject handleUpDir;
    private GameObject handleDownDir;
    private CollisionDetector collisionDetector;
    private float lastBounceTime;
    private float bouncesSum;
    private int pickUpCount;
    bool goingUp;


    // Use this for initialization
    void Start () {
        //bird = GameObject.Find("BirdObject");
        bird = GameObject.Find("FlappyBirdModel2");
        collisionDetector = FindObjectOfType<CollisionDetector>();
        lastBounceTime = 0;
        bouncesSum = 0;
        goingUp = false;
        handleUpDir = GameObject.Find("Up");
        handleDownDir = GameObject.Find("Down");
        dir = handleDownDir.transform.position - transform.localPosition; // starting direction to bottom handle
        pickUpCount = 0;
        SetScoreText();
        gameOverText.text = "";
    }

    // Update is called once per frame
    void Update() {
        //float rotationModifier;
        float currentFrameStep = speed * Time.deltaTime;
        pickUpCount++;
        Animator birdAnimator = bird.GetComponent<Animator>();

        if (Input.GetKeyDown(KeyCode.Space)) {
            lastBounceTime = bounceTime;
            bouncesSum += 1;
            goingUp = true;
            birdAnimator.Play("Armature|ArmatureAction");
        };

        if (lastBounceTime > 0) { // Bird is going up after bounce
            goingUp = true;
            dir = handleUpDir.transform.position - transform.localPosition;
            //rotationModifier = 2.5f;
            currentFrameStep *= 1.1f;
            lastBounceTime -= Time.deltaTime * 1.2f;
        }
        else { // Bird is falling down
            birdAnimator.Play("Idle state");
            bouncesSum = 0;
            goingUp = false;
            //rotationModifier = 1f;
            dir = handleDownDir.transform.position - transform.localPosition;
        }

        Quaternion targetRotation = Quaternion.LookRotation(dir);

        if (goingUp)
        {
            targetRotation = Quaternion.Euler(targetRotation.eulerAngles.x + (40f - (bouncesSum * 2 % 20) * 4), 0, 0);
            dir = new Vector3(handleUpDir.transform.position.x,
                              handleUpDir.transform.position.y * (0.4f + lastBounceTime * 0.1f + (bouncesSum % 10) * 0.05f),
                              handleUpDir.transform.position.z)
                              - transform.localPosition;
        }

        transform.Translate(dir.normalized * currentFrameStep, Space.World); // Move
        //transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, 0.02f * rotationModifier); // Rotate

        // Checking collisions, getting list of objects that collides with bird (usually it is one object
        // but I prepared list in case there are multiple collided objects)
        //List<GameObject> collidedObjects = collisionDetector.checkCollsionsWith(GameObject.Find("BirdObject"));
        List<GameObject> collidedObjects = collisionDetector.checkCollisionsWith( "Hindrances", GameObject.Find("FlappyBirdModel2"));
        //List<GameObject> collidedPoints = collisionDetector.checkCollisionsWithPoints(GameObject.Find("BirdObject"));
        List<GameObject> collidedPoints = collisionDetector.checkCollisionsWith( "Points", GameObject.Find("FlappyBirdModel2"));

        if (collidedObjects.Count != 0)
        { 
            // TODO: Implement menu with restart game option && lives counter && points counter
            Debug.Log("Collision!");
            GameObject.Destroy(bird);
            gameOverText.text = "Game Over!";


       
           
        }
        if (collidedPoints.Count != 0)
        {
            Debug.Log("Collision with Point!");
            collidedPoints.ForEach(GameObject.Destroy);
            pickUpCount += 50;
        }

        updateDirectionObjectsPositions(currentFrameStep);
        SetScoreText();
    }


    private void updateDirectionObjectsPositions(float currentFrameStep)
    {
        handleUpDir.transform.Translate(0,0,dir.normalized.z * currentFrameStep);
        handleDownDir.transform.Translate(0,0, dir.normalized.z * currentFrameStep);
    }

    public void SetScoreText()
    {
        countText.text = "Score: " + pickUpCount.ToString();
    }
}
