using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardFlip : MonoBehaviour
{
    [SerializeField] private Animator _animator;
    [SerializeField] private PokerCardData _cardData;

    public void DoFlip()
    {
        _animator.Play("Flip");
    }

    public void SwitchImage()
    {
        if (_cardData._side == EnumCardSide.Back)
        {
            _cardData._frontImage.gameObject.SetActive(true);
            _cardData._backImage.gameObject.SetActive(false);
            _cardData._side = EnumCardSide.Front;
        }
        else if (_cardData._side == EnumCardSide.Front)
        {
            _cardData._frontImage.gameObject.SetActive(false);
            _cardData._backImage.gameObject.SetActive(true);
            _cardData._side = EnumCardSide.Back;
        }
    }

    public void FlipCardToSide(EnumCardSide side)
    {
        if (side != _cardData._side)
        {
            _animator.Play("Flip");
        }
    }
}