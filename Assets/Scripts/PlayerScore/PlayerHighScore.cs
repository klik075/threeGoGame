public class PlayerHighScore
{
    public string playerName { get; private set; }
    public int highScore { get; private set; }

    public PlayerHighScore(string playerName, int highScore)
    {
        this.playerName = playerName;
        this.highScore = highScore;
    }
}

