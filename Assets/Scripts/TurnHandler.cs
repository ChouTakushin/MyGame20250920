using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnHandler : MonoBehaviour
{
    [SerializeField]public ObjectManager _om;
    private InGameManager _gm;
    public EnumGameState _gameState = EnumGameState.GameStart;

    private void Start()
    {
        _gm = _om.InGameManager;
    }

    private void Update()
    {
        switch (_gameState)
        {
            case EnumGameState.GameStart:
                _gameState = EnumGameState.Busy;
                // TODO Play Start Animation
                _gameState = EnumGameState.RoundSetup;
                break;
            case EnumGameState.RoundSetup:
                _gameState = EnumGameState.Busy;
                StartCoroutine(_gm.InitRound());
                break;
            case EnumGameState.PlayerTurnStart:
                _gameState = EnumGameState.Busy;
                // TODO Play PlayerTurn Animation
                _gm.SetCurrentPlayer(_om.PlayerController);
                _gm.JudgeUmi();
                _gameState = EnumGameState.WaitPlayerAction;
                break;
            case EnumGameState.PlayerTurnEnd:
                // TODO 
                if (_gm.JudgeGameEnd())
                {
                    _gameState = EnumGameState.GameEnd;
                    break;
                }
                _gameState = EnumGameState.CpuTurnStart;
                break;
            case EnumGameState.CpuTurnStart:
                _gm.SetCurrentPlayer(_om.CpuController);
                _gm._currentPlayer.GetComponent<CpuController>().OnTurnStart();
                _gameState = EnumGameState.WaitCpuAction;
                break;
            case EnumGameState.WaitCpuAction:
                _gameState = EnumGameState.Busy;
                _gm._currentPlayer.GetComponent<CpuController>().PerformAction();
                break;
            case EnumGameState.CpuTurnEnd:
                _gameState = EnumGameState.Busy;
                _gm._currentPlayer.GetComponent<CpuController>().OnTurnEnd();
                if (_gm.JudgeGameEnd())
                {
                    _gameState = EnumGameState.GameEnd;
                    break;
                }
                _gameState = EnumGameState.PlayerTurnStart;
                break;
            case EnumGameState.WaitSkillEnd:
            case EnumGameState.WaitPlayerAction:
            case EnumGameState.ShowSkillInfo:
            case EnumGameState.Busy:
            default:
                break;
        }
    }

    public void EndTurn(string playerTag)
    {
        if (playerTag == "PlayerSide")
        {
            _gameState = EnumGameState.PlayerTurnEnd;
        }
        else
        {
            _gameState = EnumGameState.CpuTurnEnd;
        }
    }
}