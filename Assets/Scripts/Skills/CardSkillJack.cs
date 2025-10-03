using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CardSkillJack : CardSkillBase
{
    public string CardName = "カード：Jack";

    public string Description = "スキル効果：\n" +
                                "もう一度行動できる。";

    public void SetSkillDescription(Text text)
    {
        text.text = CardName + "\n" + Description;
    }
    
    
}