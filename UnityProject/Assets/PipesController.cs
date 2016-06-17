using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PipesController : MonoBehaviour {

    
	// Use this for initialization
	void Start () {

        // Changing y positions of pipes in a random way
        GameObject[] gameObjects = FindObjectsOfType<GameObject>();
        System.Random random = new System.Random();

        foreach (GameObject singleGO in gameObjects)
        {
            if (singleGO.transform.IsChildOf(GameObject.Find("Pipes").transform))
            {
                singleGO.transform.Translate(0, (float)random.Next(1,3) , 0, Space.World);
            }
        }
        // end of Changing y positions of pipes in a random way

    }

    // Update is called once per frame
    void Update () {
        
    }
}
