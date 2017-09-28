using Cas.Common.WPF.Interfaces;
using EFSM.Designer.Common;
using EFSM.Domain;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.CommandWpf;
using System;
using System.Windows.Input;

namespace EFSM.Designer.ViewModel
{
    public class GenerationOptionsViewModel : ViewModelBase
    {
        private readonly GenerationOptions _model;
        private readonly IMarkDirty _dirtyService;
        public ICommand HeaderFilePathCommand { get; private set; }

        public GenerationOptionsViewModel(GenerationOptions model, IMarkDirty dirtyService)
        {
            _model = model ?? throw new ArgumentNullException(nameof(model));
            _dirtyService = dirtyService ?? throw new ArgumentNullException(nameof(dirtyService));
            HeaderFilePathCommand = new RelayCommand(OpenHeaderFilePathDialog);
        }

        private void OpenHeaderFilePathDialog()
        {

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