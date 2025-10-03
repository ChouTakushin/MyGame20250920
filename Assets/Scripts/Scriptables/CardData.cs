using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(menuName = "Old Maid/CardData")]
public class CardData : CardDataBase
{
    public Sprite FrontImage;
    public EnumCardSuit Suit;
    public int Rank;
    public EnumCardSide Side;
}