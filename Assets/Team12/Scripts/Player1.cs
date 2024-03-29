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
        public AudioClip[] dashSounds;

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
                if (rb.velocity.magnitude > 0.1)
                {
                    crown.transform.rotation = Quaternion.Euler(0, 0, rb.rotation);
                }
            }
            if (Controller.TaggedPlayer != gameObject)
            {
                a.SetBool("it", false);
            }
            else if (Controller.TaggedPlayer == gameObject)
            {
                a.SetBool("it", true);
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
                a.SetTrigger("dash");

                // This tutorial was used for making the random sounds work: https://youtu.be/c2c-x79PPXw?si=DRPLQoaMFgHr3oez
                AudioClip randomDash = dashSounds[Random.Range(0, dashSounds.Length)];
                audioSource.PlayOneShot(randomDash);

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

        protected override void OnTimesUp()
        {

            // Code to execute when time runs out in the game

            if (Controller.TaggedPlayer == gameObject)
            {
                gameObject.transform.position = new Vector3(1000, 1000, 0);
                gameObject.SetActive(false);
                crown.SetActive(false);
            }
        }
    }
}