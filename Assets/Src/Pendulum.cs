using System;
using UnityEngine;

namespace Test3
{
    public class Pendulum : MonoBehaviour
    {
        [SerializeField] private SpringJoint2D springJoint2D;
        [SerializeField] private CircleObject circleObject;
        [SerializeField] private LineRenderer lineRenderer;

        public void Init(float distance, Vector2 force)
        {
            springJoint2D.distance = distance;
            
            circleObject.transform.localPosition = -Vector3.up * distance;
            circleObject.Rb2D.velocity = Vector2.zero;
            circleObject.Rb2D.angularVelocity = 0;
            
            circleObject.Rb2D.AddForce(force);
            circleObject.SetColor(CircleColor.None);
        }

        public void SetColor(CircleColor circleColor)
        {
            circleObject.SetColor(circleColor);
        }

        private void Update()
        {
            lineRenderer.SetPosition(0, springJoint2D.transform.position);
            lineRenderer.SetPosition(1, circleObject.transform.position);
        }

        public void SetupOtherCircle(CircleObject otherCircleObject)
        {
            otherCircleObject.SetColor(circleObject.ColorType);
            otherCircleObject.transform.position = circleObject.transform.position;
            otherCircleObject.Rb2D.velocity = circleObject.Rb2D.velocity;
            otherCircleObject.Rb2D.angularVelocity = circleObject.Rb2D.angularVelocity;
            
            circleObject.SetColor(CircleColor.None);
        }
    }
}