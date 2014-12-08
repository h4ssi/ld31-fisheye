using UnityEngine;
using System.Collections;

public class EnemyEvents : Events {
    public override void OnDead()
    {
        Transform pos = transform.parent.parent;

        GameObject e1 = (GameObject) GameObject.Instantiate(Statics.EP1Pos, pos.position, pos.rotation);
        GameObject e2 = (GameObject)GameObject.Instantiate(Statics.EP2Pos, pos.position, pos.rotation);
        GameObject e3 = (GameObject)GameObject.Instantiate(Statics.EP3Pos, pos.position, pos.rotation);

        e1.SetActive(true);
        e2.SetActive(true);
        e3.SetActive(true);

        e1.rigidbody2D.velocity = e2.rigidbody2D.velocity = e3.rigidbody2D.velocity = pos.rigidbody2D.velocity;

        e1.rigidbody2D.AddForce(Random.insideUnitCircle, ForceMode2D.Impulse); 
        e2.rigidbody2D.AddForce(Random.insideUnitCircle, ForceMode2D.Impulse);
        e3.rigidbody2D.AddForce(Random.insideUnitCircle, ForceMode2D.Impulse);
        
        GameObject.Destroy(transform.root.gameObject);        
    }
}
