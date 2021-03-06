﻿using Cas.Common.WPF.Interfaces;
using EFSM.Designer.Common;
using EFSM.Designer.Extensions;
using EFSM.Designer.Interfaces;
using EFSM.Designer.ViewModel.TransitionEditor;
using EFSM.Domain;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.CommandWpf;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;

namespace EFSM.Designer.ViewModel
{
    public class TransitionViewModel : ViewModelBase, IMoveable, IDeleteable, ISelectable, IPropertyGridSource, INamedItem
    {
        private readonly StateMachineViewModel _parent;
        private StateViewModel _source;
        private StateViewModel _target;

        private Point _perpindicularLine;
        private Point _offset;
        private Point _center;
        private PointCollection _points;
        private const double DefaultPullLength = 40.0;
        private Point _arrowLocation;
        private Geometry _lineGeometry;
        private double _arrowAngle;
        private bool _isSelected;
        private StateMachineConditionViewModel _condition;
        private readonly Lazy<TransitionPropertyGridSource> _propertyGridSource;
        private List<Guid> _actions;
        private IMessageBoxService _messageBoxService;

        public StateMachineConditionViewModel Condition
        {
            get { return _condition; }
            set
            {
                _condition = value;
                RaisePropertyChanged();
            }
        }

        private Point _startLocation;
        private StateMachineTransition _model;
        private readonly IViewService _viewService;

        public TransitionViewModel(StateMachineViewModel parent, StateMachineTransition model, IViewService viewService, IMessageBoxService messageBoxService)
        {
            _parent = parent ?? throw new ArgumentNullException(nameof(parent));
            _model = model ?? throw new ArgumentNullException(nameof(model));
            _viewService = viewService ?? throw new ArgumentNullException(nameof(viewService));
            _messageBoxService = messageBoxService ?? throw new ArgumentNullException(nameof(messageBoxService));

            _propertyGridSource = new Lazy<TransitionPropertyGridSource>(() => new TransitionPropertyGridSource(this));

            CommandInitialize();
            ModelInitialize();
        }

        private void CommandInitialize()
        {
            DeleteCommand = new RelayCommand(Delete, CanDelete);
            EditCommand = new RelayCommand(Edit, CanEdit);
        }

        private void ModelInitialize()
        {
            PullLength = _model.PullLength == default(double) ? DefaultPullLength : _model.PullLength;
            Actions = _model.TransitionActions == null ? new List<Guid>() : _model.TransitionActions.ToList();

            if (_model.Condition != null)
            {
                _condition = new StateMachineConditionViewModel(_model.Condition);
            }
        }

        public ICommand DeleteCommand { get; private set; }
        public ICommand EditCommand { get; private set; }

        private bool CanEdit() => !Parent.IsReadOnly;

        public StateMachineTransition GetModel()
        {
            if (Condition != null)
            {
                _model.Condition = Condition.GetModel();
            }

            _model.TransitionActions = Actions.ToArray();
            var model = _model.Clone();
            return model;
        }

        public List<Guid> Actions
        {
            get { return _actions; }
            set
            {
                _actions = value;
                RaisePropertyChanged();
            }
        }

        private void Edit()
        {
            try
            {
                var viewModel = new TransitionEditorViewModel(_model, _parent, Reload, _messageBoxService);

                _viewService.ShowDialog(viewModel);
            }
            catch (Exception ex)
            {
                _messageBoxService.Show(ex);
            }
        }

        private void Reload(StateMachineTransition model)
        {
            Parent.DirtyService.MarkDirty();
            _model = model;
            ModelInitialize();
            RaisePropertyChanged(null);
            _parent.SaveUndoState();
        }

        private bool CanDelete() => !Parent.IsReadOnly;

        public StateViewModel Source
        {
            get { return _source; }
            set
            {
                if (_source != value)
                {
                    if (_source != null)
                    {
                        _source.PropertyChanged -= StatePropertyChanged;
                        _source.RemoveTransition(this);
                    }

                    if (value != null)
                    {
                        value.PropertyChanged += StatePropertyChanged;
                        value.AddTransition(this);
                    }

                    _source = value;

                    RaisePropertyChanged();

                    RefreshGeometry();
                }
            }
        }

        public void DeleteInput(StateMachineInputViewModel input)
        {
            if (Condition != null && Condition.SourceInputId.HasValue && Condition.SourceInputId.Value == input.Id)
            {
                Condition = null;
            }

            if (Condition != null && Condition.Conditions != null)
            {
                Condition.Conditions.DeleteInputFromConditions(input);
            }
        }


        public StateViewModel Target
        {
            get { return _target; }
            set
            {
                if (_target != value)
                {
                    if (_target != null)
                    {
                        _target.PropertyChanged -= StatePropertyChanged;
                        _target.RemoveTransition(this);
                    }

                    if (value != null)
                    {
                        value.PropertyChanged += StatePropertyChanged;
                        value.AddTransition(this);
                    }

                    _target = value;
                    RaisePropertyChanged();

                    RefreshGeometry();
                }
            }
        }

