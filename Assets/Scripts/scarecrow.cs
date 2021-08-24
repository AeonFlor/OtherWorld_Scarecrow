using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class scarecrow : MonoBehaviour
{
    public int keeperLV;
    public ulong keeperHP, keeperATK, defense, atkSpeed, dodge, sight;

    public Slot slot;

    void Start()
    {
        slot = null;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void attacked(ulong damage)
    {
        // �ǰ� �ִϸ��̼� ���, ü�� ����, ���� �̻� ���� �� die()
        // �ı� �� slot destroy �Լ� ����
    }

    public void die()
    {
        Debug.Log("?");
        Destroy(this.gameObject);
    }

    private void OnMouseDrag()
    {
        Vector3 mouse_pos = new Vector3(Input.mousePosition.x, Input.mousePosition.y, -Camera.main.transform.position.z);
        this.transform.position = Camera.main.ScreenToWorldPoint(mouse_pos);
        this.GetComponent<CapsuleCollider2D>().enabled = false;

        /*
        
        �ش� ����ƺ� �̹� ���� �ȿ� �ִ� ����ƺ����� Ȯ�� ( ���� ��ȣ �̿� )

        ��ġ�� �� ���� ����ƺ���
            ��ġ ���� ���� ���
                ��ġ
            ���� ���� ���
                ����ƺ� �ı�
                ����ƺ� ��� ����

        ��ġ�� �� �ִ� ����ƺ���
            �ٸ� ��ġ�� ������ ���
                �ش� ��ġ�� ����ƺ� ���ų� ������ �ٸ��ٸ�
                    ���� ���� install
                �ش� ��ġ�� ����ƺ� ������ ���ٸ�
                    ���� ���� destroy
                    ���ο� ���� ��ġ�� ����ƺ� lv + 1 ( ������ ���� ���� ����, ���� ǥ�� ��� ���� )
                    ���� ����ƺ� �ı�
            
            ���� ���� ���
                   ���� ���� install 

        */
    }

    private void OnMouseUp()
    {
        // ���� �ȿ� �ִ��� Ȯ��
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
            Debug.Log("~");

            if (object.ReferenceEquals(slot, null))
                die();

            else
                slot.install_scarecrow(this);
        }
    }
}
