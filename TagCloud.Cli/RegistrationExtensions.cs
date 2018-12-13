using Castle.MicroKernel.Registration;

namespace TagCloud
{
    public static class RegistrationExtensions
    {
        public static ComponentRegistration<TFor> WithArgument<TFor>(
            this ComponentRegistration<TFor> component,
            string name,
            object value)
            where TFor : class
        {
            return component.DependsOn(Dependency.OnValue(name, value));
        }
    }
}