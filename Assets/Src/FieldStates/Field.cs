using UnityEngine;
using UnityEngine.Events;

namespace Test3
{
    public class Field : MonoBehaviour
    {
        [SerializeField] private TriggerNotifier[] firstColumn;
        [SerializeField] private TriggerNotifier[] secondColumn;
        [SerializeField] private TriggerNotifier[] thirdColumn;
        
        private FieldContext context;
        private StateRouter<IFieldState> router;
        
        public UnityEvent OnNext { get; } = new();
        public UnityEvent OnFinish { get; } = new();
        public UnityEvent<(CircleObject, CircleObject, CircleObject)> OnExclude { get; } = new();
        
        private void Awake()
        {
            var rawColumns = new[] { firstColumn, secondColumn, thirdColumn };
            TriggerNotifier[,] notifiers = new TriggerNotifier[3, 3];

            for (int j = 0; j < rawColumns.Length; j++)
            {
                var column = rawColumns[j];
                for (int i = 0; i < column.Length; i++)
                {
                    var notifier = column[i];
                    notifiers[i, j] = notifier;
                    notifier.gameObject.SetActive(false);
                }
            }
            
            context = new FieldContext(notifiers);
            router = new StateRouter<IFieldState>(ChangeState, new StateFactory<IFieldState>());
            context.OnNext += OnNext.Invoke;
            context.OnFinish += OnFinish.Invoke;
            context.OnExcludeUnits += OnExclude.Invoke;
        }

        public void Init()
        {
            router.GoTo<InitFieldState>();
        }

        private void ChangeState(IFieldState state) => state.Apply(router, context);
    }
}