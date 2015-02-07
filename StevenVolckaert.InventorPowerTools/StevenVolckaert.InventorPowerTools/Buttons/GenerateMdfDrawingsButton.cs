﻿using System;
using Inventor;
using StevenVolckaert.InventorPowerTools.Windows;
using Environment = System.Environment;

namespace StevenVolckaert.InventorPowerTools.Buttons
{
    internal class GenerateMdfDrawingsButton : ButtonBase
    {
        private readonly GenerateMdfDrawingsWindow _generateDrawingsWindow = new GenerateMdfDrawingsWindow();

        public override string DisplayName
        {
            get { return "MDF"; }
        }

        public override string Description
        {
            get { return "Generate a drawing of every" + Environment.NewLine + "MDF part in the active document."; }
        }

        protected override void OnExecute(NameValueMap context)
        {
            try
            {
                var assemblyDocument = AddIn.GetAssemblyDocument();

                if (assemblyDocument == null)
                    return;

                var mdfParts = assemblyDocument.MdfParts();

                if (mdfParts.Count == 0)
                {
                    AddIn.ShowWarningMessageBox(
                        caption: _generateDrawingsWindow.Title,
                        messageFormat:
                            "Assembly '{0}' doesn't contain any MDF parts."
                                + Environment.NewLine + Environment.NewLine
                                + "Part file names must contain 'MDF' to be recognised as MDF parts",
                        messageArgs: assemblyDocument.FullFileName
                    );

                    return;
                }

                _generateDrawingsWindow.Show(assemblyDocument.MdfParts());
            }
            catch (Exception ex)
            {
                ShowWarningMessageBox(ex);
            }
        }
    }
}
