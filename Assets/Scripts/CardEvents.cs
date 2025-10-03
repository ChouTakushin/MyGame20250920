using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardEvents : MonoBehaviour
{
    private ObjectManager _om;
    private InGameManager _gm;
    private PokerCardData _cardData;
    private CardMove _move;

    // private void Awake()
    // {
    //     _om = GameObject.Find("==GameManager==").GetComponent<ObjectManager>();
    //     _gm = _om.InGameManager;
    //     _cardData = GetComponent<PokerCardData>();
    //     _move = GetComponent<CardMove>();
    // }

    // public void PlayCard(GameObject card1, GameObject card2, PlayerHandLayout hand)
    // {
    //     card1.transform.SetParent(_om.UICanvas.transform);
    //     card2.transform.SetParent(_om.UICanvas.transform);
    //     hand.RefreshCardList();
    //     
    //     _gm._selectedCard.GetComponent<CardSwell>().UnselectThisCard();
    //     _gm._selectedCard = null;
    //     card1.GetComponent<CardMove>().MoveCard(_om.PlayedStack);
    //     card2.GetComponent<CardMove>().MoveCard(_om.PlayedStack);
    // }

    // public IEnumerator DrawThisCard(GameObject cardDrew, GameObject pairCard, PlayerHandLayout drawHand, PlayerHandLayout selfHand)
    // {
    //     bool isPlayerTurn = selfHand.gameObject.CompareTag("PlayerSide");
    //     PlayerHandLayout oppShowView;
    //     
    //     cardDrew.transform.SetParent(_om.UICanvas.transform);
    //     drawHand.RefreshCardList();
    //     Animator cardDrewAnimator = cardDrew.GetComponent<Animator>();
    //     if (_cardData._side == EnumCardSide.Back) cardDrew.GetComponent<CardFlip>().DoFlip();
    //     yield return new WaitUntil(() => cardDrewAnimator.GetCurrentAnimatorStateInfo(0).IsName("None"));
    //     
    //     List<GameObject> pair = CommonUtils.FindPairableInLayout(_cardData);
    //     if (pair.Count > 0)
    //     {
    //         
    //     }
    //     pairCard.transform.SetParent(_om.UICanvas.transform);
    //     selfHand.RefreshCardList();
    //     _move.MoveCard(_om.PlayedStack);
    // }
    //
    // public void PlaySkillEffect(CardSkillBase skill)
    // {
    //     
    // }
}