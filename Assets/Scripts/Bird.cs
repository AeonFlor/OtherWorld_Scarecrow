using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.UI;

public class Bird : MonoBehaviour
{
    RectTransform hpBar;
    public GameObject printHP;
    GameObject canvas;
    Image cur_hpBar;

    public long birdMaxHP = 1000, birdCurHP = 1000;
    public long birdATK = 100;
    public float birdSpeed = 2f, birdATKspeed = 2f;
    public float time_AfterATK;

    private Animator animator;
    private AudioSource audio;

    Crop cropController;

    void Start()
    {
        animator = GetComponent<Animator>();
        audio = GetComponent<AudioSource>();

        canvas = GameObject.Find("Canvas");

        hpBar = Instantiate(printHP, canvas.transform).GetComponent<RectTransform>();
        cur_hpBar = hpBar.transform.GetChild(0).GetComponent<Image>();

        audio = this.gameObject.GetComponent<AudioSource>();
    }

    void Update()
    {
        Vector3 hpBar_pos = Camera.main.WorldToScreenPoint(new Vector3(transform.position.x, transform.position.y - 0.8f, 0));
        hpBar.position = hpBar_pos;
        cur_hpBar.fillAmount = (float)birdCurHP / birdMaxHP;

        time_AfterATK += Time.deltaTime;

        if (!Object.ReferenceEquals(cropController, null) && time_AfterATK >= birdATKspeed)
        {
            this.audio.Play();
            cropController.attacked(birdATK);
            time_AfterATK = 0f;
        }
    }

    public void attacked(long damage)
    {
        /*
         * 체력 감소 -> 체력이 0 이하라면 die() 함수 실행 후 종료, 아니라면 피격 애니메이션 재생
         */
        birdCurHP -= damage;

        if (birdCurHP < 1)
        {
            die();
        }

        else
        {
            //피격 애니메이션 재생
        }
    }

    public void die()
    {
        hpBar.gameObject.SetActive(false);
        gameObject.SetActive(false);

        GameManager.instance.feather += GameManager.instance.bird_drop;

        // 죽음 애니메이션 제작 후 실행
        BirdSpawner.instance.insertQueue(gameObject);
    }

    private void OnEnable()
    {
        if (hpBar != null)
        {
            hpBar.gameObject.SetActive(true);
            birdCurHP = birdMaxHP;
        }
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
            cropController = other.GetComponent<Crop>();

            if (cropController != null)
            {
                cropController.attacked(birdATK);
            }

            this.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
        }
    }
}
