using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slot : MonoBehaviour
{
    public bool isInstalled;
    public scarecrow obj;


    // Start is called before the first frame update
    void Start()
    {
        isInstalled = false;
    }

    // Update is called once per frame
    void Update()
    {
        if(isInstalled)
        {
            //attack();
        }
    }

    public void setGreen()
    {
        this.gameObject.GetComponent<SpriteRenderer>().color = new Color(160f, 255f, 160f, 100f);
    }

    public void setInvisible()
    {
        this.gameObject.GetComponent<SpriteRenderer>().color = new Color(160f, 255f, 160f, 0f);
    }

    public void install_scarecrow(scarecrow target)
    {
        obj = target;
        obj.transform.position = this.transform.position - new Vector3(0f, -0.4f, 0f);
    }

    public void destroy_scarecrow()
    {
        obj.die();
        obj = null;
        isInstalled = false;
    }
    public void attack()
    {
        // 주변 가장 가까운 적 오브젝트 하나에게 데미지 이펙트 켜면서 데미지 입히는 기능.
        // 총알은 굳이 구현할 필요 없을 듯.
    }

    public void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "scarecrow")
        {
            scarecrow tmp = other.GetComponent<scarecrow>();

            if((Object.ReferenceEquals(tmp.slot, null) && !isInstalled) || Object.ReferenceEquals(obj.slot, tmp.slot))
                install_scarecrow(tmp);

            else
            {
                if (obj.keeperLV == tmp.keeperLV)
                {
                    tmp.slot.destroy_scarecrow();
                    tmp.die();
                    ++obj.keeperLV;
                    Debug.Log(obj.keeperLV);
                }

                else
                {
                    tmp.die();
                    GameManager.instance.lumber = GameManager.instance.scarecrow_cost;
                }
            }
        }
    }
}
