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
        // 피격 애니메이션 재생, 체력 감소, 일정 이상 감소 시 die()
        // 파괴 시 slot destroy 함수 실행
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
        
        해당 허수아비가 이미 슬롯 안에 있는 허수아비인지 확인 ( 슬롯 번호 이용 )

        설치된 적 없는 허수아비라면
            위치 슬롯 안일 경우
                설치
            슬롯 밖일 경우
                허수아비 파괴
                허수아비 비용 복구

        설치된 적 있는 허수아비라면
            다른 위치의 슬롯일 경우
                해당 위치에 허수아비가 없거나 레벨이 다르다면
                    원래 슬롯 install
                해당 위치의 허수아비 레벨이 같다면
                    원래 슬롯 destroy
                    새로운 슬롯 설치된 허수아비 lv + 1 ( 레벨에 따른 성능 수정, 레벨 표기 방안 마련 )
                    현재 허수아비 파괴
            
            슬롯 밖일 경우
                   원래 슬롯 install 

        */
    }

    private void OnMouseUp()
    {
        // 슬롯 안에 있는지 확인
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
