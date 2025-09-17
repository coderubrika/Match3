using UnityEngine;

namespace Test3
{
    public class InitFieldState : IFieldState
    {
        public void Apply(StateRouter<IFieldState> router, FieldContext context)
        {
            context.ClearTriggerUnitSubscriptions();
            context.ClearUnits();
            
            context.ResetUnits();
            context.ResetColumnLevels();
            
            context.ClearNotifiers();
            context.SubscribeOnNotifiers();
            
            context.SetActiveNotifiers(false);
            context.SetActiveNotifiersRow(true, 0);
            
            router.GoTo<WaitUnitState>();
        }
    }
}