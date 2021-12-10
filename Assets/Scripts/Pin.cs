using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pin : MonoBehaviour
{
    public float standingTrashold = 3f;
    public float distToRaise = 40f;

    public bool IsStanding()
    {
        Vector3 rotationInEuler = transform.rotation.eulerAngles;

        float rotX = Mathf.Abs(270 - rotationInEuler.x);
        //print(name + " " + rotX);

        float rotZ = Mathf.Abs(rotationInEuler.z);
        //print(name + " " + rotZ);

        if (rotX < standingTrashold && rotZ < standingTrashold)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
    public void Raise()
    {
        if (IsStanding())
        {
            GetComponent<Rigidbody>().useGravity = false;
            transform.Translate(new Vector3(0, distToRaise, 0), Space.World);
            transform.rotation = Quaternion.Euler(270f, 0, 0);
        }
    }
    public void Lower()
    {
            transform.Translate(new Vector3(0, -distToRaise, 0), Space.World);
            GetComponent<Rigidbody>().useGravity = true;
    }
}

