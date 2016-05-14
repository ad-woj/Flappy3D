using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CollisionDetector : MonoBehaviour {


    public List<GameObject> checkCollsionsWith(GameObject go)
    {
        GameObject[] gameObjects = UnityEngine.Object.FindObjectsOfType<GameObject>();
        List<GameObject> collidedObjects = new List<GameObject>();

        foreach (GameObject singleGO in gameObjects){
            if (singleGO.transform.IsChildOf(GameObject.Find("Hindrances").transform) && singleGO.GetComponent<Collider>() != null)
            {
                if (isCollision(go, singleGO))
                {
                    collidedObjects.Add(singleGO);
                }
            }
        }

        return collidedObjects;
    }

    // go = game object
    // go1 - for example bird
    // go2 - for example pipe, that bird can collide with
    public bool isCollision(GameObject go1, GameObject go2)
    {
        Renderer go1_component = go1.GetComponent<Renderer>();
        Renderer go2_component = go2.GetComponent<Renderer>();

        Vector3 go1_size = new Vector3( go1_component.bounds.size.x, go1_component.bounds.size.y, go2_component.bounds.size.z );
        Vector3 go2_size = new Vector3( go2_component.bounds.size.x, go2_component.bounds.size.y, go1_component.bounds.size.z );

        Vector3 go1_position = new Vector3(go1.transform.position.x, go1.transform.position.y, go1.transform.position.z);
        Vector3 go2_position = new Vector3(go2.transform.position.x, go2.transform.position.y, go2.transform.position.z);

        Vector3 go1_mins = new Vector3(go1_position.x - go1_size.x / 2, go1_position.y - go1_size.y / 2, go1_position.z - go1_size.z / 2);
        Vector3 go1_maxes = new Vector3(go1_position.x + go1_size.x / 2, go1_position.y + go1_size.y / 2, go1_position.z + go1_size.z / 2);

        Vector3 go2_mins = new Vector3(go2_position.x - go2_size.x / 2, go2_position.y - go2_size.y / 2, go2_position.z - go2_size.z / 2);
        Vector3 go2_maxes = new Vector3(go2_position.x + go2_size.x / 2, go2_position.y + go2_size.y / 2, go2_position.z + go2_size.z / 2);


        if (  // Checking if position of one object in every axis interfers with position of another object
               ((go1_mins.x >= go2_mins.x && go1_mins.x <= go2_maxes.x)
                 || (go1_maxes.x >= go2_mins.x && go1_maxes.x <= go2_maxes.x))
            &&
               ((go1_mins.y >= go2_mins.y && go1_mins.y <= go2_maxes.y)
                 || (go1_maxes.y >= go2_mins.y && go1_maxes.y <= go2_maxes.y))
            &&
               ((go1_mins.z >= go2_mins.z && go1_mins.z <= go2_maxes.z)
                 || (go1_maxes.z >= go2_mins.z && go1_maxes.z <= go2_maxes.z))
           )
            return true; // Collision detected

        return false;  // No collision
    }
}
