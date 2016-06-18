using UnityEngine;
using System.Collections;

public class ObstacleManager : MonoBehaviour {

    public Vector2 spawnPosition;
    public int maxPositionY;
    public int minPositionY;
    public float spawningSpeed;

    public GameObject[] obstaclePrefabs;
    public GameObject PickUp;
    private GameObject currentObject;
    private float timer;
    private float pickUpTimer;

	// Use this for initialization
	void Start () {
	    timer = spawningSpeed;
        pickUpTimer = timer / 2;
        SpawnObstacle();
	}
	
	// Update is called once per frame
	void Update () {
        timer -= Time.deltaTime;
        pickUpTimer -= Time.deltaTime;
        if( timer <= 0 ) {
            timer = spawningSpeed;
            SpawnObstacle();
        }
        if( pickUpTimer <= 0 ) {
            pickUpTimer = spawningSpeed;
            SpawnPickUp();
        }
	}

    void SpawnObstacle() {
	    int positionY = Random.Range(minPositionY, maxPositionY);
        currentObject = (GameObject)Instantiate( obstaclePrefabs[0], new Vector3( transform.position.x, positionY, transform.position.z ), Quaternion.identity );
    }

    void SpawnPickUp() {
        currentObject = (GameObject)Instantiate( PickUp, PickUp.transform.position + new Vector3( 0, currentObject.transform.position.y, 0 ), Quaternion.identity );
    }
}
