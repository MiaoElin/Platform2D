using System;
using UnityEngine;
using UnityEngine.UI;

public class Panel_Pause : MonoBehaviour {

    [SerializeField] Button btn_Restart;
    [SerializeField] Button btn_Exit;

    public Action OnClickRestartHandle;
    public Action OnClickExitHandle;


    public Panel_Pause() {

    }

    public void Ctor() {
        btn_Restart.onClick.AddListener(() => {
            OnClickRestartHandle?.Invoke();
        });

        btn_Exit.onClick.AddListener(() => {
            OnClickExitHandle?.Invoke();
        });
    }
}