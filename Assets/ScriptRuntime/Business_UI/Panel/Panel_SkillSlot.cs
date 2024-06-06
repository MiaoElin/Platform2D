using System;
using UnityEngine;
using UnityEngine.UI;

public class Panel_SkillSlot : MonoBehaviour {

    [SerializeField] Image img_Mask1;
    [SerializeField] Text txt_Time1;

    [SerializeField] Image img_Mask2;
    [SerializeField] Text txt_Time2;

    [SerializeField] Image img_Mask3;
    [SerializeField] Text txt_Time3;

    [SerializeField] Image img_Mask4;
    [SerializeField] Text txt_Time4;


    float mask1TimeMax;
    float mask2TimeMax;
    float mask3TimeMax;
    float mask4TimeMax;

    internal void Ctor(float mask1TimeMax, float mask2TimeMax, float mask3TimeMax, float mask4TimeMax) {
        this.mask1TimeMax = mask1TimeMax;
        this.mask2TimeMax = mask2TimeMax;
        this.mask3TimeMax = mask3TimeMax;
        this.mask4TimeMax = mask4TimeMax;
    }

    internal void CD_Tick(float skill1Time, float skill2Time, float skill3Time, float skill4Time) {
        img_Mask1.fillAmount = SetFillAmount(mask1TimeMax, skill1Time);
        txt_Time1.text = SetTxt_Time(skill1Time);

        img_Mask2.fillAmount = SetFillAmount(mask2TimeMax, skill2Time);
        txt_Time2.text = SetTxt_Time(skill2Time);

        img_Mask3.fillAmount = SetFillAmount(mask3TimeMax, skill3Time);
        txt_Time3.text = SetTxt_Time(skill3Time);

        img_Mask4.fillAmount = SetFillAmount(mask4TimeMax, skill4Time);
        txt_Time4.text = SetTxt_Time(skill4Time);
    }

    public float SetFillAmount(float maskTimeMax, float skillTime) {
        if (maskTimeMax == 0) {
            return 0;
        } else {
            return skillTime / maskTimeMax;
        }
    }

    public string SetTxt_Time(float time) {
        if (time == 0) {
            return "";
        } else {
            return ((int)time + 1).ToString();
        }
    }
}