using UnityEngine;

namespace Test3
{
    public class CircleObject : MonoBehaviour
    {
        [SerializeField] private Rigidbody2D rb2D;
        [SerializeField] private SpriteRenderer spriteRenderer;
        [SerializeField] private Color red;
        [SerializeField] private Color blue;
        [SerializeField] private Color green;

        public Rigidbody2D Rb2D => rb2D;
        
        public CircleColor ColorType { get; private set; } = CircleColor.None;

        public void SetColor(CircleColor circleColor)
        {
            ColorType = circleColor;
            spriteRenderer.color = circleColor switch
            {
                CircleColor.Red => red,
                CircleColor.Blue => blue,
                CircleColor.Green => green,
                _ => new Color(0,0,0,0)
            };
        }
    }
    
    public enum CircleColor
    {
        None, Red, Blue, Green
    }
}