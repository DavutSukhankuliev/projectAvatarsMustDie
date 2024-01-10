using Oculus.Interaction;
using UnityEngine;

public class SpellAim : MonoBehaviour
{
    [SerializeField] private LineRenderer laserLineRenderer;
    [SerializeField] private Color wrongColor;
    [SerializeField] private Color rightColor;
    [SerializeField] private float laserWidth = 0.01f;
    [SerializeField] private float laserMaxLength = 5f;
    [SerializeField] private Transform parentPosition;
    [SerializeField] private float lerpSpeed;
    [SerializeField, Interface(typeof(ISelector))]
    private MonoBehaviour selector;

    private Vector3 _endPosition;
    private ISelector _selector;
    private bool _isLevel;
    private bool _isShape;

    public bool IsLevel => _isLevel;
    public bool IsShape => _isShape;
    public Vector3 EndPosition => _endPosition;
    
    void Start() 
    {
        _selector = selector as ISelector;
        _selector.WhenSelected += () => { _isShape = true; };
        _selector.WhenUnselected += () => { _isShape = false; };
        Vector3[] initLaserPositions = { Vector3.zero, Vector3.zero };
        laserLineRenderer.SetPositions( initLaserPositions );
        laserLineRenderer.SetWidth( laserWidth, laserWidth );
    }
 
    void Update() 
    {
        if (_isShape)
        {
            if (!laserLineRenderer.enabled)
            {
                laserLineRenderer.enabled = true;
            }
            ShootLaserFromTargetPosition();
        }
        else
        {
            laserLineRenderer.enabled = false;
        }
    }
 
    void ShootLaserFromTargetPosition()
    {
        transform.rotation = Quaternion.Lerp(transform.rotation, parentPosition.rotation, lerpSpeed);
        transform.position = Vector3.Lerp(transform.position, parentPosition.position, lerpSpeed);

        Ray ray = new Ray(transform.position, transform.forward);
        RaycastHit raycastHit;

        if(Physics.Raycast( ray, out raycastHit, laserMaxLength)) 
        {
            _endPosition = raycastHit.point;
            if (raycastHit.transform.CompareTag("Ground") || raycastHit.transform.CompareTag("Lava") || raycastHit.transform.CompareTag("Enemy") )
            {
                laserLineRenderer.material.color = rightColor;
                _isLevel = true;
            }
            else
            {
                laserLineRenderer.material.color = wrongColor;
                _isLevel = false;
            }
        }
        else
        {
            laserLineRenderer.material.color = wrongColor;
            _isLevel = false;
            _endPosition  = transform.position + (laserMaxLength * transform.forward);
        }
 
        laserLineRenderer.SetPosition( 0, transform.position );
        laserLineRenderer.SetPosition( 1, _endPosition );
    }
}
