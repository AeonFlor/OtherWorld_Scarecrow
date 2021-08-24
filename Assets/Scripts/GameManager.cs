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
    public int lumber, feather, scarecrow_cost;
    
    private bool isGameover;

    private void Awake()
    {
        if(GameManager.instance == null)
        {
            GameManager.instance = this;
        }
    }

    void Start()
    {
        isGameover = false;
        lumber = feather = 0;
        scarecrow_cost = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if(!isGameover)
        {
            lumber_text.text = lumber + "";
            feather_text.text = feather + "";


        }
    }

    public void EndGame()
    {

    }
}
