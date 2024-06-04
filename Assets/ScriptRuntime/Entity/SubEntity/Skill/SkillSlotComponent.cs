using UnityEngine;
using System.Collections.Generic;

public class SkillSlotComponent {

    Dictionary<InputKeyEnum, SkillSubEntity> all;

    public SkillSlotComponent() {
        all = new Dictionary<InputKeyEnum, SkillSubEntity>();
    }

    public void Ctor() {
    }

    public void Add(int index, SkillSubEntity skill) {
        var inputKeyEnum = GetInputKeyEnum(index);
        all.Add(inputKeyEnum, skill);
    }

    public InputKeyEnum GetInputKeyEnum(int index) {
        if (index == 0) {
            return InputKeyEnum.SKill1;
        } else if (index == 1) {
            return InputKeyEnum.SKill2;
        } else if (index == 2) {
            return InputKeyEnum.SKill3;
        } else if (index == 3) {
            return InputKeyEnum.Skill4;
        } else {
            return InputKeyEnum.None;
        }
    }
}