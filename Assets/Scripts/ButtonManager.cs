using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.UI;

public class ButtonManager : MonoBehaviour
{
    public static ButtonManager instance;

    public Button buy_scarecrow, buy_Card, deck, remain_deck, used_deck, reroll_card, Quit;

    public GameObject scarecrow_prefab;

    public bool isShow = false;

    private void Awake()
    {
        if (ButtonManager.instance == null)
        {
            ButtonManager.instance = this;
        }

        else
        {
            Destroy(this);
        }
    }

    void Start()
    {
        buy_scarecrow.onClick.AddListener(create_scarecrow);
        reroll_card.onClick.AddListener(reroll);
        Quit.onClick.AddListener(gameClose);

        // �ٸ� �κ��� �������� �������� ����
        if (!isShow)
        {
            deck.onClick.AddListener(show_deck);
        }
    }

    void create_scarecrow()
    {
        if(GameManager.instance.lumber >= GameManager.instance.scarecrow_cost)
        {
            GameManager.instance.lumber -= GameManager.instance.scarecrow_cost;

            GameObject scarecrow_ins = Instantiate(scarecrow_prefab);
            scarecrow_ins.transform.position = new Vector2(-5.9f, -1.7f);
        }

        else
        {
            // �ڿ��� ���ڶ�ٴ� �ȳ�
        }
    }

    void reroll()
    {
        if (GameManager.instance.feather >= GameManager.instance.reroll_cost)
        {
            GameManager.instance.feather -= GameManager.instance.reroll_cost;

            CardManager.instance.reroll_hand();
        }

        else
        {
            // �ڿ��� ���ڶ�ٴ� �ȳ�
        }
    }

    void show_deck()
    {
        isShow = !isShow;
        CardManager.instance.show_deck(CardManager.instance.remain_deck, isShow);
    }

    void gameClose()
    {
        Application.Quit();
    }
}
