using System.Collections.Generic;
using Utils.Classes;

namespace Overworld.Interfaces
{
    public interface Readable
    {
        public abstract List<TextUnit> Read();
    }
}