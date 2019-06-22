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
using System.Windows.Shapes;

namespace Sample01
{
    /// <summary>
    /// TagView.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class TagView : ReactiveWindow<TagViewModel>
    {
        public TagView()
        {
            InitializeComponent();
            ViewModel = new TagViewModel();

            this.WhenActivated(d =>
            {
                //DataContext = ViewModel;
                d(this.OneWayBind(ViewModel, viewModel => ViewModel.Devices, view => view.TagDITree.ItemsSource));
                d(this.OneWayBind(ViewModel, viewModel => viewModel.Devices, view => view.TagDOTree.ItemsSource));
            });
        }
    }
}
