using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PokerCardData : MonoBehaviour
{
    [SerializeField] private ObjectManager _om;
    [SerializeField] public Image _frontImage;
    [SerializeField] public Image _backImage;
    [SerializeField] public EnumCardSuit _suit;
    [SerializeField] public int _rank;
    [SerializeField] public EnumCardSide _side;
    public PlayerHandLayout _currentHand;
    // public bool _controlable;
    public bool _isSelected = false;
    public int _showTurn = 0;

    private void Start()
    {
        _om = GameObject.Find("==GameManager==").GetComponent<ObjectManager>();
    }

    public void Initialize(CardData data)
    {
        _frontImage.sprite = data.FrontImage;
        _backImage.sprite = data.BackImage;
        _suit = data.Suit;
        _rank = data.Rank;
        _side = EnumCardSide.Back;
        // _controlable = false;
        // _om.InGameManager.SwControlable += OnSwitchControlable;
    }

    // public void OnSwitchControlable(bool sw)
    // {
    //     _controlable = sw;
    // }
    
    public void UpdateParentHand(PlayerHandLayout hand)
    {
        _currentHand = hand;
    }
}