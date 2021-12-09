namespace TagsCloudContainer.MathFunctions
{
    public interface IMathFunctionResolver
    {
        IMathFunction GetFunction(MathFunctionType type);
    }
}