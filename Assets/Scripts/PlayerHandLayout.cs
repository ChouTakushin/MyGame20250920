using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine;
using UnityEngine.EventSystems;
using System.Collections.Generic;

public class PlayerHandLayout : MonoBehaviour
{
    [Header("卡牌大小与区域限制")]
    public float cardWidth = 123.2f;    // 单张卡牌宽度
    public float maxWidth = 800f;     // 手牌栏最大宽度

    [Header("弧形效果")]
    public float arcHeight = 30f;     // 弧形高度
    public float maxRotation = 15f;   // 最大旋转角度

    [Header("动画参数")]
    public float moveSpeed = 10f;     // 位置插值速度
    public float scaleUp = 1.2f;      // 悬停时放大倍数
    public float hoverOffset = 40f;   // 悬停时上移偏移量

    private List<CardSwell> cards = new List<CardSwell>();
    public GameObject _selectedCard;
    
    void Start()
    {
        RefreshCardList();
        UpdateLayout(true);
    }

    void Update()
    {
        UpdateLayout(true);
    }

    public void RefreshCardList()
    {
        cards.Clear();
        foreach (Transform child in transform)
        {
            child.gameObject.GetComponent<PokerCardData>().UpdateParentHand(this);
            var card = child.GetComponent<CardSwell>();
            if (card == null)
            {
                card = child.gameObject.AddComponent<CardSwell>();
                card.Init(this);
            }
            cards.Add(card);
        }
    }

    public void UpdateLayout(bool instant)
    {
        int count = cards.Count;
        if (count == 0) return;

        // カード間隔
        float spacing = (count == 1) ? 0 : Mathf.Min(cardWidth, (maxWidth - cardWidth) / (count - 1));
        float totalWidth = cardWidth + spacing * (count - 1);
        float startX = -totalWidth / 2f;

        for (int i = 0; i < count; i++)
        {
            CardSwell card = cards[i];

            // 標準値を求める -1~1
            float t = (float)i / (count - 1);
            float normalized = (count == 1) ? 0 : t * 2f - 1f;

            // 位置算出
            float x = startX + i * spacing;
            float y = -arcHeight * normalized * normalized + arcHeight;
            float angle = -normalized * maxRotation;

            Vector3 targetPos = new Vector3(x, y, 0);
            Quaternion targetRot = Quaternion.Euler(0, 0, angle);
            Vector3 targetScale = Vector3.one;

            if (card._isHovered)
            {
                targetPos += Vector3.up * hoverOffset;
                targetScale = Vector3.one * scaleUp;
            }

            if (instant)
            {
                card.transform.localPosition = targetPos;
                card.transform.localRotation = targetRot;
                card.transform.localScale = targetScale;
            }
            else
            {
                card.transform.localPosition = Vector3.Lerp(
                    card.transform.localPosition, targetPos, Time.deltaTime * moveSpeed);
                card.transform.localRotation = Quaternion.Lerp(
                    card.transform.localRotation, targetRot, Time.deltaTime * moveSpeed);
                card.transform.localScale = Vector3.Lerp(
                    card.transform.localScale, targetScale, Time.deltaTime * moveSpeed);
            }
        }
    }
}