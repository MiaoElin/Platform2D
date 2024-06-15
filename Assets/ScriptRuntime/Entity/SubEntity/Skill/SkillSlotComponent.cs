using UnityEngine;
using System.Collections.Generic;
using System;

public class SkillSlotComponent {

    Dictionary<InputKeyEnum, SkillSubEntity> all;

    public List<InputKeyEnum> usableSkillKeys;
    InputKeyEnum currentSkillKey;

    public SkillSlotComponent() {
        all = new Dictionary<InputKeyEnum, SkillSubEntity>();
        usableSkillKeys = new List<InputKeyEnum>();
    }

    public void Ctor() {
    }

    public void Add(int index, SkillSubEntity skill) {
        var inputKeyEnum = GetInputKeyEnum(index);
        all.Add(inputKeyEnum, skill);
    }

    public void SetCurrentKey(InputKeyEnum currentSkillKey) {
        this.currentSkillKey = currentSkillKey;
    }

    public InputKeyEnum GetCurrentKey() {
        return currentSkillKey;
    }

    public SkillSubEntity GetCurrentSkill() {
        TryGet(GetCurrentKey(), out var skill);
        return skill;
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