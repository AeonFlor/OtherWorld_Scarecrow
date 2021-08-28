using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using System.Linq;

public class CardManager : MonoBehaviour
{
    public static CardManager instance;
    public GameObject cropController;
    public GameObject[] card = new GameObject[2];
    public List<GameObject> deck, hand, remain_deck, used_deck = new List<GameObject>();
    public int hand_count = 0, used_pos;

    private void Awake()
    {
        if (CardManager.instance == null)
        {
            CardManager.instance = this;
        }

        else
        {
            Destroy(this);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        for(int i=0; i<card.Length; ++i)
        {
            // give 5 cards each
            for (int j = 0; j < 5; ++j)
            {
                GameObject temp = Instantiate(card[i]);
                temp.SetActive(false);
                deck.Add(temp);
            }
        }

        for (int i = 0; i < 5; ++i)
            hand.Add(null);

        remain_deck = deck.ToList();
        shuffle(remain_deck);

        reroll_hand();
    }

    // Update is called once per frame
    void Update()
    {
        if (remain_deck.Count == 0)
        {
            remain_deck = shuffle(used_deck).ToList();
            used_deck.Clear();
        }

        if (hand_count < 4)
        {
            Debug.Log(used_pos);

            CardAlignment(used_pos);
            ++hand_count;
        }
    }

    List<GameObject> shuffle(List<GameObject> list)
    {
        GameObject[] temp = new GameObject[list.Count];

        for (int i = 0; i < list.Count; ++i)
        {
            int rand = Random.Range(0, list.Count);

            temp[i] = list[rand];
            list[rand] = list[i];
            list[i] = temp[i];
        }

        return list;
    }

    void CardAlignment(int used_pos)
    {
        Card target = remain_deck[0].GetComponent<Card>();
        target.gameObject.SetActive(true);

        target.originPRS = new PRS(new Vector3(-3.5f + 2.5f * used_pos, -3.5f, 0), Quaternion.identity, target.transform.localScale, used_pos);
        target.MoveTransform(target.originPRS, true, 0.7f);

        hand[used_pos] = remain_deck[0];

        //gameobject 를 가리키고 있는 상태에서 remove 하면 오류나는 듯
        remain_deck[0] = null;
        remain_deck.RemoveAt(0);
    }

    public void reroll_hand()
    {
        for(int i=0; i<hand.Count; ++i)
        {
            if(!object.ReferenceEquals(hand[i], null))
            {
                hand[i].SetActive(false);
                hand[i].transform.position = new Vector3(-12f, -3f, 0f);

                used_deck.Add(hand[i]);

                hand[i] = null;
            }
        }

        for(hand_count = 0; hand_count < 5; ++hand_count)
        {
            if (remain_deck.Count == 0)
            {
                remain_deck = shuffle(used_deck).ToList();
                used_deck.Clear();
            }

            CardAlignment(hand_count);
        }

        --hand_count;
    }

    public void show_deck(List<GameObject> deck, bool isShow)
    {
        float x, y, x_pad, y_pad;

        if (isShow)
        {
            x = -7.5f;
            y = 3f;

            x_pad = y_pad = 3f;

            for (int i = 0; i < deck.Count; ++i)
            {
                deck[i].transform.position = new Vector3(x + x_pad * (i % 6), y - y_pad * (i / 6), 0);
                deck[i].SetActive(true);
                deck[i].GetComponent<BoxCollider2D>().enabled = false;
            }
        }

        else
        {
            x = -12f;
            y = 3f;

            for (int i = 0; i < deck.Count; ++i)
            {
                deck[i].transform.position = new Vector3(x, y, 0);
                deck[i].SetActive(false);
                deck[i].GetComponent<BoxCollider2D>().enabled = true;
            }
        }
    }
}
