using UnityEngine;

namespace Test3
{
    public class RecalculationUnitsState : IFieldState
    {
        public void Apply(StateRouter<IFieldState> router, FieldContext context)
        {
            context.ResetUnits();
            context.ResetColumnLevels();
            context.SetActiveNotifiers(false);
            
            var units = context.Units;
            var columnLevels = context.ColumnLevels;
            var notifiers = context.Notifiers;
            
            if (context.UnitsCount == 0)
            {
                context.SetActiveNotifiersRow(true, 0);
                router.GoTo<WaitUnitState>();
                return;
            }

            int detectedUnitsCount = 0;
            context.OnTriggerUnit += (unit, coords) =>
            {
                detectedUnitsCount += 1;
                units[coords.RowIdx, coords.ColumnIdx] = unit;
                
                int currentLevel = columnLevels[coords.ColumnIdx];
                int nextLevel = Mathf.Clamp(coords.RowIdx + 1, currentLevel, units.GetLength(0) - 1);
                columnLevels[coords.ColumnIdx] = nextLevel;

                if (detectedUnitsCount < context.UnitsCount)
                    return;
                
                context.ClearTriggerUnitSubscriptions();
                
                for (int j = 0; j < columnLevels.Length; j++)
                {
                    int level = columnLevels[j];
                    var topNotifier = notifiers[level, j];
                    var topLevelUnit = units[level, j];
                    
                    topNotifier.gameObject.SetActive(topLevelUnit == null);
                }
                
                if (context.UnitsCount < 3)
                {
                    router.GoTo<WaitUnitState>();
                    return;
                }
                
                router.GoTo<CheckingMatchesState>();
            };
        }
    }
}