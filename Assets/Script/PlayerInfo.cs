using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerInfo : MonoBehaviour
{
    [SerializeField]
    private Text text;
    [SerializeField]
    private Slider slider;

    public void Set(string str, int v)
    {
        text.text = str;
        slider.value = v;
    }

    public void Up(int v)
    {
        slider.value = v;
    }
}
