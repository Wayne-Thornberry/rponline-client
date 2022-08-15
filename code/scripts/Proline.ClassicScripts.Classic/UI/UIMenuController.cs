using System.Threading;
using System.Threading.Tasks;
using CitizenFX.Core;
using Proline.ClassicOnline.CCoreSystem; 
using CitizenFX.Core.Native;
using System.Collections.Generic;
using CitizenFX.Core.UI;
using System.Linq;
using System;
using Proline.ClassicOnline.SClassic.UI.Menu;
using Proline.ClassicOnline.CScreenRendering;

namespace Proline.ClassicOnline.SClassic.UI
{
    public class AspectRatioException : Exception
    {
        public AspectRatioException(string message) : base(message)
        {
        }
    }

    public enum MenuAlignmentOption
    {
        Left,
        Right
    }


    public class UIMenuController
    {
        public UIMenuController()
        {

        }

        public const string _texture_dict = "commonmenu";
        public const string _header_texture = "interaction_bgd";

        internal  int _scale = API.RequestScaleformMovie("INSTRUCTIONAL_BUTTONS");

        private  int ManualTimerForGC = API.GetGameTimer();

        private  MenuAlignmentOption _alignment = MenuAlignmentOption.Left;


         

        public  List<int> Menus { get; protected set; } = new List<int>();

        private  float AspectRatio => API.GetScreenAspectRatio(false);
        public  float ScreenWidth => 1080 * AspectRatio;
        public  float ScreenHeight => 1080;
        public  bool DisableMenuButtons { get; set; } = false;

        public  bool AreMenuButtonsEnabled => Menus.Any(m => m.Visible) && !Game.IsPaused &&
                                                    Screen.Fading.IsFadedIn && !API.IsPlayerSwitchInProgress() &&
                                                    !DisableMenuButtons && !Game.Player.IsDead;

        public  bool EnableManualGCs { get; set; } = true;
        public  bool DontOpenAnyMenu { get; set; } = false;
        public  bool PreventExitingMenu { get; set; } = false;
        public  bool DisableBackButton { get; set; } = false;
        public  Control MenuToggleKey { get; set; } = Control.InteractionMenu;

        internal  Dictionary<MenuItem, int> MenuButtons { get; } = new Dictionary<MenuItem, int>();

        public  int MainMenu { get; set; }

        public  MenuAlignmentOption MenuAlignment
        {
            get => _alignment;
            set
            {
                if (AspectRatio < 1.888888888888889f)
                {
                    // alignment can be whatever the resource wants it to be because this aspect ratio is supported.
                    _alignment = value;
                }
                // right aligned menus are not supported for aspect ratios 17:9 or 21:9.
                else
                {
                    // no matter what the new value would've been, the aspect ratio does not support right aligned menus, 
                    // so (re)set it to be left aligned.
                    _alignment = MenuAlignmentOption.Left;

                    // In case the value was being changed to be right aligned, throw an exception so the resource can handle this and notify the user properly.
                    if (value == MenuAlignmentOption.Right)
                        throw new AspectRatioException(
                            "Right aligned menus are not supported for aspect ratios 17:9 or 21:9.");
                }
            }
        }

        /// <summary>
        ///     Loads the texture dict for the common menu sprites.
        /// </summary>
        /// <returns></returns>
        private  async Task LoadAssets()
        {
            if (!API.HasStreamedTextureDictLoaded(_texture_dict) || !API.HasStreamedTextureDictLoaded("mpleaderboard"))
            {
                API.RequestStreamedTextureDict(_texture_dict, false);
                API.RequestStreamedTextureDict("mpleaderboard", false);
                while (!API.HasStreamedTextureDictLoaded(_texture_dict) ||
                       !API.HasStreamedTextureDictLoaded("mpleaderboard")) await BaseScript.Delay(0);
            }
        }

        /// <summary>
        ///     Unloads the texture dict for the common menu sprites.
        /// </summary>
        private  void UnloadAssets()
        {
            if (API.HasStreamedTextureDictLoaded(_texture_dict))
                API.SetStreamedTextureDictAsNoLongerNeeded(_texture_dict);
        }

