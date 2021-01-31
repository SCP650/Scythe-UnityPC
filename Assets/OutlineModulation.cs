using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class OutlineModulation : MonoBehaviour
{
    [SerializeField] TMP_Text text;
    [SerializeField] float rate;
    [SerializeField] float minWidth;
    [SerializeField] float maxWidth;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        text.outlineWidth = Mathf.Abs(Mathf.Sin(Time.time * rate)) * (maxWidth - minWidth) + minWidth;
    }
}
