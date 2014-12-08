using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

public class WithRealPos : MonoBehaviour
{
    public GameObject Pos { get; protected set; }

    public virtual void Awake()
    {
        Pos = transform.parent.parent.gameObject;
    }

    public virtual void Start()
    {
        skewPos();
    }

    private void skewPos()
    {
        transform.parent.rotation = Quaternion.identity;
        transform.parent.position = Player.skewedPos(Pos.transform.position);
        transform.parent.localScale = Player.skewedScale(Pos.transform.position);//Vector3.Scale(originalScale, Player.skewedScale(Pos.transform.position));
        transform.localPosition = Vector3.zero;
        transform.rotation = Pos.transform.rotation;
    }

    public virtual void Update()
    {
        skewPos();
    }
}

