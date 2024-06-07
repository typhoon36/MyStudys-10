using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogBox_Ctrl : MonoBehaviour
{
    //## 델리케이드 변수
    public delegate void DLT_Response();

    DLT_Response DLT_Method;

    //## 버튼
    public Button Ok_Btn = null;
    public Button Cancel_Btn = null;
    public Button Close_Btn = null;

    //## 텍스트
    public Text Content_Text = null;



    // Start is called before the first frame update
    void Start()
    {
        if(Ok_Btn != null)
        {
            Ok_Btn.onClick.AddListener(() =>
            {
                if (DLT_Method != null)
                    DLT_Method();

                Destroy(gameObject);
            });
        }

        if(Cancel_Btn != null)
        {
            Cancel_Btn.onClick.AddListener(() =>
            {
                Destroy(gameObject);
            });
        }


        if(Close_Btn != null)
        {
            Close_Btn.onClick.AddListener(() =>
            {
                Destroy(gameObject);
            });
        }


    }

    // Update is called once per frame
    void Update()
    {
        
    }




    public void InitMessage(string a_Msg, DLT_Response a_Method = null)
    {
        Content_Text.text = a_Msg;
        DLT_Method = a_Method;
    }





}
