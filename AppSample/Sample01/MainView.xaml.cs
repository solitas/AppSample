using Microsoft.Win32;
using ReactiveUI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Sample01
{
    /// <summary>
    /// MainWindow.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class MainView : ReactiveWindow<MainViewModel>
    {
        public MainView()
        {
            InitializeComponent();
            ITagService service = new TagService();
            ViewModel = new MainViewModel(service);

            this.WhenActivated(d =>
            {
                d(this.OneWayBind(ViewModel, viewModel => viewModel.ViewData, view => view.TagDataGrid.ItemsSource));
                d(this.BindCommand(ViewModel, viewModel => viewModel.Open, view => view.OpenFile));
                d(this.ViewModel.OpenFileInteraction.RegisterHandler(context =>
                {
                    string fileName = string.Empty;
                    var dialog = new OpenFileDialog
                    {
                        Filter = "xlsx files (*.xlsx)|*.xlsx"
                    };

                    var result = dialog.ShowDialog(this);
                    if (result != null && result.Value)
                    {
                        fileName = dialog.FileName;
                    }
                    context.SetOutput(fileName);
                }));
            });

        }
    }
}