        /// <summary>
        ///     Disables the most important controls for when a menu is open.
        /// </summary>
        private  void DisableControls()
        {
            #region Disable Inputs when any menu is open.

            if (CScreenRenderingAPI.IsAnyMenuOpen())
            {
                // Close all menus when the player dies.
                if (Game.PlayerPed.IsDead) CScreenRenderingAPI.CloseAllMenus();

                // Disable Gamepad/Controller Specific controls:
                if (Game.CurrentInputMode == InputMode.GamePad)
                {
                    Game.DisableControlThisFrame(0, Control.MultiplayerInfo);
                    // when in a vehicle.
                    if (Game.PlayerPed.IsInVehicle())
                    {
                        Game.DisableControlThisFrame(0, Control.VehicleHeadlight);
                        Game.DisableControlThisFrame(0, Control.VehicleDuck);
                    }
                }
                else // when not using a controller.
                {
                    Game.DisableControlThisFrame(0,
                        Control
                            .FrontendPauseAlternate); // disable the escape key opening the pause menu, pressing P still works.

                    // Disable the scrollwheel button changing weapons while the menu is open.
                    // Only if you press TAB (to show the weapon wheel) then it will allow you to change weapons.
                    if (!Game.IsControlPressed(0, Control.SelectWeapon))
                    {
                        Game.DisableControlThisFrame(24, Control.SelectNextWeapon);
                        Game.DisableControlThisFrame(24, Control.SelectPrevWeapon);
                    }
                }
                // Disable Shared Controls

                // Radio Inputs
                Game.DisableControlThisFrame(0, Control.RadioWheelLeftRight);
                Game.DisableControlThisFrame(0, Control.RadioWheelUpDown);
                Game.DisableControlThisFrame(0, Control.VehicleNextRadio);
                Game.DisableControlThisFrame(0, Control.VehicleRadioWheel);
                Game.DisableControlThisFrame(0, Control.VehiclePrevRadio);

                // Phone / Arrows Inputs
                Game.DisableControlThisFrame(0, Control.Phone);
                Game.DisableControlThisFrame(0, Control.PhoneCancel);
                Game.DisableControlThisFrame(0, Control.PhoneDown);
                Game.DisableControlThisFrame(0, Control.PhoneLeft);
                Game.DisableControlThisFrame(0, Control.PhoneRight);

                // Attack Controls
                Game.DisableControlThisFrame(0, Control.Attack);
                Game.DisableControlThisFrame(0, Control.Attack2);
                Game.DisableControlThisFrame(0, Control.MeleeAttack1);
                Game.DisableControlThisFrame(0, Control.MeleeAttack2);
                Game.DisableControlThisFrame(0, Control.MeleeAttackAlternate);
                Game.DisableControlThisFrame(0, Control.MeleeAttackHeavy);
                Game.DisableControlThisFrame(0, Control.MeleeAttackLight);
                Game.DisableControlThisFrame(0, Control.VehicleAttack);
                Game.DisableControlThisFrame(0, Control.VehicleAttack2);
                Game.DisableControlThisFrame(0, Control.VehicleFlyAttack);
                Game.DisableControlThisFrame(0, Control.VehiclePassengerAttack);
                Game.DisableControlThisFrame(0, Control.Aim);

                // When in a vehicle
                if (Game.PlayerPed.IsInVehicle())
                {
                    Game.DisableControlThisFrame(0, Control.VehicleSelectNextWeapon);
                    Game.DisableControlThisFrame(0, Control.VehicleSelectPrevWeapon);
                    Game.DisableControlThisFrame(0, Control.VehicleCinCam);
                }
            }

            #endregion
        }

        /// <summary>
        ///     Draws all the menus that are visible on the screen.
        /// </summary>
        /// <returns></returns>
        private  async Task ProcessMenus()
        {
            if (Menus.Count > 0 && CScreenRenderingAPI.IsAnyMenuOpen() && !Game.IsPaused && !Game.Player.IsDead && API.IsScreenFadedIn() &&
                !API.IsPlayerSwitchInProgress())
            {
                await LoadAssets();

                DisableControls();

                var menu = CScreenRenderingAPI.GetCurrentMenu();
                if (menu != null)
                {
                    if (DontOpenAnyMenu)
                    {
                        if (menu.Visible && !menu.IgnoreDontOpenMenus) menu.CloseMenu();
                    }
                    else if (menu.Visible)
                    {
                        menu.Draw();
                    }
                }

                if (EnableManualGCs)
                    if (API.GetGameTimer() - ManualTimerForGC > 60000)
                    {
                        GC.Collect();
                        ManualTimerForGC = API.GetGameTimer();
                    }
            }
            else
            {
                UnloadAssets();
            }
        }

