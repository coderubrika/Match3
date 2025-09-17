namespace Test3
{
    public class FinishFieldState : IFieldState
    {
        public void Apply(StateRouter<IFieldState> router, FieldContext context)
        {
            context.Finish();
        }
    }
}