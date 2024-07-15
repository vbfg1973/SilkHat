using Avalonia.Controls;
using AvaloniaEdit;
using AvaloniaEdit.TextMate;
using TextMateSharp.Grammars;

namespace SilkHat.Views
{
    public partial class SolutionView : UserControl
    {
        private TextEditor _textEditor;
        private RegistryOptions _registryOptions;
        private readonly TextMate.Installation _textMateInstallation;
        private int _currentTheme = (int)ThemeName.DarkPlus;

        public SolutionView()
        {
            InitializeComponent();
            
            _textEditor = this.FindControl<TextEditor>("Editor")!;
            _registryOptions = new RegistryOptions(
                (ThemeName)_currentTheme);

            _textMateInstallation = _textEditor.InstallTextMate(_registryOptions);
            Language csharpLanguage = _registryOptions.GetLanguageByExtension(".cs");
            _textMateInstallation.SetGrammar(_registryOptions.GetScopeByLanguageId(csharpLanguage.Id));
        }
    }
}