using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Lobby_Mgr : MonoBehaviour
{

    public Button m_ClearSvData_Btn;


    public Button Store_Btn;
    public Button MyRoom_Btn;
    public Button Exit_Btn;
    public Button GameStart_Btn;


    //## 텍스트
    public Text m_GoldText;
    public Text m_UserInfoText;

    //## 설정
    [Header("Config")]
    public Button config_Btn;
    public GameObject m_ConfigBoxObj;
    public GameObject Config_Canvas = null;



    // Start is called before the first frame update
    void Start()
    {
        Time.timeScale = 1.0f;

        GlobalValue.LoadGameData();


        if (m_ClearSvData_Btn != null)
            m_ClearSvData_Btn.onClick.AddListener(ClearSvData);



        if (Store_Btn != null)
            Store_Btn.onClick.AddListener(StoreBtnClick);

        if (MyRoom_Btn != null)
            MyRoom_Btn.onClick.AddListener(MyRoomBtnClick);

        if (Exit_Btn != null)
            Exit_Btn.onClick.AddListener(ExitBtnClick);

        if (GameStart_Btn != null)
            GameStart_Btn.onClick.AddListener(() =>
            {
                SceneManager.LoadScene("GameScene");
            });




        if (config_Btn != null)
            config_Btn.onClick.AddListener(() =>
            {
                if (m_ConfigBoxObj == null)
                    m_ConfigBoxObj = Resources.Load("ConfigBox") as GameObject;

                GameObject a_CfgBoxObj = Instantiate(m_ConfigBoxObj);
                a_CfgBoxObj.transform.SetParent(GameObject.Find("Config_Canvas").transform, false);
                Time.timeScale = 0.0f;  //일시정지 효과
            });

        Sound_Mgr.Inst.PlayBGM("sound_bgm_village_001", 0.2f);


        if (m_GoldText != null)
            m_GoldText.text = GlobalValue.g_UserGold.ToString("N0");



        if (m_UserInfoText != null)
            m_UserInfoText.text = "내 정보 : 닉네임 ( " + GlobalValue.g_NickName + ") : 점수 ("
                + GlobalValue.g_BestScore + ")";



    }

    void ClearSvData()
    {
        //로컬 데이터 삭제
        PlayerPrefs.DeleteAll();

        //스킬 카운트도 삭제
        GlobalValue.g_CurSkillCount.Clear();

        //다시 로드
        GlobalValue.LoadGameData();

        if(m_GoldText != null)
            m_GoldText.text = GlobalValue.g_UserGold.ToString("N0");



        if (m_UserInfoText != null)
            m_UserInfoText.text = "내 정보 : 닉네임 ( " + GlobalValue.g_NickName + ") : 점수 ("
                + GlobalValue.g_BestScore + ")";




    }





    private void StoreBtnClick()
    {
        //Debug.Log("상점으로 가기 버튼 클릭");
        MyLoadScene("StoreScene");
    }

    private void MyRoomBtnClick()
    {
        //Debug.Log("꾸미기 방 가기 버튼 클릭");
       MyLoadScene("MyRoomScene");
    }

    private void ExitBtnClick()
    {
        //Debug.Log("타이틀 씬으로 나가기 버튼 클릭");
        MyLoadScene("TitleScene");
    }

    // Update is called once per frame
    void Update()
    {

    }


    void MyLoadScene(string a_ScName)
    {
        bool ISFadeIn = false;

        if(Fade_Mgr.Inst != null)
        
            ISFadeIn = Fade_Mgr.Inst.SceneOutReserve(a_ScName);
        
        if(ISFadeIn == false)
        {
            SceneManager.LoadScene(a_ScName);
        }   

    }

}
