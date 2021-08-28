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
    }

    public GameObject getQueue()
    {
        GameObject target = bird_queue.Dequeue();

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

                else if(xindex == 1)
                    target.transform.localScale = new Vector3(4, 3, 1);

                target.transform.position = new Vector2(xpos[xindex], ypos);
                target.transform.rotation = new Quaternion(0, 0, 0, 0);

                // getQueue 내부에서 setActive(true) 하니 게임 중간중간 position 이 바뀌지도 않았는데 살짝 보이는 경우가 있어 수정함.
                target.SetActive(true);

                //crop 방향
                Rigidbody2D target_body = target.GetComponent<Rigidbody2D>();
                Vector2 direction = new Vector3(0, 0.5f, 0) - target.transform.position;
                direction = direction.normalized;
                target.GetComponent<Rigidbody2D>().velocity = direction * target.GetComponent<Bird>().birdSpeed;
            }

            yield return new WaitForSeconds(spawnRate);
        }

        
    }

    void Update()
    {

    }
}
