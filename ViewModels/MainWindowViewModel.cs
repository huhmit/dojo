namespace Dojo.ViewModels
{
    using Markdig;
    using Markdig.Wpf;
    using System;
    using System.IO;
    using System.Reflection;
    using System.Text;
    using System.Windows.Documents;
    using System.Windows.Input;
    using System.Xaml;
    using XamlReader = System.Windows.Markup.XamlReader;

    public class MainWindowViewModel : ViewModelBase
    {
        private FlowDocument _markdownDocument;
        private string _plainText;

        private ICommand _boldCommand;

        public FlowDocument MarkdownDocument
        {
            get
            {
                return _markdownDocument;
            }
            set
            {
                _markdownDocument = value;
                NotifyPropertyChanged(nameof(MarkdownDocument));
            }
        }

        public string PlainText
        {
            get
            {
                return _plainText;
            }
            set
            {
                _plainText = value;
                OnLoaded();
                NotifyPropertyChanged(nameof(PlainText));
            }
        }

        public MainWindowViewModel()
        {
            MarkdownDocument = new FlowDocument();
            PlainText = File.ReadAllText(@"C:\Users\charlton.cameron\Desktop\todo.md");
        }

        private static MarkdownPipeline BuildPipeline()
        {
            return new MarkdownPipelineBuilder()
                .UseSupportedExtensions()
                .Build();
        }

        private void OnLoaded()
        {
            var markdown = PlainText;
            var xaml = Markdig.Wpf.Markdown.ToXaml(markdown, BuildPipeline());
            using var stream = new MemoryStream(Encoding.UTF8.GetBytes(xaml));
            using var reader = new XamlXmlReader(stream, new MyXamlSchemaContext());
            if (XamlReader.Load(reader) is FlowDocument document)
            {
                MarkdownDocument = document;
            }
        }

        internal class MyXamlSchemaContext : XamlSchemaContext
        {
            public override bool TryGetCompatibleXamlNamespace(string xamlNamespace, out string compatibleNamespace)
            {
                if (xamlNamespace.Equals("clr-namespace:Markdig.Wpf", StringComparison.Ordinal))
                {
                    compatibleNamespace = $"clr-namespace:Markdig.Wpf;assembly={Assembly.GetAssembly(typeof(Styles)).FullName}";
                    return true;
                }
                return base.TryGetCompatibleXamlNamespace(xamlNamespace, out compatibleNamespace);
            }
        }
    }
}
