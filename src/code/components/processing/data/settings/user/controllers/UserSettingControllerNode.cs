    using Godot;
using GruInject.API.Nodes;
using SpaceMiner.src.code.components.commons.other.paths;
using SpaceMiner.src.code.components.processing.data.settings.couplers;
using System.IO;

namespace SpaceMiner.src.code.components.processing.data.settings.user.controllers
{
    public partial class UserSettingControllerNode : GruNode2D, IUserSettingController
    {
        public UserSettings Setting { get; set; }
        public SettingCoupler SettingCoupler { get; set; }
        [ExportGroup("Settings")]
        [ExportSubgroup("Graphics")]
        [Export] public Button ScreenModeButton {  get; set; }
        [Export] public Button AspectTypeButton { get; set; }
        [Export] public Button ResolutionButton { get; set; }
        [Export] public Button GraphicsQualityButton {  get; set; }
        [Export] public Button VsyncButton { get; set; }
        [ExportSubgroup("Audio")]
        [Export] public HSlider MasterVolumeSlider {  get; set; }
        [Export] public Label MasterVolumeLabel { get; set; }
        [Export] public HSlider MusicVolumeSlider { get; set; }
        [Export] public Label MusicVolumeLabel { get; set; }
        [Export] public HSlider SoundsEffectsVolumeSlider { get; set; }
        [Export] public Label SoundsEffectsVolumeLabel { get; set; }
        [ExportSubgroup("Misc")]
        [Export] public CheckBox ErrorLoggingCheck { get; set; }
        [ExportGroup("Controller")]
        [Export] public Button BackButton { get; set; }
        [ExportSubgroup("Confirm Menu")]
        [Export] public Control SaveSettingMenu { get; set; }
        [Export] public Button ApplyButton {  get; set; }
        [Export] public Button CancelButton { get; set; }
        [Export] public Button CloseButton { get; set; }
        public override void _Ready()
        {
            Setting = new(Path.Join(Godot.OS.GetUserDataDir(), ExternalPaths.USER_SETTING));
            SettingCoupler = new();
            SettingCoupler.Load(Setting);

            ScreenModeButton.Renamed += ScreenModeBtn_Renamed;
            AspectTypeButton.Renamed += AspectTypeButton_Renamed;
            ResolutionButton.Renamed += ResolutionButton_Renamed;
            GraphicsQualityButton.Renamed += GraphicsQualityButton_Renamed;
            VsyncButton.Renamed += VsyncButton_Renamed;
            MasterVolumeSlider.ValueChanged += MasterVolumeSlider_ValueChanged;
            MusicVolumeSlider.ValueChanged += MusicVolumeSlider_ValueChanged;
            SoundsEffectsVolumeSlider.ValueChanged += SoundsEffectsVolumeSlider_ValueChanged;
            ErrorLoggingCheck.Pressed += ErrorLoggingCheck_Pressed;

            ScreenModeButton.Text = Setting.GraphicsSettings.ScreenMode;
            AspectTypeButton.Text = Setting.GraphicsSettings.AspectType;
            ResolutionButton.Text = Setting.GraphicsSettings.Resolution;
            GraphicsQualityButton.Text = Setting.GraphicsSettings.GraphicsQuality;
            VsyncButton.Text = Setting.GraphicsSettings.VSync;
            MasterVolumeSlider.Value = Setting.AudioSettings.MasterVolume;
            MusicVolumeSlider.Value = Setting.AudioSettings.MusicVolume;
            SoundsEffectsVolumeSlider.Value = Setting.AudioSettings.SoundEffectsVolume;

            ErrorLoggingCheck.ButtonPressed = Setting.MiscSettings.ErrorLogging;
            
            // Back button actions:
            BackButton.Pressed += BackButton_Pressed;
            ApplyButton.Pressed += ApplyButton_Pressed;
            CancelButton.Pressed += CancelButton_Pressed;
            CloseButton.Pressed += CloseButton_Pressed;
        
            // TODO: Implement menu way.
            // ESC Opens Save setting menu, ESC again closes it with no changes.
        }

        private void CloseButton_Pressed()
        {
            SaveSettingMenu.Visible = false;
        }

        private void CancelButton_Pressed()
        {
            SettingCoupler.Load(Setting); // Cancels the setting
            // Quit setting menu
        }

        private void ApplyButton_Pressed()
        {
            SettingCoupler.Save(Setting);
            // Quit setting menu
        }

        private void BackButton_Pressed()
        {
            if (!SaveSettingMenu.Visible)
            {
                SaveSettingMenu.Visible = true;
            }
        }

        private void ErrorLoggingCheck_Pressed()
        {
            Setting.MiscSettings.ErrorLogging = ErrorLoggingCheck.ToggleMode;
        }

        private void SoundsEffectsVolumeSlider_ValueChanged(double value)
        {
            float floatValue = (float)value;
            Setting.AudioSettings.SoundEffectsVolume = floatValue;
            SoundsEffectsVolumeLabel.Text = floatValue.ToString();
        }

        private void MusicVolumeSlider_ValueChanged(double value)
        {
            float floatValue = (float)value;
            Setting.AudioSettings.MusicVolume = floatValue;
            MusicVolumeLabel.Text = floatValue.ToString();
        }

        private void MasterVolumeSlider_ValueChanged(double value)
        {
            float floatValue = (float)value;
            Setting.AudioSettings.MasterVolume = floatValue;
            MasterVolumeLabel.Text = floatValue.ToString();
        }

        private void VsyncButton_Renamed()
        {
            Setting.GraphicsSettings.VSync = VsyncButton.Text;
        }

        private void GraphicsQualityButton_Renamed()
        {
            Setting.GraphicsSettings.GraphicsQuality = GraphicsQualityButton.Text;

        }
        private void ResolutionButton_Renamed()
        {
            Setting.GraphicsSettings.Resolution = ResolutionButton.Text;
        }

        private void AspectTypeButton_Renamed()
        {
            Setting.GraphicsSettings.AspectType = AspectTypeButton.Text;
        }

        private void ScreenModeBtn_Renamed()
        {
            Setting.GraphicsSettings.ScreenMode = ScreenModeButton.Text;
        }
    }
}
