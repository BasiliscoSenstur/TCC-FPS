using System.Collections;
using UnityEditor;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public static PlayerController instance;

    [Header("Movement")]
    public CharacterController controller;
    public Transform eyeCamera;
    public float speed;
    public float jumpForce;
    [HideInInspector] public float ySpeed;

    [Header("Animation")]
    public Animator anim;
    public string currentAnimation;

    [Header("Gun")]
    public GunController activeGun;
    [HideInInspector] public Transform firePoint;
    public enum guns
    {
        Pistol,
        Machinegun,
        Sniper,
        RocketLuncher,
    }
    [HideInInspector] public guns gunActive;

    [HideInInspector] public GunController pistol;
    [HideInInspector] public GunController machinegun;
    [HideInInspector] public GunController sniper;
    [HideInInspector] public GunController rocketLuncher;
    [HideInInspector] public bool aim;

    [Header("Inputs")]
    public int sensibility;
    public bool invertX, invertY;
    public Vector3 moveInput;
    public Vector2 mouseInput;
    public Vector3 velocity;

    [Header("State")]
    public string STATE;
    public Abstract currentState;
    public Run run = new Run();
    public Walk walk = new Walk();
    public Jump jump = new Jump();

    private void Awake()
    {
        controller = GetComponent<CharacterController>();
        anim = GetComponent<Animator>();
        instance = this;
    }
    void Start()
    {
        currentState = walk;
        currentState.EnterState(this);
        activeGun = pistol;
    }

    void Update()
    {
        //Inputs
        Vector3 verticalInput = transform.forward * Input.GetAxis("Vertical");
        Vector3 horizontalInput = transform.right * Input.GetAxis("Horizontal");

        moveInput = horizontalInput + verticalInput;
        moveInput.Normalize();
        moveInput *= speed;

        //Gravity
        ySpeed += Physics.gravity.y * Time.deltaTime;

        //Jump
        if (controller.isGrounded)
        {
            ySpeed = -0.2f;
            if (Input.GetButtonDown("Jump"))
            {
                ySpeed = jumpForce;
            }
        }
        else
        {
            SwitchState(jump);
        }

        velocity = moveInput;
        velocity.y = ySpeed;

        //Change Gun
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            SwitchGun();
        }
        firePoint = activeGun.firePoint;

        //Rotation - Mouse Input
        mouseInput = new Vector2(Input.GetAxisRaw("Mouse X"), Input.GetAxisRaw("Mouse Y")) * sensibility;
        if (invertX)
        {
            mouseInput.x = -mouseInput.x;
        }
        if (invertY)
        {
            mouseInput.y = -mouseInput.y;
        }

        //aim
        if (Input.GetMouseButtonDown(1))
        {
            aim = true;
        }
        if (Input.GetMouseButtonUp(1))
        {
            aim = false;
        }

        Rotation();

        currentState.LogicsUpdateState(this);

        STATE = currentState.ToString();
    }

    public void SwitchState(Abstract newState)
    {
        currentState.ExitState(this);
        currentState = newState;
        currentState.EnterState(this);
    }

    public void Rotation()
    {
        transform.rotation = Quaternion.Euler(transform.rotation.eulerAngles + new Vector3(0f, mouseInput.x, 0f));
        eyeCamera.rotation = Quaternion.Euler(eyeCamera.transform.rotation.eulerAngles + new Vector3(-mouseInput.y, 0f, 0f));
    }

    public void Movement()
    {
        controller.Move(velocity * Time.deltaTime);
    }

    public void ChangeAnimation(string newAnimation)
    {
        if(currentAnimation == newAnimation)
        {
            return;
        }
        currentAnimation = newAnimation;
        anim.Play(currentAnimation);
    }

    public void Shot()
    {
        RaycastHit hit;
        if (Physics.Raycast(eyeCamera.position, eyeCamera.forward, out hit, 200f))
        {
            firePoint.LookAt(hit.point);
        }
        FireShot();
    }

    void FireShot()
    {
        if (activeGun.currentAmmo > 0)
        {
            activeGun.currentAmmo--;
            UIController.instance.UpdateAmmoDisplay();
            Instantiate(activeGun.bullet, firePoint.position, firePoint.rotation);
        }
    }

    public void ChangeGun()
    {
        switch (gunActive)
        {
            case guns.Pistol:
                pistol.gameObject.SetActive(false);
                activeGun = machinegun;
                gunActive = guns.Machinegun;
                machinegun.gameObject.SetActive(true);
                break;

            case guns.Machinegun:
                machinegun.gameObject.SetActive(false);
                activeGun = sniper;
                gunActive = guns.Sniper;
                sniper.gameObject.SetActive(true);
                break;

            case guns.Sniper:
                sniper.gameObject.SetActive(false);
                activeGun = rocketLuncher;
                gunActive = guns.RocketLuncher;
                rocketLuncher.gameObject.SetActive(true);
                break;

            case guns.RocketLuncher:
                rocketLuncher.gameObject.SetActive(false);
                activeGun = pistol;
                gunActive = guns.Pistol;
                pistol.gameObject.SetActive(true);
                break;
        }
    }

    public void SwitchGun()
    {
        StartCoroutine(OnOffGunCo());
    }

    IEnumerator OnOffGunCo()
    {
        activeGun.enabled = false;
        ChangeGun();
        yield return new WaitForSeconds(0.27f);
        activeGun.enabled = true;
        UIController.instance.UpdateAmmoDisplay();
    }
}