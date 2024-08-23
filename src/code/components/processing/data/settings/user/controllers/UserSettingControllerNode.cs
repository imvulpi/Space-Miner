    using Godot;
using GruInject.API.Nodes;
using SpaceMiner.src.code.components.commons.godot.project_settings.display.graphics;
using SpaceMiner.src.code.components.commons.godot.project_settings.display.window.size;
using SpaceMiner.src.code.components.commons.godot.project_settings.display.window.stretch;
using SpaceMiner.src.code.components.commons.other.paths;
using SpaceMiner.src.code.components.experiments.testing.scripts.MenusTest;
using SpaceMiner.src.code.components.processing.data.settings.couplers;
using SpaceMiner.src.code.components.processing.ui.menu.interfaces;
using System;
using System.IO;
using static Godot.DisplayServer;

namespace SpaceMiner.src.code.components.processing.data.settings.user.controllers
{
    public partial class UserSettingControllerNode : GruNode2D, IUserSettingController, IMenuContainer
    {
        public UserSettings Setting { get; set; }
        public SettingCoupler SettingCoupler { get; set; }
        [ExportGroup("Settings")]
        [ExportSubgroup("Graphics")]
        [Export] public Button WindowModeButton {  get; set; }
        [Export] public ItemList WindowModeList {  get; set; }
        [Export] public Button AspectTypeButton { get; set; }
        [Export] public ItemList AspectTypeList { get; set; }
        [Export] public Button ResolutionButton { get; set; }
        [Export] public Button GraphicsQualityButton {  get; set; }
        [Export] public ItemList GraphicsQualityList { get; set; }
        [Export] public Button VsyncButton { get; set; }
        [Export] public ItemList VsyncList { get; set; }
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
        public IMenuManager MenuManager { get; set; }
        public IMenu Menu { get; set; }

        public override void _Ready()
        {
            Setting = new(Path.Join(Godot.OS.GetUserDataDir(), ExternalPaths.USER_SETTING));
            SettingCoupler = new();
            SettingCoupler.Load(Setting);

            GraphicsQualityList.ItemSelected += GraphicsQualityList_ItemSelected;
            WindowModeList.ItemSelected += WindowModeList_ItemSelected;
            AspectTypeList.ItemSelected += AspectTypeList_ItemSelected;
            VsyncList.ItemSelected += VsyncList_ItemSelected;
            ResolutionButton.Renamed += ResolutionButton_Renamed;
            MasterVolumeSlider.ValueChanged += MasterVolumeSlider_ValueChanged;
            MusicVolumeSlider.ValueChanged += MusicVolumeSlider_ValueChanged;
            SoundsEffectsVolumeSlider.ValueChanged += SoundsEffectsVolumeSlider_ValueChanged;
            ErrorLoggingCheck.Pressed += ErrorLoggingCheck_Pressed;

            WindowModeButton.Text = Setting.GraphicsSettings.WindowMode.ToString();
            AspectTypeButton.Text = Setting.GraphicsSettings.AspectType.ToString();
            ResolutionButton.Text = Setting.GraphicsSettings.Resolution;
            GraphicsQualityButton.Text = Setting.GraphicsSettings.GraphicsQuality.ToString();
            VsyncButton.Text = Setting.GraphicsSettings.VSync.ToString();
            MasterVolumeSlider.Value = Setting.AudioSettings.MasterVolume;
            MusicVolumeSlider.Value = Setting.AudioSettings.MusicVolume;
            SoundsEffectsVolumeSlider.Value = Setting.AudioSettings.SoundEffectsVolume;

            ErrorLoggingCheck.ButtonPressed = Setting.MiscSettings.ErrorLogging;
            
            // Back button actions:
            BackButton.Pressed += BackButton_Pressed;
            ApplyButton.Pressed += ApplyButton_Pressed;
            CancelButton.Pressed += CancelButton_Pressed;
            CloseButton.Pressed += CloseButton_Pressed;

            Menu.EscActionDelegate = SettingEscPressed;
        }

        public override void _Process(double delta)
        {
            if (Input.IsActionJustPressed("Esc"))
            {
                SaveSettingMenu.Visible = !SaveSettingMenu.Visible;
            }
        }

        private bool SettingEscPressed(IMenuManager manager)
        {
            return true;
        }

        private void VsyncList_ItemSelected(long index)
        {
            Setting.GraphicsSettings.VSync = (VSyncMode)index;
        }

        private void AspectTypeList_ItemSelected(long index)
        {
            Setting.GraphicsSettings.AspectType = (AspectType)index;
        }

        private void WindowModeList_ItemSelected(long index)
        {
            Setting.GraphicsSettings.WindowMode = (WindowMode)index;
        }

        private void GraphicsQualityList_ItemSelected(long index)
        {
            Setting.GraphicsSettings.GraphicsQuality = (GraphicsQuality)index;
        }

        private void CloseButton_Pressed()
        {
            SaveSettingMenu.Visible = false;
        }

        private void CancelButton_Pressed()
        {
            SettingCoupler.Load(Setting); // Cancels the setting
            Menu.Close();
        }

        private void ApplyButton_Pressed()
        {
            SettingCoupler.Save(Setting);
            Menu.Close();
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
        private void ResolutionButton_Renamed()
        {
            Setting.GraphicsSettings.Resolution = ResolutionButton.Text;
        }
    }
}
