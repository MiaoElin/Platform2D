using UnityEngine;
using System.Collections.Generic;

public class InputEntity {

    public Vector2 moveAxis;

    public bool isJumpKeyDown;
    public bool isInteractKeyDown;

    public List<InputKeyEnum> waitToCastSkills;

    public InputEntity() {
        waitToCastSkills = new List<InputKeyEnum>();
    }

    public void Process() {
        moveAxis = Vector2.zero;
        if (Input.GetKey(KeyCode.LeftArrow)) {
            moveAxis.x = -1;
        } else if (Input.GetKey(KeyCode.RightArrow)) {
            moveAxis.x = 1;
        }

        if (Input.GetKey(KeyCode.UpArrow)) {
            moveAxis.y = 1;
        } else if (Input.GetKey(KeyCode.DownArrow)) {
            moveAxis.y = -1;
        }
        moveAxis.Normalize();

        // Jump
        isJumpKeyDown = Input.GetKeyDown(KeyCode.Space);
        isInteractKeyDown = Input.GetKeyDown(KeyCode.U);

        waitToCastSkills.Clear();

        if (Input.GetKey(KeyCode.J)) {
            AddSkillkey(InputKeyEnum.SKill1);
        } else {
            RemoveSkillKey(InputKeyEnum.SKill1);
        }

        if (Input.GetKey(KeyCode.K)) {
            AddSkillkey(InputKeyEnum.SKill2);
        } else {
            RemoveSkillKey(InputKeyEnum.SKill2);
        }

        if (Input.GetKey(KeyCode.L)) {
            AddSkillkey(InputKeyEnum.SKill3);
        } else {
            RemoveSkillKey(InputKeyEnum.SKill3);
        }

        // if (Input.GetKey(KeyCode.V)) {
        //     AddSkillkey(InputKeyEnum.Skill4);
        // } else {
        //     RemoveSkillKey(InputKeyEnum.Skill4);
        // }

    }


    public void AddSkillkey(InputKeyEnum inputKeyEnum) {
        if (!waitToCastSkills.Contains(inputKeyEnum)) {
            waitToCastSkills.Add(inputKeyEnum);
        }
    }

    public void RemoveSkillKey(InputKeyEnum inputKeyEnum) {
        if (waitToCastSkills.Contains(inputKeyEnum)) {
            waitToCastSkills.Remove(inputKeyEnum);
        }
    }
}