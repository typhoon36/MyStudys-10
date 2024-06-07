using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Store_Mgr : MonoBehaviour
{
    public Button BackBtn;

    //## 정보표시
    public Text m_UserInfoText = null;


    public GameObject m_Item_ScContent;
    //스크롤뷰 컨텐츠 차일드로 생성될 부모 오브젝트

    public GameObject m_SkProduct_Node;

    SkProduct_Node[] m_SkNodList;
    //아이템 목록들


    //## 구입 저장변수
    //스킬 아이템 구매여부
    SkillType m_BuySkType;
    //구입 프로세스 진입 후 상태 저장(차감 골드)
    int m_SvMyGold;

    //스킬 보유수 증가 백업
    int m_SvSkCount = 0;



    // Start is called before the first frame update
    void Start()
    {



        GlobalValue.LoadGameData();



        if (BackBtn != null)
            BackBtn.onClick.AddListener(BackBtnClick);






        if (m_UserInfoText != null)
            m_UserInfoText.text = "닉네임 ( " + GlobalValue.g_NickName + ") : 보유 골드 ( " +
                GlobalValue.g_UserGold+ " ) ";

        //## 아이템 목록 생성
        GameObject a_ItemObj = null;
        SkProduct_Node a_SkNode = null;

        for (int i = 0; i < GlobalValue.g_SkDataList.Count; i++)
        {
            a_ItemObj = Instantiate(m_SkProduct_Node, m_Item_ScContent.transform);
            a_SkNode = a_ItemObj.GetComponent<SkProduct_Node>();
            a_SkNode.InitData(GlobalValue.g_SkDataList[i].m_SkType);
            a_ItemObj.transform.SetParent(m_Item_ScContent.transform, false);
        }

        //## 아이템 목록 갱신
        RefreshSKItemList();


    }

    // Update is called once per frame
    void Update()
    {

    }

    void BackBtnClick()
    {
        SceneManager.LoadScene("LobbyScene");
    }



    //## 아이템 목록 갱신
    void RefreshSKItemList()
    {
        if (m_Item_ScContent != null)
        {
            if (m_SkNodList == null || m_SkNodList.Length <= 0)
            {
                m_SkNodList = m_Item_ScContent.GetComponentsInChildren<SkProduct_Node>();
            }
        }

        for (int i = 0; i < m_SkNodList.Length; i++)
        {
            m_SkNodList[i].RefreshState();
        }



    }


    //## 스킬 구매
    public void BuySkill(SkillType a_SkType)
    {
        //### 리스트뷰에 있는 가격 버튼 눌렀을시

        string a_Msg = "";
        bool a_NeedDel = false;
        Skill_Info a_SkInfo = GlobalValue.g_SkDataList[(int)a_SkType];


        if (5 <= GlobalValue.g_CurSkillCount[(int)a_SkType])
        {
            a_Msg = "아이템은 5개를 초과할수 없습니다.";
        }
        else if (GlobalValue.g_UserGold < a_SkInfo.m_Price)
        {
            a_Msg = "보유 골드가 부족합니다.";
        }
        else
        {
            a_Msg = "구매하시겠습니까?";
            a_NeedDel = true; // 구매

        }

        m_BuySkType = a_SkType;
        m_SvMyGold = GlobalValue.g_UserGold;

        m_SvMyGold -= a_SkInfo.m_Price;

        m_SvSkCount = GlobalValue.g_CurSkillCount[(int)a_SkType];

        m_SvSkCount++;


        //## 다이얼로그 생성
        GameObject a_DlgRsc = Resources.Load("DialogBox") as GameObject;

        GameObject a_DlgBox_Obj = Instantiate(a_DlgRsc);

        GameObject a_Canvas = GameObject.Find("Canvas");

        a_DlgBox_Obj.transform.SetParent(a_Canvas.transform, false);

        DialogBox_Ctrl a_DlgBox = a_DlgBox_Obj.GetComponent<DialogBox_Ctrl>();

        if (a_DlgBox != null)
        {
            if (a_NeedDel == true)
            {
                a_DlgBox.InitMessage(a_Msg, TryBuySkItem);
            }
            else
            {
                a_DlgBox.InitMessage(a_Msg);
            }
        }


    }

    //## 구매확정
    void TryBuySkItem()
    {
        if (m_BuySkType < SkillType.Skill_0 || SkillType.SkCount <= m_BuySkType)
            return;

        //골드 조정
        GlobalValue.g_UserGold = m_SvMyGold;

        //스킬 보유수 증가 조정
        GlobalValue.g_CurSkillCount[(int)m_BuySkType] = m_SvSkCount;

        RefreshSKItemList();

        m_UserInfoText.text = "닉네임 ( " + GlobalValue.g_NickName + ") : 보유 골드 ( " +
                GlobalValue.g_UserGold + " ) ";

        //## 데이터 저장
        PlayerPrefs.SetInt("UserGold", GlobalValue.g_UserGold);
        PlayerPrefs.SetInt($"Skill_Item_{(int)m_BuySkType}",
            GlobalValue.g_CurSkillCount[(int)m_BuySkType]);



    }


}
