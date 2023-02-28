using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Item : MonoBehaviour
{
    public Vector3 MyPos;
    public Vector3 ContentSize;
    public RectTransform scrollRectContent;
    public Scrollbar verticalScrollbar;
    public ScrollRect scrollRect;
    public float value;
    public TextMeshProUGUI nameText;
    public float ScrollFactor;
    public float DiffRatio;
    void Start()
    {
        nameText.text = gameObject.name;
    }

    // Update is called once per frame
    void Update()
    {
        MyPos = GetComponent<RectTransform>().position;
        ContentSize = scrollRectContent.sizeDelta;
        value = verticalScrollbar.size;
        if(MyPos.y<670)
        {
            //ScrollFactor = (670 - MyPos.y) / 100;
            
        }
    }

    public void OnClickItem()
    {
        if(MyPos.y<670)
        {
            DiffRatio = 1-((670 - MyPos.y) / 100);
            ScrollFactor = (verticalScrollbar.size / 2.75f) * DiffRatio;
            scrollRect.verticalScrollbar.value = scrollRect.verticalScrollbar.value + ScrollFactor;
        }
    }
}
