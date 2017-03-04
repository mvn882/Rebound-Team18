﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GravityPortal : MonoBehaviour
{
    public Vector3 GravityDirectionVector = new Vector3(-1, 0, 0);
    private PlayerController _playerController;
    private Transform _playerTransform;
    private Transform _transSelf;
    public float RotateDistance = 20.0f;
    private bool _entered = false;
    private Vector3 _playerStartUpVector;
    private Vector3 _rightVector3;
    private float _step = 0;
    private float _timer = 0;
    private float _angle = 0;
    private float _switchAngle = 0;
    void Start()
    {
        _transSelf = this.transform;
        var playerGameObject = GameObject.FindWithTag("Player");
        if (playerGameObject != null)
        {
            _playerTransform = playerGameObject.transform;
            _playerStartUpVector = _playerTransform.up;
            _playerController = playerGameObject.GetComponent<PlayerController>();
        }
    }
    void Update()
    {
        if (_entered)
        {
            Vector3 direction = _playerTransform.position - _transSelf.position;
            float distance = Vector3.Dot(direction, _transSelf.forward);
            float dot = Vector3.Dot(direction.normalized, _transSelf.forward);

            float range = (distance / RotateDistance);
            if (range < 1.05f && range > -0.05f)
            {
                Vector3 up = Vector3.zero;
                _angle = Vector3.Angle(_playerTransform.up, -GravityDirectionVector);
                Debug.Log("Angle between " + -GravityDirectionVector + " and " +_playerTransform.up + " = " + _angle);

                if (range > 0)
                {
                    if (range < 0.5f)
                    {
                        up = Vector3.Lerp(_playerStartUpVector, _rightVector3, range * 2.0f);
                    }
                    else if (_angle > 0)
                    {
                        up = Vector3.Lerp(_rightVector3, -GravityDirectionVector, (range - 0.5f) * 2.0f);
                    }
                    else
                    {
                        return;
                    }
                }
                else
                {
                    up = _playerStartUpVector;
                }

                up.Normalize();
                _playerController.SetUpVector(up);
            }
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            if (!_entered && !_playerController.GetLockMovement())
            {
                _playerStartUpVector = _playerController.GetUpVector();
                _angle = Vector3.Angle(_playerStartUpVector, -GravityDirectionVector);
                if (_angle > 178)
                {
                    _rightVector3 = _playerController.GetRightVector();
                }
                else
                {
                    _rightVector3 = _playerStartUpVector - GravityDirectionVector;
                    _rightVector3.Normalize();
                }

                _switchAngle = _angle / 2.0f;

                _entered = true;
            }
        }
    }
}
