using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.SocialPlatforms.Impl;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class gameManager : MonoBehaviour
{
    public static gameManager I;

    bool isPaused = false;

    public GameObject card;
    public GameObject firstCard;
    public GameObject secondCard;

    public Text timeTxt;
    public GameObject resultPanel;
    public static float time = 60.0f;

    public Text trytimeTxt;
    public static float trytime = 0;

    public AudioClip success;
    public AudioClip fail;
    public AudioSource audioSource;

    public bool isClickable = true;

    public Animator panelAnimator;
    public UnityEngine.UI.Image panelImage;
    public Text panelName;
    public Text panelExplanation;

    public static float score = 0;
    public static float totalScore = 0;
    public Text recentlyScoreTxt;
    public Text bestScoreTxt;

    private void Awake()
    {
        I = this;
    }


    // Start is called before the first frame update
    void Start()
    {
        PlayerPrefs.SetInt("isNormalClear", 0);

        initGame();

        int[] members = { 0, 0, 1, 1, 4, 4, 5, 5, 8, 8, 9, 9 };
        //int[] rtans = { 0, 0, 1, 1, 2, 2, 3, 3, 4, 4, 5, 5, 6, 6, 7, 7 };

        members = members.OrderBy(item => Random.Range(-1.0f, 1.0f)).ToArray();

        for (int i = 0; i < 12; i++)
        {
            GameObject newCard = Instantiate(card);
            newCard.transform.parent = GameObject.Find("Cards").transform;
            newCard.GetComponent<card>().cardNum = members[i];
            newCard.GetComponent<card>().initCard();

            float x = (i % 4) * 1.4f - 2.1f;
            float y = (i / 4) * 1.4f - 2.3f;
            newCard.transform.position = new Vector3(x, y, 0);

            string memberName = "Image" + members[i].ToString();
            newCard.transform.Find("front").GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>(memberName);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(isPaused == false) time -= Time.deltaTime;
        timeTxt.text = time.ToString("N2");

        if (time < 20.0f)
        {
            GameObject.Find("timeTxt").GetComponent<Text>().color = Color.red;
            GameObject.Find("Main Camera").GetComponent<Camera>().backgroundColor = Color.yellow;
        }

        if (time < 0.0f)
        {
            totalScore = 200 + score + time - trytime;

            if (PlayerPrefs.HasKey("bestScore") == false)
            {
                PlayerPrefs.SetFloat("bestScore", totalScore);
            }
            else
            {
                if (PlayerPrefs.GetFloat("bestScore") < totalScore)
                {
                    PlayerPrefs.SetFloat("bestScore", totalScore);
                }
            }
            bestScoreTxt.text = PlayerPrefs.GetFloat("bestScore").ToString("N2");

            recentlyScoreTxt.text = totalScore.ToString("N2");

            resultPanel.SetActive(true);
            Time.timeScale = 0.0f;
        }
    }

    void initGame()
    {
        isPaused = false;
        resultPanel.SetActive(false);
        Time.timeScale = 1.0f;
        trytime = 0;
        trytimeTxt.text = trytime.ToString("N0");
    }

    public void isMatched()
    {
        int firstCardNum = firstCard.GetComponent<card>().cardNum;
        int secondCardNum = secondCard.GetComponent<card>().cardNum;

        if (firstCardNum == secondCardNum)
        {
            score += 10;
            audioSource.PlayOneShot(success, 0.5f);

            ShowSuccessPanel();
            firstCard.GetComponent<card>().destroyCard();
            secondCard.GetComponent<card>().destroyCard();

            int cardsLeft = GameObject.Find("Cards").transform.childCount;
            if (cardsLeft == 2)
            {
                PlayerPrefs.SetInt("isNormalClear", 1);
                Invoke("successGame", 7.0f);
            }
        }
        else
        {
            score -= 5;
            audioSource.PlayOneShot(fail, 0.5f);
            time -= 3;
            firstCard.GetComponent<card>().closeCard();
            secondCard.GetComponent<card>().closeCard();
        }

        firstCard = null;
        secondCard = null;

        trytime += 1;
        trytimeTxt.text = trytime.ToString("N0");
    }
    
    public void ShowSuccessPanel()
    {
        Invoke("pauseGame", 0.5f);

        int cardNum = firstCard.GetComponent<card>().cardNum;

        panelImage.sprite = Resources.Load<Sprite>("Image" + cardNum.ToString());
        panelName.text = firstCard.GetComponent<card>().cardMemberName[cardNum];
        panelExplanation.text = firstCard.GetComponent<card>().cardExplanation[cardNum];

        panelAnimator.SetBool("isSuccess", true);
        Invoke("disablePanel", 5.0f);
        Invoke("resumeGame", 6.5f);
    }

    public void disablePanel()
    {
        panelAnimator.SetBool("isSuccess", false);
    }

    public void pauseGame()
    {
        isPaused = true;
    }

    public void resumeGame()
    {
        isPaused = false;
    }

    public void successGame()
    {
        totalScore = 200 + score + time - trytime;
        recentlyScoreTxt.text = totalScore.ToString("N2");

        if (PlayerPrefs.HasKey("bestScore") == false)
        {
            PlayerPrefs.SetFloat("bestScore", totalScore);
        }
        else
        {
            if (PlayerPrefs.GetFloat("bestScore") < totalScore)
            {
                PlayerPrefs.SetFloat("bestScore", totalScore);
            }
        }
        bestScoreTxt.text = PlayerPrefs.GetFloat("bestScore").ToString("N2");
        resultPanel.SetActive(true);
        Time.timeScale = 0.0f;
    }

    public void retryGame()
    {
        resultPanel.SetActive(false);
        Time.timeScale = 1.0f;
        trytime = 0;
        trytimeTxt.text = trytime.ToString("N0");
        time = 60f;
        AudioManager.instance.resetAudio();
        SceneManager.LoadScene("MainScene");
    }

    public void moveMainMenu()
    {
        resultPanel.SetActive(false);
        Time.timeScale = 1.0f;
        trytime = 0;
        trytimeTxt.text = trytime.ToString("N0");
        time = 60f;
        AudioManager.instance.resetAudio();
        SceneManager.LoadScene("StartScene");
    }
}
