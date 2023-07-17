using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace BugGameNameSpace
{
    [RequireComponent(typeof (Web))]
    [RequireComponent(typeof (Rigidbody))]
    public class PlayerBug : Bug
    {
        public event System.Action<int> OnTakeDamage;
        [Range(0, 2f)]
        [SerializeField] private float clingTime = .9f;
        [SerializeField] private float emptyClingTime = .45f;
        [SerializeField] private Animator anim;
        [SerializeField] private Material material;

        [SerializeField] private AudioSource audioSource;
        [SerializeField] private AudioClip deathClip;
        [SerializeField] private AudioClip takeHitClip;
        [SerializeField] private AudioClip[] webClips;

        Vector3 moveAmount;
        Vector3 smoothMoveVelocity;
        Rigidbody myRigidbody;
        Web web;
        public bool isClinging;
        Color defaultColor;


        protected override void Start()
        {
            base.Start();
            myRigidbody = GetComponent<Rigidbody>();
            web = GetComponent<Web>();
            anim = GetComponent<Animator>();
            defaultColor = material.color;
            audioSource = GetComponent<AudioSource>();
        }

        void Update()
        {
            if (!dead && !isClinging)
            {
                Vector3 inputVector = new Vector3(Input.GetAxisRaw("Horizontal"), 0, Input.GetAxisRaw("Vertical")).normalized;
                Vector3 velocity = inputVector * 1.2f;
                moveAmount = Vector3.SmoothDamp(velocity, moveAmount, ref smoothMoveVelocity, .15f);
                LookAtPoint(velocity);
                WebInput();
                //Animating(inputVector.x, inputVector.y);
                Animating(inputVector);
            }

            CheckYPosition();
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
                float clingingTime = clingedSomething ? clingTime : emptyClingTime ;
                //print("cligned something = " + clingedSomething + " + clinging time = " + clingingTime);
                isClinging = true;
                moveAmount = Vector3.zero;
                Invoke("DisableCling", (clingingTime + .25f));
                int randomClipIndex = Random.Range(0, 2);
                audioSource.clip = webClips[randomClipIndex];
                audioSource.Play();
            }
        }

        void DisableCling()
        {
            isClinging = false;
        }

        public override void TakeBite(int damage)
        {
            material.color = Color.red;
            audioSource.clip = takeHitClip;
            audioSource.Play();
            Invoke("ColorChange", .2f);
            base.TakeBite(damage);

            if (OnTakeDamage != null)
            {
                OnTakeDamage(health);
            }
        }

        protected override void Die()
        {
            if (!dead)
            {
                base.Die();
                moveAmount = Vector3.zero;
                anim.SetTrigger("Die");
                audioSource.clip = deathClip;
                audioSource.Play();
                //Destroy(gameObject, 8);
            }
        }

        void CheckYPosition()
        {
            if (transform.position.y < 0)
            {
                Die();
            }
        }

        void Animating(float horizontal, float vertical)
        {
            bool walking = horizontal != 0f || vertical != 0f;
            print("horizontal = " + horizontal);
            anim.SetBool("IsWalking", walking);
        }

        void Animating(Vector3 inputVector)
        {
            bool walking = inputVector.sqrMagnitude != 0;
            if (isClinging)
            {
                walking = false;
            }
            anim.SetBool("IsWalking", walking);
        }

        void ColorChange()
        {
            material.color = defaultColor;
        }
    }
}