        internal  async Task DrawInstructionalButtons()
        {
            if (!Game.IsPaused && !Game.Player.IsDead && API.IsScreenFadedIn() && !API.IsPlayerSwitchInProgress())
            {
                var menu = CScreenRenderingAPI.GetCurrentMenu();
                if (menu != null && menu.Visible && menu.EnableInstructionalButtons)
                {
                    if (!API.HasScaleformMovieLoaded(_scale))
                        _scale = API.RequestScaleformMovie("INSTRUCTIONAL_BUTTONS");
                    while (!API.HasScaleformMovieLoaded(_scale)) await BaseScript.Delay(0);

                    API.PushScaleformMovieFunction(_scale, "CLEAR_ALL");
                    API.EndScaleformMovieMethod();


                    for (var i = 0; i < menu.InstructionalButtons.Count; i++)
                    {
                        var text = menu.InstructionalButtons.ElementAt(i).Value;
                        var control = menu.InstructionalButtons.ElementAt(i).Key;

                        API.PushScaleformMovieFunction(_scale, "SET_DATA_SLOT");
                        API.PushScaleformMovieMethodParameterInt(i);
                        var buttonName = API.GetControlInstructionalButton(0, (int)control, 1);
                        API.PushScaleformMovieMethodParameterString(buttonName);
                        API.PushScaleformMovieMethodParameterString(text);
                        API.EndScaleformMovieMethod();
                    }

                    API.PushScaleformMovieFunction(_scale, "DRAW_INSTRUCTIONAL_BUTTONS");
                    API.PushScaleformMovieMethodParameterInt(0);
                    API.EndScaleformMovieMethod();

                    API.DrawScaleformMovieFullscreen(_scale, 255, 255, 255, 255, 0);
                    return;
                }
            }

            DisposeInstructionalButtonsScaleform();
        }

        private  void DisposeInstructionalButtonsScaleform()
        {
            if (API.HasScaleformMovieLoaded(_scale)) API.SetScaleformMovieAsNoLongerNeeded(ref _scale);
        }


        #region Process Menu Buttons

        /// <summary>
        ///     Process the select & go back/cancel buttons.
        /// </summary>
        /// <returns></returns>
        private async Task ProcessMainButtons()
        {
            if (CScreenRenderingAPI.IsAnyMenuOpen())
            {
                var currentMenu = CScreenRenderingAPI.GetCurrentMenu();
                if (currentMenu != null && !DontOpenAnyMenu)
                {
                    if (PreventExitingMenu)
                    {
                        Game.DisableControlThisFrame(0, Control.FrontendPause);
                        Game.DisableControlThisFrame(0, Control.FrontendPauseAlternate);
                    }

                    if (CScreenRenderingAPI.IsAnyMenuOpen() && AreMenuButtonsEnabled)
                    {
                        // Select / Enter
                        if (Game.IsDisabledControlJustReleased(0, Control.FrontendAccept) ||
                            Game.IsControlJustReleased(0, Control.FrontendAccept) ||
                            Game.IsDisabledControlJustReleased(0, Control.VehicleMouseControlOverride) ||
                            Game.IsControlJustReleased(0, Control.VehicleMouseControlOverride))
                        {
                            if (CScreenRenderingAPI.GetMenuItemSize() > 0) CScreenRenderingAPI.SelectMenuItem(CScreenRenderingAPI.GetCurrentSelectedIndex());
                        }
                        // Cancel / Go Back
                        else if (Game.IsDisabledControlJustReleased(0, Control.PhoneCancel) && !DisableBackButton)
                        {
                            // Wait for the next frame to make sure the "cinematic camera" button doesn't get "re-enabled" before the menu gets closed.
                            await BaseScript.Delay(0);
                            currentMenu.GoBack();
                        }
                        else if (Game.IsDisabledControlJustReleased(0, Control.PhoneCancel) && PreventExitingMenu &&
                                 !DisableBackButton)
                        {
                            // if there's a parent menu, allow going back to that, but don't allow a 'top-level' menu to be closed.
                            if (currentMenu.ParentMenu != null) currentMenu.GoBack();
                            await BaseScript.Delay(0);
                        }
                    }
                }
            }
        }

        /// <summary>
        ///     Returns true when one of the 'up' controls is currently pressed, only if the button can be active according to some
        ///     conditions.
        /// </summary>
        /// <returns></returns>
        private bool IsUpPressed()
        {
            // Return false if the buttons are not currently enabled.
            if (!AreMenuButtonsEnabled) return false;

            // when the player is holding TAB, while not in a vehicle, and when the scrollwheel is being used, return false to prevent interferring with weapon selection.
            if (!Game.PlayerPed.IsInVehicle())
                if (Game.IsControlPressed(0, Control.SelectWeapon))
                    if (Game.IsControlPressed(0, Control.SelectNextWeapon) ||
                        Game.IsControlPressed(0, Control.SelectPrevWeapon))
                        return false;

            // return true if the scrollwheel up or the arrow up key is being used at this frame.
            if (Game.IsControlPressed(0, Control.FrontendUp) ||
                Game.IsDisabledControlPressed(0, Control.FrontendUp) ||
                Game.IsControlPressed(0, Control.PhoneScrollBackward) ||
                Game.IsDisabledControlPressed(0, Control.PhoneScrollBackward))
                return true;

            // return false if none of the conditions matched.
            return false;
        }

