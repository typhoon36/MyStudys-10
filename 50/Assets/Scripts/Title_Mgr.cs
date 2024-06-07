using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Title_Mgr : MonoBehaviour
{
    public Button StartBtn;

    // Start is called before the first frame update
    void Start()
    {
        StartBtn.onClick.AddListener(StartClick);

        Sound_Mgr.Inst.PlayBGM("sound_bgm_title_001", 0.2f);

       
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void StartClick()
    {
        
        bool ISFadeIn = false;

        if(Fade_Mgr.Inst != null)
        {
            ISFadeIn= Fade_Mgr.Inst.SceneOutReserve("LobbyScene");
        }
        else
        {
            SceneManager.LoadScene("LobbyScene");
        }

        
    }
}
