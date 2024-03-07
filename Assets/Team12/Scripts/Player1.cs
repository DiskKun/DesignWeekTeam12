using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

namespace team12
{
    public class Player1 : MicrogameInputEvents
    {
        Vector2 direction;
        public float max_speed = 600; 
        public float speed = 300;
        
        public float boostForce = 100;
        Rigidbody2D rb;
        public float dashCoolDown;
        float coolDownTime = 2.5f;
        GameObject crown;

        SpriteRenderer sr;

        Animator a;

        public AudioSource audioSource;
        public AudioClip phasingThroughObjects;

        // Start is called before the first frame update
        void Start()
        {
            rb = GetComponent<Rigidbody2D>();
            crown = GameObject.Find("Crown");
            sr = GetComponent<SpriteRenderer>();
            a = GetComponent<Animator>();

        }

        // Update is called once per frame
        void Update()
        {
            direction = stick.normalized;

            dashCoolDown -= Time.deltaTime;
            dashCoolDown = Mathf.Clamp(dashCoolDown, 0, coolDownTime);

            if (Controller.TaggedPlayer == gameObject)
            {
                crown.transform.position = transform.position;
            }
            
        }

        private void FixedUpdate()
        {
            rb.AddForce(direction * speed * Time.deltaTime);
            if (rb.velocity.magnitude > 0.1)
            {
                rb.rotation = Mathf.Rad2Deg * Mathf.Atan2(rb.velocity.y * Vector2.up.x - rb.velocity.x * Vector2.up.y, rb.velocity.x * Vector2.up.x + rb.velocity.y * Vector2.up.y);

            }

        }

        protected override void OnButton1Pressed(InputAction.CallbackContext context)
        {
            if (dashCoolDown <= 0)
            {
                a.SetTrigger("Dash");

                audioSource.PlayOneShot(phasingThroughObjects);

                rb.AddForce(direction * boostForce);

                dashCoolDown = coolDownTime;

            }
        }

        private void OnCollisionEnter2D(Collision2D collision)
        {
            if (collision.gameObject.layer == 6)
            {
                Controller.setSinceLastTouch = true;
                rb.AddForce(-(collision.gameObject.transform.position - transform.position).normalized * speed);
            }
        }
        private void OnCollisionExit2D(Collision2D collision)
        {
            Controller.setSinceLastTouch = false;
        }
    }
}