        /// <summary>
        ///     Returns true when one of the 'down' controls is currently pressed, only if the button can be active according to
        ///     some conditions.
        /// </summary>
        /// <returns></returns>
        private bool IsDownPressed()
        {
            // Return false if the buttons are not currently enabled.
            if (!AreMenuButtonsEnabled) return false;

            // when the player is holding TAB, while not in a vehicle, and when the scrollwheel is being used, return false to prevent interferring with weapon selection.
            if (!Game.PlayerPed.IsInVehicle())
                if (Game.IsControlPressed(0, Control.SelectWeapon))
                    if (Game.IsControlPressed(0, Control.SelectNextWeapon) ||
                        Game.IsControlPressed(0, Control.SelectPrevWeapon))
                        return false;

            // return true if the scrollwheel down or the arrow down key is being used at this frame.
            if (Game.IsControlPressed(0, Control.FrontendDown) ||
                Game.IsDisabledControlPressed(0, Control.FrontendDown) ||
                Game.IsControlPressed(0, Control.PhoneScrollForward) ||
                Game.IsDisabledControlPressed(0, Control.PhoneScrollForward))
                return true;

            // return false if none of the conditions matched.
            return false;
        }

        /// <summary>
        ///     Processes the menu toggle button to check if the menu should open or close.
        /// </summary>
        /// <returns></returns>
        private async Task ProcessToggleMenuButton()
        {
            if (!Game.IsPaused && !API.IsPauseMenuRestarting() && API.IsScreenFadedIn() &&
                !API.IsPlayerSwitchInProgress() && !Game.Player.IsDead)
            {
                if (CScreenRenderingAPI.IsAnyMenuOpen())
                {
                    if (Game.CurrentInputMode == InputMode.MouseAndKeyboard)
                        if ((Game.IsControlJustPressed(0, MenuToggleKey) ||
                             Game.IsDisabledControlJustPressed(0, MenuToggleKey)) && !PreventExitingMenu)
                        {
                            var menu = CScreenRenderingAPI.GetCurrentMenu();
                            CScreenRenderingAPI.CloseMenu();
                        }
                }
                else
                {
                    if (Game.CurrentInputMode == InputMode.GamePad)
                    {
                        var tmpTimer = API.GetGameTimer();
                        while ((Game.IsControlPressed(0, Control.InteractionMenu) ||
                                Game.IsDisabledControlPressed(0, Control.InteractionMenu)) && !Game.IsPaused &&
                               API.IsScreenFadedIn() && !Game.Player.IsDead && !API.IsPlayerSwitchInProgress() &&
                               !DontOpenAnyMenu)
                        {
                            if (API.GetGameTimer() - tmpTimer > 400)
                            {
                                if (MainMenu != null)
                                {
                                    CScreenRenderingAPI.OpenMenu(0);
                                }
                                else
                                {
                                    if (Menus.Count > 0) Menus[0].OpenMenu();
                                }

                                break;
                            }

                            await BaseScript.Delay(0);
                        }
                    }
                    else
                    {
                        if ((Game.IsControlJustPressed(0, MenuToggleKey) ||
                             Game.IsDisabledControlJustPressed(0, MenuToggleKey)) && !Game.IsPaused &&
                            API.IsScreenFadedIn() && !Game.Player.IsDead && !API.IsPlayerSwitchInProgress() &&
                            !DontOpenAnyMenu)
                            if (Menus.Count > 0)
                            {
                                if (MainMenu != null)
                                    CScreenRenderingAPI.OpenMenu(0);
                                else
                                    Menus[0].OpenMenu();
                            }
                    }
                }
            }
        }

