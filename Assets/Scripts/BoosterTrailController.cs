using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoosterTrailController : MonoBehaviour
{

    public float dieTime;

    BoosterController booster;
    public GameObject trailGeneratorPrefab;
    public bool flag = false;

    public class Item
    {
        public float time;
        public GameObject gameObject;
        public bool flag = true;
        public Item(GameObject g, float t)
        {
            time = t;
            gameObject = g;
        }
    }

    public List<Item> items = new List<Item>();

    // Use this for initialization
    void Start()
    {
        booster = transform.parent.GetComponent<BoosterController>();
    }

    // Update is called once per frame
    void Update()
    {


        for (int i = items.Count - 1; i >= 0; i--)
        {
            if (items[i].time > 0)
            {
                if(!items[i].flag)  items[i].time -= Time.deltaTime;
            }
            else
            {
                Destroy(items[i].gameObject);
                items.RemoveAt(i);
            }
        }
        if (booster.currentPower > 0)
        {
            if (flag == false)
            {
                flag = true;
                GameObject g = Instantiate(trailGeneratorPrefab, transform.position, transform.rotation) as GameObject;
                g.transform.parent = transform.parent;
                items.Add(new Item(g, dieTime));
            }
            
        }
        else
        {
            if(flag == true)
            {
                flag = false;
                foreach(Item i in items)
                {
                   var em = i.gameObject.GetComponent<ParticleSystem>().emission;
                    em.enabled = false;
                    i.flag = false;
                }
            }
        }
    }
}