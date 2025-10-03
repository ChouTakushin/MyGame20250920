using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum EnumGameState
{
    GameStart,
    RoundSetup,
    PlayerTurnStart,
    WaitPlayerAction,
    PlayerTurnEnd,
    CpuTurnStart,
    WaitCpuAction,
    CpuTurnEnd,
    ShowSkillInfo,
    WaitPlayerConfirm,
    WaitSkillEnd,
    PlaySkillEffect,
    WaitPlayerSkillTarget,
    WaitCpuSkillTarget,
    ApplySkillEffect,
    CheckSkillEffect,
    Busy,
    GameEnd
}