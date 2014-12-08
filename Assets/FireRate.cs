using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

class FireRate
{
    private float lastFire = float.MinValue;
    private float delay;
    public float Rate
    {
        get
        {
            return 1f / delay;
        }
        set
        {
            delay = 1f / value;
        }
    }

    public bool TryFire()
    {
        float t = Time.time;
        if (t < lastFire + delay)
        {
            return false;
        }
        lastFire = t;
        return true;
    }
}

