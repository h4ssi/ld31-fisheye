using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class HP : MonoBehaviour
{
    private int hp;
    public int maxHp;

    private Slider slider;
    private Image sliderColorAccess;

    private const float midThresh = 2f / 3f;
    private const float lowThresh = 1f / 3f;

    public void Damage(int delta)
    {
        if (hp > 0)
        {
            hp = Mathf.Max(0, hp - delta);
            UpdateBar();
            HandleDead();
        }
    }

    void Awake()
    {
        slider = GetComponentInChildren<Slider>();
        if (slider != null)
        {
            sliderColorAccess = slider.fillRect.gameObject.GetComponentInChildren<Image>();
        }
        hp = maxHp;
    }

    void Start()
    {
        UpdateBar();
        HandleDead();
    }

    private void UpdateBar()
    {
        if (slider != null)
        {
            slider.value = (float)hp / (float)maxHp;
            if (hp == maxHp)
            {
                slider.gameObject.SetActive(false);
            }
            else if (slider.value > midThresh)
            {
                slider.gameObject.SetActive(true);
                sliderColorAccess.color = Color.green;
            }
            else if (slider.value > lowThresh)
            {
                slider.gameObject.SetActive(true);
                sliderColorAccess.color = Color.yellow;
            }
            else
            {
                slider.gameObject.SetActive(true);
                sliderColorAccess.color = Color.red;
            }
        }
    }

    public void Upright()
    {
        // set canvas to be upright
        if (slider != null)
        {
            slider.transform.parent.rotation = Quaternion.identity;
        }
    }

    private void HandleDead()
    {
        if (hp <= 0)
        {
            Events e = gameObject.GetComponentInChildren<Events>();
            if (e != null)
            {
                e.OnDead();
            }
            else
            {
                GameObject.Destroy(gameObject);
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        Upright();
    }
}
