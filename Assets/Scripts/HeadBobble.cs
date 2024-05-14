using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeadBobble : MonoBehaviour
{
    /*
    [Range(0.001f, 0.01f)]
    public float amount = 0.002f;
    [Range(1f, 30f)]

    public float frequency = 10.0f;

    [Range(10f, 100f)]
    public float smooth = 10.0f;
    private void Update()
    {
        CheckForHeadBob();
    }

    private void CheckForHeadBob()
    {
        float inputMagnitude = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical")).magnitude;

        if(inputMagnitude > 0)
        {
            StartHeadBob();
        }
    }

    private Vector3 StartHeadBob()
    {
        Vector3 pos = Vector3.zero;
        pos.y = Mathf.Lerp(pos.y, Mathf.Sin(Time.deltaTime * frequency) * amount * 1.4f, smooth * Time.deltaTime);
        pos.x = Mathf.Lerp(pos.x, Mathf.Cos(Time.deltaTime * frequency / 2f) * amount * 1.6f, smooth * Time.deltaTime);
        transform.localPosition += pos;

        return pos;
    }
    */
    [SerializeField] private bool enable = true;

    [SerializeField, Range(0, 0.1f)] private float amplitude = 0.015f;
    [SerializeField, Range(0, 30f)] private float frequnecy = 0.015f;

    [SerializeField] private Transform _camera = null;
    [SerializeField] private Transform cameraHolder = null;

    private float toggleSpeed = 3.0f;
    private Vector3 startPos;
    private CharacterController controller;

    private void Awake()
    {
        controller = GetComponent<CharacterController>();
        startPos = _camera.localPosition;
    }
    private void ResetPosition()
    {
        if (_camera.localPosition == startPos) return;
        _camera.localPosition = Vector3.Lerp(_camera.localPosition, startPos, 1 * Time.deltaTime);
    }
    private void PlayMotion(Vector3 motion)
    {
        _camera.localPosition += motion;
    }
    private void CheckMotion()
    {
        float speed = new Vector3(controller.velocity.x, 0, controller.velocity.z).magnitude;

        if (speed < toggleSpeed) return;
        if (!controller.isGrounded) return;

        PlayMotion(FootStepMotion());
    }

    private Vector3 FootStepMotion()
    {
        Vector3 pos = Vector3.zero;
        pos.y += Mathf.Sin(Time.deltaTime * frequnecy) * amplitude;
        pos.x += Mathf.Sin(Time.deltaTime * frequnecy / 2) * amplitude * 2;
        return pos;

    }



    void Update()
    {
        if (!enable) return;

        CheckMotion();
        ResetPosition();
        _camera.LookAt(FocusTarget());
    }

    private Vector3 FocusTarget()
    {
        Vector3 pos = new Vector3(transform.position.x, transform.position.y + cameraHolder.localPosition.y, transform.position.z);
        pos += cameraHolder.forward * 15.0f;
        return pos;
    }
}
