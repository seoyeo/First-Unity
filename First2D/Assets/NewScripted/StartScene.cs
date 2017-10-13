using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class StartScene : MonoBehaviour {

    public GameObject SettingButton;
    public GameObject SettingView;
    public GameObject StartGame;
    public Toggle MusicToggle;
    public AudioSource StartBackMusic;

    public delegate void MusicOpen();
    public MusicOpen OpenMusic;

    public delegate void MusicClose();
    public MusicClose CloseMusic;
	// Use this for initialization
	void Start () {
        SettingView.SetActive(false);
	}
	
	// Update is called once per frame
	void Update () {
        MusicToggle.onValueChanged.AddListener(MusicSetting);
	}

#region 开始游戏
    public void LoadGame()
    {
        SceneManager.LoadScene("level");
    }
    #endregion

#region 打开设置
    public void OpenSettiing()
    {
        SettingView.SetActive(true);
        SettingButton.SetActive(false);
        StartGame.SetActive(false);
    }
    #endregion

#region 关闭设置
    public void CloseSetting()
    {
        SettingView.SetActive(false);
        SettingButton.SetActive(true);
        StartGame.SetActive(true);
    }
    #endregion

#region 音效开关
    public void MusicSetting(bool isCheck)
    {
        if (isCheck)//声音开
        {
            if (this.OpenMusic != null)
            {
                this.OpenMusic();
            }
                StartBackMusic.Play();
        }else if(isCheck == false)//声音关
        {
            if (this.CloseMusic != null)
            {
                this.CloseMusic();
            }
                StartBackMusic.Stop();
        }
    }
#endregion

}
