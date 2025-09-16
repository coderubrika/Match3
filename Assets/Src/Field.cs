using UnityEngine;
using UnityEngine.Events;

namespace Test3
{
    public class Field : MonoBehaviour
    {
        [SerializeField] private TriggerNotifier[] firstColumn;
        [SerializeField] private TriggerNotifier[] secondColumn;
        [SerializeField] private TriggerNotifier[] thirdColumn;

        private int[] columnLevels;
        
        private TriggerNotifier[,] fieldNotifiers;
        private CircleColor[,] fieldFill;
        private Timer timer;
        private UpdateSource updateSource;
        
        public UnityEvent OnNext { get; } = new();
        public UnityEvent OnFinish { get; } = new();
        
        private void Awake()
        {
            updateSource = ServiceLocator.Instance.Get<UpdateSource>();
        }

        public void Init()
        {
            columnLevels = new int[3];
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
            
            timer?.Dispose();
            
            if (rowIdx < 2)
            {
                columnLevels[columnIdx] = rowIdx + 1;
                timer = new Timer(updateSource, () => HandleNext(rowIdx + 1, columnIdx), 1f);
            }

            else
            {
                var (first, second) = GetOtherIndices(columnIdx);
                if (columnLevels[first] < 2 || columnLevels[second] < 2)
                {
                    timer = new Timer(updateSource, OnNext.Invoke, 1f);
                }
                else
                {
                    timer = new Timer(updateSource, OnFinish.Invoke, 1f);
                }
            }

            CircleObject circleObject = circleCollider.GetComponent<CircleObject>();
            fieldFill[rowIdx, columnIdx] = circleObject.ColorType;
            
            Debug.Log($"{fieldFill[rowIdx, columnIdx]} in [{rowIdx}][{columnIdx}]");
        }

        private void HandleNext(int rowIdx, int columnIdx)
        {
            fieldNotifiers[rowIdx, columnIdx].gameObject.SetActive(true);
            OnNext.Invoke();
        }
        
        private (int, int) GetOtherIndices(int index)
        {
            return index switch
            {
                0 => (1, 2),
                1 => (0, 2), 
                2 => (0, 1),
                _ => (-1, -1)
            };
        }

    }
}