using UnityEngine;

public class SkillSubEntity {

    public int typeID;
    public int id;
    public float damageRate;
    public int bulletTypeID;

    public SkillCastStateModel castState;

    public SkillSubEntity() {
        castState = new SkillCastStateModel();
    }
}