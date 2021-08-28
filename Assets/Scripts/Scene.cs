using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Scene : MonoBehaviour
{
    public Button gameStart, gameClose;
    public GameObject Ending_Image;
    private AudioSource audio;

    // Start is called before the first frame update
    void Start()
    {
        gameStart.onClick.AddListener(load_game);
        gameClose.onClick.AddListener(close_game);

        audio = Ending_Image.GetComponent<AudioSource>();
        this.audio.Play();
    }


    void load_game()
    {
        SceneManager.LoadScene("Game");
    }

    void close_game()
    {
        Application.Quit();
    }

}
