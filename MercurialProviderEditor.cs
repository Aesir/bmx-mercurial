﻿using Inedo.BuildMaster.Extensibility.Providers;
using Inedo.BuildMaster.Web.Controls;
using Inedo.BuildMaster.Web.Controls.Extensions;
using Inedo.Web.Controls;

namespace Inedo.BuildMasterExtensions.Mercurial
{
    /// <summary>
    /// Defines the common fields of a Mercurial provider editor.
    /// </summary>
    internal sealed class MercurialProviderEditor : ProviderEditorBase
    {
        private SourceControlFileFolderPicker exePath;
        private ValidatingTextBox txtTagUser;

        public override void BindToForm(ProviderBase extension)
        {
            this.EnsureChildControls();

            var provider = (MercurialProvider)extension;
            this.exePath.Text = provider.HgExecutablePath;
            this.txtTagUser.Text = provider.CommittingUser;
        }

        public override ProviderBase CreateFromForm()
        {
            this.EnsureChildControls();

            var provider = new MercurialProvider
            {
                HgExecutablePath = this.exePath.Text,
                CommittingUser = this.txtTagUser.Text
            };

            return provider;
        }

        protected override void CreateChildControls()
        {
            this.exePath = new SourceControlFileFolderPicker
            {
                DisplayMode = SourceControlBrowser.DisplayModes.FoldersAndFiles,
                ServerId = this.EditorContext.ServerId,
                Required = true
            };


            this.txtTagUser = new ValidatingTextBox { Width = 300 };

            this.Controls.Add(
                 new FormFieldGroup("Mercurial Username",
                     "The username used for tagging builds.",
                     false,
                     new StandardFormField("Username:", this.txtTagUser)
                     ),
                 new FormFieldGroup("Mercurial Exe Path",
                     "The executable path for hg (hg.exe on Windows).",
                     false,
                     new StandardFormField("Path:", this.exePath)
                     )
                );
        }
    }
}
