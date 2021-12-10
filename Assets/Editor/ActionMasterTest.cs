using System;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using System.Linq;

[TestFixture]
public class ActionMasterTest
{
    private List<int> pinFalls;
    private ActionMaster.Action endTurn = ActionMaster.Action.EndTurn;
    private ActionMaster.Action tidy = ActionMaster.Action.Tidy;
    private ActionMaster.Action reset = ActionMaster.Action.Reset;
    private ActionMaster.Action endGame = ActionMaster.Action.EndGame;

    [SetUp]
    public void Setup()
    {    
        pinFalls = new List<int>();
    }

    [Test] //тестовый тест
    public void T00PassingTest()
    {
        Assert.AreEqual(1, 1);
    }

    [Test] //проверяет что после выбивания страйка в первом броске ход завершается
    public void T01OneStrikeReturnsEndTurn()
    {
        pinFalls.Add(10);
        Assert.AreEqual(endTurn, ActionMaster.NextAction(pinFalls));
    }
    [Test] //тестируем состояние тайди
    public void T02Bowl8Pins()
    {
        pinFalls.Add(8);
        Assert.AreEqual(tidy, ActionMaster.NextAction(pinFalls));
    }
    [Test] // тестируем состояние spare
    public void T03SpareBowl2And8Pins()
    {
        int[] rolls = { 2, 8 };
        Assert.AreEqual(endTurn, ActionMaster.NextAction(rolls.ToList()));
    }

    [Test] // тестируем состояние reset если на 19 броске был страйк
    public void T04StrikeBowl19()
    {
        int[] rolls = { 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 10 };
        Assert.AreEqual(reset, ActionMaster.NextAction(rolls.ToList()));
    }

    [Test] // тестируем состояние reset если на 19 и 20 получился spare
    public void T05SpareBowl19And20()
    {
        int[] rolls = { 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 9 };
        Assert.AreEqual(reset, ActionMaster.NextAction(rolls.ToList()));
    }
    [Test] // тестируем состояние КОНЕЦ ИГРЫ если на 21 броске выбито 9 кеглей
    public void T06EndGameStrikeSpare()
    {
        int[] rolls = { 8, 2, 3, 7, 3, 4, 2, 4, 5, 5, 6, 4, 3, 3, 8, 2, 2, 1, 8, 2, 9 };
        Assert.AreEqual(endGame, ActionMaster.NextAction(rolls.ToList()));
    }
    [Test] // тестируем состояние конец игры если за 19 и 20 бросок мы не сбили все кегли
    public void T07EndGame20Bowl()
    {
        int[] rolls = { 8, 2, 3, 7, 3, 4, 2, 4, 5, 5, 6, 4, 3, 3, 8, 2, 2, 1, 2, 2 };
        Assert.AreEqual(endGame, ActionMaster.NextAction(rolls.ToList()));
    }
    [Test] // тестируем состояние тайди и конец игры если на 20 броске мы выбили 3 кегли а на 21 - 7
    public void T08EndGameStrike19Bowl()
    {
        int[] rolls = { 8, 2, 3, 7, 3, 4, 2, 4, 5, 5, 6, 4, 3, 3, 8, 2, 2, 1, 10, 3, 7 };
        Assert.AreEqual(endGame, ActionMaster.NextAction(rolls.ToList()));
    }

    [Test] //тестируем состояние тайди, если на 20 броске мы сбили 0 кегль
    public void T09Strike19BowlAnd1Frame0Pins()
    {
        int[] rolls = { 8, 2, 3, 7, 3, 4, 2, 4, 5, 5, 6, 4, 3, 3, 8, 2, 2, 1, 10, 0 };
        Assert.AreEqual(tidy, ActionMaster.NextAction(rolls.ToList()));
    }

    [Test] //тестируем состояние reset если на 20 броске выбили страйк и у нас получился spare
    public void T10Strike19BowlAnd1Frame0Pins()
    {
        int[] rolls = { 8, 2, 3, 7, 3, 4, 2, 4, 5, 5, 6, 4, 3, 3, 8, 2, 2, 1, 0, 10 };
        Assert.AreEqual(reset, ActionMaster.NextAction(rolls.ToList()));
    }

    [Test] //тестируем прибавление к броскам 1 раз если мы сначала сбили 0 кегль, а потом сбили все
    public void T11Bowl1Pins0AndStrike()
    {
        int[] rolls = { 0, 10 };
        Assert.AreEqual(endTurn, ActionMaster.NextAction(rolls.ToList()));
    }

    [Test] //тестируем три страйка в последнем раунде
    public void T12Strike3()
    {
        int[] rolls = { 8, 2, 3, 7, 3, 4, 2, 4, 5, 5, 6, 4, 3, 3, 8, 2, 2, 1, 10, 10, 10 };
        Assert.AreEqual(endGame, ActionMaster.NextAction(rolls.ToList()));
    }
}
