using BlankApp2.ViewModels;
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

namespace BlankApp2.Views
{
    /// <summary>
    /// UserControl1.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class TCPChat : UserControl
    {
        public TCPChat()
        {
            InitializeComponent();

            var viewModel = (TCPChatViewModel)DataContext;
            viewModel.Message.CollectionChanged += Message_CollectionChanged;
        }
        private void Message_CollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            if(e.NewItems != null)
            {
                foreach(string message in e.NewItems)
                {
                    var paragraph = new Paragraph(new Run(message));
                    ChatRichTextBox.Document.Blocks.Add(paragraph);


                }
            }
        }
    }

}
