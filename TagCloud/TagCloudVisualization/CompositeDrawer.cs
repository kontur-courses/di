using System.Collections.Generic;
using System.Linq;

namespace TagCloudVisualization
{
    /// <summary>
    /// Composes one or more drawers;
    /// </summary>
    public class CompositeDrawer
    {
        private readonly List<IWordDrawer> drawers;

        /// <summary>
        /// Aggregates drawers so that the earliest drawer is being used to draw word;
        /// </summary>
        public CompositeDrawer( IEnumerable<IWordDrawer> drawers)
        {
            this.drawers = drawers.ToList();
        }

        /// <summary>
        ///     Sets the drawer with highest priority that checks given wordInfo to draw next word.
        ///     Priority sets as order of drawers in constructor: earlier position means higher priority.
        /// </summary>
        public bool TryGetDrawer(WordInfo wordInfo, out IWordDrawer drawer)
        {
            drawer = drawers.FirstOrDefault(d => d.Check(wordInfo));
            return drawer != null;
        }
    }
}
