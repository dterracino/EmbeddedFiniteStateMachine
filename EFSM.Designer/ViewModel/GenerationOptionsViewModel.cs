using Cas.Common.WPF.Interfaces;
using EFSM.Designer.Common;
using EFSM.Designer.Const;
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
        public ICommand HeaderFilePathDialogCommand { get; private set; }
        public ICommand CodeFilePathDialogCommand { get; private set; }

        public GenerationOptionsViewModel(GenerationOptions model, IMarkDirty dirtyService)
        {
            _model = model ?? throw new ArgumentNullException(nameof(model));
            _dirtyService = dirtyService ?? throw new ArgumentNullException(nameof(dirtyService));
            HeaderFilePathDialogCommand = new RelayCommand<string>(OpenHeaderFilePathDialog);
            CodeFilePathDialogCommand = new RelayCommand<string>(OpenCodeFilePathDialog);
        }

        private void OpenCodeFilePathDialog(string projectFilePath)
        {
            SaveFileDialog openFileDialog = new SaveFileDialog
            {
                Filter = DesignerConstants.CodeFileFilter
            };

            if (openFileDialog.ShowDialog() == true)
            {
                CodeFilePath = GetRelativePathToProject(projectFilePath, openFileDialog.FileName);
            }
        }

        private void OpenHeaderFilePathDialog(string projectFilePath)
        {
            SaveFileDialog openFileDialog = new SaveFileDialog
            {
                Filter = DesignerConstants.HeaderFileFilter
            };

            if (openFileDialog.ShowDialog() == true)
            {
                HeaderFilePath = GetRelativePathToProject(projectFilePath, openFileDialog.FileName);
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