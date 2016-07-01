using UnityEngine;
using System.Collections;

public class ObstacleManager : MonoBehaviour {

    public Vector3 spawnPosition;
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
        float tmp = spawnPosition.z;
        Obstacle pipe = obstaclePrefabs[0].GetComponent<Obstacle>();
        float speed = pipe != null ? pipe.speed : 7.0f;
        for( spawnPosition /= 3; spawnPosition.z < tmp; spawnPosition.z += speed * spawningSpeed )
        {          
            Debug.Log( spawnPosition.z );
            Vector3 modifier = new Vector3( 0, Random.Range(minPositionY, maxPositionY), 0);
            currentObject = (GameObject)Instantiate( obstaclePrefabs[0], obstaclePrefabs[0].transform.position + modifier + spawnPosition, Quaternion.identity );
        }
        spawnPosition.z = tmp;
        //SpawnObstacle();
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
        //choosing random position of the obstacle
	    Vector3 modifier = new Vector3( 0, Random.Range(minPositionY, maxPositionY), 0);
        // choosing random obtacle
        int index = Random.Range(0, obstaclePrefabs.Length*3);
        if( index >= obstaclePrefabs.Length )
            index = 0;
        currentObject = (GameObject)Instantiate( obstaclePrefabs[index], obstaclePrefabs[index].transform.position + modifier + spawnPosition, Quaternion.identity );
    }

    void SpawnPickUp() {
        currentObject = (GameObject)Instantiate( PickUp, PickUp.transform.position + spawnPosition + new Vector3( 0, currentObject.transform.position.y, 0 ), Quaternion.identity );
    }
}
