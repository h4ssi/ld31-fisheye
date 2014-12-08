using UnityEngine;
using System.Collections;

public class Orbits : MonoBehaviour {
    public float radius;
    public float duration;
    public float day;

    private float state;
    private float rate;
    private float dayRate;

	// Use this for initialization
	void Start () {
        state = Random.value % (2 * Mathf.PI);
        rate = 2 * Mathf.PI / duration;
        dayRate = 360 / day;
	}
	
	// Update is called once per frame
	void Update () {
        state += rate * Time.deltaTime;

        transform.localPosition = new Vector3(Mathf.Sin(state), Mathf.Cos(state), 0f) * radius;

        transform.Rotate(0f, 0f, Time.deltaTime * dayRate);
	}
}
