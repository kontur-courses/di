using System.Collections.Generic;

namespace CloodLayouter.Infrastructer
{
    public interface IConverter<TypeInput,TypeOutput>
    {
        TypeOutput Convert(TypeInput Data);
    }
}