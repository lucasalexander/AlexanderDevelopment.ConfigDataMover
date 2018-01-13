// --------------------------------------------------------------------------------------------------------------------
// JobError.xaml.cs
//
// Copyright 2015-2018 Lucas Alexander
// 
// Licensed under the Apache License, Version 2.0 (the "License");
// you may not use this file except in compliance with the License.
// You may obtain a copy of the License at
// 
// http://www.apache.org/licenses/LICENSE-2.0
// 
// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS,
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// See the License for the specific language governing permissions and
// limitations under the License.
// --------------------------------------------------------------------------------------------------------------------

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
using System.Windows.Shapes;

namespace AlexanderDevelopment.ConfigDataMover.Wpf
{
    /// <summary>
    /// Interaction logic for JobError.xaml
    /// </summary>
    public partial class JobError : Window
    {
        public JobError()
        {
            InitializeComponent();
        }

        public void SetDetails(string label, string details)
        {
            messageTextBox.Text = label;
            errorTextBox.Text = details;
        }

        private void closeButton_Click(object sender, EventArgs e)
        {
            this.Close();
        }


        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Clipboard.SetText(errorTextBox.Text);
        }
    }
}