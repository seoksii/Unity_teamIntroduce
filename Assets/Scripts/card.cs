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

    public int cardNum = 0;
    public string[] cardMemberName =
    {
        "<�ſ켮>",
        "<�ſ켮>",
        "<�ſ켮>",
        "<�ſ켮>",
        "<������>",
        "<������>",
        "<������>",
        "<������>",
        "<�ּ���>",
        "<�ּ���>",
        "<�ּ���>",
        "<�ּ���>"
    };

    public string[] cardExplanation =
    {
        "��� �Դ� ķ ȭ���Դϴ�.\n�� ����� ���۸����� �����̿� ������ ����ũ ��Ƽ�Դϴ�.\n������� ������ ���� �����ؼ� ���ø� �ʹ� �����ϳ�����!",
        "��𼭵� ���������Ŀ�� �Ǵ� ���� �� �����Դϴ�.\n������ �� ���� �׻� ��ſ� ������ �ӿ� ������ ����ǵ��� ����� ���� �� ��Ÿ���̿���!",
        "û������ �����մϴ�!\n�츮 ���� û����ó�� ���������� ���־����� �׷� ���� �Ǳ⸦ �ٶ�� �ֽ��ϴ�.",
        "���� �Բ� �� �ճ��� �ʹ����� ���˴ϴ�!\n�츮 �� ��� �׻� �ǰ��ϰ� ���� �� �ֱ⸦ �ٶ��ϴ�.",
        "�� �����Դϴ�!\n������ �������� ����� �ǰ� �;� ������ �ڽ��ϴ�.\n�츮 �ٰ��� ������ �ؿ�!",
        "���� �����ϴ� ĳ������ ����Ǫ���Դϴ�.\n����Ǫ���� ��縮Ʈ�����ε��� ��ο� ģ�ϰ� �����ϴ�.\n���� ��ο� ģ�ϰ� �����Կ�!",
        "���� �����ϴ� ĳ������ �����Դϴ�.\n������ ������ �ڱ� ���� ������ �մϴ�.\n���� ���� ���� ���� ������ �ҰԿ�!",
        "���� �����ϴ� ������ ġŲ�Դϴ�.\nġŲ�� ������ ��������.\n���� ������ �����ϴ� ����� �ǰڽ��ϴ�!",
        "����� �ƽó���?\n����Ŀ����Ŀ ������� ���� ��̷� ��Ÿ�� ���� �����߽��ϴ�.\n������ �ε巯�� ����ƽ �뷡�� ���� �Ͻô°� ����?",
        "������ ���ſ� ���� ��ܺ��ô°� ����?\n���� ������ ���� ��ٺ��� �ÿ��ϰ� �������� �� �������� ����",
        "���� ���� ���� �����Ͻó���?\n������ ������ ���� ���� �����ϴ� ���� �Դϴ�!",
        "�����ִ� ���ٵ� ����ġ�� ������ �ΰ� �Դϴ�.\n��� ���ٵ� ����ġ�� ������ �����Ű���?\n���ϸ� ���� ������ �� ��մ� ������ �����ϴ�~"
    };

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void initCard()
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
