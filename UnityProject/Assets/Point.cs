using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Point : MonoBehaviour {

    //private GameObject point;
    private bool up = true;
    private int counter = 0;
    private float step = 0.0005f;
    
    void Start()
    {
        //point = GameObject.Find("Point");
    }

    void Update()
    {
        //Random random = new Random();
        //int randomInt = random.Next(0, 10);

        GameObject[] gameObjects = UnityEngine.Object.FindObjectsOfType<GameObject>();
        List<GameObject> points = new List<GameObject>();

        foreach (GameObject singleGO in gameObjects)
        {
            if (singleGO.transform.IsChildOf(GameObject.Find("Points").transform))
            {
                points.Add(singleGO);
            }                
        }

        if (counter > 50)
        {
            counter = 0;
            up = !up;
        }
        else
            counter++;

        foreach (GameObject point in points)
        {
            //step = randomInt / 10000.0f;  
            if (up)
                point.transform.Translate(-0.5f * step * counter, 2.5f * step * counter, -step * counter);

            else
                point.transform.Translate(0.5f * step * counter, -2.5f * step * counter, step * counter);
        }
    }
}
