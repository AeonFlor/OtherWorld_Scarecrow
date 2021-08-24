using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BirdSpawner : MonoBehaviour
{
    public Queue<GameObject> bird_queue = new Queue<GameObject>();
    public GameObject bird;
    public static BirdSpawner instance;

    public float ypos;
    public int xindex;
    public int[] xpos = new int[] { -11, 11 };

    public float spawnRate;
    
    void Start()
    {
        spawnRate = Random.Range(1f, 3f);

        instance = this;

        for (int i = 0; i < 20; ++i)
        {
            GameObject bird_object = Instantiate(bird, this.gameObject.transform);
            bird_queue.Enqueue(bird_object);
            bird_object.SetActive(false);
        }

        StartCoroutine(birdSpawn());
    }
    public void insertQueue(GameObject target)
    {
        bird_queue.Enqueue(target);
        target.SetActive(false);
    }

    public GameObject getQueue()
    {
        GameObject target = bird_queue.Dequeue();
        target.SetActive(true);

        return target;
    }

    IEnumerator birdSpawn()
    {
        while(true)
        {
            spawnRate = spawnRate = Random.Range(1f, 3f);

            if (bird_queue.Count != 0)
            {
                ypos = Random.Range(-4, 6);
                xindex = Random.Range(0, 2);

                GameObject target = getQueue();

                if (xindex == 0)
                    target.transform.localScale = new Vector3(-4, 3, 1);

                target.transform.position = new Vector2(xpos[xindex], ypos);
                target.transform.rotation = new Quaternion(0, 0, 0, 0);
            }

            yield return new WaitForSeconds(spawnRate);
        }

        
    }

    void Update()
    {

    }
}
