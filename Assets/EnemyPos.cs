using UnityEngine;
using System.Collections;

public class EnemyPos : MonoBehaviour {
    private FireRate bulletRate;

    void Awake ()
    {
        bulletRate = new FireRate();
        bulletRate.Rate = 4f;
    }
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        if (!Statics.PlayerPos.gameObject.activeInHierarchy)
        {
            return;
        }

        Vector3 attackVector = Statics.PlayerPos.transform.position - transform.position ;

        float dir = Mathf.Sign(Vector3.Dot(transform.forward, Vector3.Cross(transform.up, attackVector)));
        float angle = Vector3.Angle(attackVector, transform.up);

        float off = (Mathf.Min(90f, angle) / 90f);

        rigidbody2D.AddTorque(5f * off * off * dir);

        float dist = Vector3.Magnitude(attackVector);
        float onCourse = 1f - off;
        float thrust = Mathf.Min(dist, onCourse); // thurst if at distance and on course!

        rigidbody2D.AddForce(transform.up * thrust * 3f);

        if (angle < 45f && bulletRate.TryFire())
        {
            Bullet.Fire(transform);
        }
	}
}
