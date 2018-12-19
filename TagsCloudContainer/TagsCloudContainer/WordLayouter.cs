using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;

namespace TagsCloudContainer
{
    internal class WordLayouter
    {
        private readonly Func<Word, Size> _getWordSize;
        private readonly IRectangleLayout _layout;
        private readonly List<Word> _wordsToDraw;
        private List<Rectangle> _coordinates;

        public WordLayouter(IWordStorage wordStorage, Func<Word, Size> getWordSize, IRectangleLayout layout)
        {
            _wordsToDraw = wordStorage.ToIOrderedEnumerable().ToList();
            _getWordSize = getWordSize;
            _layout = layout;
        }

        public IEnumerable<ItemToDraw<Word>> GetItemsToDraws()
        {
            PlaceWords();

            return _wordsToDraw
                .Select((word, i) => new ItemToDraw<Word>(
                    word, _coordinates[i].X, 
                    _coordinates[i].Y, 
                    _coordinates[i].Width, 
                    _coordinates[i].Height))
                .ToList();
        }

        private void PlaceWords()
        {
            var wordSizes = _wordsToDraw
                .Select(w => _getWordSize(w))
                .ToList();

            foreach (var size in wordSizes)
                _layout.PutNextRectangle(size);

            _coordinates = _layout.GetCoordinatesToDraw().ToList();
        }
    }
}
