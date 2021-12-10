using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;

public class ScoreMaster 
{
    //возвращает комулятивный счет, как в карточках для боулинга
    public static List<int> ScoreCumulative(List<int> rolls)
    {
        List<int> cumulativeScores = new List<int>();
        int runningTotal = 0;
        foreach (int frameScore in ScoreFrames(rolls))
        {
            runningTotal += frameScore;
            cumulativeScores.Add(runningTotal);
        }
        return cumulativeScores;
    }
    
    //дает суммарный счет двух клетов в раунде, НЕ КУМУЛЯТИВНЫЙ.
    public static List<int> ScoreFrames(List<int> rolls)
    {
        List<int> frames = new List<int>();
        for (int i = 1; i < rolls.Count; i += 2)
        {
            if (frames.Count == 10)                                     //если прошло 10 раундов, то выходим из цикла
            {
                break;
            }
            if (rolls[i - 1] + rolls[i] < 10)                           //если имеется открытый фрейм, то просто складываем сумму сбитых кеглей
            {
                frames.Add(rolls[i - 1] + rolls[i]);
            }
            
            if (rolls.Count - i <= 1)                                    // если следующий бросок пустой, то выходим из цикла.
            {
                break;
            }
            if (rolls[i - 1] == 10)                                     // если мы выбиваем страйк
            {
                i--;                                                    //вычитаем для читаемости когда -1 из i, так как массив начинается с 0
                frames.Add(10 + rolls[i + 1] + rolls[i + 2]);           //прибавляем 10 очков и очки двух последующих бросков
            }
            else if (rolls[i - 1] + rolls[i] == 10)                     //если мы выбиваем спейр
            {
                frames.Add(10 + rolls[i + 1]);
            }
        }
        return frames;
    }
}
