using System;
using System.Collections.Generic;
using System.Linq;

namespace ConsoleTagClouder
{
    public class TypesPrefixParser
    {
        private IEnumerable<Type> FindInterfacesWithPrefix<TInterface>(string prefix)=>
            AppDomain.CurrentDomain.GetAssemblies().SelectMany(s => s.GetTypes())
                .Where(t => typeof(TInterface).IsAssignableFrom(t))
                .Where(n=>n.Name.ToLower().StartsWith(prefix.ToLower()));
    }
}