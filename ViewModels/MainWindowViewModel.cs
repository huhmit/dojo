using Dojo.Commands;
using Markdig;
using Markdig.Wpf;
using System;
using System.Linq;
using System.IO;
using System.Reflection;
using System.Text;
using System.Windows.Documents;
using System.Windows.Input;
using System.Xaml;
using XamlReader = System.Windows.Markup.XamlReader;

namespace Dojo.ViewModels
{
    public class MainWindowViewModel : ViewModelBase
    {
        #region Private Fields
        private FlowDocument _markdownDocument;
        private string _plainText, _selectedText;
        private int _caretIndex;
        #endregion

        #region Public Fields
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
        public string SelectedText
        {
            get
            {
                return _selectedText;
            }
            set
            {
                _selectedText = value;
                NotifyPropertyChanged(nameof(SelectedText));
            }
        }
        public int CaretIndex
        {
            get
            {
                return _caretIndex;
            }
            set
            {
                _caretIndex = value;
                NotifyPropertyChanged(nameof(CaretIndex));
            }
        }
        #endregion

        #region Commands
        private ICommand _boldCommand, _italicCommand, _strikethroughCommand, _headingCommand, 
            _blockQuoteCommand, _codeCommand, _linkCommand, _tableCommand, _imageCommand, 
            _unorderedListCommand, _orderedListCommand, _checkListCommand;

        public ICommand BoldCommand
        {
            get
            {
                if (_boldCommand == null)
                {
                    _boldCommand = new RelayCommand(param => Bold(), null);
                }
                return _boldCommand;
            }
        }
        public ICommand ItalicCommand
        {
            get
            {
                if (_italicCommand == null)
                {
                    _italicCommand = new RelayCommand(param => Italic(), null);
                }
                return _italicCommand;
            }
        }
        public ICommand StrikethroughCommand
        {
            get
            {
                if (_strikethroughCommand == null)
                {
                    _strikethroughCommand = new RelayCommand(param => Strikethrough(), null);
                }
                return _strikethroughCommand;
            }
        }
        public ICommand HeadingCommand
        {
            get
            {
                if (_headingCommand == null)
                {
                    _headingCommand = new RelayCommand(param => Heading(), null);
                }
                return _headingCommand;
            }
        }
        public ICommand BlockQuoteCommand
        {
            get
            {
                if (_blockQuoteCommand == null)
                {
                    _blockQuoteCommand = new RelayCommand(param => BlockQuote(), null);
                }
                return _blockQuoteCommand;
            }
        }
        public ICommand CodeCommand
        {
            get
            {
                if (_codeCommand == null)
                {
                    _codeCommand = new RelayCommand(param => Code(), null);
                }
                return _codeCommand;
            }
        }
        public ICommand LinkCommand
        {
            get
            {
                if (_linkCommand == null)
                {
                    _linkCommand = new RelayCommand(param => Link(), null);
                }
                return _linkCommand;
            }
        }
        public ICommand TableCommand
        {
            get
            {
                if (_tableCommand == null)
                {
                    _tableCommand = new RelayCommand(param => Table(), null);
                }
                return _tableCommand;
            }
        }
        public ICommand ImageCommand
        {
            get
            {
                if (_imageCommand == null)
                {
                    _imageCommand = new RelayCommand(param => Image(), null);
                }
                return _imageCommand;
            }
        }
        public ICommand UnorderedListCommand
        {
            get
            {
                if (_unorderedListCommand == null)
                {
                    _unorderedListCommand = new RelayCommand(param => UnorderedList(), null);
                }
                return _unorderedListCommand;
            }
        }
        public ICommand OrderedListCommand
        {
            get
            {
                if (_orderedListCommand == null)
                {
                    _orderedListCommand = new RelayCommand(param => OrderedList(), null);
                }
                return _orderedListCommand;
            }
        }
        public ICommand CheckListCommand
        {
            get
            {
                if (_checkListCommand == null)
                {
                    _checkListCommand = new RelayCommand(param => CheckList(), null);
                }
                return _checkListCommand;
            }
        }
        #endregion

