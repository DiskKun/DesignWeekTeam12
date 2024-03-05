using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

namespace team12
{
    public class Player1 : MicrogameInputEvents
    {
        Vector2 direction;
        float speed = 300;
        float boostForce = 500;
        Rigidbody2D rb;
        public float dashCoolDown;
        float coolDownTime = 5;
        GameObject crown;

        public bool isStartingIt;

        public static bool touching;
        // Start is called before the first frame update
        void Start()
        {
            rb = GetComponent<Rigidbody2D>();
            crown = GameObject.Find("Crown");
            if (isStartingIt)
            {
                //Controller.SetTaggedPlayer(gameObject);
            }
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
            

        }

        protected override void OnButton1Pressed(InputAction.CallbackContext context)
        {
            if (dashCoolDown <= 0)
            {
                rb.AddForce(direction * boostForce);
                dashCoolDown = coolDownTime;
            }
        }

        private void OnCollisionEnter2D(Collision2D collision)
        {
            if (collision.gameObject.layer == 6)
            {
                Controller.setSinceLastTouch = true;
            }
        }
        private void OnCollisionExit2D(Collision2D collision)
        {
            Controller.setSinceLastTouch = false;
        }
    }
}
