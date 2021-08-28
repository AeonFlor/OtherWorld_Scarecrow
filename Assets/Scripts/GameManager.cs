using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public GameObject resourceText;
    public Text lumber_text, feather_text;
    public int lumber, feather, scarecrow_cost, bird_drop, reroll_cost;
    public AudioSource audio;

    private void Awake()
    {
        if(GameManager.instance == null)
        {
            GameManager.instance = this;
        }

        else
        {
            Destroy(this);
        }
    }

    void Start()
    {
        Screen.SetResolution(1200, 600, true);

        lumber = feather = 100;
        scarecrow_cost = 5;
        bird_drop = 5;
        reroll_cost = 5;

        audio = this.GetComponent<AudioSource>();
        audio.Play();
    }

    // Update is called once per frame
    void Update()
    {
        lumber_text.text = "∏Ò¿Á : " + lumber;
        feather_text.text = "±Í≈– : " + feather;
    }

    public void EndGame(bool isGood)
    {
        if(isGood)
            SceneManager.LoadScene("goodEnding");

        else
            SceneManager.LoadScene("badEnding");
    }
}
