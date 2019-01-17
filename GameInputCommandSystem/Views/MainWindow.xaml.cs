﻿using System;
using System.Windows;

/**
 Copyright [2019] [Terence Doerksen]

 Licensed under the Apache License, Version 2.0 (the "License");
 you may not use this file except in compliance with the License.
 You may obtain a copy of the License at

 http://www.apache.org/licenses/LICENSE-2.0

 Unless required by applicable law or agreed to in writing, software
 distributed under the License is distributed on an "AS IS" BASIS,
 WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
 See the License for the specific language governing permissions and
 limitations under the License.
 */
namespace GameInputCommandSystem
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        //private MainController controller;
        private static GateKeeper server = new GateKeeper();
        private bool isRunning = false;

        public MainWindow()
        {
            InitializeComponent();
            LoadSettings();
        }

        private void btnStart_Click(object sender, RoutedEventArgs e)
        {
            int port = Int16.Parse(txtPort.Text);
            SetApplication(txtTarget.Text);
            SetPassword(txtPassword.Text);
            SetPort(port);
            ToggleServer();
            SaveSettings(txtTarget.Text, txtPassword.Text, port);
        }

        private void LoadSettings()
        {
            txtTarget.Text = Properties.Settings.Default.target;
            txtPassword.Text = Properties.Settings.Default.password;
            int port = Properties.Settings.Default.port;
            if (port == 0)
                port = 8091;
            txtPort.Text = port.ToString();
        }

        private void SaveSettings(string target, string password, int port)
        {
            Properties.Settings.Default.target = target;
            Properties.Settings.Default.password = password;
            Properties.Settings.Default.port = port;
            Properties.Settings.Default.Save();
        }

        private void ToggleServer()
        {
            if (isRunning)
            {
                server.Stop();
                isRunning = !isRunning;
            }
            else
            {
                isRunning = server.Start();
            }
            toggleText();
        }

        private void toggleText()
        {
            if (!isRunning)
                btnStart.Content = "Start";
            else
                btnStart.Content = "Stop";
        }

        internal void SetPort(int newPort)
        {
            server.Port = newPort;
        }

        internal void SetPassword(string text)
        {
            server.Password = text;
        }

        internal void SetApplication(string text)
        {
            server.Application = text;
        }
    }
}