using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CollisionDetector : MonoBehaviour {

    // objectTag - tag of objects which 'go' can collide with
    // i.e. objectTag = "Pipes"
    public List<GameObject> checkCollisionsWith(string objectTag, GameObject go)
    {
        GameObject[] gameObjects = FindObjectsOfType<GameObject>();
        List<GameObject> collidedObjects = new List<GameObject>();

        foreach (GameObject singleGO in gameObjects){
            if( singleGO.tag == objectTag && singleGO.GetComponent<Collider>() != null )
            {
                if( objectTag.Equals( "AngryBird" ) )
                {
                    Debug.Log( "Angry ");
                    if( isSphereCollision( go, singleGO ))
                        collidedObjects.Add(singleGO);

                } else if(isBoxCollision(go, singleGO))
                {
                    collidedObjects.Add(singleGO);
                }
            }
        }

        return collidedObjects;
    }

    public List<GameObject> checkCollisionsWithPoints(GameObject go)
    {
        //Point[] gameObjects = FindObjectsOfType<Point>();
        List<GameObject> collidedObjects = new List<GameObject>();

        //foreach (var singleGO in gameObjects)
        //{
        //    if (singleGO.GetComponent<Collider>() != null)
        //    {
        //        if (isCollision(go, singleGO.gameObject))
        //        {
        //            collidedObjects.Add(singleGO.gameObject);
        //        }
        //        else
        //            Debug.Log(gameObject.tag);
        //    }
        //}
        //GameObject[] gameObjects = FindObjectsOfType<GameObject>();
        //List<GameObject> collidedObjects = new List<GameObject>();

        //foreach (GameObject singleGO in gameObjects)
        //{
        //    if (singleGO.transform.IsChildOf(GameObject.Find("Points").transform) && singleGO.GetComponent<Collider>() != null)
        //    {
        //        if (isCollision(go, singleGO))
        //        {
        //            collidedObjects.Add(singleGO);
        //        }
        //    }
        //}

        return collidedObjects;
    }

    // go = game object
    // go1 - for example bird
    // go2 - for example pipe, that bird can collide with
    public bool isBoxCollision(GameObject go1, GameObject go2)
    {
        BoxCollider go1_component = go1.GetComponent<BoxCollider>();
        BoxCollider go2_component = go2.GetComponent<BoxCollider>();

        Vector3 go1_size = go1_component.bounds.size;
        Vector3 go2_size = go2_component.bounds.size;

        Vector3 go1_position = go1.transform.position;
        Vector3 go2_position = go2.transform.position;

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

    public bool isSphereCollision( GameObject goBox, GameObject goSphere )
    {
        BoxCollider go1_component = goSphere.GetComponent<BoxCollider>();
        SphereCollider go2_component = goBox.GetComponent<SphereCollider>();
        if( go1_component == null || go2_component == null )
            return false;

        float radius = go2_component.radius;
        Vector3 spherePos = goSphere.transform.position;
        Vector3 boxPos = goBox.transform.position;
        Vector3 boxSize = go1_component.bounds.size;
        Vector3 go1_mins = new Vector3(boxPos.x - boxSize.x / 2, boxPos.y - boxSize.y / 2, boxPos.z - boxSize.z / 2);
        Vector3 go1_maxes = new Vector3(boxPos.x + boxSize.x / 2, boxPos.y + boxSize.y / 2, boxPos.z + boxSize.z / 2);

        if( (( go1_mins.x >= radius - spherePos.x && go1_mins.x <= radius + spherePos.x ) 
                && ( go1_mins.y >= radius - spherePos.y && go1_mins.y <= radius + spherePos.y ) 
                && ( go1_mins.z >= radius - spherePos.z && go1_mins.z <= radius + spherePos.z ))
            || (( go1_maxes.x >= radius - spherePos.x && go1_maxes.x <= radius + spherePos.x )
                && ( go1_maxes.y >= radius - spherePos.y && go1_maxes.y <= radius + spherePos.y )
                && ( go1_maxes.z >= radius - spherePos.z && go1_maxes.z <= radius + spherePos.z )) )
                return true;

        return false;
    }
}
