using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UI;


public class Bird : MonoBehaviour {

    public Text countText;
    public Text gameOverText;
    public float speed = 15f;
    public float bounceTime = 1.9f;
    public float upDelta = 0.01f;
    public float downDelta = -0.01f;
    public float maxHeight = 100;
    public float minHeight = -50;

    private GameObject bird;

    private Vector3 dir;

    private CollisionDetector collisionDetector;
    private float lastBounceTime;
    private float bouncesSum;
    private int pickUpCount;
    bool goingUp;


    // Use this for initialization
    void Start () {
        bird = GameObject.Find("FlappyBirdModel2");
        collisionDetector = FindObjectOfType<CollisionDetector>();
        lastBounceTime = 0;
        bouncesSum = 0;
        goingUp = false;

        pickUpCount = 0;
        SetScoreText();

        if (gameOverText != null)
            gameOverText.text = "";
    }

    // Update is called once per frame
    void Update() {
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

            if (bird.transform.position.y + upDelta >= maxHeight || bird.transform.position.y + downDelta <= minHeight)
            {
                dir = new Vector3(0, 0, 0);
            } else
            {
                dir = new Vector3(0, upDelta, 0);
            }
                                

            currentFrameStep *= 1.1f;
            lastBounceTime -= Time.deltaTime * 3.0f;
        }
        else { // Bird is falling down
            birdAnimator.Play("Idle state");
            bouncesSum = 0;
            goingUp = false;

            if (bird.transform.position.y + downDelta < minHeight)
            {
                dir = new Vector3(0, 0, 0);
            }
            else
            {

                dir = new Vector3(0, downDelta, 0);
            }

            dir = new Vector3(0, downDelta, 0);
        }

        Quaternion targetRotation = Quaternion.LookRotation(dir);

        if (goingUp)
        {
            if (bird.transform.position.y + upDelta >= maxHeight)
            {
                dir = new Vector3(0, 0, 0);
            }
            else
            {
                dir = new Vector3(0, upDelta, 0);
            }
        }

        transform.Translate(dir.normalized * currentFrameStep, Space.World); // Move
                                                                             //transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, 0.02f * rotationModifier); // Rotate

        // Checking collisions, getting list of objects that collides with bird (usually it is one object
        // but I prepared list in case there are multiple collided objects)
        //List<GameObject> collidedObjects = collisionDetector.checkCollsionsWith(GameObject.Find("BirdObject"));

        GameObject birdObject = GameObject.Find("FlappyBirdModel2");

        if (birdObject != null && collisionDetector != null)
        {
            List<GameObject> collidedObjects = collisionDetector.checkCollisionsWith("Pipes", birdObject);
            //List<GameObject> collidedPoints = collisionDetector.checkCollisionsWithPoints(GameObject.Find("BirdObject"));
            List<GameObject> collidedPoints = collisionDetector.checkCollisionsWith("CollectiblePoint", birdObject);

            if (collidedObjects.Count != 0)
            {
                // TODO: Implement menu with restart game option && lives counter && points counter
                Debug.Log("Collision!");
                GameObject.Destroy(bird);
                //gameOverText.text = "Game Over!";
                UnityEngine.SceneManagement.SceneManager.LoadScene(3);

            }
            if (collidedPoints.Count != 0)
            {
                Debug.Log("Collision with Point!");
                collidedPoints.ForEach(GameObject.Destroy);
                pickUpCount += 50;
            }

            SetScoreText();
        } else
        {
            return;
        }
       
    }


    public void SetScoreText()
    {
        if (countText != null)
            countText.text = "Score: " + pickUpCount.ToString();
    }
}
