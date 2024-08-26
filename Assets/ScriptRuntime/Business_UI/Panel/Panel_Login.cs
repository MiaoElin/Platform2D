using UnityEngine;
using UnityEngine.UI;
using System;

public class Panel_Login : MonoBehaviour {

    [SerializeField] Button btn_Start;
    [SerializeField] Button btn_Setting;
    [SerializeField] Button btn_Exit;

    public Action OnStarClickHandle;
    public Action OnSettingClickHandle;
    public Action OnExitHandle;

    public AudioClip clip;

    public void Ctor() {
        btn_Start.onClick.AddListener(() => {
            OnStarClickHandle.Invoke();
        });

        btn_Setting.onClick.AddListener(() => {
            OnSettingClickHandle.Invoke();
        });

        btn_Exit.onClick.AddListener(() => {
            OnExitHandle.Invoke();
        });
    }

    public void Hide() {
        gameObject.SetActive(false);
    }

    public void Show() {
        gameObject.SetActive(true);
    }

}
