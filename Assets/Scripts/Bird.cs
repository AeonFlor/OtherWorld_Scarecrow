using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bird : MonoBehaviour
{
    public ulong birdHP = 1000;
    public ulong birdATK = 100;
    public float birdSpeed = 2f;
    public float xpos, ypos;

    private Rigidbody2D birdRigidbody;
    private Animator animator;
    private AudioSource audio;

    public Vector2 direction;

    void Start()
    {
        birdRigidbody = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        audio = GetComponent<AudioSource>();

        // crop ����
        direction = new Vector3(0, 0.5f, 0) - birdRigidbody.transform.position;
        direction = direction.normalized;
        birdRigidbody.velocity = direction * birdSpeed;
    }

    void Update()
    {
        
    }

    public void attacked()
    {
        /*
         * ü�� ���� -> ü���� 0 ���϶�� die() �Լ� ���� �� ����, �ƴ϶�� �ǰ� �ִϸ��̼� ���
         */
    }

    public void die()
    {
        gameObject.SetActive(false);
        birdHP = 1000;
        // ���� ������ ���� �ڵ�
        // ���� �ִϸ��̼� ���� �� ����
        BirdSpawner.instance.insertQueue(gameObject);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "scarecrow")
        {
            scarecrow scarecrowController = other.GetComponent<scarecrow>();

            if (scarecrowController != null)
            {
                scarecrowController.attacked(birdATK);
            }
        }

        else if (other.tag == "crop")
        {
            Crop cropController = other.GetComponent<Crop>();

            if (cropController != null)
            {
                cropController.attacked(birdATK);

            }

            birdRigidbody.velocity = Vector2.zero;
        }
    }
}
