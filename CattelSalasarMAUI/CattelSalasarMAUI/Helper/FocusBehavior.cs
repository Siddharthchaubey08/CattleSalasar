using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CattelSalasarMAUI.Helper
{
    public class FocusBehavior : Behavior<DatePicker>
    {
        public static readonly BindableProperty IsFocusedProperty =
            BindableProperty.Create(nameof(IsFocused), typeof(bool), typeof(FocusBehavior), false, propertyChanged: OnIsFocusedChanged);

        public bool IsFocused
        {
            get => (bool)GetValue(IsFocusedProperty);
            set => SetValue(IsFocusedProperty, value);
        }

        private static void OnIsFocusedChanged(BindableObject bindable, object oldValue, object newValue)
        {
            if (bindable is FocusBehavior behavior && newValue is bool isFocused && isFocused && behavior.AssociatedObject != null)
            {
                behavior.AssociatedObject.Focus();
                behavior.IsFocused = false; // Reset the property to prevent re-triggering
            }
        }

        protected override void OnAttachedTo(DatePicker bindable)
        {
            base.OnAttachedTo(bindable);
            AssociatedObject = bindable;
        }

        protected override void OnDetachingFrom(DatePicker bindable)
        {
            base.OnDetachingFrom(bindable);
            AssociatedObject = null;
        }

        private DatePicker AssociatedObject { get; set; }
    }
}
