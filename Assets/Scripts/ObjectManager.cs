using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ObjectManager : MonoBehaviour
{
    [SerializeField] public GameObject CenterPoint;
    [SerializeField] public PlayerController PlayerController;
    [SerializeField] public CpuController CpuController;
    [SerializeField] public GameObject PlayerHand;
    [SerializeField] public GameObject PlayerShowArea;
    [SerializeField] public GameObject CpuHand;
    [SerializeField] public GameObject CpuShowArea;
    [SerializeField] public GameObject PlayedStack;
    [SerializeField] public GameObject SkillView;
    [SerializeField] public Image SkillViewVignette;
    [SerializeField] public GameObject SkillViewCardPosition;
    [SerializeField] public Text SkillViewDescription;
    [SerializeField] public PokerDeck CardDeck;
    [SerializeField] public InGameManager InGameManager;
    [SerializeField] public UIController UIController;
    [SerializeField] public Camera MainCamera;
    [SerializeField] public Canvas UICanvas;
    [SerializeField] public TurnHandler TurnHandler;
    [SerializeField] public GameObject ResultView;
    [SerializeField] public Image ResultViewVignette;
    [SerializeField] public Text ResultText;
    [SerializeField] public Button RestartButton;

    [SerializeField] public Image UmiImage;
    [SerializeField] public Sprite Umi_Start;
    [SerializeField] public Sprite Umi_Happy;
    [SerializeField] public Sprite Umi_Danger;
    [SerializeField] public Sprite Umi_Finished;

    // [SerializeField] public ??? SkillDict;
}