using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.UI;

public class Slot : MonoBehaviour
{
    public scarecrow obj;
    public float time_AfterATK;
    public Text level_text;
    Text level_text_ins;
    GameObject canvas;

    // Start is called before the first frame update
    void Start()
    {
        time_AfterATK = 0f;

        canvas = GameObject.Find("Canvas");
        level_text_ins = Instantiate(level_text, canvas.transform);
        level_text_ins.GetComponent<RectTransform>().position = Camera.main.WorldToScreenPoint(new Vector3(transform.position.x, transform.position.y - 0.5f, 0));

        level_text_ins.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        time_AfterATK += Time.deltaTime;

        if(!Object.ReferenceEquals(obj, null) && time_AfterATK >= obj.KeeperATKspeed)
        {
            attack();
            time_AfterATK = 0f;
        }
    }

    public void setGreen()
    {
        level_text_ins.gameObject.SetActive(false);
        this.gameObject.GetComponent<SpriteRenderer>().color = new Color(160f / 255f, 255f / 255f, 160f / 255f, 100f / 255f);
    }

    public void setInvisible()
    {
        level_text_ins.gameObject.SetActive(true);
        this.gameObject.GetComponent<SpriteRenderer>().color = new Color(160f / 255f, 255f / 255f, 160f / 255f, 0f / 255f);
    }

    public void install_scarecrow(scarecrow target)
    {
        obj = target;
        setInvisible();
        target.slot = this;
        level_text_ins.text = "<LV : " + obj.keeperLV + ">";
        obj.transform.position = this.transform.position - new Vector3(0f, -0.4f, 0f);
    }

    public void destroy_scarecrow()
    {
        obj.die();
        setGreen();
        obj = null;
    }
    public void attack()
    {
        Collider2D[] colls = Physics2D.OverlapCircleAll(this.transform.position, obj.atkRange);

        for (int i = 0; i < colls.Length; ++i)
        {
            if (colls[i].tag == "enemy")
            {
                Bird enemy = colls[i].GetComponent<Bird>();

                obj.attack(enemy);
            }
        }
    }

    public void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "scarecrow")
        {
            scarecrow tmp = other.GetComponent<scarecrow>();

            if (Object.ReferenceEquals(tmp.slot, null))
            {
                if (Object.ReferenceEquals(obj, null))
                    install_scarecrow(tmp);

                else
                {
                    if (obj.keeperLV == tmp.keeperLV)
                    {
                        level_text_ins.text = "<LV : " + ++obj.keeperLV + ">";
                    }

                    tmp.die();
                }
            }

            else if (!Object.ReferenceEquals(obj, null) && Object.ReferenceEquals(obj.slot, tmp.slot))
                install_scarecrow(tmp);

            else
            {
                if (!Object.ReferenceEquals(obj, null) && obj.keeperLV == tmp.keeperLV)
                {
                    tmp.slot.destroy_scarecrow();
                    tmp.die();
                    level_text_ins.text = "<LV : " + ++obj.keeperLV + ">";
                }

                else
                {
                    tmp.slot.install_scarecrow(tmp);
                }
            }
        }
    }
}
