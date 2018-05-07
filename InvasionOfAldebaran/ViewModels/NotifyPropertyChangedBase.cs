using Caliburn.Micro;
using System;
using System.ComponentModel;
using System.Linq.Expressions;
using System.Runtime.CompilerServices;

namespace InvasionOfAldebaran.ViewModels
{
    public class NotifyPropertyChangedBase : Screen, INotifyPropertyChanged
    {
        #region Protected

        /// <summary>Notifies a property changed.</summary>
        /// <param name="property">The property.</param>
        protected void NotifyPropertyChanged(string property)
        {
            if (this.PropertyChanged != null)
            {
                this.PropertyChanged(this, new PropertyChangedEventArgs(property));
            }
        }

        /// <summary>Raises this object's <see cref="PropertyChanged" /> event.</summary>
        /// <param name="propertyName">Name of the property.</param>
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            this.PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }

        /// <summary>Raises this object's <see cref="PropertyChanged" /> event.</summary>
        /// <typeparam name="T">The property type.</typeparam>
        /// <param name="propertySelector">The property selector.</param>
        protected void OnPropertyChanged<T>(Expression<Func<T>> propertySelector)
        {
            if (propertySelector == null)
            {
                throw new ArgumentNullException("propertySelector");
            }
            var propertyName = ((MemberExpression)propertySelector.Body).Member.Name;
            // ReSharper disable once ExplicitCallerInfoArgument
            this.OnPropertyChanged(propertyName);
        }

        #endregion Protected

        #region INotifyPropertyChanged Members

        // no ugly null check necessary, we initialized an empty delegate!
        /// <summary>Occurs when a property value changes.</summary>
        public event PropertyChangedEventHandler PropertyChanged = delegate { };

        #endregion INotifyPropertyChanged Members
    }
}