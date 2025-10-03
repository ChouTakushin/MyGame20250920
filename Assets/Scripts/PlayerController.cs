
public class PlayerController : PlayerControllerBase
{
    public override void OnTurnStart()
    {
        // _om.TurnHandler._gameState = EnumGameState.PlayerTurnStart;
        
    }
    public override void SelectCard(PokerCardData card)
    {
        _action.SelectCard(card);
    }

    public override void DrawCard(PokerCardData card)
    {
        _action.DrawCard(card);
    }
}