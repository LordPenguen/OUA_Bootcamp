using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace BugGameNameSpace
{
    [RequireComponent(typeof (Web))]
    [RequireComponent(typeof (Rigidbody))]
    public class PlayerBug : Bug
    {
        [Range(0, 2f)]
        [SerializeField] private float clingTime = .60f;
        [SerializeField] private float emptyClingTime = .30f;
        Vector3 moveAmount;
        Vector3 smoothMoveVelocity;
        Rigidbody myRigidbody;
        Web web;
        bool isClinging = false;


        protected override void Start()
        {
            base.Start();
            myRigidbody = GetComponent<Rigidbody>();
            web = GetComponent<Web>();
        }

        void Update()
        {
            Vector3 inputVector = new Vector3(Input.GetAxisRaw("Horizontal"), 0, Input.GetAxisRaw("Vertical")).normalized;
            Vector3 velocity = inputVector * 1.3f;
            moveAmount = Vector3.SmoothDamp(velocity, moveAmount, ref smoothMoveVelocity, .15f);
            LookAtPoint(velocity);
            WebInput();
        }

        void FixedUpdate()
        {
            if (!isClinging)
            {
                myRigidbody.MovePosition(myRigidbody.position + moveAmount * Time.deltaTime);
            }            
        }

        void LookAtPoint(Vector3 lookPoint)
        {
            if (lookPoint != Vector3.zero)
            {
                //Vector3 heightCorrectedPoint = new Vector3(lookPoint.x, transform.position.y, lookPoint.z);
                transform.forward = lookPoint;
            }
        }

        void WebInput()
        {
            if (Input.GetMouseButtonDown(0))
            {
                bool clingedSomething = web.ShootWeb(clingTime);
                float clingingTime = clingedSomething ? clingTime : emptyClingTime;
                //print("cligned something = " + clingedSomething + " + clinging time = " + clingingTime);
                isClinging = true;
                moveAmount = Vector3.zero;
                Invoke("DisableCling", clingingTime);
            }
        }

        void DisableCling()
        {
            isClinging = false;
        }
    }
}