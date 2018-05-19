using UnityEngine;

public class CarController : MonoBehaviour {

    private Rigidbody2D _body;
    private GameObject _wFront, _wBack;

    private float _forceMotor = 0f;
    private float _rotationSpeed = 0f;

    private float movement = 0f;
    private float rotation = 0f;

    private Transform _transform;

    public CarController(Rigidbody2D body, GameObject wFront, GameObject wBack, Transform transform, float forceMotor, float rotationSpeed)
    {
        _body = body;
        _wFront = wFront;
        _wBack = wBack;
        _transform = transform;
        _forceMotor = forceMotor;
        _rotationSpeed = rotationSpeed;
    }

    public void Move(bool _isGrounded)
    {
        if (movement != 0f)
        {
            if (_isGrounded) _body.AddForce(_transform.right * _forceMotor * Time.fixedDeltaTime);
            else if (!_isGrounded) _body.AddForce(_transform.right * 0f * Time.fixedDeltaTime);
        }

        _body.AddTorque(rotation * _rotationSpeed * Time.fixedDeltaTime);
    }

    public void Controller(bool _isGrounded)
    {
        if (OnTouch.GetTouch())
        {
            if (!_isGrounded)
            {
                rotation = 1;
                _body.drag = 0f;
            } else if (_isGrounded)
            {
                movement = _forceMotor * -1;
                _body.drag = 0f;
            }
        } else
        {
            movement = 0f;
            rotation = 0f;
            if (_wFront.GetComponent<CollisionWheel>().IsGrounded() && _wBack.GetComponent<CollisionWheel>().IsGrounded()) _body.drag = 0.5f;
            else if (!_wFront.GetComponent<CollisionWheel>().IsGrounded() && _wBack.GetComponent<CollisionWheel>().IsGrounded()) _body.drag = 0.15f;
        }
    }

    public Rigidbody2D GetBody()
    {
        return _body;
    }
    public float GetMovement()
    {
        return movement;
    }
    public float GetRotation()
    {
        return rotation;
    }
}