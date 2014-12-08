using UnityEngine;
using System.Collections;

public class Enemy : WithRealPos {
    public static void spawn(Vector3 pos)
    {
        ((GameObject)GameObject.Instantiate(Statics.EnemyPos, pos, Statics.EnemyPos.transform.rotation)).SetActive(true);
    }
}
