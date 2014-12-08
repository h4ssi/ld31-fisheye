using UnityEngine;
using System.Collections;

public class PlayerPos : MonoBehaviour
{
    public bool Laser = false;

    void Awake()
    {
        EdgeCollider2D c = GetComponent<EdgeCollider2D>();
        c.points = new Vector2[] {
            new Vector2(0,100),
            Vector2.zero
        };

        //Physics2D.IgnoreCollision(c, GetComponent<PolygonCollider2D>());
    }

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        rigidbody2D.AddTorque(-3f * Input.GetAxis("Horizontal"));


        rigidbody2D.AddForce(((Vector2)Vector3.Scale(transform.up, new Vector3(Input.GetAxis("Vertical"), Input.GetAxis("Vertical")))) * 3f);
    }

    void OnTriggerStay2D(Collider2D other)
    {
        if (Laser)
        {
            HP hp = other.gameObject.GetComponent<HP>();
            if (hp != null)
            {
                hp.Damage(3);
            }
        }
    }
}
