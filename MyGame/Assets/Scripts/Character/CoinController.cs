
public class CoinController 
{  
    private int _coinsCount =5;
    public void CollectCoin()
    {
        _coinsCount--;
        CheckCoins();
    }

    public void CheckCoins()
    {
        if (_coinsCount <= 0)
        {
            UiController.instance.OnWinPanel();
        }
    }

}
