using System;
using System.Collections.Generic;
using System.Reactive.Linq;
using DynamicData;
using ReactiveUI;
using Sample01.Model;

namespace Sample01
{
    public class TagService : ReactiveObject, ITagService
    {
        private static object _lock = new object();
        public SourceList<IncomingTag> Tags { get; private set; }
        private bool _fileLoaded;
        public bool FileLoaded
        {
            get => _fileLoaded;
            set => this.RaiseAndSetIfChanged(ref _fileLoaded, value);
        }
        public IObservable<bool> IsValid { get; private set; }

        public TagService()
        {
            Tags = new SourceList<IncomingTag>();

            IsValid = this.WhenAnyValue(x => x.FileLoaded)
                .ObserveOn(RxApp.MainThreadScheduler)
                .Select(loaded => !loaded);
        }

        public void LoadTagFromFile(string fileName)
        {
            var tags = ExcelHelper.GetTagsInFile(fileName);
            if (tags != null)
            {
                lock (_lock)
                {
                    Tags.AddRange(tags);
                }

                FileLoaded = true;
            }
        }

        public void SaveTagToFile(string fileName)
        {

        }
    }
}
