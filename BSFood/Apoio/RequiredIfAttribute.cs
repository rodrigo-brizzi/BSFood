﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations;

namespace BSFood.Apoio
{
    /// <summary>
    /// Performs validation on a member if the property with the specified name (and on the same instance as the member we are validating) equals one of
    /// the values provided in this constructor.
    /// </summary>
    public sealed class RequiredIfAttribute : ValidationAttribute
    {
        /// <summary>
        /// The name of the property which must equal one of 'Values' for validation to occur.
        /// </summary>
        public string PropertyName { get; private set; }

        /// <summary>
        /// Gets or sets the values which when 'PropertyName' matches one or more of them, validation will occur.
        /// </summary>
        public object[] Values { get; private set; }

        public RequiredIfAttribute(string propertyName, params object[] equalsValues)
        {
            this.PropertyName = propertyName;
            this.Values = equalsValues;
        }

        /// <summary>
        /// Performs the conditional validation.
        /// </summary>
        /// <param name="value">This is the actual value of the property who has the RequiredIf attribute on it (e.g. the property that we are validating).</param>
        /// <param name="validationContext">The validation context which contains the instance that owns the member which we are validating.</param>
        /// <returns>Returns ValidationResult.Success upon success, and a ValidationResult with the specified ErrorMessage upon failure.</returns>
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            bool isRequired = this.IsRequired(validationContext);

            if (isRequired && string.IsNullOrEmpty(Convert.ToString(value)))
                return new ValidationResult(this.ErrorMessage, new string[] { validationContext.MemberName });
            return ValidationResult.Success;
        }

        /// <summary>
        /// Determines whether or not validation should occur.
        /// </summary>
        /// <param name="validationContext">The validation context which contains the instance that owns the member which we are validating.</param>
        /// <returns>Returns true if validation should occur, false otherwise.</returns>
        private bool IsRequired(ValidationContext validationContext)
        {
            var property = validationContext.ObjectType.GetProperty(this.PropertyName);
            var currentValue = property.GetValue(validationContext.ObjectInstance, null);

            foreach (var val in this.Values)
                if (object.Equals(currentValue, val))
                    return true;
            return false;
        }
    }
}
