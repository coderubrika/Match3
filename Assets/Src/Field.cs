using System;
using System.Linq;
using UnityEngine;

namespace Test3
{
    public class Field : MonoBehaviour
    {
        [SerializeField] private TriggerNotifier[] firstColumn;
        [SerializeField] private TriggerNotifier[] secondColumn;
        [SerializeField] private TriggerNotifier[] thirdColumn;

        private TriggerNotifier[,] fieldNotifiers;
        private CircleColor[,] fieldFill;
        private Timer timer;
        private UpdateSource updateSource;

        private void Awake()
        {
            updateSource = ServiceLocator.Instance.Get<UpdateSource>();
        }

        public void Init()
        {
            fieldFill = new CircleColor[3, 3];
            fieldNotifiers = new TriggerNotifier[3, 3];
            
            ResetColumn(firstColumn, 0);
            ResetColumn(secondColumn, 1);
            ResetColumn(thirdColumn, 2);
        }

        private void ResetColumn(TriggerNotifier[] column, int columnIdx)
        {
            for (int i = 0; i < 3; i++)
            {
                TriggerNotifier notifier = column[i];
                notifier.ClearAllSubscriptions();
                notifier.gameObject.SetActive(i == 0);

                int rowIdx = i;
                notifier.OnTriggerEnter += col => OnFoundCircle(col, rowIdx, columnIdx);
                fieldNotifiers[i, columnIdx] = notifier;
            }
        }

        private void OnFoundCircle(Collider2D circleCollider, int rowIdx, int columnIdx)
        {
            fieldNotifiers[rowIdx, columnIdx].gameObject.SetActive(false);
            
            if (rowIdx < 2)
            {
                timer?.Dispose();
                timer = new Timer(
                    updateSource, 
                    () => fieldNotifiers[rowIdx + 1, columnIdx].gameObject.SetActive(true), 
                    1f);

            }

            CircleObject circleObject = circleCollider.GetComponent<CircleObject>();
            fieldFill[rowIdx, columnIdx] = circleObject.ColorType;
            
            Debug.Log($"{fieldFill[rowIdx, columnIdx]} in [{rowIdx}][{columnIdx}]");
        }
    }

    public enum CircleColor
    {
        None, Red, Blue, Green
    }
}