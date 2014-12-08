using UnityEngine;
using System.Collections;

public class Player : WithRealPos
{
    private const float skew = 2f;
    private const float skewi = 1f / skew;
    private const float pii = 1f / Mathf.PI;

    public static Vector3 skewedPos(Vector3 realPos)
    {
        return new Vector3(
            skewedPosComponent(Statics.PlayerPos.transform.position.x, realPos.x),
            skewedPosComponent(Statics.PlayerPos.transform.position.y, realPos.y),
            realPos.z);
    }

    public static float skewedPosComponent(float player, float real)
    {
        float delta = real - player;
        //return Mathf.Sign(delta) * (-skewi * Mathf.Exp(-skew * Mathf.Abs(delta)) + skewi);
        return Mathf.Sign(delta) * (pii * Mathf.Atan(Mathf.Abs(delta) / skew));
    }

    public static Vector3 skewedScale(Vector3 realPos)
    {
        return new Vector3(
            skewedScaleComponent(Statics.PlayerPos.transform.position.x, realPos.x),
            skewedScaleComponent(Statics.PlayerPos.transform.position.y, realPos.y),
            1f);
    }

    public static float skewedScaleComponent(float player, float real)
    {
        float delta = real - player;
        //return Mathf.Exp(-skew * Mathf.Abs(real - player));
        return pii * skew / (skew * skew + delta * delta);
    }


    private LineRenderer lineRenderer;

    private FireRate bulletRate;
    private FireRate laserRate;

    private float gridSpacing = 5f;
    private Vector3[] ps;
    private void genPoints()
    {
        ps = new Vector3[Statics.Stars.Length];
        for (int i = 0; i < Statics.Stars.Length; i++)
        {
            ps[i] = Statics.Stars[i].transform.position * gridSpacing;

        }
    }

    public override void Awake()
    {
        base.Awake();

        transform.localPosition = Vector3.zero;
        bulletRate = new FireRate();
        bulletRate.Rate = 4;

        laserRate = new FireRate();
        laserRate.Rate = 0.5f;

        lineRenderer = GetComponent<LineRenderer>();

        genPoints();
    }

    // Update is called once per frame
    public override void Update()
    {
        base.Update();

        //this.transform.rotation = Statics.PlayerPos.transform.rotation;

        float x = Statics.PlayerPos.transform.position.x;
        float y = Statics.PlayerPos.transform.position.y;
        float offsetX = x % (2f * gridSpacing);
        float offsetY = y % (2f * gridSpacing);

        for (int l = 0; l < Statics.Stars.Length; l++)
        {
            float xx = -offsetX + ps[l].x;
            if (Mathf.Abs(xx) > gridSpacing)
            {
                xx -= 2f * gridSpacing * Mathf.Sign(xx);
            }

            float yy = -offsetY + ps[l].y;
            if (Mathf.Abs(yy) > gridSpacing)
            {
                yy -= 2f * gridSpacing * Mathf.Sign(yy);
            }
            xx = skewedPosComponent(0f, xx);
            yy = skewedPosComponent(0f, yy);
            Statics.Stars[l].transform.position = new Vector3(xx, yy, -1);
        }

        if (Input.GetAxis("Fire1") > 0f && bulletRate.TryFire())
        {
            Bullet.FFire(Statics.PlayerPos.transform);
        }

        if (Statics.PlayerPos.Laser || (Input.GetAxis("Fire2") > 0f && laserRate.TryFire()))
        {
            if (!Statics.PlayerPos.Laser)
            {
                Statics.PlayerPos.Laser = true;
                StartCoroutine("FireMyLaser");
            }
            Laser.draw(lineRenderer, Statics.PlayerPos.transform.position - new Vector3(0,0,-1), Statics.PlayerPos.transform.up, 10f);
        }
        else
        {
            Laser.hide(lineRenderer);
        }
    }

    IEnumerator FireMyLaser()
    {
        for (int i = 0; i == 0; i++)
        {
            yield return new WaitForSeconds(.3f);
        }
        Statics.PlayerPos.Laser = false;
    }
}
