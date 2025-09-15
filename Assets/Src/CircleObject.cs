using UnityEngine;

namespace Test3
{
    public class CircleObject : MonoBehaviour
    {
        [SerializeField] private SpriteRenderer spriteRenderer;
        [SerializeField] private Color red;
        [SerializeField] private Color blue;
        [SerializeField] private Color green;
        
        public CircleColor ColorType { get; } = CircleColor.None;

        public void SetColor(CircleColor circleColor)
        {
            spriteRenderer.color = circleColor switch
            {
                CircleColor.Red => red,
                CircleColor.Blue => blue,
                CircleColor.Green => green,
                _ => Color.white
            };
        }
    }
}