using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CameraController : MonoBehaviour
{
    private static CameraController instance;
    public static CameraController Instance => instance;

    private void Awake()
    {
        instance = this;
    }

    public CinemachineImpulseSource impulseSource;

    public void PlayerShakeAnimation()
    {
        impulseSource.GenerateImpulse();
    }

}
