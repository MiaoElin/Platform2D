using UnityEngine;
using UnityEngine.UI;

public class Panel_Tips : MonoBehaviour {

    [SerializeField] Button btn_Close;

    public void Ctor() {
        btn_Close.onClick.AddListener(() => {
            gameObject.SetActive(false);
            Time.timeScale = 1;
        });
    }
}
