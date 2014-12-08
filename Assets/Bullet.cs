using UnityEngine;
using System.Collections;

public class Bullet : WithRealPos
{
    public static void Fire(Transform origin)
    {
        Vector3 direction = origin.up;
        GameObject bulletPos = (GameObject)GameObject.Instantiate(Statics.BulletPos, origin.position, origin.rotation);
        bulletPos.SetActive(true);

        Destroy(bulletPos, 3);

        bulletPos.rigidbody2D.AddForce(((Vector2)direction) * 0.5f, ForceMode2D.Impulse);
        Physics2D.IgnoreCollision(bulletPos.collider2D, origin.collider2D);
    }
    public static void FFire(Transform origin)
    {
        Vector3 direction = origin.up;
        GameObject bulletPos = (GameObject)GameObject.Instantiate(Statics.FBulletPos, origin.position, origin.rotation);
        bulletPos.SetActive(true);

        Destroy(bulletPos, 3);

        bulletPos.rigidbody2D.AddForce(((Vector2)direction) * 0.5f, ForceMode2D.Impulse);
        Physics2D.IgnoreCollision(bulletPos.collider2D, origin.collider2D);
    }
}
