using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    private Gun currentGun;
    private CloseWeapon currentCloseWeapon;
    //Speed Control
    [SerializeField] private float walkSpeed;
    [SerializeField] private float runSpeed;
    [SerializeField] private float crouchSpeed;

    private float applySpeed;

    [SerializeField] private float jumpForce;

    //State
    private bool isWalk = false;
    private bool isRun = false;
    private bool isCrouch = false;
    private bool isGround = true;

    //movement check
    private Vector3 lastPos;


    //sitting pos
    [SerializeField]
    private float crouchPosY;
    private float originPosY;
    private float applyCrouchPosY;

    //Ground 
    private CapsuleCollider capsuleCollider;

    //Sensitivity
    [SerializeField] private float lookSensitivity;

    //Camera Limit
    [SerializeField]
    private float cameraRotationLimit;
    private float currentCameraRotationX;

    //Component
    [SerializeField] private Camera theCamera;
    private Rigidbody myRigid;
    private GunController theGunController;
    private Crosshair theCrosshair;
    private StatusController theStatusController;


    // Start is called before the first frame update
    void Start()
    {
        capsuleCollider = GetComponent<CapsuleCollider>();
        myRigid = GetComponent<Rigidbody>();
        theCrosshair = FindObjectOfType<Crosshair>();
        theGunController = FindObjectOfType<GunController>();
        currentGun = FindObjectOfType<Gun>();
        theStatusController = FindObjectOfType<StatusController>();

        //initialize
        originPosY = theCamera.transform.localPosition.y; // local - Camera base
        applyCrouchPosY = originPosY;
        applySpeed = walkSpeed;

    }

    // Update is called once per frame
    void Update()
    {
        IsGround();
        TryJump();
        TryRun();
        TryCrouch();
        Move();
        if (!Inventory.inventoryActivated)
        {
            CameraRotation();
            CharacterRotation();
        }

    }
    // regular frame & good at physics engine ( ex.rigidbody, distance)
    private void FixedUpdate()
    {
        MoveCheck();
    }
    // Crouch Key
    private void TryCrouch()
    {
        if (Input.GetKeyDown(KeyCode.LeftControl))
        {
            Crouch();
        }
    }
    // Crouch Act
    private void Crouch()
    {
        isCrouch = !isCrouch; // true - false switch
        theCrosshair.CrouchingAnimation(isCrouch);
        if (isCrouch)
        {
            applySpeed = crouchSpeed;
            applyCrouchPosY = crouchPosY;
        }
        else
        {
            applySpeed = walkSpeed;
            applyCrouchPosY = originPosY;
        }
        StartCoroutine(CrouchCoroutine());
    }

    // Soft Crouch movement
    IEnumerator CrouchCoroutine()
    {
        float _posY = theCamera.transform.localPosition.y;
        int count = 0;

        while (_posY != applyCrouchPosY)
        {
            count++;
            //Lerp : Linear interpolation
            _posY = Mathf.Lerp(_posY, applyCrouchPosY, 0.1f);
            theCamera.transform.localPosition = new Vector3(0, _posY, 0);
            if (count > 15) break; //Lerp while exit
            yield return null; // per 1 frame
        }
        theCamera.transform.localPosition = new Vector3(0, applyCrouchPosY, 0f); // Lerp while exit >> position = 0


    }

    // is player on Ground 
    private void IsGround()
    {
        //extents : half , 0.1f : resolve stair bug 
        isGround = Physics.Raycast(transform.position, Vector3.down, capsuleCollider.bounds.extents.y + 0.1f);
        theCrosshair.JumpingAnimation(!isGround);
    }

    // Jump Key
    private void TryJump()
    {
        if (Input.GetKeyDown(KeyCode.Space) && isGround && isGround && theStatusController.GetCurrentSp() > 0)
        {
            Jump();
        }
    }

    // Jump Act
    private void Jump()
    {
        // crouch and jump : no crouch
        if (isCrouch)
        {
            Crouch();
        }
        theStatusController.DecreaseStamina(100);
        myRigid.velocity = transform.up * jumpForce;
    }

    // Run Key
    private void TryRun()
    {
        if (Input.GetKey(KeyCode.LeftShift) && theStatusController.GetCurrentSp() > 0)
        {
            Running();
        }
        if (Input.GetKeyUp(KeyCode.LeftShift) || theStatusController.GetCurrentSp() <= 0)
        {
            RunningCancel();
        }
    }

    // Run Act
    private void Running()
    {
        // crouch and run : no crouch
        if (isCrouch)
        {
            Crouch();
        }
        theGunController.CancelFineSight();

        isRun = true;
        theCrosshair.RunningAnimation(isRun);
        theStatusController.DecreaseStamina(1);
        applySpeed = runSpeed;
    }

    // Run Cancel
    private void RunningCancel()
    {
        isRun = false;
        theCrosshair.RunningAnimation(isRun);
        applySpeed = walkSpeed;
    }

    // Move Key and Act
    private void Move()
    {
        float _moveDirX = Input.GetAxisRaw("Horizontal");
        float _moveDirZ = Input.GetAxisRaw("Vertical");

        Vector3 _moveHorizontal = transform.right * _moveDirX; // transform.right : (1,0,0)
        Vector3 _moveVertical = transform.forward * _moveDirZ; // forward : (0,0,1)

        // (1,0,1) = 2  >> normalized : (0.5,0,0.5) = 1 ,, same direction and total 1 is best
        Vector3 _velocity = (_moveHorizontal + _moveVertical).normalized * applySpeed;

        myRigid.MovePosition(transform.position + _velocity * Time.deltaTime);
    }

    private void MoveCheck()
    {
        if (!isRun && !isCrouch && isGround)
        {
            if (Vector3.Distance(lastPos, transform.position) >= 0.01f) // distance a from b
                isWalk = true;
            else
                isWalk = false;

            theCrosshair.WalkingAnimation(isWalk);
            lastPos = transform.position;
        }
    }
    // Camera Up and Down Rotation
    private void CameraRotation()
    {
        float _xRotation = Input.GetAxisRaw("Mouse Y"); // mouse is 2D
        float _cameraRotationX = _xRotation * lookSensitivity;
        currentCameraRotationX -= _cameraRotationX; // minus : mouse reverse
        //Clamp : max,min limit
        currentCameraRotationX = Mathf.Clamp(currentCameraRotationX, -cameraRotationLimit, cameraRotationLimit);

        theCamera.transform.localEulerAngles = new Vector3(currentCameraRotationX, 0f, 0f);
    }

    // Character Left and Right Rotation
    private void CharacterRotation()
    {
        //left,right rotation with Camera
        float _yRotation = Input.GetAxis("Mouse X");
        Vector3 _characterRotationY = new Vector3(0f, _yRotation, 0f) * lookSensitivity;
        myRigid.MoveRotation(myRigid.rotation * Quaternion.Euler(_characterRotationY)); // convert Euler to Quaternion
    }
}
