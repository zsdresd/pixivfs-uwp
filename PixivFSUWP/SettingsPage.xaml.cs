﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading.Tasks;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Security.Credentials;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using static PixivFSUWP.Data.OverAll;

// https://go.microsoft.com/fwlink/?LinkId=234238 上介绍了“空白页”项模板

namespace PixivFSUWP
{
    /// <summary>
    /// 可用于自身或导航至 Frame 内部的空白页。
    /// </summary>
    public sealed partial class SettingsPage : Page
    {
        public SettingsPage()
        {
            this.InitializeComponent();
            _ = loadContentsAsync();
        }

        async Task loadContentsAsync()
        {
            var imgTask = LoadImageAsync(currentUser.Avatar170);
            txtID.Text = currentUser.ID.ToString();
            txtName.Text = currentUser.Username;
            txtAccount.Text = "@" + currentUser.UserAccount;
            txtEmail.Text = currentUser.Email;
            imgAvatar.ImageSource = await imgTask;
        }

        private void BtnLogout_Click(object sender, RoutedEventArgs e)
        {
            var vault = new PasswordVault();
            try
            {
                vault.Remove(GetCredentialFromLocker(passwordResource));
                vault.Remove(GetCredentialFromLocker(refreshTokenResource));
            }
            catch { }
            finally
            {
                ((Frame.Parent as Grid).Parent as MainPage).Frame.Navigate(typeof(LoginPage));
            }
        }
    }
}