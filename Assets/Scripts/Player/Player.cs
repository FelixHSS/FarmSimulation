using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private Rigidbody2D Rb { get; set; }
    [field: SerializeField] private float Speed { get; set; }
    private float InputX { get; set; }
    private float InputY { get; set; }

    private Vector2 MovementInput { get; set; }

    private Animator[] Animators { get; set; }
    private bool IsMoving { get; set; }
    private bool IsInputDisabled { get; set; }

    private void Awake()
    {
        Rb = GetComponent<Rigidbody2D>();
        Animators = GetComponentsInChildren<Animator>();
    }

    private void OnEnable()
    {
        EventHandler.BeforeSceneUnloadEvent += OnBeforeSceneUnloadEvent;
        EventHandler.AfterSceneLoadedEvent += OnAfterSceneLoadedEvent;
        EventHandler.MoveToPositionEvent += OnMoveToPositionEvent;
    }

    private void OnDisable()
    {
        EventHandler.BeforeSceneUnloadEvent -= OnBeforeSceneUnloadEvent;
        EventHandler.AfterSceneLoadedEvent -= OnAfterSceneLoadedEvent;
        EventHandler.MoveToPositionEvent -= OnMoveToPositionEvent;
    }

    private void OnMoveToPositionEvent(Vector3 targetPostion)
    {
        transform.position = targetPostion;
    }

    private void OnAfterSceneLoadedEvent()
    {
        IsInputDisabled= false;
    }

    private void OnBeforeSceneUnloadEvent()
    {
        IsInputDisabled = true;
    }

    private void Update()
    {
        if (!IsInputDisabled)
            PlayerInput();
        SwitchAnimation();
    }

    private void FixedUpdate()
    {
        Movement();
    }

    private void PlayerInput()
    {
        InputX = Input.GetAxisRaw("Horizontal");
        InputY = Input.GetAxisRaw("Vertical");

        // Move diagonally
        if (InputX != 0 && InputY != 0)
        {
            InputX *= 0.6f;
            InputY *= 0.6f;
        }

        // Switch moving speed
        if (Input.GetKey(KeyCode.LeftShift))
        {
            InputX *= 0.5f;
            InputY *= 0.5f;
        }

        MovementInput = new Vector2(InputX, InputY);

        IsMoving = MovementInput != Vector2.zero;
    }

    private void Movement()
    {
        Rb.MovePosition(Rb.position + Speed * Time.deltaTime * MovementInput);
    }

    private void SwitchAnimation()
    {
        foreach (var animator in Animators)
        {
            animator.SetBool("IsMoving", IsMoving);
            if (IsMoving)
            {
                animator.SetFloat("InputX", InputX);
                animator.SetFloat("InputY", InputY);
            }
        }
    }
}
