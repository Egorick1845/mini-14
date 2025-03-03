using Content.Client.Info;
using Content.Client.UserInterface.Systems.EscapeMenu;
using Content.Client.UserInterface.Systems.Guidebook;
using Robust.Client.AutoGenerated;
using Robust.Client.Console;
using Robust.Client.UserInterface;
using Robust.Client.UserInterface.XAML;

namespace Content.Client.Lobby.UI
{
    [GenerateTypedNameReferences]
    public sealed partial class LobbyGui : UIScreen
    {
        [Dependency] private readonly IClientConsoleHost _consoleHost = default!;
        [Dependency] private readonly IUriOpener _uriOpener = default!;

        public LobbyGui()
        {
            RobustXamlLoader.Load(this);
            IoCManager.InjectDependencies(this);
            SetAnchorPreset(MainContainer, LayoutPreset.Wide);
            SetAnchorPreset(Background, LayoutPreset.Wide);
            SetAnchorPreset(ParallaxControl, LayoutPreset.Wide);

            LeaveButton.OnPressed += _ => _consoleHost.ExecuteCommand("disconnect");
            OptionsButton.OnPressed += _ => UserInterfaceManager.GetUIController<OptionsUIController>().ToggleWindow();

            Wiki.OnPressed += _ => _uriOpener.OpenUri(new Uri(""));
            Boosty.OnPressed += _ => _uriOpener.OpenUri(new Uri(""));
            Discord.OnPressed += _ => _uriOpener.OpenUri(new Uri(""));
            Telegram.OnPressed += _ => _uriOpener.OpenUri(new Uri(""));
            Site.OnPressed += _ => _uriOpener.OpenUri(new Uri(""));

            Rules.OnPressed += _ => new RulesAndInfoWindow().Open();
            Guidebook.OnPressed += _ => UserInterfaceManager.GetUIController<GuidebookUIController>().ToggleGuidebook();
            Changelog.OnPressed += _ => UserInterfaceManager.GetUIController<ChangelogUIController>().ToggleWindow();
        }

        public void SwitchState(LobbyGuiState state)
        {
            switch (state)
            {
                case LobbyGuiState.Default:
                    CharacterSetupState.Visible = false;
                    SplitContainer.Visible = true;
                    break;
                case LobbyGuiState.CharacterSetup:
                    CharacterSetupState.Visible = true;
                    SplitContainer.Visible = false;
                    UserInterfaceManager.GetUIController<LobbyUIController>().ReloadCharacterSetup();
                    break;
            }
        }

        public enum LobbyGuiState : byte
        {
            /// <summary>
            ///  The default state, i.e., what's seen on launch.
            /// </summary>
            Default,
            /// <summary>
            ///  The character setup state.
            /// </summary>
            CharacterSetup
        }
    }
}
