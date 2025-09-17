using UnityEngine;

namespace Test3
{
    public class CheckingMatchesState : IFieldState
    {
        private readonly UpdateSource updateSource;
        private readonly (int, int, int, int, int, int)[] lines =
        {
            // Horizontal
            (0,0, 0,1, 0,2),
            (1,0, 1,1, 1,2),
            (2,0, 2,1, 2,2),
        
            // Vertical
            (0,0, 1,0, 2,0),
            (0,1, 1,1, 2,1),
            (0,2, 1,2, 2,2),
        
            // Diagonal
            (0,0, 1,1, 2,2),
            (0,2, 1,1, 2,0)
        };

        public CheckingMatchesState()
        {
            updateSource = ServiceLocator.Instance.Get<UpdateSource>();
        }
        
        public void Apply(StateRouter<IFieldState> router, FieldContext context)
        {
            CircleObject[,] units = context.Units;
            
            foreach (var line in lines)
            {
                var a = units[line.Item1, line.Item2];
                var b = units[line.Item3, line.Item4];
                var c = units[line.Item5, line.Item6];

                if (a.ColorType != CircleColor.None && a.ColorType == b.ColorType && b.ColorType == c.ColorType)
                {
                    context.ExcludeUnits((a, b, c));
                    context.SetWaitDropAllUnits(new Timer(updateSource, router.GoTo<RecalculationUnitsState>, 1f));
                    return;
                }
            }

            if (context.UnitsCount < 9)
            {
                router.GoTo<WaitUnitState>();
                return;
            }
            
            router.GoTo<FinishFieldState>();
        }
    }
}