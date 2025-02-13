using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    public float speed;
    public float sprintIncrease = 3f;
    public float dashDistance = 3f;
    public float dashDuration = 0.2f;
    public float dashCooldown = 1.5f;

    public Animator animator;
    public GameObject attackPrefab; // Prefab ataku
    public Transform attackSpawnPoint; // Punkt, z którego wychodzi atak

    private Vector3 direction;
    private float currentSpeed;
    private bool isDashing;
    private float dashTimer;
    private float dashCooldownTimer;

    private void Start()
    {
        currentSpeed = speed;
    }

    private void Update()
    {
        if (isDashing)
        {
            dashTimer -= Time.deltaTime;
            if (dashTimer <= 0)
            {
                isDashing = false;
                currentSpeed = speed;
            }
            return;
        }

        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");

        direction = new Vector3(horizontal, vertical, 0);

        AnimateMovement(direction);

        // Sprint
        if (Input.GetKey(KeyCode.LeftShift))
        {
            currentSpeed = speed + sprintIncrease;
        }
        else
        {
            currentSpeed = speed;
        }

        // Dash
        if (Input.GetKeyDown(KeyCode.Space) && direction.magnitude > 0 && dashCooldownTimer <= 0)
        {
            StartCoroutine(Dash(direction));
            dashCooldownTimer = dashCooldown;
        }

        // Atak
        if (Input.GetMouseButtonDown(0)) // Lewy przycisk myszy
        {
            Attack();
        }


        // Odliczanie cooldownu
        if (dashCooldownTimer > 0)
        {
            dashCooldownTimer -= Time.deltaTime;
        }
    }

    private void FixedUpdate()
    {
        if (!isDashing)
        {
            transform.position += direction.normalized * currentSpeed * Time.deltaTime;
        }
    }

    void AnimateMovement(Vector3 direction)
    {
        if (animator != null)
        {
            if (direction.magnitude > 0)
            {
                animator.SetBool("isMoving", true);
                animator.SetFloat("Horizontal", direction.x);
                animator.SetFloat("Vertical", direction.y);
            }
            else
            {
                animator.SetBool("isMoving", false);
            }
        }
    }

    private IEnumerator Dash(Vector3 dashDirection)
    {
        isDashing = true;
        dashTimer = dashDuration;
        currentSpeed = 0f;

        Vector3 targetPosition = transform.position + dashDirection.normalized * dashDistance;

        float startTime = Time.time;
        while (Time.time < startTime + dashDuration)
        {
            transform.position = Vector3.Lerp(transform.position, targetPosition, (Time.time - startTime) / dashDuration);
            yield return null;
        }

        isDashing = false;
        currentSpeed = speed;
    }

   private void Attack()
    {
        if (attackPrefab != null && attackSpawnPoint != null)
        {
            GameObject attackInstance = Instantiate(attackPrefab, attackSpawnPoint.position, attackSpawnPoint.rotation);
            Destroy(attackInstance, 0.01f); // Zniszcz instancję ataku po 0.01 sekundy
        }
        else
        {
            Debug.LogError("Attack prefab or spawn point not assigned!");
        }
    }
}
