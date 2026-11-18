using UnityEngine;
[RequireComponent(typeof(LineRenderer))]

public class BeamWeapon : Weapon
{
    [SerializeField] LineRenderer lineRenderer;
    [SerializeField] Transform muzzle;
    [SerializeField] Color startColor = Color.red;
    [SerializeField] Color endColor = Color.purple;
    [SerializeField] float lineThickness = 25f;

    protected override void Awake()
    {
        base.Awake();
        if (!lineRenderer) lineRenderer = GetComponentInParent<LineRenderer>();
    }

    protected override void Fire()
    {
        targeter?.CurrentTarget.TakeDamage(damage);
    }
    protected override void Update()
    {
        base.Update();


        if(lineRenderer)
        {
            if (targeter && targeter.CurrentTarget != null)
            {
                lineRenderer.positionCount = 2;
                Vector3 lineStart = muzzle.position;       //sono due variabili locali
                Vector3 lineEnd = targeter.TargetPoint;    //altezza + altezza occhi


                lineRenderer.SetPosition(0, lineStart);
                lineRenderer.SetPosition(1, lineEnd);

                lineRenderer.startWidth = lineThickness;
                lineRenderer.endWidth = lineThickness;
            }
            else
            {
                lineRenderer.positionCount = 0;
                lineRenderer.startColor = new Color(0, 0, 0, 0);
                lineRenderer.endColor = new Color(0, 0, 0, 0);
            }
        }
        
    }
}
