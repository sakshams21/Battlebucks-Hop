using UnityEngine;

public class Test : MonoBehaviour
{
    [SerializeField]
    private float distance, time;

    private float speed, startSpeed, acceleration;

    public bool hasGameStarted;

    private void Start()
    {
        hasGameStarted = false;

        startSpeed = 2 * distance / time;
        acceleration = -0.995f * startSpeed / time;
        speed = startSpeed;
    }

    private void FixedUpdate()
    {

        if (!hasGameStarted) return;

        speed += acceleration * Time.fixedDeltaTime;
        Vector3 temp = new Vector3(0, speed * Time.fixedDeltaTime, 0);
        transform.localPosition += temp;
        temp = transform.localPosition;

        if(temp.y < 0f)
        {
            speed = startSpeed;
        }
    }
}