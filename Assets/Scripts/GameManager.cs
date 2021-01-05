using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{

    public static GameManager Instance { get; set; }
    private void Awake()
    {
        if (Instance == null)
            Instance = this;
    }

    Camera Cam;

    [SerializeField]
    protected Trajectory trajectory;
    [SerializeField]
    protected Joystick joystick;
    [SerializeField]
    protected Ball Ball;
    [SerializeField]
    protected GameObject target;

    [SerializeField]
    protected float pushForce = 4f;

    protected bool IsDragging { get { return (joystick && (joystick.Horizontal != 0 || joystick.Vertical != 0)); } }

    protected Vector3 startPoint = Vector3.zero;
    protected Vector3 endPoint;
    protected Vector3 direction;
    protected Vector3 force;
    protected float distance;
    private bool clicked;
    Vector3 lookAtPosition;
    // Start is called before the first frame update
    void Start()
    {
        Cam = Camera.main;
        endPoint = Ball.transform.position;
        startPoint = new Vector3(endPoint.x, endPoint.y, 0);
        Ball.DesactivateRb();
        //OnDragStart();
    }



    // Update is called once per frame
    void Update()
    {
        lookAtPosition = Cam.ScreenToWorldPoint(new Vector3(Screen.width / 2, Screen.height / 2, Cam.nearClipPlane));
        Debug.DrawLine(Cam.transform.position, lookAtPosition);
        //Change for tactile :scream:
        if (joystick)
        {

        }
        if (Input.GetMouseButtonUp(0))
        {
        }
        if (IsDragging)
        {
            OnDrag();
        }
    }
    #region Begin : Drag
    protected void OnDragStart()
    {

        Ball.DesactivateRb();
        trajectory.Show();
    }

    protected void OnDrag()
    {
        //endPoint = Cam.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, -Cam.transform.position.z));

        //For debug
        Debug.DrawLine(startPoint, endPoint, Color.red);
        trajectory.UpdateDots(Ball.Pos, force);
    }

    protected void OnDragEnd()
    {
        distance = Vector2.Distance(lookAtPosition, Ball.transform.position);
        direction = (Ball.transform.position - lookAtPosition).normalized;
        Debug.LogWarning(distance);
        force = direction * distance * pushForce;
        Ball.ActivateRb();
        Ball.Push(force);
        trajectory.Hide();
    }

    #endregion End : Drag

    public void Click()
    {
        OnDragStart();
        clicked = true;
    }
    public void Unclick()
    {

        OnDragEnd();

        clicked = false;
    }
}
