using UnityEngine;
using UnityEngine.UI;
using System;

public class Panel_Result : MonoBehaviour {

    [SerializeField] Button btn_Restart;
    [SerializeField] Button btn_BackMenu;

    public Action OnRestartClickHandle;
    public Action OnBackMenuClickHandle;

    public void Ctor() {
        btn_Restart.onClick.AddListener(() => {
            OnRestartClickHandle.Invoke();
        });

        btn_BackMenu.onClick.AddListener(() => {
            OnBackMenuClickHandle.Invoke();
        });
    }
}