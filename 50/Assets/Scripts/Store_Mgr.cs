using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Store_Mgr : MonoBehaviour
{
    public Button BackBtn;

    //## ����ǥ��
    public Text m_UserInfoText = null;


    public GameObject m_Item_ScContent;
    //��ũ�Ѻ� ������ ���ϵ�� ������ �θ� ������Ʈ

    public GameObject m_SkProduct_Node;

    SkProduct_Node[] m_SkNodList;
    //������ ��ϵ�


    //## ���� ���庯��
    //��ų ������ ���ſ���
    SkillType m_BuySkType;
    //���� ���μ��� ���� �� ���� ����(���� ���)
    int m_SvMyGold;

    //��ų ������ ���� ���
    int m_SvSkCount = 0;



    // Start is called before the first frame update
    void Start()
    {



        GlobalValue.LoadGameData();



        if (BackBtn != null)
            BackBtn.onClick.AddListener(BackBtnClick);






        if (m_UserInfoText != null)
            m_UserInfoText.text = "�г��� ( " + GlobalValue.g_NickName + ") : ���� ��� ( " +
                GlobalValue.g_UserGold+ " ) ";

        //## ������ ��� ����
        GameObject a_ItemObj = null;
        SkProduct_Node a_SkNode = null;

        for (int i = 0; i < GlobalValue.g_SkDataList.Count; i++)
        {
            a_ItemObj = Instantiate(m_SkProduct_Node, m_Item_ScContent.transform);
            a_SkNode = a_ItemObj.GetComponent<SkProduct_Node>();
            a_SkNode.InitData(GlobalValue.g_SkDataList[i].m_SkType);
            a_ItemObj.transform.SetParent(m_Item_ScContent.transform, false);
        }

        //## ������ ��� ����
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



    //## ������ ��� ����
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


    //## ��ų ����
    public void BuySkill(SkillType a_SkType)
    {
        //### ����Ʈ�信 �ִ� ���� ��ư ��������

        string a_Msg = "";
        bool a_NeedDel = false;
        Skill_Info a_SkInfo = GlobalValue.g_SkDataList[(int)a_SkType];


        if (5 <= GlobalValue.g_CurSkillCount[(int)a_SkType])
        {
            a_Msg = "�������� 5���� �ʰ��Ҽ� �����ϴ�.";
        }
        else if (GlobalValue.g_UserGold < a_SkInfo.m_Price)
        {
            a_Msg = "���� ��尡 �����մϴ�.";
        }
        else
        {
            a_Msg = "�����Ͻðڽ��ϱ�?";
            a_NeedDel = true; // ����

        }

        m_BuySkType = a_SkType;
        m_SvMyGold = GlobalValue.g_UserGold;

        m_SvMyGold -= a_SkInfo.m_Price;

        m_SvSkCount = GlobalValue.g_CurSkillCount[(int)a_SkType];

        m_SvSkCount++;


        //## ���̾�α� ����
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

    //## ����Ȯ��
    void TryBuySkItem()
    {
        if (m_BuySkType < SkillType.Skill_0 || SkillType.SkCount <= m_BuySkType)
            return;

        //��� ����
        GlobalValue.g_UserGold = m_SvMyGold;

        //��ų ������ ���� ����
        GlobalValue.g_CurSkillCount[(int)m_BuySkType] = m_SvSkCount;

        RefreshSKItemList();

        m_UserInfoText.text = "�г��� ( " + GlobalValue.g_NickName + ") : ���� ��� ( " +
                GlobalValue.g_UserGold + " ) ";

        //## ������ ����
        PlayerPrefs.SetInt("UserGold", GlobalValue.g_UserGold);
        PlayerPrefs.SetInt($"Skill_Item_{(int)m_BuySkType}",
            GlobalValue.g_CurSkillCount[(int)m_BuySkType]);



    }


}
