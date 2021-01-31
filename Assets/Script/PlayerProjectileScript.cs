using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerProjectileScript : MonoBehaviour
{
    [SerializeField]
    private float rotationSpeed;
    [SerializeField]
    private float timeToStayAlive = 3.0f;

    private float timeAlive;
    private Transform realScythe;

    // Start is called before the first frame update
    void Start()
    {
        timeAlive = 0;
        realScythe = transform.GetChild(0);
    }

    // Update is called once per frame
    void Update()
    {
        if (timeAlive >= timeToStayAlive) Destroy(this.gameObject);
        timeAlive += Time.deltaTime;

        realScythe.Rotate(new Vector3(0, rotationSpeed, 0));
    }

    private void OnCollisionEnter(Collision collision)
    {
        Destroy(this.gameObject);
    }
}
