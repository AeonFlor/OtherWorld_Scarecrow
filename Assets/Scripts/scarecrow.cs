using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class scarecrow : MonoBehaviour
{
    public long keeperHP, keeperATK, defense, dodge, keeperLV;
    public float atkRange, KeeperATKspeed;

    public Slot slot;

    void Start()
    {
        slot = null;
        keeperLV = 1;
        keeperATK = 100;
        atkRange = 5f;
        KeeperATKspeed = 2f;
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void attacked(long damage)
    {
        // �ǰ� �ִϸ��̼� ���, ü�� ����, ���� �̻� ���� �� die()
        // �ı� �� slot destroy �Լ� ����
    }

    public void attack(Bird target)
    {
        //����ƺ� ���� �ִϸ��̼� ���
        target.attacked(keeperATK * keeperLV);
    }

    public void die()
    {
        Destroy(this.gameObject);
    }

    private void OnMouseDrag()
    {
        Vector3 mouse_pos = new Vector3(Input.mousePosition.x, Input.mousePosition.y, -Camera.main.transform.position.z);
        this.transform.position = Camera.main.ScreenToWorldPoint(mouse_pos);
        this.GetComponent<CapsuleCollider2D>().enabled = false;
    }

    private void OnMouseUp()
    {
        bool check = false;

        this.GetComponent<CapsuleCollider2D>().enabled = true;

        Collider2D[] colls = Physics2D.OverlapCircleAll(this.transform.position, 0.4f);

        for (int i = 0; i < colls.Length; ++i)
        {
            if (colls[i].tag == "slot")
                check = true;
        }

        if (!check)
        {
            if (object.ReferenceEquals(slot, null))
            {
                GameManager.instance.lumber += GameManager.instance.scarecrow_cost;
                die();
            }

            else
                slot.install_scarecrow(this);
        }
    }
}
