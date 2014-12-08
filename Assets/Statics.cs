using UnityEngine;
using System.Collections;

public class Statics : MonoBehaviour
{

    public static GameObject BulletPos { get; private set; }
    public static GameObject FBulletPos { get; private set; }
    public static GameObject Bullet { get; private set; }
    public static PlayerPos PlayerPos { get; private set; }
    public static GameObject[] Stars { get; private set; }
    public static Player Player { get; private set; }
    public static Enemy Enemy { get; private set; }
    public static GameObject EnemyPos { get; private set; }
    public static GameObject EP1Pos { get; private set; }
    public static GameObject EP2Pos { get; private set; }
    public static GameObject EP3Pos { get; private set; }
    public static GameObject P1Pos { get; private set; }
    public static GameObject P2Pos { get; private set; }
    public static GameObject P3Pos { get; private set; }

    
    void Awake()
    {
        BulletPos = GameObject.Find("BulletPos");
        FBulletPos = GameObject.Find("FBulletPos");
        Bullet = GameObject.Find("Bullet");
        BulletPos.SetActive(false);
        FBulletPos.SetActive(false);

        PlayerPos = GameObject.Find("PlayerPos").GetComponent<PlayerPos>();
        Player = GameObject.Find("Player").GetComponent<Player>();

        Stars = GameObject.FindGameObjectsWithTag("Star");
 
        EnemyPos = GameObject.Find("EnemyPos");
        Enemy = GameObject.Find("Enemy").GetComponent<Enemy>();
        EnemyPos.SetActive(false);

        EP1Pos = GameObject.Find("EP1Pos");
        EP1Pos.SetActive(false);

        EP2Pos = GameObject.Find("EP2Pos");
        EP2Pos.SetActive(false);

        EP3Pos = GameObject.Find("EP3Pos");
        EP3Pos.SetActive(false);

        P1Pos = GameObject.Find("P1Pos");
        P1Pos.SetActive(false);

        P2Pos = GameObject.Find("P2Pos");
        P2Pos.SetActive(false);

        P3Pos = GameObject.Find("P3Pos");
        P3Pos.SetActive(false);
    }

    void Start()
    {
    }

    private float nextEnemyDelay = 5f;
    private float lastEnemy = float.MinValue;

    void Update()
    {
        if (Time.time >= lastEnemy + nextEnemyDelay)
        {
            for(;;) {
                Vector3 spawn = new Vector3(Random.Range(-10f,+10f), Random.Range(-10f,+10f), 0);
                if (Vector3.Distance(spawn, PlayerPos.transform.position) > 2f)
                {
                    Enemy.spawn(spawn);
                    break;
                }
            }
            lastEnemy = Time.time;
            nextEnemyDelay = Mathf.Clamp(nextEnemyDelay - 0.33f, 0.1f, float.MaxValue);
        }
    }
}