        #region C'tor
        public MainWindowViewModel()
        {
            MarkdownDocument = new FlowDocument();
            PlainText = File.ReadAllText(@"C:\Users\charlton.cameron\Desktop\todo.md");
            SelectedText = "";
        }
        #endregion

        #region Private Methods
        private void Bold()
        {
            if (string.IsNullOrEmpty(SelectedText))
            {
                SelectedText = "**strong text**";
                return;
            }
            SelectedText = $"**{SelectedText}**";
        }
        private void Italic()
        {
            if (string.IsNullOrEmpty(SelectedText))
            {
                SelectedText = "*emphasized  text*";
                return;
            }
            SelectedText = $"*{SelectedText}*";
        }
        private void Strikethrough()
        {
            if (string.IsNullOrEmpty(SelectedText))
            {
                SelectedText = "~~strikethrough text~~";
                return;
            }
            SelectedText = $"~~{SelectedText}~~";
        }
        private void Heading() 
        {
            if (string.IsNullOrEmpty(SelectedText))
            {
                SelectedText = "\n# Heading";
                return;
            }

            int headingStart, headingEnd;
            headingStart = 0;
            headingEnd = 0;

            for (int i = CaretIndex -1; i > 0; i--)
            {
                if (PlainText[i] != '#' || PlainText[i] != ' ')
                {
                    break;
                }
                headingStart = i;
            }

            headingEnd = CaretIndex + SelectedText.Length;

            if (PlainText[headingStart] != '#')
            {
                SelectedText = $"## {SelectedText}";
            }
            else
            {
                string[] splitHeading = PlainText[headingStart..headingEnd].Split(' ');
                string hashes = splitHeading[0];

                PlainText = PlainText.Remove(headingStart, 1).Trim();
            }
        }
        private void BlockQuote()
        {
            if (string.IsNullOrEmpty(SelectedText))
            {
                SelectedText = "> Blockquote";
                return;
            }
            SelectedText = $"> {SelectedText}";
        }
        private void Code()
        {
            if (string.IsNullOrEmpty(SelectedText))
            {
                SelectedText = " `Blockquote` ";
                return;
            }
            SelectedText = $"\n\n`{SelectedText}`\n";
        }
        private void Link()
        {
            if (string.IsNullOrEmpty(SelectedText))
            {
                SelectedText = "[enter link description here](link)";
                return;
            }
            SelectedText = $"[{SelectedText}](link)";
        }
        private void Table()
        {
            if (string.IsNullOrEmpty(SelectedText))
            {
                SelectedText = "|  |  |\n|--|--|\n|  |  |";
                return;
            }
            SelectedText = $"\n|{SelectedText}|  |\n|{new string('n', SelectedText.Length)}|--|\n|{new string(' ', SelectedText.Length)}|  |\n";
        }
        private void Image()
        {
            if (string.IsNullOrEmpty(SelectedText))
            {
                SelectedText = "![enter image description here](link)";
                return;
            }
            SelectedText = $"![{SelectedText}](link)";
        }
        private void UnorderedList()
        {
            if (string.IsNullOrEmpty(SelectedText))
            {
                SelectedText = "- List Item";
                return;
            }
            SelectedText = $"\n- {SelectedText}\n";
            // pressing enter on a list item should 
        }
        private void OrderedList()
        {
            if (string.IsNullOrEmpty(SelectedText))
            {
                SelectedText = "1. List Item";
                return;
            }
            SelectedText = $"\n1. {SelectedText}\n";
            // pressing enter on a list item should 
        }
        private void CheckList()
        {
            if (string.IsNullOrEmpty(SelectedText))
            {
                SelectedText = "- [ ] List Item";
                return;
            }
            SelectedText = $"\n- [ ] {SelectedText}\n";
            // pressing enter on a list item should 
        }
        #endregion

        #region Helpers
        private string GetLine(int index)
        {
            int start, end;
            start = 0;
            end = 0;

            for (int i = index; i > 0; i--)
            {
                if (PlainText[i] == '\n')
                {
                    break;
                }
                start = i;
            }

            for (int i = index; i < PlainText.Length; i++)
            {
                if (PlainText[i] == '\n')
                {
                    break;
                }
                end = i;
            }

            return PlainText[start..end];
        }
        #endregion

        #region Markdown Methods
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
        #endregion
    }
}
