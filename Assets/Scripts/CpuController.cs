using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Random = UnityEngine.Random;

public class CpuController : PlayerControllerBase
{
    public PokerCardData _skillCardInOppShow;
    public List<PokerCardData> _pairInHand;
    public EnumCpuStrategy _strategy = EnumCpuStrategy.None;
    
    public override void OnTurnStart()
    {
        _strategy = GetStrategy();
    }

    public void PerformAction()
    {
        switch (_strategy)
        {
            case EnumCpuStrategy.DrawCardFromOppShow:
                StartCoroutine(DoDrawCardFromOppShow(_skillCardInOppShow));
                break;
            case EnumCpuStrategy.DrawCardFromOppHand:
                StartCoroutine(DoDrawCardFromOppHand());
                break;
            case EnumCpuStrategy.PlayPair:
                StartCoroutine(DoPlayPair());
                break;
            default:
                break;
        }
    } 
    
    public override void SelectCard(PokerCardData card)
    {
        _action.SelectCard(card);
    }

    public override void DrawCard(PokerCardData card)
    {
        _action.DrawCard(card);
    }

    public EnumCpuStrategy GetStrategy()
    {
        _skillCardInOppShow = FindSkillCardInHand(_oppShowArea);
        _pairInHand = FindPairInHand();
        if (_skillCardInOppShow != null)
        {
            return EnumCpuStrategy.DrawCardFromOppShow;
        }
        if (_pairInHand != null && _pairInHand.Count >= 2)
        {
            return EnumCpuStrategy.PlayPair;
        }
        return EnumCpuStrategy.DrawCardFromOppHand;
    }

    public List<PokerCardData> FindPairInHand()
    {
        List<PokerCardData> selfHandCards = new List<PokerCardData>();
        foreach (Transform card in _hand.transform)
        {
            selfHandCards.Add(card.GetComponent<PokerCardData>());
        }
        foreach (Transform card in _showArea.transform)
        {
            selfHandCards.Add(card.GetComponent<PokerCardData>());
        }

        for (int i = 0; i < selfHandCards.Count; i++)
        {
            for (int j = i + 1; j < selfHandCards.Count; j++)
            {
                if (selfHandCards[i]._rank == selfHandCards[j]._rank)
                {
                    List<PokerCardData> pairCards = new List<PokerCardData>();
                    pairCards.Add(selfHandCards[i]);
                    pairCards.Add(selfHandCards[j]);
                    return pairCards;
                }
            }
        }
        return null;
    }

    public PokerCardData FindSkillCardInHand(PlayerHandLayout hand)
    {
        foreach (Transform card in hand.transform)
        {
            PokerCardData cardData = card.GetComponent<PokerCardData>();
            if (cardData._rank == 1 || cardData._rank == 11 || cardData._rank == 12 || cardData._rank == 13)
            {
                return cardData;
            }
        }
        return null;
    }

    public GameObject GetRandomCardFromHand(PlayerHandLayout hand)
    {
        int childIndex = Random.Range(0, hand.transform.childCount);
        return hand.transform.GetChild(childIndex).gameObject;
    }

    public IEnumerator DoDrawCardFromOppShow(PokerCardData card)
    {
        _action.DrawCard(card);
        yield return new WaitUntil(() =>
        {
            return _om.TurnHandler._gameState == EnumGameState.WaitCpuAction ||
                   _om.TurnHandler._gameState == EnumGameState.CpuTurnEnd;
        });
        if (_om.TurnHandler._gameState == EnumGameState.WaitCpuAction)
        {
            GameObject cardToPlay = _foundPairs[Random.Range(0, _foundPairs.Count)];
            _action.SelectCard(cardToPlay.GetComponent<PokerCardData>());
        }
    }

    public IEnumerator DoDrawCardFromOppHand()
    {
        if(_selectedCards.Count == 0)
        {
            _action.DrawCard(GetRandomCardFromHand(_oppHand).GetComponent<PokerCardData>());
        }
        yield return new WaitForSeconds(0.5f);
        if (_selectedCards.Count > 0)
        {
            _action.SelectCard(_foundPairs[Random.Range(0, _foundPairs.Count)].GetComponent<PokerCardData>());
        }
    }
    public IEnumerator DoPlayPair()
    {
        if (_pairInHand == null || _pairInHand.Count < 2)
        {
            Debug.LogError("DoPlayPair()：ペアカードの数がおかしいぞ");
            yield break;
        }
        _action.SelectCard(_pairInHand[0]);
        yield return new WaitForSeconds(1f);
        if (_pairInHand[0]._side == EnumCardSide.Back)
        {
            _pairInHand[0].GetComponent<CardFlip>().DoFlip();
        }
        if (_pairInHand[1]._side == EnumCardSide.Back)
        {
            _pairInHand[1].GetComponent<CardFlip>().DoFlip();
        }
        _action.SelectCard(_pairInHand[1]);
    }

    public void OnTurnEnd()
    {
        _strategy = EnumCpuStrategy.None;
        _skillCardInOppShow = null;
        _pairInHand = null;
    }
}