        private void StatePropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(ILocation.Location))
            {
                RefreshGeometry();
            }
        }

        private void RefreshGeometry()
        {
            var source = Source;
            var target = Target;

            if (source == null || target == null)
                return;

            //Determine the centerpoint of the line
            Point center = GraphicsUtil.FindCenter(source.Location, target.Location);

            //Get the perpindicular line
            Point perpindicularPoint = GraphicsUtil.GetPerpindicularPoint(center, target.Location, PullLength);

            Center = center;
            PerpindicularPoint = perpindicularPoint;

            //Create the points for the bezier line
            Points = new PointCollection(new Point[]
                {
                    source.Location,
                    GraphicsUtil.GetPerpindicularPoint(center, target.Location, PullLength * 2.25),
                    target.Location
                });

            var bezierSegment = new BezierSegment(source.Location,
                    GraphicsUtil.GetPerpindicularPoint(center, target.Location, PullLength * 2.25),
                    target.Location, true);

            var pathFigure = new PathFigure(source.Location, new PathSegment[]
            {
                bezierSegment
            }, false);

            LineGeometry = new PathGeometry()
            {
                Figures =
                {
                    pathFigure
                }
            };

            var targetGeometry = target.GetStateGeometry();

            var intersection = GraphicsUtil.GetIntersectionPoint(LineGeometry, targetGeometry);

            if (intersection.HasValue)
            {
                ArrowLocation = intersection.Value.Intersection;

                ArrowAngle = GraphicsUtil.GetAngle(new Point(0, 0), intersection.Value.Tangent) - 90;
            }
        }

        public PointCollection Points
        {
            get { return _points; }
            private set
            {
                _points = value;
                RaisePropertyChanged();
            }
        }

        public Point Center
        {
            get { return _center; }
            private set
            {
                _center = value;
                RaisePropertyChanged();
            }
        }

        public Point Offset
        {
            get { return _offset; }
            private set
            {
                _offset = value;
                RaisePropertyChanged();
            }
        }

        public Point PerpindicularPoint
        {
            get { return _perpindicularLine; }
            private set
            {
                _perpindicularLine = value;
                RaisePropertyChanged();
            }
        }

        public string Name
        {
            get { return _model.Name; }
            set
            {
                _model.Name = value;
                RaisePropertyChanged();
                _parent.SaveUndoState();
            }
        }

        public TransitionToolTipViewModel ToolTip => this.CreateToolTip();

        public double PullLength
        {
            get { return _model.PullLength; }
            set
            {
                _model.PullLength = value;
                RaisePropertyChanged();
                RefreshGeometry();
                //Parent.DirtyService.MarkDirty();
            }
        }

        public Point ArrowLocation
        {
            get { return _arrowLocation; }
            private set
            {
                _arrowLocation = value;
                RaisePropertyChanged();
            }
        }

        public double ArrowAngle
        {
            get { return _arrowAngle; }
            private set
            {
                _arrowAngle = value;
                RaisePropertyChanged();
            }
        }

        public Geometry LineGeometry
        {
            get { return _lineGeometry; }
            private set
            {
                _lineGeometry = value;
                RaisePropertyChanged();
            }
        }

        //Use this to find the closest point on a line
        //http://stackoverflow.com/questions/4438244/how-to-calculate-shortest-2d-distance-between-a-point-and-a-line-segment-in-all

        #region IMoveable

        public Point Location
        {
            get { return PerpindicularPoint; }
            set
            {
                var source = Source;
                var target = Target;

                if (source == null || target == null)
                    return;

                //Get a perpindicular point along this line
                Point perpindicularPoint = GraphicsUtil.GetPerpindicularPoint(Center, target.Location, 100);

                //Find the point on that line that is closest to the location
                var closestPoint = GraphicsUtil.FindClosestPointOnSegment(value, perpindicularPoint, Center, false);

                //Create the vector
                var vector = closestPoint - Center;

                //Determine the angle
                var angle = Vector.AngleBetween(vector, target.Location - source.Location);

                //Determine if the angle puts us on the "other side" of the straight line between the centerpoints of the two states.
                var length = vector.Length;

                //We need to make the length negative
                if (angle < 0)
                {
                    length *= -1;
                }

                //Finally - we're good.
                PullLength = length;
            }
        }

        public void SoftMove(Point point)
        {
            Location = point;
        }

        #endregion

        public void Delete()
        {
            try
            {
                Parent.DeleteOfSelected();
            }
            catch (Exception ex)
            {
                _messageBoxService.Show(ex);
            }
        }

        public void DeleteOutputId(Guid outputId)
        {
            if (Actions.Contains(outputId))
            {
                Actions.Remove(outputId);
            }
        }

        public bool IsSelected
        {
            get { return _isSelected; }
            set
            {
                _isSelected = value;
                RaisePropertyChanged();
            }
        }

        public StateMachineViewModel Parent => _parent;

        public object PropertyGridData => _propertyGridSource.Value;

        void IMoveable.StartMove()
        {
            _startLocation = Location;
        }

        void IMoveable.ContinueMove(Vector vector)
        {
            SoftMove(_startLocation + vector);
        }

        void IMoveable.CancelMove()
        {
            SoftMove(_startLocation);
        }

        void IMoveable.CompleteMove(Vector vector)
        {
            Location = _startLocation + vector;
            Parent.SaveUndoState();
        }
    }
}
