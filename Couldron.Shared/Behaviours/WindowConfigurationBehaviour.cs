﻿using System;
using System.Reflection;
using System.Windows;
using System.Windows.Media;

namespace Couldron.Behaviours
{
    /// <summary>
    /// Provides a behaviour to configure the window that will contain the view
    /// </summary>
    [BehaviourUsage(false)]
    public sealed class WindowConfigurationBehaviour : FrameworkElement, IBehaviour<FrameworkElement>
    {
        #region Dependency Property IsDialog

        /// <summary>
        /// Identifies the <see cref="IsDialog" /> dependency property
        /// </summary>
        public static readonly DependencyProperty IsDialogProperty = DependencyProperty.Register(nameof(IsDialog), typeof(bool), typeof(WindowConfigurationBehaviour), new PropertyMetadata(false));

        /// <summary>
        /// Gets or sets the <see cref="IsDialog" /> Property
        /// </summary>
        public bool IsDialog
        {
            get { return (bool)this.GetValue(IsDialogProperty); }
            set { this.SetValue(IsDialogProperty, value); }
        }

        #endregion Dependency Property IsDialog

        #region Dependency Property Icon

        /// <summary>
        /// Identifies the <see cref="Icon" /> dependency property
        /// </summary>
        public static readonly DependencyProperty IconProperty = DependencyProperty.Register(nameof(Icon), typeof(ImageSource), typeof(WindowConfigurationBehaviour), new PropertyMetadata(null, WindowConfigurationBehaviour.OnIconChanged));

        /// <summary>
        /// Occures if the <see cref="Icon"/> property has changed
        /// </summary>
        public event EventHandler IconChanged;

        /// <summary>
        /// Gets or sets the <see cref="Icon" /> Property
        /// </summary>
        public ImageSource Icon
        {
            get { return (ImageSource)this.GetValue(IconProperty); }
            set { this.SetValue(IconProperty, value); }
        }

        private static void OnIconChanged(DependencyObject d, DependencyPropertyChangedEventArgs args)
        {
            var dependencyObject = d as WindowConfigurationBehaviour;

            if (dependencyObject == null)
                return;

            if (dependencyObject.IconChanged != null)
                dependencyObject.IconChanged(dependencyObject, EventArgs.Empty);
        }

        #endregion Dependency Property Icon

        #region Dependency Property ResizeMode

        /// <summary>
        /// Identifies the <see cref="ResizeMode" /> dependency property
        /// </summary>
        public static readonly DependencyProperty ResizeModeProperty = DependencyProperty.Register(nameof(ResizeMode), typeof(ResizeMode), typeof(WindowConfigurationBehaviour), new PropertyMetadata(ResizeMode.CanResizeWithGrip));

        /// <summary>
        /// Gets or sets the <see cref="ResizeMode" /> Property
        /// </summary>
        public ResizeMode ResizeMode
        {
            get { return (ResizeMode)this.GetValue(ResizeModeProperty); }
            set { this.SetValue(ResizeModeProperty, value); }
        }

        #endregion Dependency Property ResizeMode

        #region Dependency Property ShowInTaskbar

        /// <summary>
        /// Identifies the <see cref="ShowInTaskbar" /> dependency property
        /// </summary>
        public static readonly DependencyProperty ShowInTaskbarProperty = DependencyProperty.Register(nameof(ShowInTaskbar), typeof(bool), typeof(WindowConfigurationBehaviour), new PropertyMetadata(true));

        /// <summary>
        /// Gets or sets the <see cref="ShowInTaskbar" /> Property
        /// </summary>
        public bool ShowInTaskbar
        {
            get { return (bool)this.GetValue(ShowInTaskbarProperty); }
            set { this.SetValue(ShowInTaskbarProperty, value); }
        }

        #endregion Dependency Property ShowInTaskbar

        #region Dependency Property Title

        /// <summary>
        /// Identifies the <see cref="Title" /> dependency property
        /// </summary>
        public static readonly DependencyProperty TitleProperty = DependencyProperty.Register(nameof(Title), typeof(string), typeof(WindowConfigurationBehaviour), new PropertyMetadata("", WindowConfigurationBehaviour.OnTitleChanged));

        /// <summary>
        /// Occures if the <see cref="Title"/> property has changed
        /// </summary>
        public event EventHandler TitleChanged;

        /// <summary>
        /// Gets or sets the <see cref="Title" /> Property
        /// </summary>
        public string Title
        {
            get { return (string)this.GetValue(TitleProperty); }
            set { this.SetValue(TitleProperty, value); }
        }

        private static void OnTitleChanged(DependencyObject d, DependencyPropertyChangedEventArgs args)
        {
            var dependencyObject = d as WindowConfigurationBehaviour;

            if (dependencyObject == null || args.NewValue == null)
                return;

            if (dependencyObject.TitleChanged != null)
                dependencyObject.TitleChanged(dependencyObject, EventArgs.Empty);
        }

        #endregion Dependency Property Title

        #region Dependency Property Topmost

        /// <summary>
        /// Identifies the <see cref="Topmost" /> dependency property
        /// </summary>
        public static readonly DependencyProperty TopmostProperty = DependencyProperty.Register(nameof(Topmost), typeof(bool), typeof(WindowConfigurationBehaviour), new PropertyMetadata(false));

