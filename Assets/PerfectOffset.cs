using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PerfectOffset : MonoBehaviour
{
    Camera main;
    Camera cam;
    [SerializeField] Vector3 offset;
    [SerializeField] Image displayPlane;

    RenderTexture tex;

    // Start is called before the first frame update
    void Start()
    {
        main = Camera.main;
        cam = GetComponent<Camera>();
        CopyCamera();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void LateUpdate()
    {
               transform.position = main.transform.position + offset;
    }

    void CopyCamera()
    {
        cam.CopyFrom(main);
        tex = new RenderTexture((int)displayPlane.rectTransform.rect.width, (int)displayPlane.rectTransform.rect.height, 32);
        cam.targetTexture = tex;
        displayPlane.material.mainTexture = tex;
    }
}
