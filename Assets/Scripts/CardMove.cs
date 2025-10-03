using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardMove : MonoBehaviour
{
    [SerializeField] public float _moveSpeed = 10f;
    [SerializeField] public PokerCardData _cardData;
    private PlayerHandLayout _hand;
    private ObjectManager _om;
    public bool _moving = false;

    private void Start()
    {
        _om = GameObject.Find("==GameManager==").GetComponent<ObjectManager>();
    }

    // public void MoveCard(GameObject dest)
    // {
    //     _hand = dest.GetComponent<PlayerHandLayout>();
    //     _moving = true;
    //     _movingDest = dest;
    //     transform.localPosition = Vector3.Lerp(transform.localPosition, dest.transform.localPosition, Time.deltaTime * _moveSpeed);
    //     if (Vector3.Distance(transform.localPosition, dest.transform.localPosition) <= 0.5f)
    //     {
    //         _moving = false;
    //         SetParent(_hand.transform);
    //         if(_hand != null){
    //             _hand.RefreshCardList();
    //             _hand.UpdateLayout(true);
    //         }
    //         else
    //         {
    //             transform.localPosition = Vector3.zero;
    //         }
    //     }
    // }
    public IEnumerator MoveAndDoAction(GameObject dest, Action callback)
    {
        // SetParent(_om.UICanvas.transform);
        _hand = dest.GetComponent<PlayerHandLayout>();
        _moving = true;
        while (Vector3.Distance(transform.position, dest.transform.position) > 0.5f)
        {
            transform.position = Vector3.Lerp(transform.position, dest.transform.position, Time.deltaTime * _moveSpeed);
            yield return null;
        }
        _moving = false;
        SetParent(dest.transform);
        if(_hand != null){
            _hand.RefreshCardList();
            _hand.UpdateLayout(true);
        }
        else
        {
            transform.localPosition = Vector3.zero;
        }
        callback?.Invoke();
    }
    public IEnumerator MoveCard(GameObject dest)
    {
        // SetParent(_om.UICanvas.transform);
        _hand = dest.GetComponent<PlayerHandLayout>();
        _moving = true;
        while (Vector3.Distance(transform.position, dest.transform.position) > 0.5f)
        {
            transform.position = Vector3.Lerp(transform.position, dest.transform.position, Time.deltaTime * _moveSpeed);
            yield return null;
        }
        _moving = false;
        SetParent(dest.transform);
        if(_hand != null){
            _hand.RefreshCardList();
            _hand.UpdateLayout(true);
        }
        else
        {
            transform.localPosition = Vector3.zero;
        }
    }
    // public void MoveCard(GameObject dest)
    // {
    //     _hand = dest.GetComponent<PlayerHandLayout>();
    //     _moving = true;
    //     _movingDest = dest;
    //     transform.position = Vector3.Lerp(transform.position, dest.transform.position, Time.deltaTime * _moveSpeed);
    //     if (Vector3.Distance(transform.position, dest.transform.position) <= 0.5f)
    //     {
    //         _moving = false;
    //         SetParent(dest.transform);
    //         if(_hand != null){
    //             _hand.RefreshCardList();
    //             _hand.UpdateLayout(true);
    //         }
    //         else
    //         {
    //             transform.localPosition = Vector3.zero;
    //         }
    //     }
    // }

    // public void MoveToTransform(GameObject dest)
    // {
    //     _moving = true;
    //     _movingDest = dest;
    //     transform.localPosition = Vector3.Lerp(transform.localPosition, dest.transform.localPosition, Time.deltaTime * _moveSpeed);
    //     if (Vector3.Distance(transform.localPosition, dest.transform.localPosition) <= 0.5f)
    //     {
    //         _moving = false;
    //         SetParent(dest.transform);
    //     }
    // }
    
    public void MoveToHandInstantly(Transform dest)
    {
        transform.position = dest.position;
    }

    public void SetParent(Transform parent, int index = -99)
    {
        transform.SetParent(parent);
        if (index != -99)
        {
            transform.SetSiblingIndex(index);
        }
        else
        {
            transform.SetAsLastSibling();
        }
        _cardData.UpdateParentHand(parent.GetComponent<PlayerHandLayout>());
    }

    private void FixedUpdate()
    {
        // if (_moving)
        // {
        //     MoveCard(_movingDest);
        // }
    }
}