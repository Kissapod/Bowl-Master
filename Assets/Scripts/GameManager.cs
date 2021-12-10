using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private List<int> rolls = new List<int>();
    private Ball ball;
    private PinSetter pinSetter;
    private ScoreDisplay scoreDisplay;

    private void Start()
    {
        ball = FindObjectOfType<Ball>();
        pinSetter = FindObjectOfType<PinSetter>();
        scoreDisplay = FindObjectOfType<ScoreDisplay>();
    }

    public void Bowl(int pinFall)
    {
        rolls.Add(pinFall);                                         // добавляем в наш список количество упавших кеглей
        ball.ResetBall();                                           // скидываем позицию шара
        pinSetter.PerformAction(ActionMaster.NextAction(rolls));    // получаем действие в зависимости от количества сбитых кеглей
        scoreDisplay.FillRolls(rolls);
        scoreDisplay.FillFrames(ScoreMaster.ScoreCumulative(rolls));
    }
}
