using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading;
using UnityEngine;
using UnityEngine.UI;

public class card : MonoBehaviour
{
    public Animator anim;

    public AudioClip flip;
    public AudioSource audioSource;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void openCard()
    {
        audioSource.PlayOneShot(flip);

        if (gameManager.I.isClickable == false) return;

        anim.SetBool("isOpen", true);
        transform.Find("back").gameObject.GetComponent<Renderer>().material.color = Color.gray;

        if (gameManager.I.firstCard == null)
        {
            gameObject.GetComponent<Button>().interactable = false;
            gameManager.I.firstCard = gameObject;
        }
        else
        {
            gameObject.GetComponent<Button>().interactable = false;
            disableClick();
            gameManager.I.secondCard = gameObject;
            gameManager.I.isMatched();
        }
    }

    public void enableClick()
    {
        gameManager.I.isClickable = true;
    }

    public void disableClick()
    {
        gameManager.I.isClickable = false;
    }

    public void destroyCard()
    {
        Invoke("destroyCardInvoke", 1.0f);
    }

    void destroyCardInvoke()
    {
        Destroy(gameObject);
        enableClick();
    }

    public void closeCard()
    {
        Invoke("closeCardInvoke", 1.0f);
        Invoke("enableClick", 1.5f);
    }

    void closeCardInvoke()
    {
        gameObject.GetComponent<Button>().interactable = true;
        anim.SetBool("isOpen", false);
    }
}
