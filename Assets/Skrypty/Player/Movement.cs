using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    public float speed;

    public Animator animator;

    private Vector3 direction;

    public float Speed { get; private set; }
    public Animator Animator { get; private set; }

    private void Update()
    {
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");

        direction = new Vector3(horizontal, vertical,0);

        AnimateMovement(direction);

        
    }

    private void FixedUpdate()
    {
        this.transform.position += direction.normalized * speed * Time.deltaTime;
    }

    void AnimateMovement(Vector3 direction)
    {
        if(animator != null)
        {
            if(direction.magnitude > 0)
            {
                animator.SetBool("isMoving",true);

                animator.SetFloat("Horizontal",direction.x);
                animator.SetFloat("Vertical", direction.y);
            }
            else
            {
                animator.SetBool("isMoving", false);
            }
        }
    }
}
