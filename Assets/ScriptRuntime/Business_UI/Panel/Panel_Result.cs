using UnityEngine;
using UnityEngine.UI;
using System;

public class Panel_Result : MonoBehaviour {

    [SerializeField] Text title;
    [SerializeField] public Button btn_NewGame;
    [SerializeField] Button btn_Exit;

    public Action OnNewGameClickHandle;
    public Action OnExitClickHandle;

    public void Ctor() {
        btn_NewGame.onClick.AddListener(() => {
            OnNewGameClickHandle.Invoke();
        });

        btn_Exit.onClick.AddListener(() => {
            OnExitClickHandle.Invoke();
        });
    }

    public void SetTitle(bool isWin) {
        if (isWin) {
            title.text = "WIN!";
        } else {
            title.text = "LOSE!";
        }
    }

}