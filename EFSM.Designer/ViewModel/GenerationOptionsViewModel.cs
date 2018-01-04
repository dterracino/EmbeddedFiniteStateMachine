using Cas.Common.WPF.Interfaces;
using EFSM.Designer.Common;
using EFSM.Designer.Const;
using EFSM.Designer.Extensions;
using EFSM.Domain;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.CommandWpf;
using Microsoft.Win32;
using System;
using System.Windows.Input;

namespace EFSM.Designer.ViewModel
{
    public class GenerationOptionsViewModel : ViewModelBase
    {
        private readonly GenerationOptions _model;
        private readonly IMarkDirty _dirtyService;
        private IMessageBoxService _messageBoxService;
        public ICommand HeaderFilePathDialogCommand { get; private set; }
        public ICommand CodeFilePathDialogCommand { get; private set; }

        public GenerationOptionsViewModel(GenerationOptions model, IMarkDirty dirtyService, IMessageBoxService messageBoxService)
        {
            _model = model ?? throw new ArgumentNullException(nameof(model));
            _dirtyService = dirtyService ?? throw new ArgumentNullException(nameof(dirtyService));
            _messageBoxService = messageBoxService ?? throw new ArgumentNullException(nameof(messageBoxService));
            HeaderFilePathDialogCommand = new RelayCommand<string>(OpenHeaderFilePathDialog);
            CodeFilePathDialogCommand = new RelayCommand<string>(OpenCodeFilePathDialog);
        }

        private void OpenCodeFilePathDialog(string projectFilePath)
        {
            try
            {
                SaveFileDialog openFileDialog = new SaveFileDialog
                {
                    Filter = DesignerConstants.CodeFileFilter
                };

                if (openFileDialog.ShowDialog() == true)
                {
                    CodeFilePath = GetRelativePathToProject(projectFilePath, openFileDialog.FileName);
                    //CodeFilePath = openFileDialog.FileName;
                }
            }
            catch (Exception ex)
            {
                _messageBoxService.Show(ex);
            }
        }

        private void OpenHeaderFilePathDialog(string projectFilePath)
        {
            try
            {
                SaveFileDialog openFileDialog = new SaveFileDialog
                {
                    Filter = DesignerConstants.HeaderFileFilter
                };

                if (openFileDialog.ShowDialog() == true)
                {
                    //HeaderFilePath = openFileDialog.FileName;
                    CodeFilePath = GetRelativePathToProject(projectFilePath, openFileDialog.FileName);
                }
            }
            catch (Exception ex)
            {
                _messageBoxService.Show(ex);
            }
        }

        private string GetRelativePathToProject(string projectFilePath, string filePath)
        {
            if (!string.IsNullOrWhiteSpace(projectFilePath) && filePath.Contains(projectFilePath))
            {
                filePath = filePath.Replace(projectFilePath, string.Empty);
            }

            return filePath;
        }

        public string CodeFilePath
        {
            get { return _model.CodeFilePath; }
            set
            {
                _model.CodeFilePath = value;
                RaisePropertyChanged();
                _dirtyService.MarkDirty();
            }
        }

        public string HeaderFilePath
        {
            get { return _model.HeaderFilePath; }
            set
            {
                _model.HeaderFilePath = value;
                RaisePropertyChanged();
                _dirtyService.MarkDirty();
            }
        }

        public string DocumentationFolder
        {
            get { return _model.DocumentationFolder; }
            set
            {
                _model.DocumentationFolder = value;
                RaisePropertyChanged();
                _dirtyService.MarkDirty();
            }
        }


        public string HeaderFileHeader
        {
            get { return _model.HeaderFileHeader; }
            set
            {
                _model.HeaderFileHeader = value;
                RaisePropertyChanged();
                _dirtyService.MarkDirty();
            }
        }

        public string HeaderFileFooter
        {
            get { return _model.HeaderFileFooter; }
            set
            {
                _model.HeaderFileFooter = value;
                RaisePropertyChanged();
                _dirtyService.MarkDirty();
            }
        }

        public GenerationOptions GetModel()
        {
            return _model.Clone();
        }
    }
}