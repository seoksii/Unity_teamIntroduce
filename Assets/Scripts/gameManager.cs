using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class gameManager : MonoBehaviour
{
    public static gameManager I;

    public GameObject card;
    public GameObject firstCard;
    public GameObject secondCard;

    public Text timeTxt;
    public GameObject resultPanel;
    public static float time = 60.0f;

    public Text trytimeTxt;
    int trytime = 0;

    public AudioClip success;
    public AudioClip fail;
    public AudioSource audioSource;

    public bool isClickable = true;

    private void Awake()
    {
        I = this;
    }


    // Start is called before the first frame update
    void Start()
    {
        initGame();

        int[] rtans = { 0, 0, 1, 1, 2, 2, 3, 3, 4, 4, 5, 5 };
        //int[] rtans = { 0, 0, 1, 1, 2, 2, 3, 3, 4, 4, 5, 5, 6, 6, 7, 7 };

        rtans = rtans.OrderBy(item => Random.Range(-1.0f, 1.0f)).ToArray();

        for (int i = 0; i < 12; i++)
        {
            GameObject newCard = Instantiate(card);
            newCard.transform.parent = GameObject.Find("Cards").transform;

            float x = (i % 4) * 1.4f - 2.1f;
            float y = (i / 4) * 1.4f - 2.3f;
            newCard.transform.position = new Vector3(x, y, 0);

            string rtanName = "rtan" + rtans[i].ToString();
            newCard.transform.Find("front").GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>(rtanName);
        }
    }

    // Update is called once per frame
    void Update()
    {
        time -= Time.deltaTime;
        timeTxt.text = time.ToString("N2");

        if (time < 20.0f)
        {
            GameObject.Find("timeTxt").GetComponent<Text>().color = Color.red;
        }

        if (time < 0.0f)
        {
            resultPanel.SetActive(true);
            Time.timeScale = 0.0f;
        }
    }

    void initGame()
    {
        resultPanel.SetActive(false);
        Time.timeScale = 1.0f;
        trytime = 0;
        trytimeTxt.text = trytime.ToString("N0");
    }

    public void isMatched()
    {
        string firstCardImage = firstCard.transform.Find("front").GetComponent<SpriteRenderer>().sprite.name;
        string secondCardImage = secondCard.transform.Find("front").GetComponent<SpriteRenderer>().sprite.name;

        if (firstCardImage == secondCardImage)
        {
            audioSource.PlayOneShot(success);
            firstCard.GetComponent<card>().destroyCard();
            secondCard.GetComponent<card>().destroyCard();

            int cardsLeft = GameObject.Find("Cards").transform.childCount;
            if (cardsLeft == 2)
            {
                resultPanel.SetActive(true);
                Time.timeScale = 0.0f;
            }
        }
        else
        {
            audioSource.PlayOneShot(fail);
            time -= 3;
            firstCard.GetComponent<card>().closeCard();
            secondCard.GetComponent<card>().closeCard();
        }

        firstCard = null;
        secondCard = null;

        trytime += 1;
        trytimeTxt.text = trytime.ToString("N0");
    }

    public void retryGame()
    {
        SceneManager.LoadScene("MainScene");
    }

    public void moveMainMenu()
    {
        SceneManager.LoadScene("StartScene");
    }
}
