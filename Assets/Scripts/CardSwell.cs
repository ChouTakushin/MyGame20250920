using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class CardSwell : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
{
    private ObjectManager _om;
    private InGameManager _gm;
    private CardEvents _ev;
    public bool _isHovered { get; set; } = false;
    private int originalSibling;
    public PokerCardData _cardData;
    public GameObject pairCard;
    public List<GameObject> pairs;

    private void Start()
    {
        _om = GameObject.Find("==GameManager==").GetComponent<ObjectManager>();
        _gm = _om.InGameManager;
        _ev = GetComponent<CardEvents>();
        _cardData = GetComponent<PokerCardData>();
        pairCard = null;
    }
    public void Init(PlayerHandLayout layout)
    {
        _cardData._currentHand = layout;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (_om.TurnHandler._gameState != EnumGameState.WaitPlayerAction || !_gm._currentPlayer.gameObject.CompareTag("PlayerSide")) return;
        if (_gm._UmiFlg != EnumUmi.None && _gm._UmiFlg != EnumUmi.Finished)
        {
            if (_cardData._rank == 0)
            {
                _gm._UmiFlg = EnumUmi.Happy;
                _om.UIController.SwitchUmi(EnumUmi.Happy);
            }
            else
            {
                _gm._UmiFlg = EnumUmi.Danger;
                _om.UIController.SwitchUmi(EnumUmi.Danger);
            }
        }
        // if (!_cardData._controlable) return;
        // originalSibling = transform.GetSiblingIndex();
        // transform.SetAsLastSibling();
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (_om.TurnHandler._gameState != EnumGameState.WaitPlayerAction || !_gm._currentPlayer.gameObject.CompareTag("PlayerSide")) return;
        if (_gm._UmiFlg != EnumUmi.None && _gm._UmiFlg != EnumUmi.Finished)
        {
            _om.UIController.SwitchUmi(EnumUmi.Start);
        }
        // if (_cardData._controlable == false || _isSelected) return;
        // isHovered = false;
        // transform.SetSiblingIndex(originalSibling);
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (eventData.button != PointerEventData.InputButton.Left) return;
        if (_gm._currentPlayer._selectedCards.Count != 0 && !_cardData._currentHand.gameObject.CompareTag(_gm._currentPlayer.gameObject.tag)) return;
        if (_gm._UmiFlg == EnumUmi.Danger)
        {
            _gm._UmiFlg = EnumUmi.Finished;
            _om.UIController.SwitchUmi(EnumUmi.Finished);
            _om.UmiImage.GetComponent<Animator>().Play("shake");
        }
        OnCardClicked(eventData);
    }

    public void OnCardClicked(PointerEventData eventData)
    {
        if (_om.TurnHandler._gameState != EnumGameState.WaitPlayerAction
            && _om.TurnHandler._gameState != EnumGameState.WaitPlayerSkillTarget) return;
        if (CommonUtils.CardBelongsToPlayer(_cardData))
        {
            _gm._currentPlayer.SelectCard(_cardData);
        }
        else
        {
            _gm._currentPlayer.DrawCard(_cardData);
        }
        
        // 選択済みカードの場合、非選択にする
        // if (_cardData._isSelected)　
        // {
        //     _gm._currentPlayer.SelectCard(_cardData);
        // }
        // else // 選択済みの場合
        // {
        //     _gm._currentPlayer.SelectCard(_cardData);
            // GameObject selectedCard = CommonUtils.GetSelectedCard(_om);
            // // カードがプレイヤー側のものの場合
            // if (CommonUtils.CardBelongsToPlayer(_cardData))
            // {
            //     if(selectedCard == null)
            //     {
            //         SelectThisCard();
            //     }
            //     else if (_cardData._rank == selectedCard.GetComponent<PokerCardData>()._rank)
            //     {
            //         // TODO プレイ処理
            //         _ev.PlayCard();
            //     }
            // }
            // else
            // {
            //     if (selectedCard == null)
            //     {
            //         // TODO ドロー処理
            //         // TODO プレイ処理
            //         Debug.Log("DRAW AND PLAY THE CARD!!!");
            //     }
            // }
        // }
        
    }
    // public void UnselectThisCard()
    // {
    //     _isHovered = false;
    //     foreach (var card in pairs)
    //     {
    //         PokerCardData cardData = card.GetComponent<PokerCardData>();
    //         cardData._frontImage.color = Color.white;
    //     }
    //     pairs.Clear();
    //     _gm._selectedCard = null;
    //     _cardData._isSelected = false;
    // }

    // public void SelectThisCard()
    // {
    //     // カードを選択済みにする
    //     _isHovered = true;
    //     // ペアを検索してハイライト
    //     pairs = CommonUtils.FindPairableInLayout(_cardData, _om.);
    //     foreach (var card in pairs)
    //     {
    //         PokerCardData cardData = card.GetComponent<PokerCardData>();
    //         cardData._frontImage.color = Color.yellow;
    //     }
    //     _gm._selectedCard = this.gameObject;
    //     _isSelected = true;
    // }
}