using System;
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

        private readonly ITagService _tagService;

        public MainViewModel(ITagService tagService)
        {
            _tagService = tagService;

            ViewData = new ObservableCollectionExtended<IncomingTag>();
            _tagService.Tags
                .Connect()
                .ObserveOn(RxApp.MainThreadScheduler)
                .Bind(ViewData)
                .Subscribe();

            Open = ReactiveCommand.CreateFromTask(async () =>
            {
                var fileName = await OpenFileInteraction.Handle(Unit.Default);
                return Task.Run(() =>
                {
                    _tagService.LoadTagFromFile(fileName);
                });

            }, _tagService.IsValid);

        }

        [Reactive]
        public ObservableCollectionExtended<IncomingTag> ViewData
        {
            get; set;
        }

        public ICommand Open
        {
            get;
        }
    }
}
