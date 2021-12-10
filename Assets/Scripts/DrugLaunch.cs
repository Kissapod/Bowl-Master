using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(Ball))]
public class DrugLaunch : MonoBehaviour
{
    private Vector3 drugStart, drugEnd;
    private float timeStart, timeEnd;
    private Ball ball;
    // Start is called before the first frame update
    void Start()
    {
        ball = GetComponent<Ball>();
    }

    public void DrugStart()
    {
        // время и место клика мыши или пальца
        drugStart = Input.mousePosition;
        timeStart = Time.time;
    }
    public void DrugEnd()
    {
        // запускает шар
        drugEnd = Input.mousePosition;
        timeEnd = Time.time;

        float drugTime = timeEnd - timeStart;
        float velocityX = (drugEnd.x - drugStart.x) / drugTime;
        float velocityZ = (drugEnd.y - drugStart.y) / drugTime;
        if (!ball.inPlay)
        {
            Vector3 launchVelocity = new Vector3(velocityX, 0f, velocityZ);
            ball.Launch(launchVelocity);
        }
    }

    public void MoveStart (float amount)
    {
        if (!ball.inPlay)
        {
            float xPos = Mathf.Clamp(ball.transform.position.x + amount, -50f, 50f); //накладывает ограничения в указанном диапазоне
            float yPos = ball.transform.position.y;
            float zPos = ball.transform.position.z;
            ball.transform.position = new Vector3(xPos,yPos,zPos);
        }
    }
}