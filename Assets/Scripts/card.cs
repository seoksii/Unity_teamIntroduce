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
        "<신우석>",
        "<신우석>",
        "<신우석>",
        "<신우석>",
        "<조용준>",
        "<조용준>",
        "<조용준>",
        "<조용준>",
        "<최성원>",
        "<최성원>",
        "<최성원>",
        "<최성원>"
    };

    public string[] cardExplanation =
    {
        "평소 게더 캠 화면입니다.\n뒤 배경은 슈퍼마리오 오디세이에 나오는 뉴동크 시티입니다.\n사람들을 만나는 것을 좋아해서 도시를 너무 좋아하나봐요!",
        "어디서든 분위기메이커가 되는 것이 제 장점입니다.\n협업을 할 때도 항상 즐거운 분위기 속에 협업이 진행되도록 만드는 것이 제 스타일이에요!",
        "청바지를 좋아합니다!\n우리 팀도 청바지처럼 오래갈수록 멋있어지는 그런 팀이 되기를 바라고 있습니다.",
        "팀과 함께 할 앞날이 너무나도 기대됩니다!\n우리 팀 모두 항상 건강하고 좋은 일 있기를 바랍니다.",
        "제 사진입니다!\n언제나 긍정적인 사람이 되고 싶어 따봉을 박습니다.\n우리 다같이 열심히 해요!",
        "제가 좋아하는 캐릭터인 폼폼푸린입니다.\n폼폼푸린은 골든리트리버인데요 모두와 친하게 지냅니다.\n저도 모두와 친하게 지낼게요!",
        "제가 좋아하는 캐릭터인 무민입니다.\n무민은 언제나 자기 일을 열심히 합니다.\n저도 제가 맡은 일을 열심히 할게요!",
        "제가 좋아하는 음식인 치킨입니다.\n치킨은 누구나 좋아하죠.\n저도 누구나 좋아하는 사람이 되겠습니다!",
        "장범준 아시나요?\n버스커버스커 장범준을 보고 취미로 기타를 배우기 시작했습니다.\n가끔은 부드러운 어쿠스틱 노래로 힐링 하시는건 어떨까요?",
        "가끔은 무거운 락도 즐겨보시는게 어떨까요?\n더운 여름도 락을 듣다보면 시원하게 날려버릴 순 없더라고요 ㅋㅋ",
        "오픈 월드 게임 좋아하시나요?\n젤다의 전설은 제가 제일 좋아하는 게임 입니다!",
        "감성있는 닌텐도 스위치의 빨간색 로고 입니다.\n모두 닌텐도 스위치를 즐기신적 있으신가요?\n포켓몬 젤다 동물의 숲 재밌는 게임이 많습니다~"
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
