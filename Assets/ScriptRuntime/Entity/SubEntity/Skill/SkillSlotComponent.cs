using UnityEngine;
using System.Collections.Generic;

public class SkillSlotComponent {

    Dictionary<InputKeyEnum, SkillSubEntity> all;

    public SkillSlotComponent() {
        all = new Dictionary<InputKeyEnum, SkillSubEntity>();
    }

    public void Ctor() {
        
    }

}