using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PinCounter : MonoBehaviour
{
    public Text standingDisplay;

    private GameManager gameManager;
    private bool ballOutOfPlay = false;
    private float lastChangeTime;
    private int lastSettlesCount = 10;
    private int lastStandingCount = -1;

    private void Start()
    {
        gameManager = FindObjectOfType<GameManager>();
    }

    void Update()
    {
        standingDisplay.text = CountStanding().ToString();
        standingDisplay.color = Color.white;
        if (ballOutOfPlay)
        {
            CheckStanding();
            standingDisplay.color = Color.red;
        }
    }

    public void Reset()
    {
        lastSettlesCount = 10;
    }

    private void OnTriggerExit(Collider collision)
    {
        if (collision.gameObject.name == "Ball")
        {
            ballOutOfPlay = true;
        }
    }
    //метод контролирующий стоячие кегли
    void CheckStanding()
    {
        int currentStanding = CountStanding();      //новая переменная, которой присваивается значение несбитых кегль

        if (currentStanding != lastStandingCount)   // если количество стоячих кегль не равно -1 и так далее
        {
            lastChangeTime = Time.time;             // то присваивам переменной значение текущего времени от начала игры
            lastStandingCount = currentStanding;    //lastStandingCount присваиваем последнее значение стоячих кегль
            return;
        }

        float settleTime = 3f; //создаем переменную, которая будет ждать 3 секунды, чтобы кегли стабилизировались
        if ((Time.time - lastChangeTime) > settleTime) //если прошло больше чем 3 секунды, то
        {
            PinsHaveSettled();  //вызываем метод
        }
    }

    void PinsHaveSettled()
    {
        int pinFall = lastSettlesCount - CountStanding();
        lastSettlesCount = CountStanding();
        lastStandingCount = -1;
        ballOutOfPlay = false;
        standingDisplay.color = Color.green;
        gameManager.Bowl(pinFall);
    }
    public int CountStanding()
    {
        int standing = 0;

        foreach (Pin pin in FindObjectsOfType<Pin>())
        {
            if (pin.IsStanding())
            {
                standing++;
            }
        }
        return standing;
    }
}
