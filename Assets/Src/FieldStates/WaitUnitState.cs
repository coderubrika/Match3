using UnityEngine;

namespace Test3
{
    public class WaitUnitState : IFieldState
    {
        private readonly UpdateSource updateSource;
        private readonly float dropFromTriggerTime;
        
        public WaitUnitState()
        {
            updateSource = ServiceLocator.Instance.Get<UpdateSource>();
            GameConfig gameConfig = ServiceLocator.Instance.Get<GameConfig>();
            dropFromTriggerTime = gameConfig.DropFromTriggerTime;
        }
        
        public void Apply(StateRouter<IFieldState> router, FieldContext context)
        {
            context.OnTriggerUnit += (unit, coords) =>
            {
                context.ClearTriggerUnitSubscriptions();
                
                var units = context.Units;
                context.AddUnit(unit);
                units[coords.RowIdx, coords.ColumnIdx] = unit;
                
                var notifier = context.Notifiers[coords.RowIdx, coords.ColumnIdx];
                int currentLevel = context.ColumnLevels[coords.ColumnIdx];
                int nextLevel = Mathf.Clamp(coords.RowIdx + 1, currentLevel, units.GetLength(0) - 1);
                context.ColumnLevels[coords.ColumnIdx] = nextLevel;
                
                notifier.gameObject.SetActive(false);
                var nextNotifier = context.Notifiers[nextLevel, coords.ColumnIdx];
                nextNotifier.gameObject.SetActive(nextNotifier != notifier);
                
                context.SetWaitDropUnit(new Timer(updateSource, router.GoTo<CheckingMatchesState>, dropFromTriggerTime));
            };
            
            context.Next();
        }
    }
}