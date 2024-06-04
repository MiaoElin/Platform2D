using UnityEngine;
using System.Collections.Generic;
using System;

public class SkillSlotComponent {

    Dictionary<InputKeyEnum, SkillSubEntity> all;
    public List<InputKeyEnum> waitToCastKeys;

    public SkillSlotComponent() {
        all = new Dictionary<InputKeyEnum, SkillSubEntity>();
        waitToCastKeys = new List<InputKeyEnum>();
    }

    public void Ctor() {
    }

    public void Add(int index, SkillSubEntity skill) {
        var inputKeyEnum = GetInputKeyEnum(index);
        all.Add(inputKeyEnum, skill);
    }

    public void AddCastKey(InputKeyEnum inputKeyEnum) {
        bool has = waitToCastKeys.Contains(inputKeyEnum);
        if (!has) {
            waitToCastKeys.Add(inputKeyEnum);
        }
    }

    public InputKeyEnum GetLastKey() {
        // Debug.Log(waitToCastKeys.Count);
        return waitToCastKeys[waitToCastKeys.Count - 1];
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

    public bool TryGet(InputKeyEnum key, out SkillSubEntity skill) {
        return all.TryGetValue(key, out skill);
    }

    public void Foreach(Action<SkillSubEntity> action) {
        foreach (var value in all) {
            action.Invoke(value.Value);
        }
    }
}