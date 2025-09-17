namespace Test3
{
    public interface IFieldState
    {
        public void Apply(StateRouter<IFieldState> router, FieldContext context);
    }
}