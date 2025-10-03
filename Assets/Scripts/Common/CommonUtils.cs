using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CommonUtils
{
    // public static List<GameObject> FindPairableInLayout(PokerCardData card, PlayerHandLayout selfHand, ObjectManager om)
    // {
    //     List<GameObject> rst = new List<GameObject>();
    //     PlayerHandLayout oppShowView;
    //     if (selfHand.gameObject.CompareTag("PlayerSide"))
    //     {
    //         oppShowView = om.CpuShowArea.GetComponent<PlayerHandLayout>();
    //     }
    //     else
    //     {
    //         oppShowView = om.PlayerShowArea.GetComponent<PlayerHandLayout>();
    //     }
    //     foreach (Transform child in selfHand.transform)
    //     {
    //         if (ReferenceEquals(child.gameObject, card.gameObject) == false 
    //             && child.gameObject.GetComponent<PokerCardData>()._rank == card._rank)
    //         {
    //             rst.Add(child.gameObject);
    //         }
    //     }
    //     foreach (Transform child in oppShowView.transform)
    //     {
    //         if (child.gameObject.GetComponent<PokerCardData>()._rank == card._rank)
    //         {
    //             rst.Add(child.gameObject);
    //         }
    //     }
    //     return rst;
    // }
    public static List<GameObject> FindPairableInLayout(PokerCardData card, PlayerHandLayout selfHand, PlayerHandLayout selfShowArea, PlayerHandLayout oppShowArea)
    {
        List<GameObject> rst = new List<GameObject>();
        foreach (Transform child in selfHand.transform)
        {
            if (ReferenceEquals(child.gameObject, card.gameObject) == false 
                && child.gameObject.GetComponent<PokerCardData>()._rank == card._rank)
            {
                rst.Add(child.gameObject);
            }
        }
        foreach (Transform child in selfShowArea.transform)
        {
            if (ReferenceEquals(child.gameObject, card.gameObject) == false 
                && child.gameObject.GetComponent<PokerCardData>()._rank == card._rank)
            {
                rst.Add(child.gameObject);
            }
        }
        if (oppShowArea != null){
            foreach (Transform child in oppShowArea.transform)
            {
                if (ReferenceEquals(child.gameObject, card.gameObject) == false 
                    && child.gameObject.GetComponent<PokerCardData>()._rank == card._rank)
                {
                    rst.Add(child.gameObject);
                }
            }
        }
        return rst;
    }

    public static GameObject GetSelectedCard(ObjectManager om)
    {
        return om.InGameManager._selectedCard;
    }

    public static bool CardBelongsToPlayer(PokerCardData card)
    {
        return card._currentHand.gameObject.CompareTag("PlayerSide");
    }

    public static bool CardBelongsToSelf(PlayerControllerBase self, PokerCardData card)
    {
        return self.gameObject.CompareTag(card._currentHand.gameObject.tag);
    }

    public static bool CardHasSkill(PokerCardData card)
    {
        if (card._rank == 1 || card._rank == 11 || card._rank == 12 || card._rank == 13)
        {
            return true;
        }
        return false;
    }

    public static void SwitchCardHighlight(GameObject card, bool sw)
    {
        Color color = sw ? Color.yellow : Color.white;
        card.GetComponent<PokerCardData>()._frontImage.color = color;
    }

    public static IEnumerator WaitAndDoAction(float sec, Action action)
    {
        yield return new WaitForSeconds(sec);
        action.Invoke();
    }
}