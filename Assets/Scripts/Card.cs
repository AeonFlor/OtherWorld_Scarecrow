using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using DG.Tweening;

public class Card : MonoBehaviour
{
    public PRS originPRS;

    public int id, cost, grow_up, lumber_get;

    private void OnMouseDrag()
    {
        Vector3 mouse_pos = new Vector3(Input.mousePosition.x, Input.mousePosition.y, -Camera.main.transform.position.z);
        this.transform.position = Camera.main.ScreenToWorldPoint(mouse_pos);
    }

    private void OnMouseUp()
    {
        if (this.transform.position.y > -0.5f && GameManager.instance.lumber >= cost)
        {
            this.gameObject.SetActive(false);
            this.transform.position = new Vector3(-12f, -3f, 0f);
            CardManager.instance.used_deck.Add(this.gameObject);

            GameManager.instance.lumber -= cost;

            CardManager.instance.cropController.GetComponent<Crop>().proceedCur += grow_up;
            GameManager.instance.lumber += lumber_get;

            CardManager.instance.hand_count -= 1;
            CardManager.instance.used_pos = originPRS.hand_pos;
        }

        else
        {
            this.transform.position = this.originPRS.pos;
        }
    }

    public void MoveTransform(PRS prs, bool useDotween, float dotweenTime = 0)
    {
        if(useDotween)
        {
            transform.DOMove(prs.pos, dotweenTime);
            transform.DORotateQuaternion(prs.rot, dotweenTime);
            transform.DOScale(prs.scale, dotweenTime);
        }

        else
        {
            transform.position = prs.pos;
            transform.rotation = prs.rot;
            transform.localScale = prs.scale;
        }
    }
}
