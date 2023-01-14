using UnityEngine;

[RequireComponent(typeof(LineRenderer))]
public class PhysicalRope : MonoBehaviour
{
    [SerializeField] private GameObject _ropeElement;
    [SerializeField] private Transform _startPoint;
    [SerializeField] private Transform _endPoint;
    [SerializeField] private int _ropeCount = 4;
    private LineRenderer _lineRenderer;

    private void Awake()
    {
        _lineRenderer = GetComponent<LineRenderer>();    
    }

    private void FixedUpdate()
    {
        if(transform.childCount > 0)
        {
            for (int i = 0; i < transform.childCount; i++)
            {
                Transform child = transform.GetChild(i);
                _lineRenderer.SetPosition(i,child.position);
            }
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void CreateRope(Vector3 startPoint, Vector3 endPoint)
    {
        SpringJoint eachSpring;
        SpringJoint prevSpring;
        Vector3 beetwen;
        Vector3 elementPos;
        float beetwenDistance;

        if(transform.childCount > 0)
        {
            for(int i = 0; i < transform.childCount; i ++)
            {
                Transform child = transform.GetChild(i);
                Destroy(child.gameObject);
            }

            _lineRenderer.positionCount = 0;
            transform.DetachChildren();
        }

        beetwen = endPoint - startPoint;
        elementPos = (beetwen / _ropeCount);
        beetwenDistance = elementPos.magnitude;
        _lineRenderer.positionCount = _ropeCount;

        eachSpring = Instantiate(_ropeElement, startPoint, Quaternion.identity).GetComponent<SpringJoint>();
        eachSpring.transform.SetParent(transform);
        eachSpring.minDistance = beetwenDistance;
        eachSpring.maxDistance = beetwenDistance;
        prevSpring = eachSpring;

        for (int i = 1; i < _ropeCount; i++)
        {
            Vector3 elementPosition = startPoint + elementPos * i;

            eachSpring = Instantiate(_ropeElement, elementPosition, Quaternion.identity).GetComponent<SpringJoint>();
            eachSpring.transform.SetParent(transform);
            prevSpring.connectedBody = eachSpring.GetComponent<Rigidbody>();
            eachSpring.minDistance = beetwenDistance;
            eachSpring.maxDistance = beetwenDistance;
            prevSpring = eachSpring;
        }

        prevSpring.GetComponent<Rigidbody>().isKinematic = true;
    }
}
