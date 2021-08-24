using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.UI;

public class ButtonManager : MonoBehaviour
{
    public Button buy_scarecrow, buy_card, deck, remain_deck, used_deck;

    public GameObject scarecrow_prefab;

    void Start()
    {
        buy_scarecrow.onClick.AddListener(create_scarecrow);
    }

    void create_scarecrow()
    {
        // ������ ���� ���� �� �پ��� ���

        Vector2 mouse_pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        GameObject scarecrow_ins = Instantiate(scarecrow_prefab);
        scarecrow_ins.transform.position = mouse_pos;
    }
    
}
