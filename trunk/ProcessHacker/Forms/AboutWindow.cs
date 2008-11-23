﻿/*
 * Process Hacker
 * 
 * Copyright (C) 2008 wj32
 * 
 * This program is free software: you can redistribute it and/or modify
 * it under the terms of the GNU General Public License as published by
 * the Free Software Foundation, either version 3 of the License, or
 * (at your option) any later version.
 *
 * This program is distributed in the hope that it will be useful,
 * but WITHOUT ANY WARRANTY; without even the implied warranty of
 * MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
 * GNU General Public License for more details.
 *
 * You should have received a copy of the GNU General Public License
 * along with this program.  If not, see <http://www.gnu.org/licenses/>.
 */

using System;
using System.Diagnostics;
using System.Text;
using System.Windows.Forms;

namespace ProcessHacker
{
    partial class AboutWindow : Form
    {
        public AboutWindow()
        {
            InitializeComponent();

            labelVersion.Text = Application.ProductVersion;
        }

        private void buttonClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void TryStart(string command)
        {
            try
            {
                Process.Start(command);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Could not start process:\n\n" + ex.Message, "Process Hacker", MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        private void linkHexBox_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            TryStart("http://sourceforge.net/projects/hexbox");
        }

        private void linkVistaMenu_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            TryStart("http://wyday.com");
        }

        private void linkFamFamFam_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            TryStart("http://www.famfamfam.com/lab/icons/silk/");
        }

        private void linkSourceforge_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            TryStart("http://sourceforge.net/projects/processhacker");
        }

        private void linkEmail_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            TryStart("mailto:wj32.64@gmail.com");
        }

        private void buttonMoreInfo_Click(object sender, EventArgs e)
        {
            StringBuilder sb = new StringBuilder();

            sb.AppendLine(
                string.Format("Loaded symbols: {0} symbols over {1} modules.", Symbols.SymbolCount, Symbols.LibraryCount));

            InformationBox box = new InformationBox(sb.ToString());

            box.ShowDialog();
        }

        private void linkAsm_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            TryStart("http://www.ollydbg.de");
        }
    }
}
