using UnityEngine;
using TriInspector;
using UnityEngine.UI;

[CreateAssetMenu(menuName = "TM/TM_Config", fileName = "TM_Config")]
public class ConfigTM : ScriptableObject {
    [Title("Login")]
    public AudioClip sfx_btn_Interact;
    public float sfx_btn_Interact_Voluem;

    public AudioClip sfx_BGM_Login;
    public float volume_Sfx_Login;

    public Sprite backSceneBG;
    public Sprite backSceneMid;
    public Sprite backSceneFront;

}
