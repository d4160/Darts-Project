using System;
using System.Collections;
using System.Collections.Generic;
using d4160.GameFoundation;
using d4160.GameFramework;
using Photon.Pun;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(MultipleStatCalculator))]
public class NetworkingPlayer : PUNEntityBehaviour
{
    public static GameObject LocalEntityInstance;

    private InputValueActions _inputValueActions;
    private InputButtonActions _inputButtonActions;

    protected override void Awake()
    {
        _inputValueActions = GetComponentInChildren<InputValueActions>();
        _inputButtonActions = GetComponentInChildren<InputButtonActions>();

        base.Awake();
    }

    protected void Start()
    {
        if (PhotonView.IsMine)
        {
        }
    }

    protected void OnEnable()
    {
        if (PhotonView.IsMine)
        {
            // Register to input events
        }
    }

    protected void OnDisable()
    {
        if (PhotonView.IsMine)
        {
            // Unregister to input events
        }
    }

    protected void OnDestroy()
    {
        if (PhotonView.IsMine)
        {
            PhotonNetwork.Destroy(this.gameObject);
        }
    }
}
