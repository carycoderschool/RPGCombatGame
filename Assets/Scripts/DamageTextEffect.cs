using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DamageTextEffect : MonoBehaviour
{
    RectTransform rect;
    float posx;
    float posy;
    Color32 text;
    // Start is called before the first frame update
    void Start()
    {
        rect = gameObject.GetComponent<RectTransform>();
        posx = rect.anchoredPosition.x;
        posy = rect.anchoredPosition.y;
        text = gameObject.GetComponent<Text>().color;
    }

    // Update is called once per frame
    void Update()
    {
        if (text.a > 1)
        {
            rect.Translate(Vector3.up);
            text.a -= 5;
            gameObject.GetComponent<Text>().color = text;
        } else if (text.a < 1)
        {
            rect.anchoredPosition = new Vector3(posx, posy);
        }
        
    }
    public void StartFloating()
    {
        text.a = 255;
    }
}
