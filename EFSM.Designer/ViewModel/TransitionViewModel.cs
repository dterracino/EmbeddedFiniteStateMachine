using EFSM.Designer.Common;
using EFSM.Designer.Extensions;
using EFSM.Designer.Interfaces;
using EFSM.Domain;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.CommandWpf;
using System;
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
        private double _pullLength = 40.0;
        private Point _arrowLocation;
        private Geometry _lineGeometry;
        private double _arrowAngle;
        private bool _isSelected;

        private readonly Lazy<TransitionPropertyGridSource> _propertyGridSource;
        //private IList<StateMachineActionMetadata> _actions;
        private StateMachineCondition _condition;

        private Point _startLocation;
        private IUndoProvider _undoProvider;
        private StateMachineTransition _model;

        public TransitionViewModel(StateMachineViewModel parent, StateMachineTransition model, IUndoProvider undoProvider)
        {
            _parent = parent ?? throw new ArgumentNullException(nameof(parent));
            _undoProvider = undoProvider ?? throw new ArgumentNullException(nameof(undoProvider));
            _model = model ?? throw new ArgumentNullException(nameof(model));

            _propertyGridSource = new Lazy<TransitionPropertyGridSource>(() => new TransitionPropertyGridSource(this));

            DeleteCommand = new RelayCommand(Delete, CanDelete);
            EditCommand = new RelayCommand(Edit, CanEdit);
        }

        public ICommand DeleteCommand { get; private set; }
        public ICommand EditCommand { get; private set; }

        private bool CanEdit()
        {
            return !Parent.IsReadOnly;
        }

        public StateMachineTransition GetModel()
        {
            return _model.Clone();
        }

        private void Edit()
        {
            //var viewModel = new TransitionEditorViewModel(this,
            //    Parent.Outputs.Items.ToArray(),
            //    Parent.Inputs.Items.ToArray());

            //var dialog = new TransitionEditorDialog()
            //{
            //    DataContext = viewModel
            //};

            //if (dialog.ShowDialog() == true)
            //{
            //    Parent.DirtyService.MarkDirty();
            //}
        }

        private bool CanDelete()
        {
            return !Parent.IsReadOnly;
        }

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
            var center = GraphicsUtil.FindCenter(source.Location, target.Location);

            //Get the perpindicular line
            var perpindicularPoint = GraphicsUtil.GetPerpindicularPoint(center, target.Location, PullLength);

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
                _undoProvider.SaveUndoState();
                //Parent.DirtyService.MarkDirty();
            }
        }

        public TransitionToolTipViewModel ToolTip
        {
            get { return this.CreateToolTip(); }
        }

        public double PullLength
        {
            get { return _pullLength; }
            set
            {
                _pullLength = value;
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

        //public IList<StateMachineActionMetadata> Actions
        //{
        //    get { return _actions; }
        //    set
        //    {
        //        _actions = value;
        //        RaisePropertyChanged();
        //        Parent.DirtyService.MarkDirty();
        //    }
        //}

        //public ConditionMetadata Condition
        //{
        //    get { return _condition; }
        //    set
        //    {
        //        _condition = value;
        //        RaisePropertyChanged();
        //        Parent.DirtyService.MarkDirty();
        //    }
        //}

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
                var perpindicularPoint = GraphicsUtil.GetPerpindicularPoint(Center, target.Location, 100);

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
            Source = null;
            Target = null;
            _parent.RemoveTransition(this);
            //Call undo provider
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

        public StateMachineViewModel Parent
        {
            get { return _parent; }
        }

        public object PropertyGridData
        {
            get { return _propertyGridSource.Value; }
        }

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
        }
    }
}
