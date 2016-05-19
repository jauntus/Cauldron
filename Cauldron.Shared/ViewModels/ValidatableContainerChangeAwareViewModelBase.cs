﻿using System;
using System.Collections.Generic;

namespace Cauldron.ViewModels
{
    /// <summary>
    /// Represents the Base class of a ViewModel that can have registered child viewmodels
    /// </summary>
    public abstract class ValidatableContainerChangeAwareViewModelBase : ValidatableChangeAwareViewModelBase, IContainerViewModel
    {
        private ViewModelContainerHandler handler;

        /// <summary>
        /// Initializes a new instance of <see cref="ValidatableContainerChangeAwareViewModelBase"/>
        /// </summary>
        /// <param name="id">A unique identifier of the viewmodel</param>
        public ValidatableContainerChangeAwareViewModelBase(Guid id) : base(id)
        {
            this.handler = new ViewModelContainerHandler(this);
            this.handler.Changed += Handler_Changed;
        }

        /// <summary>
        /// Initializes a new instance of <see cref="ValidatableContainerChangeAwareViewModelBase"/>
        /// </summary>
        [Inject]
        public ValidatableContainerChangeAwareViewModelBase() : base()
        {
            this.handler = new ViewModelContainerHandler(this);
            this.handler.Changed += Handler_Changed;
        }

        /// <summary>
        /// Returns a registered Child ViewModel
        /// </summary>
        /// <typeparam name="T">The type of the viewModel</typeparam>
        /// <returns>The viewModel otherwise null</returns>
        public T GetRegistered<T>() where T : class, IViewModel => this.handler.GetRegistered<T>();

        /// <summary>
        /// Returns a registered child viewmodel
        /// </summary>
        /// <param name="id">The id of the viewmodel</param>
        /// <returns>The viewmodel otherwise null</returns>
        public IViewModel GetRegistered(Guid id) => this.handler.GetRegistered(id);

        /// <summary>
        /// Registers a child model to the current ViewModel
        /// </summary>
        /// <param name="childViewModel">The view model that requires registration</param>
        /// <returns>The id of the viewmodel</returns>
        /// <exception cref="ArgumentNullException">The parameter <paramref name="childViewModel"/> is null</exception>
        public Guid Register(IViewModel childViewModel) => this.handler.Register(childViewModel);

        /// <summary>
        /// Registers a collection of child models to the current view model
        /// </summary>
        /// <typeparam name="T">The type of the viewmodels</typeparam>
        /// <param name="childViewModels">The collection of the view models that required registration</param>
        /// <exception cref="ArgumentNullException">The parameter <paramref name="childViewModels"/> is null</exception>
        public void Register<T>(IEnumerable<T> childViewModels) where T : IViewModel => this.handler.Register(childViewModels);

        /// <summary>
        /// Unregisters a registered viewmodel. This will also dispose the viewmodel.
        /// </summary>
        /// <param name="childId">The id of the registered viewmodel</param>
        public void UnRegister(Guid childId) => this.handler.UnRegister(childId);

        /// <summary>
        /// Unregisters a collection of registered viewModels. This will also dispose the view models
        /// </summary>
        /// <typeparam name="T">The type of the viewmodels</typeparam>
        /// <param name="childViewModels">The collection of the view models that required unregistration</param>
        /// <exception cref="ArgumentNullException">The parameter <paramref name="childViewModels"/> is null</exception>
        public void UnRegister<T>(IEnumerable<T> childViewModels) where T : IViewModel => this.handler.UnRegister(childViewModels);

        /// <summary>
        /// Unregisters a registered viewmodel. This will also dispose the viewmodel.
        /// </summary>
        /// <param name="childViewModel">The viewmodel that requires unregistration</param>
        /// <exception cref="ArgumentNullException">The parameter <paramref name="childViewModel"/> is null</exception>
        public void UnRegister(IViewModel childViewModel) => this.handler.UnRegister(childViewModel);

        /// <summary>
        /// Occures after <see cref="IDisposable.Dispose"/> has been invoked
        /// </summary>
        /// <param name="disposeManaged">true if managed resources requires disposing</param>
        protected override void OnDispose(bool disposeManaged)
        {
            if (disposeManaged)
                this.handler.Dispose();
        }

        private void Handler_Changed(object sender, EventArgs e)
        {
            (sender as IChangeAwareViewModel).IsNotNull(x =>
            {
                this.IsChanged |= x.IsChanged;
            });
        }
    }
}