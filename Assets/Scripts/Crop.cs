using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.UI;

public class Crop : MonoBehaviour
{
    public long cropCurHP = 10000, cropMaxHP = 10000, proceedCur = 0, proceedMax = 300;

    RectTransform hpBar;
    RectTransform proceedBar;
    public GameObject printHP, printProceed;
    GameObject canvas;
    Image cur_hpBar, cur_proceedBar;

    void Start()
    {
        canvas = GameObject.Find("Canvas");

        hpBar = Instantiate(printHP, canvas.transform).GetComponent<RectTransform>();
        proceedBar = Instantiate(printProceed, canvas.transform).GetComponent<RectTransform>();

        cur_hpBar = hpBar.transform.GetChild(0).GetComponent<Image>();
        cur_proceedBar = proceedBar.transform.GetChild(0).GetComponent<Image>();
    }

    // Update is called once per frame
    void Update()
    {
        if (cropCurHP <= 0)
            GameManager.instance.EndGame(false);

        if (proceedCur >= proceedMax)
            GameManager.instance.EndGame(true);

        Vector3 hpBar_pos = Camera.main.WorldToScreenPoint(new Vector3(transform.position.x, transform.position.y - 0.8f, 0));
        hpBar.position = hpBar_pos;
        cur_hpBar.fillAmount = (float)cropCurHP / cropMaxHP;

        Vector3 proceedBar_pos = Camera.main.WorldToScreenPoint(new Vector3(transform.position.x, transform.position.y - 1.2f, 0));
        proceedBar.position = proceedBar_pos;
        cur_proceedBar.fillAmount = (float)proceedCur / (float)proceedMax;
    }

    public void attacked(long damage)
    {
        // 피격 애니메이션 재생, 체력 감소, 일정 이상 감소 시 die()
        // 게임 종료.
        cropCurHP -= damage;

        if (cropCurHP > 0)
        {
            //피격 애니메이션 재생
        }
    }
}
