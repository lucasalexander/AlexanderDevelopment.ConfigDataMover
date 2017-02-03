// --------------------------------------------------------------------------------------------------------------------
// about.cs
//
// Copyright 2015-2017 Lucas Alexander
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
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AlexanderDevelopment.ConfigDataMover
{
    public partial class About : Form
    {
        public About()
        {
            InitializeComponent();

            string apacheText = @"Licensed under the Apache License, Version 2.0 (the 'License'); you may not use this file except in compliance with the License. You may obtain a copy of the License at

http://www.apache.org/licenses/LICENSE-2.0

Unless required by applicable law or agreed to in writing, software distributed under the License is distributed on an 'AS IS' BASIS, WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied. See the License for the specific language governing permissions and limitations under the License.";

            StringBuilder licenseSb = new StringBuilder();
            licenseSb.AppendLine("Dynamics CRM Configuration Data Mover\n");
            licenseSb.AppendLine("Version " + System.Reflection.Assembly.GetExecutingAssembly().GetName().Version + "\n");
            licenseSb.AppendLine("Copyright " + DateTime.Now.Year.ToString() + " Lucas Alexander\n");
            licenseSb.AppendLine(apacheText.Replace("'", "\""));
            string aboutText = licenseSb.ToString();
            this.label1.Text = aboutText;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
