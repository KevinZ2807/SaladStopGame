using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;
using TMPro;
using DG.Tweening;

    public class PlayerController : MonoBehaviour
    {
        [Header("Attributes")]
        [SerializeField] private VariableJoystick joystick;
        [SerializeField] private Animator m_animator = null;
        [SerializeField] private Rigidbody m_rigidBody = null;
        [SerializeField] private Button jumpButton;
        [SerializeField] private Button actionButton;
        [SerializeField] private Button sprintButton;
        [SerializeField] private LayerMask layerMask;
        [SerializeField] private ParticleSystem runningEffect;

        [SerializeField] private List<Transform> stuffs = new List<Transform>();
        [SerializeField] private Transform holdingPlace;
        [SerializeField] private int maxiumCarry = 20;

        [Header("References")]
        [SerializeField] private float m_moveSpeed = 2;
        [SerializeField] private float m_jumpForce = 4;
        
        private float m_currentV = 0;
        private float m_currentH = 0;

        private readonly float m_interpolation = 10;
        private readonly float m_walkScale = 0.33f;

        private bool m_sprintToggle = true;
        private bool m_wasGrounded;
        private Vector3 m_currentDirection = Vector3.zero;

        private float m_jumpTimeStamp = 0;
        private float m_minJumpInterval = 0.25f;
        private bool m_jumpInput = false;

        private bool m_isGrounded;

        private Vector3 lastMoveDir;

        
        private float YAxis, delay;



        // ---------------------------------------- COLLISION ----------------------------------------
        private List<Collider> m_collisions = new List<Collider>();
        private void OnCollisionEnter(Collision collision)
        {
            ContactPoint[] contactPoints = collision.contacts;
            for (int i = 0; i < contactPoints.Length; i++)
            {
                if (Vector3.Dot(contactPoints[i].normal, Vector3.up) > 0.5f)
                {
                    if (!m_collisions.Contains(collision.collider))
                    {
                        m_collisions.Add(collision.collider);
                    }
                    m_isGrounded = true;
                }
            }
        }

        private void OnCollisionStay(Collision collision)
        {
            ContactPoint[] contactPoints = collision.contacts;
            bool validSurfaceNormal = false;
            for (int i = 0; i < contactPoints.Length; i++)
            {
                if (Vector3.Dot(contactPoints[i].normal, Vector3.up) > 0.5f)
                {
                    validSurfaceNormal = true; break;
                }
            }

            if (validSurfaceNormal)
            {
                m_isGrounded = true;
                if (!m_collisions.Contains(collision.collider))
                {
                    m_collisions.Add(collision.collider);
                }
            }
            else
            {
                if (m_collisions.Contains(collision.collider))
                {
                    m_collisions.Remove(collision.collider);
                }
                if (m_collisions.Count == 0) { m_isGrounded = false; }
            }
        }

        private void OnCollisionExit(Collision collision)
        {
            if (m_collisions.Contains(collision.collider))
            {
                m_collisions.Remove(collision.collider);
            }
            if (m_collisions.Count == 0) { m_isGrounded = false; }
        }

        private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("PlaceDown")) {
            Debug.Log("Start Working");
            other.GetComponent<ServiceTable>().Work();
        }   
        if (other.CompareTag("dollar"))
        {
            Destroy(other.gameObject);
            GameManager.instance.UpdateMoney(5);
        }
    }


        // ------------------------------------ AWAKE ------------------------------------------
        private void Awake()
        {
            if (!m_animator) { gameObject.GetComponent<Animator>(); }
            if (!m_rigidBody) { gameObject.GetComponent<Rigidbody>(); }
            if (m_sprintToggle) sprintButton.image.color = Color.green; else sprintButton.image.color = Color.red;
        }

        private void Start() {
            stuffs.Add(holdingPlace);
        }
        
        // ------------------------------------ UPDATE ------------------------------------------
        private void Update() {   
            stackingStuff();
        }
        void stackingStuff() {
            if (stuffs.Count > 1) {
                for (int i = 1; i < stuffs.Count; i++)
                {
                    var firstPaper = stuffs.ElementAt(i - 1);
                    var secondPaper = stuffs.ElementAt(i);
                    
                    secondPaper.position = new Vector3(Mathf.Lerp(secondPaper.position.x,firstPaper.position.x,Time.deltaTime * 15f),
                    Mathf.Lerp(secondPaper.position.y,firstPaper.position.y + 0.17f,Time.deltaTime * 15f),firstPaper.position.z);
                }
            }
        }
        public void OnJumpPressed() {
            if (!m_jumpInput)
            {
                m_jumpInput = true;
            }
        }
        public void OnActionPressed() {
            m_animator.SetTrigger("Pickup");
        }
        public void OnSprintPressed() {
            m_sprintToggle = !m_sprintToggle;
            if (m_sprintToggle) sprintButton.image.color = Color.green;
            else sprintButton.image.color = Color.red;
        }

        // ------------------------------------ FIXED UPDATE ------------------------------------------
        private void FixedUpdate()
        {
            m_animator.SetBool("Grounded", m_isGrounded);
            DirectUpdate();
            m_wasGrounded = m_isGrounded;
            m_jumpInput = false;
        }

        void DetectInteraction(float h, float v) {
            //float interactDistance = 2f;
            Vector3 moveDir = new Vector3(h, 0f, v);
            if (moveDir != Vector3.zero) lastMoveDir = moveDir; 
            if (Physics.Raycast(transform.position,transform.forward,out var raycastHit,1f)) { // Create a raycast to detect object in front
                if (raycastHit.collider.CompareTag("Pickup") && stuffs.Count < 21) {
                    Debug.Log(raycastHit);
                    var stuff = raycastHit.collider.transform;
                    stuff.rotation = Quaternion.Euler(stuff.rotation.x,Random.Range(0f,180f),stuff.rotation.z);
                    stuffs.Add(stuff);
                    //stuff.parent = null;
                    stuff.parent = gameObject.transform.Find("inventory");
                    stuff.GetComponent<Stuffs>().CancelDestruction();
                }
                if (raycastHit.collider.CompareTag("PlaceDown") && stuffs.Count > 1) {
                    var WorkDesk = raycastHit.collider.transform.GetChild(0);

                    //if (WorkDesk.childCount > 0) {
                    //YAxis = WorkDesk.GetChild(WorkDesk.childCount - 1).position.y;
                    //}
                    //else {
                    YAxis = WorkDesk.position.y;
                    //}

                    for (var index = stuffs.Count - 1; index >= 1; index--) {
                        stuffs[index].DOJump(new Vector3(WorkDesk.position.x, YAxis, WorkDesk.position.z), 2f, 1, 0.2f)
                            .SetDelay(delay).SetEase(Ease.Flash);

                        stuffs.ElementAt(index).parent = WorkDesk;
                        stuffs.RemoveAt(index);

                        YAxis += 0.05f;
                        delay += 0.02f;
                    }
                
                }
            }
        }

        private void RunningParticle() {
            if (isRunning && !runningEffect.isPlaying) {
                runningEffect.Play();
            }
            else if (!isRunning && runningEffect.isPlaying) {
                runningEffect.Stop();
            }     
        }


        private bool isRunning = false;
        
        private void DirectUpdate()
        {
            float v = joystick.Vertical * m_moveSpeed;
            float h = joystick.Horizontal * m_moveSpeed;
            
            DetectInteraction(h, v);
            
            if (!m_sprintToggle)
            {
                v *= m_walkScale;
                h *= m_walkScale;
            }
            if (Mathf.Sqrt(v * v + h * h) >= 0.5f) {isRunning = true;}
            else {isRunning = false;}

            RunningParticle();

            RotateCharacter(v, h);
            JumpingAndLanding();
        }
        void  RotateCharacter(float v, float h) {  // Calculate the direction for character to look to
            
            Transform camera = Camera.main.transform;
            
            m_currentV = Mathf.Lerp(m_currentV, v, Time.deltaTime * m_interpolation);
            m_currentH = Mathf.Lerp(m_currentH, h, Time.deltaTime * m_interpolation);

            Vector3 direction = camera.forward * m_currentV + camera.right * m_currentH;

            float directionLength = direction.magnitude;
            direction.y = 0;
            direction = direction.normalized * directionLength;

            if (direction != Vector3.zero)
            {
                // Direction character looking to
                m_currentDirection = Vector3.Slerp(m_currentDirection, direction, Time.deltaTime * m_interpolation);

                transform.rotation = Quaternion.LookRotation(m_currentDirection);
                transform.position += m_currentDirection * m_moveSpeed * Time.deltaTime;

                m_animator.SetFloat("MoveSpeed", direction.magnitude);
            }
        }
        private void JumpingAndLanding()
        {
            bool jumpCooldownOver = (Time.time - m_jumpTimeStamp) >= m_minJumpInterval;

            if (jumpCooldownOver && m_isGrounded && m_jumpInput)
            {
                m_jumpTimeStamp = Time.time;
                m_rigidBody.AddForce(Vector3.up * m_jumpForce, ForceMode.Impulse);
            }
        }

    }
