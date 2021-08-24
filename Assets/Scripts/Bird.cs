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

        // crop 방향
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
         * 체력 감소 -> 체력이 0 이하라면 die() 함수 실행 후 종료, 아니라면 피격 애니메이션 재생
         */
    }

    public void die()
    {
        gameObject.SetActive(false);
        birdHP = 1000;
        // 깃털 일정량 증가 코드
        // 죽음 애니메이션 제작 후 실행
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
