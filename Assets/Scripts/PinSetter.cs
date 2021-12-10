using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PinSetter : MonoBehaviour
{
    public GameObject pinSet;

    private PinCounter pinCounter;
    private Animator animator;
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        pinCounter = FindObjectOfType<PinCounter>();
    }

    // уничтожаем кегли если они вылетают из пинсеттера
    private void OnTriggerExit(Collider other)
    {
        Pin pin = other.gameObject.GetComponent<Pin>(); 
        if (pin)
        {
            Destroy(other.gameObject);
        }
    }

    public void RaisePins()
    {
        foreach (Pin pin in FindObjectsOfType<Pin>())
        {
            pin.Raise();
        }
    }

    public void LowerPins()
    {
        foreach (Pin pin in FindObjectsOfType<Pin>())
        {
            pin.Lower();
        }
    }

    public void Renew()
    {
        Instantiate(pinSet, new Vector3(0, 40f, 1829), Quaternion.identity);
    }

    public void PerformAction(ActionMaster.Action action)
    {
        switch (action)
        {
            case ActionMaster.Action.Tidy:
                animator.SetTrigger("tidyTrigger");
                break;
            case ActionMaster.Action.Reset:
                animator.SetTrigger("resetTrigger");
                pinCounter.Reset();
                break;
            case ActionMaster.Action.EndTurn:
                animator.SetTrigger("resetTrigger");
                pinCounter.Reset();
                break;
            case ActionMaster.Action.EndGame:
                print("Конец игры");
                break;
            default:
                Debug.LogError("Что то пошло не так");
                break;
        }
    }
}
