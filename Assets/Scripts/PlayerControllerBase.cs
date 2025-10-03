using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class PlayerControllerBase : MonoBehaviour
{
    [SerializeField] protected ObjectManager _om;
    [SerializeField] public PlayerHandLayout _hand;
    [SerializeField] public PlayerHandLayout _showArea;
    [SerializeField] public PlayerHandLayout _oppHand;
    [SerializeField] public PlayerHandLayout _oppShowArea;
    [SerializeField] public PlayerActions _action;
    public List<PokerCardData> _selectedCards = new List<PokerCardData>();
    public List<GameObject> _foundPairs;

    public abstract void OnTurnStart();
    public abstract void SelectCard(PokerCardData card);
    public abstract void DrawCard(PokerCardData card);
}