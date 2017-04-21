﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Launcher : MonoBehaviour
{
    public GameObject _coll;
    public float power = 1000;
    // Use this for initialization
    void Start()
    {
        GameObject.FindGameObjectWithTag("Player").GetComponent<PhysicsPlayerController>().SetLockMovement(true);

    }

    void Update()
    {
        if (Input.GetButtonDown("Slam"))
        {
            Launch();
        }

    }
    private void Launch()
    {
        StartCoroutine(playanimation());

    }

    private IEnumerator playanimation()
    {
        var pos = _coll.transform.position;
        var rot = _coll.transform.rotation;
        GetComponent<Animation>()["launch"].speed = 1;
        GetComponent<Animation>()["launch"].time = 0;
        GetComponent<Animation>().Play();
        _coll.GetComponent<Rigidbody>().AddForce(0, 0, power);
        yield return new WaitForSeconds(0.1f);
        _coll.GetComponent<Rigidbody>().velocity = Vector3.zero;
        _coll.transform.position = pos;
        _coll.transform.rotation = rot;
        GameObject.FindGameObjectWithTag("Player").GetComponent<PhysicsPlayerController>().SetLockMovement(false);
    }
}
