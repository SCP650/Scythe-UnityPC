using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SoulUIController : MonoBehaviour
{
    
    [SerializeField] float minRadius;
    [SerializeField] float maxRadius;

    [SerializeField] Color mixedColor;
    [SerializeField] Color demonColor;
    [SerializeField] Color humanColor;
    [SerializeField] Color baseColor;

    Material mat;
    Soul soul;

    public static readonly float mixedDegree = Mathf.Deg2Rad * 90f;
    public static readonly float demonDegree = Mathf.Deg2Rad * 330f;
    public static readonly float humanDegree = Mathf.Deg2Rad * 210f;

    // Start is called before the first frame update
    void Start()
    {
        mat = GetComponent<Image>().material;
        soul = Soul.singleton;
    }

    // Update is called once per frame
    void Update()
    {
        mat.SetColor("_Color3", Color.Lerp(baseColor, mixedColor, soul.percentMixedSlayer));
        mat.SetColor("_Color2", Color.Lerp(baseColor, demonColor, soul.percentDemonSlayer));
        mat.SetColor("_Color1", Color.Lerp(baseColor, humanColor, soul.percentHumanSlayer));
        if (soul.numKilled > soul.startExpandingAt)
        {
            float diff = (soul.numKilled - soul.startExpandingAt);
            float total = (soul.stopExpandingAt - soul.startExpandingAt);
            float expansionAmount = Mathf.Clamp(diff / total, 0.0f, 0.95f);
            float expandArea = maxRadius - minRadius;
            print($"Expand area is {expandArea}");
            print($"ExpandAmount is {expansionAmount}");
            print($"Mixed together is {expandArea * expansionAmount * soul.percentMixedSlayer}");
            makeMixedPoint(expandArea * expansionAmount * soul.percentMixedSlayer + minRadius);
            makeDemonPoint(expandArea * expansionAmount * soul.percentDemonSlayer + minRadius);
            makeHumanPoint(expandArea * expansionAmount * soul.percentHumanSlayer + minRadius);


        }
        else
        {
            makeMixedPoint(minRadius);
            makeDemonPoint(minRadius);
            makeHumanPoint(minRadius);
        }
    }
    
    public void makeMixedPoint(float radius)
    {
        print($"Making mixed point with rad {radius}");
        print($"Percent mixed is {soul.percentMixedSlayer}");
        Vector4 point = new Vector4(Mathf.Cos(mixedDegree), Mathf.Sin(mixedDegree), 0.0f, 0.0f);
        point = point * radius + new Vector4(0.5f, 0.5f, 0.0f, 0.0f);
        mat.SetVector("_Coord1", point);
    }
    public void makeDemonPoint(float radius)
    {
        Vector4 point = new Vector4(Mathf.Cos(demonDegree), Mathf.Sin(demonDegree), 0.0f, 0.0f);
        point = point * radius + new Vector4(0.5f, 0.5f, 0.0f, 0.0f);
        mat.SetVector("_Coord3", point);
    }

    public void makeHumanPoint(float radius)
    {
        Vector4 point = new Vector4(Mathf.Cos(humanDegree), Mathf.Sin(humanDegree), 0.0f, 0.0f);
        point = point * radius + new Vector4(0.5f, 0.5f, 0.0f, 0.0f);
        mat.SetVector("_Coord2", point);
    }
}
