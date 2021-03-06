﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamFade : MonoBehaviour
{

    public GameObject Player;
    private List<GameObject> hitObjects = new List<GameObject>();
    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        //Debug.Log(hitObjects.Count);

        Vector3 direction = Player.transform.position - transform.position;
        RaycastHit hit;
        bool rayFound = Physics.Raycast(transform.position, direction, out hit);
        Debug.DrawRay(transform.position, direction, Color.red);

        if (rayFound)
        {
            if (hit.distance < direction.magnitude)
            {
                GameObject hitObj = hit.transform.gameObject;

                if (!hitObjects.Contains(hitObj))
                {
                    if (hitObj.tag != "Player" && hitObj.tag != "Portal")
                        hitObjects.Add(hitObj);
                }
                // Change Alpha value
                if (hitObj.GetComponent<Renderer>())
                {
                    Color c = hitObj.GetComponent<Renderer>().material.color;
                    c.a = 0.2f;
                    hit.transform.gameObject.GetComponent<Renderer>().material.color = c;
                }
                // ****
            }
        }
        for (int i = 0; i < hitObjects.Count; i++)
        {
            if (hitObjects[i] != null)
            {
                if (hit.transform != null)
                {

                    if (hit.transform.gameObject != hitObjects[i])
                    {
                        if (hitObjects[i].GetComponent<Renderer>())
                        {
                            Color c = hitObjects[i].GetComponent<Renderer>().material.color;
                            // Change Alpha value
                            c.a = 1.0f;
                            hitObjects[i].GetComponent<Renderer>().material.color = c;
                            // ***

                            // Delete outside of the foreach, so store locally
                            hitObjects.Remove(hitObjects[i]);
                        }
                    }
                }
            }
        }
    }
}
