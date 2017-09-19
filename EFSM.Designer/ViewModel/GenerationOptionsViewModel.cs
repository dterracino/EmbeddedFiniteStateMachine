using System;
using Cas.Common.WPF.Interfaces;
using EFSM.Designer.Common;
using EFSM.Domain;
using EFSM.Generator;
using GalaSoft.MvvmLight;

namespace EFSM.Designer.ViewModel
{
    public class GenerationOptionsViewModel : ViewModelBase
    {
        private readonly GenerationOptions _model;
        private readonly IDirtyService _dirtyService;

        public GenerationOptionsViewModel(GenerationOptions model, IDirtyService dirtyService)
        {
            if (model == null) throw new ArgumentNullException(nameof(model));
            if (dirtyService == null) throw new ArgumentNullException(nameof(dirtyService));
            _model = model;
            _dirtyService = dirtyService;
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