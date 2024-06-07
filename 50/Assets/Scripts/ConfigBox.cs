using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;



public class ConfigBox : MonoBehaviour
{
    public Button m_Ok_Btn = null;
    public Button m_Close_Btn = null;

    public InputField NickInputField = null;

    public Toggle m_Sound_Toggle = null;
    public Slider m_Sound_Slider = null;

    HeroCtrl m_RefHero = null;

    // Start is called before the first frame update
    void Start()
    {
        if (m_Ok_Btn != null)
            m_Ok_Btn.onClick.AddListener(OkBtnClick);

        if (m_Close_Btn != null)
            m_Close_Btn.onClick.AddListener(CloseBtnClick);

        if (m_Sound_Toggle != null)
            m_Sound_Toggle.onValueChanged.AddListener(SoundOnOff);
        //üũ ���°� ����Ǿ��� �� ȣ��Ǵ� �Լ��� ����ϴ� �ڵ�

        if (m_Sound_Slider != null)
            m_Sound_Slider.onValueChanged.AddListener(SliderChanged);
        //�����̵� ���°� ���� �Ǿ��� �� ȣ��Ǵ� �Լ� ����ϴ� �ڵ�

        m_RefHero = FindObjectOfType<HeroCtrl>();
        //Hierarchy�ʿ��� HeroCtrl ������Ʈ�� �پ��ִ� ���ӿ�����Ʈ�� ã�Ƽ� ��ü�� ã�ƿ��� ���

        //--- üũ����, �����̵����, �г��� �ε� �� UI ��Ʈ�ѿ� ����
        int a_SoundOnOff = PlayerPrefs.GetInt("SoundOnOff", 1);
        if (m_Sound_Toggle != null)
        {
            if (a_SoundOnOff == 1)
                m_Sound_Toggle.isOn = true;
            else
                m_Sound_Toggle.isOn = false;

            m_Sound_Toggle.isOn = (a_SoundOnOff == 1) ? true : false;
        }

        if (m_Sound_Slider != null)
            m_Sound_Slider.value = PlayerPrefs.GetFloat("SoundVolume", 1.0f);

        //Text a_Placehoder = null;
        if (NickInputField != null)
        {
            NickInputField.text = GlobalValue.g_NickName;

            NickInputField.onEndEdit.AddListener((string a_NickName) =>
            {
                GlobalValue.g_NickName = a_NickName.Trim();
                PlayerPrefs.SetString("NickName", GlobalValue.g_NickName);
                PlayerPrefs.Save();

                GlobalValue.LoadGameData();
            });

        }

    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OkBtnClick()
    {
        

        Time.timeScale = 1.0f;  //�Ͻ����� Ǯ���ֱ�
        Destroy(gameObject);
    }

    private void CloseBtnClick()
    {
        Time.timeScale = 1.0f;  //�Ͻ����� Ǯ���ֱ�
        Destroy(gameObject);
    }

    private void SoundOnOff(bool value) //üũ ���°� ����Ǿ��� �� ȣ��Ǵ� �Լ�
    {
       
        int a_IntV = (value == true) ? 1 : 0;
        PlayerPrefs.SetInt("SoundOnOff", a_IntV);

        Sound_Mgr.Inst.SoundOnOff(value);    //���� �� / ��
        //--- üũ ���� ����
    }

    private void SliderChanged(float value)
    { //value 0.0f ~ 1.0f �����̵� ���°� ���� �Ǿ��� �� ȣ��Ǵ� �Լ�
        PlayerPrefs.SetFloat("SoundVolume", value);
        Sound_Mgr.Inst.SoundVolume(value);
    }
}
