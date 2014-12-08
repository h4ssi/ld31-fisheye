using UnityEngine;
using System.Collections;

public class skewedMovement : MonoBehaviour
{
    float realX = 0;
    float realY = 0;


    Vector3 origScale;

    LineRenderer r;

    void Awake()
    {
        r = GetComponent<LineRenderer>();
    }

    // Use this for initialization
    void Start()
    {
        Enemy.spawn(new Vector3(1, 1, 0));

        origScale = transform.localScale;
    }

    // Update is called once per frame
    void Update()
    {
        realX = 2f * Mathf.Sin(Time.timeSinceLevelLoad);
        realY = 2f * Mathf.Cos(Time.timeSinceLevelLoad);

        Vector3 pos = new Vector3(realX, realY, 0);

        transform.localScale = Vector3.Scale(origScale, Player.skewedScale(pos));
        transform.localPosition = Player.skewedPos(pos);

        Vector3 d = (Vector3.zero - pos);
        d.Normalize();

        Laser.draw(r, pos, Quaternion.AngleAxis(45, transform.forward) * d, 10f);
    }
}
