using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class CustomParticleSystem : MonoBehaviour
{

    private class PartParams
    {
        public GameObject partprefab;
        public float speed;
        public Vector3 dir;
        public float lifeTime;
        public float initLife;
        public PartParams(GameObject prefab, float speed, Vector3 dir, float lifeTime, float initLife)
        {
            partprefab = prefab;
            this.speed = speed;
            this.dir = dir;
            this.lifeTime = lifeTime;
            this.initLife = initLife;
        }

    }
    public float particleLifeTime;
    public float particleSpeed;
    public int density;
    public Vector2 xPositionRange;
    public Vector2 yPositionRange;
    public Vector2 zPositionRange;

    private bool Effectenabled;
    public GameObject prefab;

    private List<PartParams> GameObjects;
    private List<GameObject> Particles;
    private float nextUpdate = 2f;

    // Use this for initialization
    void Start()
    {
        GameObjects = new List<PartParams>();
        for (int i = 0; i < density; i++)
        {
            Vector3 pos = new Vector3(Random.Range(-0.3f, 0.3f), Random.Range(-0.3f, 0.3f), Random.Range(-0.3f, 0.3f));
            GameObjects.Add(new PartParams((GameObject)Instantiate(prefab, transform.position + pos, transform.rotation), particleSpeed, new Vector3(Random.Range(xPositionRange.x, xPositionRange.y), Random.Range(yPositionRange.x, yPositionRange.y), Random.Range(zPositionRange.x, zPositionRange.y)), Random.Range(0.5f, 1f)*particleLifeTime, 0));
            pos = new Vector3(Random.Range(-0.3f, 0.3f), Random.Range(-0.3f, 0.3f), Random.Range(-0.3f, 0.3f));
            GameObjects.Add(new PartParams((GameObject)Instantiate(prefab, transform.position + pos, transform.rotation), particleSpeed, new Vector3(Random.Range(xPositionRange.x, xPositionRange.y), Random.Range(yPositionRange.x, yPositionRange.y), Random.Range(zPositionRange.x, zPositionRange.y)), Random.Range(0.3f, 1f)*particleLifeTime, 0));
            pos = new Vector3(Random.Range(-0.3f, 0.3f), Random.Range(-0.3f, 0.3f), Random.Range(-0.3f, 0.3f));
            GameObjects.Add(new PartParams((GameObject)Instantiate(prefab, transform.position + pos, transform.rotation), particleSpeed, new Vector3(Random.Range(xPositionRange.x, xPositionRange.y), Random.Range(yPositionRange.x, yPositionRange.y), Random.Range(zPositionRange.x, zPositionRange.y)), Random.Range(0.4f, 1f)*particleLifeTime, 0));
        }
    }
    // Update is called once per frame
    void Update()
    {
        float fadeTime = particleLifeTime * 0.8f;
        foreach(var temp in GameObjects.ToList())
        {
            temp.initLife += Time.deltaTime;
            temp.partprefab.transform.Translate(temp.dir * Time.deltaTime * Random.Range(0, 1f) * temp.speed);

            MeshRenderer renderer = temp.partprefab.GetComponentInChildren<MeshRenderer>();
            if(temp.initLife >= fadeTime && renderer)
            {
                //renderer.materials[0].color -= new Color(0, 0, 0, 0.1f);
            }

            if(temp.initLife >= temp.lifeTime)
            {
                ResetParticle(temp);
                //MeshRenderer origin = prefab.GetComponentInChildren<MeshRenderer>();
                //if( origin )
                    //renderer.material.color = origin.materials[0].color;
            }
        }


        //if (GameObjects.Count > 15)
        // {
        //     var temp = GameObjects.First();
        // GameObject.Destroy(temp.partprefab);
        //      GameObjects.Remove(temp);
        //  }
    }

    void ResetParticle( PartParams particle )
    {
        particle.initLife = 0;
        particle.lifeTime = Random.Range(0.6f, 1f)*particleLifeTime;
        particle.dir = new Vector3(Random.Range(xPositionRange.x, xPositionRange.y), Random.Range(yPositionRange.x, yPositionRange.y), Random.Range(zPositionRange.x, zPositionRange.y));
        particle.partprefab.transform.position = transform.position + new Vector3(Random.Range(-0.3f, 0.3f), Random.Range(-0.3f, 0.3f), Random.Range(-0.3f, 0.3f));
    }
}