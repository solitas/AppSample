using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reactive;
using System.Reactive.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using DynamicData;
using DynamicData.Binding;
using ReactiveUI;
using ReactiveUI.Fody.Helpers;
using Sample01.Model;

namespace Sample01
{
    public class MainViewModel : ReactiveObject
    {
        // interactions
        public Interaction<Unit, string> OpenFileInteraction { get; } = new Interaction<Unit, string>();

        private IObservable<bool> _isAvailable;
        private readonly SourceList<IncomingTag> _tagSources;
        public MainViewModel()
        {
            _tagSources = new SourceList<IncomingTag>();
            _isAvailable = this.WhenAnyValue(x => x.OpenFileName)
                               .Select(x => string.IsNullOrEmpty(x));
            Tags = new ObservableCollectionExtended<IncomingTag>();

            _tagSources.Connect()
                       .ObserveOn(RxApp.MainThreadScheduler)
                       .Bind(Tags)
                       .Subscribe();
            
            Open = ReactiveCommand.Create(() =>
            {
                OpenFileInteraction
                .Handle(Unit.Default)
                .Subscribe(fileName =>
                {
                    if (string.IsNullOrEmpty(fileName)) return;
                    // Tags Clear
                    Tags.Clear();

                    var tags = ExcelHelper.GetTagsInFile(fileName);

                    
                    if (tags != null)
                    {
                        OpenFileName = fileName;

                        foreach (var tag in tags)
                        {
                            Console.WriteLine(tag);
                        }

                        _tagSources.AddRange(tags);
                    }
                });

            }, _isAvailable);


        }

        public IObservableCollection <IncomingTag> Tags
        {
            get;
        }

        [Reactive]
        public string OpenFileName
        {
            get; set;
        }

        public ICommand Open
        {
            get;
        }
    }
}