        /// <summary>
        ///     Process left/right/up/down buttons (also holding down buttons will speed up after 3 iterations)
        /// </summary>
        /// <returns></returns>
        private async Task ProcessDirectionalButtons()
        {
            // Return if the buttons are not currently enabled.
            if (!AreMenuButtonsEnabled) return;

            // Get the currently open menu.
            var currentMenu = GetCurrentMenu();
            // If it exists.
            if (currentMenu != null && !DontOpenAnyMenu && currentMenu.Size > 0)
                if (currentMenu.Visible)
                {
                    // Check if the Go Up controls are pressed.
                    if (IsUpPressed())
                    {
                        // Update the currently selected item to the new one.
                        currentMenu.GoUp();

                        // Get the current game time.
                        var time = API.GetGameTimer();
                        var times = 0;
                        var delay = 200;

                        // Do the following as long as the controls are being pressed.
                        while (IsUpPressed() && IsAnyMenuOpen() && GetCurrentMenu() != null)
                        {
                            // Update the current menu.
                            currentMenu = GetCurrentMenu();

                            // Check if the game time has changed by "delay" amount.
                            if (API.GetGameTimer() - time > delay)
                            {
                                // Increment the "changed indexes" counter
                                times++;

                                // If the controls are still being held down after moving 3 indexes, reduce the delay between index changes.
                                if (times > 2) delay = 150;

                                // Update the currently selected item to the new one.
                                currentMenu.GoUp();

                                // Reset the time to the current game timer.
                                time = API.GetGameTimer();
                            }

                            // Wait for the next game tick.
                            await BaseScript.Delay(0);
                        }
                    }

                    // Check if the Go Down controls are pressed.
                    else if (IsDownPressed())
                    {
                        currentMenu.GoDown();

                        var time = API.GetGameTimer();
                        var times = 0;
                        var delay = 200;
                        while (IsDownPressed() && GetCurrentMenu() != null)
                        {
                            currentMenu = GetCurrentMenu();
                            if (API.GetGameTimer() - time > delay)
                            {
                                times++;
                                if (times > 2) delay = 150;
                                currentMenu.GoDown();

                                time = API.GetGameTimer();
                            }

                            await BaseScript.Delay(0);
                        }
                    }

                    // Check if the Go Left controls are pressed.
                    else if (Game.IsDisabledControlJustPressed(0, Control.PhoneLeft) &&
                             !Game.IsControlPressed(0, Control.SelectWeapon))
                    {
                        var item = currentMenu.GetMenuItems()[currentMenu.CurrentIndex];
                        if (item.Enabled) //&& item is MenuItemButton)
                        {
                            currentMenu.GoLeft();
                            var time = API.GetGameTimer();
                            var times = 0;
                            var delay = 200;
                            while (Game.IsDisabledControlPressed(0, Control.PhoneLeft) &&
                                   !Game.IsControlPressed(0, Control.SelectWeapon) && GetCurrentMenu() != null &&
                                   AreMenuButtonsEnabled)
                            {
                                currentMenu = GetCurrentMenu();
                                if (API.GetGameTimer() - time > delay)
                                {
                                    times++;
                                    if (times > 2) delay = 150;
                                    currentMenu.GoLeft();
                                    time = API.GetGameTimer();
                                }

                                await BaseScript.Delay(0);
                            }
                        }
                    }

                    // Check if the Go Right controls are pressed.
                    else if (Game.IsDisabledControlJustPressed(0, Control.PhoneRight) &&
                             !Game.IsControlPressed(0, Control.SelectWeapon))
                    {
                        var item = currentMenu.GetMenuItems()[currentMenu.CurrentIndex];
                        if (item.Enabled)
                        {
                            currentMenu.GoRight();
                            var time = API.GetGameTimer();
                            var times = 0;
                            var delay = 200;
                            while ((Game.IsDisabledControlPressed(0, Control.PhoneRight) ||
                                    Game.IsControlPressed(0, Control.PhoneRight)) &&
                                   !Game.IsControlPressed(0, Control.SelectWeapon) && GetCurrentMenu() != null &&
                                   AreMenuButtonsEnabled)
                            {
                                currentMenu = GetCurrentMenu();
                                if (API.GetGameTimer() - time > delay)
                                {
                                    times++;
                                    if (times > 2) delay = 150;
                                    currentMenu.GoRight();
                                    time = API.GetGameTimer();
                                }

                                await BaseScript.Delay(0);
                            }
                        }
                    }
                }
        }


        public async Task Execute(object[] args, CancellationToken token)
        {
            // Dupe protection
            if (CCoreSystem.CCoreSystemAPI.GetInstanceCountOfScript("UIMenuController") > 1)
                return;


            while (!token.IsCancellationRequested)
            {
                await ProcessMenus();
                await DrawInstructionalButtons();
                await ProcessMainButtons();
                await ProcessDirectionalButtons();
                await ProcessToggleMenuButton();

                await BaseScript.Delay(0);
            }
        }
    } 
}
#endregion