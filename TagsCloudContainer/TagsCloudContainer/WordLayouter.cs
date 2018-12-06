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
        private IWordStorage _wordStorage;
        private List<Word> _resultWords;
        private List<Rectangle> _coordinates;

        public WordLayouter(Func<Word, Size> getWordSize, IRectangleLayout layout)
        {
            _getWordSize = getWordSize;
            _layout = layout;
        }

        public IEnumerable<ItemToDraw<Word>> GetItemsToDraws(IWordStorage wordStorage)
        {
            PlaceWords(wordStorage);

            return _resultWords
                .Select((word, i) => new ItemToDraw<Word>(
                    word, _coordinates[i].X, 
                    _coordinates[i].Y, 
                    _coordinates[i].Width, 
                    _coordinates[i].Height))
                .ToList();
        }

        private void PlaceWords(IWordStorage wordStorage)
        {
            _wordStorage = wordStorage;

            var wordSizes = _wordStorage.ToList()
                .Select(w => _getWordSize(w))
                .ToList();

            foreach (var size in wordSizes)
                _layout.PutNextRectangle(size);

            _resultWords = _wordStorage.ToList();
            _coordinates = _layout.GetCoordinatesToDraw().ToList();
        }
    }
}
