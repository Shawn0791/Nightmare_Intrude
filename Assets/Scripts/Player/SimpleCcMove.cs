using UnityEngine;
//using DG.Tweening;

public class SimpleCcMove : MonoBehaviour
{
    public CharacterController cc;

    public float speed = 3;

    public float gravity = 8;

    public float jumpSpeed = 12;

    Vector2 _moveDirection;
    float _dropSpeed;
    bool _upKeyDown;
    bool _downKeyDown;
    bool _rightKeyDown;
    bool _leftKeyDown;
    Vector3 extraForce;
    public float extraForcePower = 1;

    void Update()
    {
        _moveDirection = new Vector2(0, 0);
        ReadKeyDown();
        ReadKeyUp();
        SetKeyBoolValue();

        Move();
        ApplyExtraForce();

        if (!cc.isGrounded)
        { Drop(); }
        else { _dropSpeed = 0; }
    }

    private void ReadKeyDown()
    {
        if (Input.GetKeyDown(KeyCode.D))
            _rightKeyDown = true;

        if (Input.GetKeyDown(KeyCode.A))
            _leftKeyDown = true;

        if (Input.GetKeyDown(KeyCode.W))
            _upKeyDown = true;

        if (Input.GetKeyDown(KeyCode.S))
            _downKeyDown = true;

        if (Input.GetKey(KeyCode.Space))
            Jump();

        if (Input.GetKey(KeyCode.X))
        {
            extraForce = new Vector3(-extraForcePower, 0, 0);
        }
    }

    private void ReadKeyUp()
    {
        if (Input.GetKeyUp(KeyCode.RightArrow))
            _rightKeyDown = false;

        if (Input.GetKeyUp(KeyCode.LeftArrow))
            _leftKeyDown = false;

        if (Input.GetKeyUp(KeyCode.UpArrow))
            _upKeyDown = false;

        if (Input.GetKeyUp(KeyCode.DownArrow))
            _downKeyDown = false;
    }

    private void SetKeyBoolValue()
    {
        if (_rightKeyDown)
            _moveDirection.x += 1;

        if (_leftKeyDown)
            _moveDirection.x -= 1;

        if (_upKeyDown)
            _moveDirection.y += 1;

        if (_downKeyDown)
            _moveDirection.y -= 1;
    }

    private void Move()
    {
        if (Mathf.Approximately(_moveDirection.magnitude, 0))
        {
            Stop();
            return;
        }

        Vector3 moveDir = new Vector3(_moveDirection.x, 0, _moveDirection.y);
        cc.Move(moveDir.normalized * speed * Time.deltaTime);

        var rot = Quaternion.LookRotation(moveDir);
        //turn round
        //transform.DOLocalRotate(new Vector3(0, rot.eulerAngles.y, 0), 0.6f);
    }

    private void ApplyExtraForce()
    {
        if (Mathf.Approximately(extraForce.magnitude, 0))
        {
            return;
        }

        cc.Move(extraForce * Time.deltaTime);
        extraForce *= 0.5f;
    }

    void Stop()
    {
        cc.Move(Vector3.zero);
    }

    void Drop()
    {
        _dropSpeed += gravity * Time.deltaTime;
        cc.Move(Vector3.down * Time.deltaTime * _dropSpeed);
    }

    void Jump()
    {
        if (cc.isGrounded)
            _dropSpeed = -jumpSpeed;
    }
}