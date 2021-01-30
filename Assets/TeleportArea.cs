using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeleportArea : MonoBehaviour
{
    [SerializeField] TeleportArea twin;

    [SerializeField] bool swapX;
    [SerializeField] bool swapZ;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider collision)
    {
        GameObject swapping = collision.gameObject;
        float y = swapping.transform.position.y;
        Vector3 offset = transform.position - swapping.transform.position;
        if (!swapX)
        {
            offset = Vector3.Scale(offset, new Vector3(-1, 1, 1));
        }
        if (!swapZ)
        {

            offset = Vector3.Scale(offset, new Vector3(1, 1, -1));
        }
        swapping.transform.position = twin.transform.position + offset * 1.05f;
        swapping.transform.position += new Vector3(0, y - swapping.transform.position.y, 0);
    }
}
