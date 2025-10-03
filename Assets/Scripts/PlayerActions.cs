using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerActions : MonoBehaviour
{
    [SerializeField] private ObjectManager _om;
    [SerializeField] private PlayerControllerBase _ctrl;

    public void SelectCard(PokerCardData card)
    {
        if (card._isSelected) // カードが選択済みの場合
        {
            if (!CommonUtils.CardBelongsToPlayer(card)) return;
            
            card.GetComponent<CardSwell>()._isHovered = false;
            card._isSelected = false;
            foreach (var pairCard in _ctrl._foundPairs)
            {
                CommonUtils.SwitchCardHighlight(pairCard, false);
            }
            _ctrl._foundPairs.Clear();
            _ctrl._selectedCards.Remove(card);
            return;
        }
        if (_ctrl._selectedCards.Count >= 2)
        {
            Debug.LogError("選択カード数がおかしいぞ");
            return;
        }
        if (_ctrl._selectedCards.Count > 0 && card._rank != _ctrl._selectedCards[0]._rank) return;
        if (_ctrl._selectedCards.Count > 0 && !_ctrl._foundPairs.Contains(card.gameObject)) return;
        _ctrl._selectedCards.Add(card);
        if(_ctrl._selectedCards.Count >= 2)
        {
            PlayCard();
        }
        else
        {
            card._isSelected = true;
            card.GetComponent<CardSwell>()._isHovered = true;
            _ctrl._foundPairs = CommonUtils.FindPairableInLayout(card, _ctrl._hand, _ctrl._showArea, _ctrl._oppShowArea);
            foreach (GameObject pairCard in _ctrl._foundPairs)
            {
                CommonUtils.SwitchCardHighlight(pairCard, true);
            }
        }
    }

    public void UnselectCard(PokerCardData card)
    {
        
    }

    public void DrawCard(PokerCardData card)
    {
        StartCoroutine(DoDrawCard(card));
    }

    public IEnumerator DoDrawCard(PokerCardData card)
    {
        // 親transform変更
        card.transform.SetParent(_om.UICanvas.transform);
        card._currentHand.RefreshCardList();
        yield return card.GetComponent<CardMove>().MoveAndDoAction(_om.CenterPoint, () =>
        {
            card.GetComponent<CardFlip>().FlipCardToSide(EnumCardSide.Front);
        });
        yield return new WaitForSeconds(0.5f);
        // ペアカード検索
        _ctrl._foundPairs = CommonUtils.FindPairableInLayout(card, _ctrl._hand, _ctrl._showArea, null);
        if (_ctrl._foundPairs.Count > 0)
        {
            // ペアある時、カードを選択状態に、ペアをハイライト、次のアクションを待つ
            card._isSelected = true;
            _ctrl._selectedCards.Add(card);
            foreach(GameObject pairCard in _ctrl._foundPairs){
                CommonUtils.SwitchCardHighlight(pairCard, true);
            }
            _om.TurnHandler._gameState = GetStateWaitAction(gameObject.tag);
        }
        else
        {
            // ペアない時、手札に加えてターン終了
            card.GetComponent<CardFlip>().FlipCardToSide(gameObject.CompareTag("PlayerSide") ? EnumCardSide.Front : EnumCardSide.Back);
            yield return card.GetComponent<CardMove>().MoveAndDoAction(_ctrl._hand.gameObject, null);
            _om.TurnHandler._gameState = GetStateTurnEnd(gameObject.tag);
        }
    }

    public void PlayCard()
    {
        foreach (var pairCard in _ctrl._foundPairs)
        {
            CommonUtils.SwitchCardHighlight(pairCard, false);
        }
        StartCoroutine(DoPlayCard());
    }

    public IEnumerator DoPlayCard()
    {
        PokerCardData card1 = _ctrl._selectedCards[0];
        PokerCardData card2 = _ctrl._selectedCards[1];
        card1.transform.SetParent(_om.UICanvas.transform);
        card2.transform.SetParent(_om.UICanvas.transform);
        _ctrl._hand.RefreshCardList();
        _ctrl._oppShowArea.RefreshCardList();
        if(card1._side == EnumCardSide.Back) card1.GetComponent<CardFlip>().DoFlip();
        if(card2._side == EnumCardSide.Back) card2.GetComponent<CardFlip>().DoFlip();
        card1._isSelected = false;
        card2._isSelected = false;
        
        // TODO スキル処理できたらコメント解除
        bool cardHasSkill = CommonUtils.CardHasSkill(card1);
        cardHasSkill = false;
        if (cardHasSkill)
        {
            StartCoroutine(card1.GetComponent<CardMove>().MoveAndDoAction(_om.SkillViewCardPosition, () =>
            {
                // TODO ShowSkillView
            }));
            StartCoroutine(card2.GetComponent<CardMove>().MoveCard(_om.SkillViewCardPosition));
            yield return new WaitUntil(() => _om.SkillView.activeInHierarchy);
            yield return new WaitUntil(() => _om.SkillView.activeInHierarchy == false);
        }
        
        StartCoroutine(card1.GetComponent<CardMove>().MoveCard(_om.PlayedStack));
        yield return card2.GetComponent<CardMove>().MoveAndDoAction(_om.PlayedStack, () =>
        {
            if (cardHasSkill)
            {
                // TODO スキルCoroutine開始
            }
            else
            {
                _om.TurnHandler.EndTurn(gameObject.tag);
            }
        });
        _ctrl._selectedCards.Clear();
        yield return null;
    }

    public EnumGameState GetStateWaitAction(string playerTag)
    {
        if (playerTag == "PlayerSide")
        {
            return EnumGameState.WaitPlayerAction;
        }
        else
        { 
            return EnumGameState.WaitCpuAction;
        }
    }
    public EnumGameState GetStateTurnEnd(string playerTag)
    {
        if (playerTag == "PlayerSide")
        {
            return EnumGameState.PlayerTurnEnd;
        }
        else
        {
            return EnumGameState.CpuTurnEnd;
        }
    }
    public EnumGameState GetStateWaitSkillTarget(string playerTag)
    {
        if (playerTag == "PlayerSide")
        {
            return EnumGameState.WaitPlayerSkillTarget;
        }
        else
        {
            return EnumGameState.WaitCpuSkillTarget;
        }
    }
}