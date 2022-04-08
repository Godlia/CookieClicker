using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cookiespawner : MonoBehaviour
{
    public float Xrandom = 14.358f;
    public float cookieDelay = 0.125f;
    private float timestamp = 0;
    public GameObject cookie;
    // Start is called before the first frame update
    void Start()
    {
        timestamp = Time.time;
    }

    // Update is called once per frame
    void Update()
    {
        if(timestamp < Time.time) {
            float XrandomRanger = Random.Range(-Xrandom/2, Xrandom/2);
            GameObject cookieObj = Instantiate(cookie, new Vector3(XrandomRanger, 0, 0) + this.transform.position, new Quaternion(90, 0, 0, 0));
            timestamp = Time.time + cookieDelay;
            Destroy(cookieObj, 1.5f);
        }

    }
    void OnDrawGizmos() {
        Gizmos.DrawCube(transform.position, new Vector3(Xrandom, 0.1f, 0.1f));        
    }
}
