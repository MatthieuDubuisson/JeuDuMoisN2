using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerScript : MonoBehaviour
{
    //set Camera, character controller, Mesh
    public GameObject Player;
    public GameObject SmokeFx;
    CharacterController controller;
    Transform cameraMain;

    //set move speed and run speed variables
    public float moveSpeed = 4.0f;
    public float runSpeed = 7.0f;

    //set physics variables
    private float verticalVelocity;
    public float gravity = -14.0f;
    private float jumpForce = 7.0f;
    public bool IsOnTheGround = true;

    //set capacity variables
    public bool AllowDoubleJump = false;
    public bool IsTransformed = false;

    //set smooth camera follow variables
    public float turnSmoothTime = 0.2f;
    float turnSmoothVelocity;
    float velocityY;

    //set smooth speed variables
    public float speedSmoothTime = 0.1f;
    float speedSmoothVelocity;
    float currentSpeed;


    private void OnTriggerEnter(Collider other)
    {
        //Check plateform collision to allow the jump
        if (other.gameObject.tag == "Plateform")
        {

            IsOnTheGround = true;
            AllowDoubleJump = false;
        }

        if (other.gameObject.tag == "Win")
        {
            SceneManager.LoadScene("WinScreen");
        }

    }

    private void OnTriggerStay(Collider other)
    {
        //Check if the player is in the enemy red zone and restart level
        if (other.gameObject.tag == "RedLight")
        {
            if (!IsTransformed)
            {
                SceneManager.LoadScene("SampleScene");
                SfxManager.sfxInstance.Alarm.PlayOneShot(SfxManager.sfxInstance.AlarmSnd);
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        //Check plateform collision to reset the double jump
        if (other.gameObject.tag == "Plateform")
        {
            AllowDoubleJump = true;
        }

    }


    void Start()
    {
        //Call camera, characterController and Mesh
        cameraMain = Camera.main.transform;
        controller = GetComponent<CharacterController>();
        Player = GameObject.Find("Player");
        SmokeFx.SetActive(false);
    }

    public IEnumerator  Waiting()
    {
        yield return new WaitForSeconds(1f);
        SmokeFx.SetActive(false);

    }

    void Update()
    {
        //set basic movement inputs

        Vector2 input = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
        Vector2 inputDir = input.normalized;

        //Move Player in the main camera direction
        if (inputDir != Vector2.zero)
        {
            float playerRotation = Mathf.Atan2(inputDir.x, inputDir.y) * Mathf.Rad2Deg + cameraMain.eulerAngles.y;
            transform.eulerAngles = Vector3.up * Mathf.SmoothDampAngle(transform.eulerAngles.y, playerRotation, ref turnSmoothVelocity, turnSmoothTime);
        }

        //Running

        bool running = Input.GetKey(KeyCode.LeftShift);
        float playerSpeed = ((running) ? runSpeed : moveSpeed) * inputDir.magnitude;
        currentSpeed = Mathf.SmoothDamp(currentSpeed, playerSpeed, ref speedSmoothVelocity, speedSmoothTime);

        //player move and add physics
        velocityY -= Time.deltaTime * gravity;
        Vector3 velocity = transform.forward * currentSpeed + Vector3.up * velocityY;
        controller.Move(velocity * Time.deltaTime);

        //Jump
        if (IsOnTheGround)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                IsOnTheGround = false;
                velocityY = jumpForce;
                SfxManager.sfxInstance.Audio.PlayOneShot(SfxManager.sfxInstance.Jump);
            }
        }

        //DoubleJump
        if (!IsOnTheGround && AllowDoubleJump)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                velocityY = jumpForce;
                AllowDoubleJump = false;
                SfxManager.sfxInstance.DoubleJump.PlayOneShot(SfxManager.sfxInstance.DoubleJ);

            }
        }

        //Transform Player Mesh and forbids to move/jump
        if (Input.GetKeyDown(KeyCode.E) && !IsTransformed)
        {
            IsTransformed = true;
            SmokeFx.SetActive(true);
            StartCoroutine(Waiting());
            Player.GetComponent<MeshRenderer>().enabled = false;
            SfxManager.sfxInstance.Transformed.PlayOneShot(SfxManager.sfxInstance.Transfo);

            moveSpeed = 0.0f;
            runSpeed = 0.0f;
            jumpForce = 0.0f;
        }

        //Retransform Player Mesh and allow to move/jump
        if (Input.GetKeyDown(KeyCode.R) && IsTransformed)
        {
            IsTransformed = false;
            SmokeFx.SetActive(true);
            StartCoroutine(Waiting());
            Player.GetComponent<MeshRenderer>().enabled = true;
            SfxManager.sfxInstance.DeTransformed.PlayOneShot(SfxManager.sfxInstance.DeTransfo);

            moveSpeed = 4.0f;
            runSpeed = 7.0f;
            jumpForce = 7.0f;
        }

    }

}
