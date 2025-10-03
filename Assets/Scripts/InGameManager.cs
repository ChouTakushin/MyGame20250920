using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

public class InGameManager : MonoBehaviour
{
    [SerializeField]public List<GameObject> _cards;
    [SerializeField]public Transform _centerPoint;
    [SerializeField]public ObjectManager _om;
    [SerializeField]public GameObject _cardPrefab;
    [SerializeField]public Canvas _canvas;
    public GameObject _selectedCard;
    public EnumClickAction _clickAction;
    public TurnHandler _turnHandler;
    public PlayerControllerBase _currentPlayer;

    public delegate void SwitchControlable(bool sw);
    public SwitchControlable SwControlable;
    public EnumUmi _UmiFlg = EnumUmi.None;
    private void Start()
    {
        _cards = new List<GameObject>();
    }

    // private void Update()
    // {
    //     if (Input.GetKeyDown(KeyCode.Space))
    //     {
    //         StartCoroutine(InitRound());
    //     }
    // }

    public IEnumerator InitRound()
    {
        foreach (CardData card in _om.CardDeck.Cards)
        {
            GameObject newCard = Instantiate(_cardPrefab, _centerPoint.position, Quaternion.identity, _canvas.transform);
            PokerCardData pcd = newCard.GetComponent<PokerCardData>();
            pcd.Initialize(card);
            _cards.Add(newCard);
        }
        _cards = _cards.OrderBy(a => Guid.NewGuid()).ToList();
        yield return DealCards();
        yield return new WaitForSeconds(1f);
        yield return FlipPlayerHand();
        yield return new WaitForSeconds(1f);
        _om.TurnHandler._gameState = EnumGameState.PlayerTurnStart;
    }
    
    public IEnumerator DealCards()
    {
        bool handFlg = true;
        GameObject currentHand = _om.PlayerHand;
        
        foreach (GameObject card in _cards)
        {
            currentHand = handFlg ? _om.PlayerHand : _om.CpuHand;
            StartCoroutine(card.GetComponent<CardMove>().MoveAndDoAction(currentHand, null));
            yield return new WaitForSeconds(0.1f);
            handFlg = !handFlg;
        }
    }

    public IEnumerator FlipPlayerHand()
    {
        foreach (CardFlip card in _om.PlayerHand.GetComponentsInChildren<CardFlip>())
        {
            card.DoFlip();
            yield return new WaitForSeconds(0.01f);
        }
    }

    public void SetCurrentPlayer(PlayerControllerBase controller)
    {
        _currentPlayer = controller;
    }

    public bool JudgeGameEnd()
    {
        int playerCardCount = _om.PlayerHand.transform.childCount + _om.PlayerShowArea.transform.childCount;
        int cpuCardCount = _om.CpuHand.transform.childCount + _om.CpuShowArea.transform.childCount;
        // Playerの手札にカードがない場合、勝利
        if (playerCardCount == 0)
        {
            _om.ResultView.SetActive(true);
            _om.ResultText.text = "You win!";
            return true;
        }

        // Cpuの手札にJokerしかない場合、勝利
        if (cpuCardCount == 1)
        {
            PokerCardData onlyCard = _om.CpuHand.transform.childCount == 1 ? _om.CpuHand.transform.GetChild(0).GetComponent<PokerCardData>() : _om.CpuShowArea.transform.GetChild(0).GetComponent<PokerCardData>();
            if (onlyCard._rank == 0)
            {
                _om.ResultView.SetActive(true);
                _om.ResultText.text = "You win!";
                return true;
            }
        }
        
        // Cpuの手札にカードがない場合、敗北
        if (cpuCardCount == 0)
        {
            _om.ResultView.SetActive(true);
            _om.ResultText.text = "You lose!";
            return true;
        }

        // Playerの手札にJokerしかない場合、勝利
        if (cpuCardCount == 1)
        {
            PokerCardData onlyCard = _om.CpuHand.transform.childCount == 1 ? _om.CpuHand.transform.GetChild(0).GetComponent<PokerCardData>() : _om.CpuShowArea.transform.GetChild(0).GetComponent<PokerCardData>();
            if (onlyCard._rank == 0)
            {
                _om.ResultView.SetActive(true);
                _om.ResultText.text = "You lose!";
                return true;
            }
        }

        return false;
    }

    public void JudgeUmi()
    {
        if (_om.CpuHand.transform.childCount == 2)
        {
            foreach (Transform card in _om.CpuHand.transform)
            {
                if (card.gameObject.GetComponent<PokerCardData>()._rank == 0)
                {
                    _om.InGameManager._UmiFlg = EnumUmi.Start;
                    _om.UIController.SetUmiActivate(true);
                    break;
                }
            }
        }
        else
        {
            _om.InGameManager._UmiFlg = EnumUmi.None;
            _om.UIController.SetUmiActivate(false);
        }
    }
    public void RestartGame()
    {
        SceneManager.LoadScene("InGame", LoadSceneMode.Single);
    }
}