using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class startManager : MonoBehaviour
{
    private startManager I;

    public GameObject normalClear;

    private void Awake()
    {
        I = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void startGame()
    {
        PlayerPrefs.SetInt("difficulty", 0);
        SceneManager.LoadScene("MainScene");
    }

    public void startHardGame()
    {
        if (PlayerPrefs.GetInt("isNormalClear") == 1)
        {
            PlayerPrefs.SetInt("difficulty", 1);
            SceneManager.LoadScene("MainScene");
        }
        else
        {
            normalClear.SetActive(true);
            Invoke("saf", 3f);
        }
    }

    void saf()
    {
        normalClear.SetActive(false);
    }
}