        /// <summary>
        /// Gets or sets the <see cref="Topmost" /> Property
        /// </summary>
        public bool Topmost
        {
            get { return (bool)this.GetValue(TopmostProperty); }
            set { this.SetValue(TopmostProperty, value); }
        }

        #endregion Dependency Property Topmost

        #region Dependency Property WindowStartupLocation

        /// <summary>
        /// Identifies the <see cref="WindowStartupLocation" /> dependency property
        /// </summary>
        public static readonly DependencyProperty WindowStartupLocationProperty = DependencyProperty.Register(nameof(WindowStartupLocation), typeof(WindowStartupLocation), typeof(WindowConfigurationBehaviour), new PropertyMetadata(WindowStartupLocation.CenterOwner));

        /// <summary>
        /// Gets or sets the <see cref="WindowStartupLocation" /> Property
        /// </summary>
        public WindowStartupLocation WindowStartupLocation
        {
            get { return (WindowStartupLocation)this.GetValue(WindowStartupLocationProperty); }
            set { this.SetValue(WindowStartupLocationProperty, value); }
        }

        #endregion Dependency Property WindowStartupLocation

        #region Dependency Property WindowState

        /// <summary>
        /// Identifies the <see cref="WindowState" /> dependency property
        /// </summary>
        public static readonly DependencyProperty WindowStateProperty = DependencyProperty.Register(nameof(WindowState), typeof(WindowState), typeof(WindowConfigurationBehaviour), new PropertyMetadata(WindowState.Normal));

        /// <summary>
        /// Gets or sets the <see cref="WindowState" /> Property
        /// </summary>
        public WindowState WindowState
        {
            get { return (WindowState)this.GetValue(WindowStateProperty); }
            set { this.SetValue(WindowStateProperty, value); }
        }

        #endregion Dependency Property WindowState

        #region Dependency Property WindowStyle

        /// <summary>
        /// Identifies the <see cref="WindowStyle" /> dependency property
        /// </summary>
        public static readonly DependencyProperty WindowStyleProperty = DependencyProperty.Register(nameof(WindowStyle), typeof(WindowStyle), typeof(WindowConfigurationBehaviour), new PropertyMetadata(WindowStyle.SingleBorderWindow));

        /// <summary>
        /// Gets or sets the <see cref="WindowStyle" /> Property
        /// </summary>
        public WindowStyle WindowStyle
        {
            get { return (WindowStyle)this.GetValue(WindowStyleProperty); }
            set { this.SetValue(WindowStyleProperty, value); }
        }

        #endregion Dependency Property WindowStyle

        #region Behaviour implementation

        private FrameworkElement _associatedObject;

        /// <summary>
        /// Gets the <see cref="DependencyObject"/> to which the behavior is attached.
        /// </summary>
        public FrameworkElement AssociatedObject
        {
            get { return this._associatedObject; }
            set
            {
                if (this._associatedObject == value)
                    return;

                this._associatedObject = value;

                if (this._associatedObject == null)
                    return;

                this.SetBinding(DataContextProperty, this._associatedObject, nameof(FrameworkElement.DataContext));
            }
        }

        /// <summary>
        /// Gets a value that indicates the behaviour was assigned from a template
        /// </summary>
        public bool IsAssignedFromTemplate { get; private set; }

        /// <summary>
        /// Creates a shallow copy of the instance
        /// </summary>
        /// <returns>A copy of the behaviour</returns>
        IBehaviour IBehaviour.Copy()
        {
            var type = this.GetType();
            var behaviour = Activator.CreateInstance(type) as WindowConfigurationBehaviour;

            var props = type.GetProperties().ToArray<PropertyInfo>();

            for (int i = 0; i < props.Length; i++)
            {
                var prop = props[i];

                try
                {
                    // exclude ResourceDictionaries and Styles
                    if (prop.CanWrite && prop.CanRead && prop.PropertyType != typeof(ResourceDictionary) && prop.PropertyType != typeof(Style))
                        prop.SetValue(behaviour, prop.GetValue(this));
                }
                catch
                {
                    // Happens sometimes, but it's not important if something bad happens
                }
            }

            behaviour.IsAssignedFromTemplate = true;
            return behaviour;
        }

        /// <summary>
        /// Sets the behaviour's associated object
        /// </summary>
        /// <param name="obj">The associated object</param>
        void IBehaviour.SetAssociatedObject(object obj)
        {
            if (obj == null)
                return;

            this.AssociatedObject = obj as FrameworkElement;

            if (this._associatedObject == null)
                throw new Exception(string.Format("The Type of AssociatedObject \"{0}\" does not match with T \"{1}\"", obj.GetType(), typeof(FrameworkElement)));
        }

        #region IDisposable

        /// <summary>
        /// Occures if the object has been disposed
        /// </summary>
        public event EventHandler Disposed;

        /// <summary>
        /// Gets a value indicating if the object has been disposed or not
        /// </summary>
        public bool IsDisposed { get { return false; } }

        /// <summary>
        /// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
        /// </summary>
        public void Dispose()
        {
            // Implementation not required
        }

        #endregion IDisposable

        #endregion Behaviour implementation

        /// <summary>
        /// Initializes a new instance of <see cref="WindowConfigurationBehaviour"/>
        /// </summary>
        public WindowConfigurationBehaviour()
        {
            this.MinHeight = 120;
            this.MinWidth = 300;
        }
    }
}