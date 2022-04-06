using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float wood;
    public float stone;
    public float gold;
    public int storageCount;

    public float forwardSpeed;
    public float horizontalSpeed;
    private float moveSmoothness = .5f;
    [SerializeField] private Transform leftPoint;
    [SerializeField] private Transform rightPoint;
    public Transform Storage;
    private Animator animator;

    private Touch touch;
    private float deltaTouch;

    [SerializeField] private float lerpTime;
    public bool reversed = false;
    public AnimationCurve rotationCurve;

    private void Start()
    {
        animator = GetComponent<Animator>();
    }

    private void Update()
    {
        Movements();
    }

    public void Reverse()
    {
        reversed = !reversed;

        if (reversed)
            LeanTween.rotateY(gameObject, transform.localRotation.eulerAngles.y + 180f, lerpTime).setEase(rotationCurve);
        else
            LeanTween.rotateY(gameObject, transform.localRotation.eulerAngles.y - 180f, lerpTime).setEase(rotationCurve);

        Debug.Log("turned");
    }

    public void Gather(Goodie.Goodies _goodiesType, int _value, GameObject _object)
    {
        storageCount++;

        switch (_goodiesType)
        {
            case Goodie.Goodies.wood:
                wood += _value;
                break;
            case Goodie.Goodies.stone:
                stone += _value;
                break;
            case Goodie.Goodies.gold:
                gold += _value;
                break;
        }

    }

    private void Movements()
    {
        //idle running
        transform.Translate(Vector3.forward * Time.deltaTime * (forwardSpeed/100));

        if (leftPoint.gameObject.active && rightPoint.gameObject.active)
        {
            //horizontal moving
            if (Input.touchCount > 0)
            {
                touch = Input.GetTouch(0);

                if (touch.phase == TouchPhase.Moved)
                {
                    deltaTouch = touch.deltaPosition.x;

                    moveSmoothness += deltaTouch * Time.deltaTime * (horizontalSpeed / 100);
                }
            }

            Vector3 leftPose = new Vector3(leftPoint.position.x, transform.position.y, leftPoint.position.z);

            Vector3 rightPose = new Vector3(rightPoint.position.x, transform.position.y, rightPoint.position.z);

            transform.position = Vector3.Lerp(leftPose, rightPose, moveSmoothness);
        }
        
    }
}
