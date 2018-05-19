using UnityEngine;

public class GameParams {

    public static int GetScore() {
        return Data.score;
    }

    public static void AddScore(int val) {
        Data.score += val;
    }

	public static void SetScore(int val) {
		Data.score =  val;
	}

    public static int GetLastScore() {
        return Data.lastScore;
    }

    public static void AddLastScore(int val) {
        Data.lastScore += val;
    }

    public static void SetLastScore(int val) {
        Data.lastScore = val;
    }

    public static int GetTopScore() {
        return Data.topScore;
    }

    public static void SetTopScore(int val) {
        Data.topScore = val;
    }

	public static int GetCoins() {
        return Data.coins;
	}

	public static void AddCoins(int val) {
        Data.coins += val;
	}

	public static void SetCoins(int val) {
        Data.coins = val;
	}
}
