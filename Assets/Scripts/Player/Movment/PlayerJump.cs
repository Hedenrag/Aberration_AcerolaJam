using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(GroundDetection))]
public class PlayerJump : MonoBehaviour
{
    [SerializeField] float _jumpForce;

    [SerializeField, HideInNormalInspector] GroundDetection _groundDetection;
    [SerializeField, HideInNormalInspector] Rigidbody _rb;

    bool _jumped;
    bool _jumpInCD = false;
    float _jumpCDTime = 0.1f;

    public bool blockJump;

    private void OnValidate()
    {
        _groundDetection = GetComponent<GroundDetection>();
        _rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        Debug.Log("_groundDetection:" + _groundDetection.Grounded);
        if (blockJump) return;
        if(Input.GetKeyDown(KeyCode.Space))
        {
            TryJump();
        }
    }

    private void FixedUpdate()
    {
        if(_groundDetection.Grounded && !_jumpInCD)
        {
            _jumped = false;
        }
    }

    private void TryJump()
    {
        if (_groundDetection.Grounded)
        {
            DoJump();
        }
        else if (_groundDetection.Sliding)
        {
            if (!_jumped)
            {
                DoJump();
            }
        }
    }

    void DoJump()
    {
        _rb.AddForce(transform.up * _jumpForce, ForceMode.Impulse);
        _jumped = true;
        StartCoroutine(JumpCD());
    }

    IEnumerator JumpCD()
    {
        _jumpInCD = true;
        yield return new WaitForSeconds(_jumpCDTime);
        _jumpInCD = false;
    }
}
