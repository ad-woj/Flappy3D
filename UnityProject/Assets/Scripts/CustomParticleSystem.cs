using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class CustomParticleSystem : MonoBehaviour
{

    private struct PartParams
    {
        public GameObject partprefab;
        public float speed;
        public Vector3 dir;
        public float lifeTime;
        public float initLife;
        public PartParams(GameObject prefab, float speed, Vector3 dir, float lifeTime, float initLife = 0)
        {
            this.partprefab = prefab;
            this.speed = speed;
            this.dir = dir;
            this.lifeTime = lifeTime;
            this.initLife = initLife;
        }

    }

    private bool Effectenabled;
    public GameObject prefab;

    private List<PartParams> GameObjects;
    private float nextUpdate = 2f;

    // Use this for initialization
    void Start()
    {
        GameObjects = new List<PartParams>();
        for (int i = 0; i < 30; i++)
        {
            GameObjects.Add(new PartParams((GameObject)Instantiate(prefab, transform.position, transform.rotation), 10f, new Vector3(Random.Range(-1f, 1f), Random.Range(1f, 3f), 0), Random.Range(1f, 3f)));
            GameObjects.Add(new PartParams((GameObject)Instantiate(prefab, transform.position, transform.rotation), 10f, new Vector3(Random.Range(-1f, 1f), Random.Range(1f, 3f), 0), Random.Range(1f, 3f)));
            GameObjects.Add(new PartParams((GameObject)Instantiate(prefab, transform.position, transform.rotation), 10f, new Vector3(Random.Range(-1f, 1f), Random.Range(1f, 3f), 0), Random.Range(1f, 3f)));
        }
    }
    // Update is called once per frame
    void Update()
    {
        for (int i = 0; i < GameObjects.Count; i++)
        {
            PartParams temp = GameObjects[i];
            temp.partprefab.transform.Translate(temp.dir * Time.deltaTime * Random.Range(0, 2f));
            if (Time.time >= nextUpdate && GameObjects.Count > 0)
            {
                nextUpdate = Mathf.FloorToInt(Time.time) + 1;
                foreach (var temp2 in GameObjects.ToList())
                {
                    Debug.Log("Usuwam: ");
                    GameObject.Destroy(temp2.partprefab);
                    GameObjects.Remove(temp2);
                }
            }
        }

        


        //if (GameObjects.Count > 15)
        // {
        //     var temp = GameObjects.First();
        // GameObject.Destroy(temp.partprefab);
        //      GameObjects.Remove(temp);
        //  }



    }
}