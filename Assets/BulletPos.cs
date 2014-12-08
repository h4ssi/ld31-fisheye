using UnityEngine;
using System.Collections;

public class BulletPos : MonoBehaviour {

    void OnCollisionEnter2D(Collision2D c)
    {
        GameObject.Destroy(gameObject);

        HP hp = c.collider.gameObject.GetComponent<HP>();
        if (hp != null)
        {
            hp.Damage(1);
        }
    }
}
