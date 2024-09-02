using System;
using UnityEngine;
using UnityEngine.UI;

public class Panel_Pause : MonoBehaviour {

    [SerializeField] Button btn_Restart;
    [SerializeField] Button btn_Resume;
    [SerializeField] Button btn_Exit;

    public Action OnClickRestartHandle;
    public Action OnClickResumeHandle;
    public Action OnClickExitHandle;


    public Panel_Pause() {

    }

    public void Ctor() {
        btn_Restart.onClick.AddListener(() => {
            OnClickRestartHandle?.Invoke();
        });

        btn_Resume.onClick.AddListener(() => {
            OnClickResumeHandle.Invoke();
        });

        btn_Exit.onClick.AddListener(() => {
            OnClickExitHandle?.Invoke();
        });
    }
}