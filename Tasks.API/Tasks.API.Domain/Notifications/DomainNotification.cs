using FluentValidation.Results;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Tasks.API.Domain.Notifications
{
    public class DomainNotification
    {
        private readonly List<string> _erros;

        public DomainNotification()
        {
            _erros = new List<string>();
        }

        public bool IsValid => !_erros.Any();

        public IReadOnlyCollection<string> Errors => _erros;

        public void AddNotification(string notification)
        {
            ValidateNotification(notification);
            _erros.Add(notification);
        }

        public void AddNotification(IEnumerable<string> notifications)
        {
            ValidateNotification(notifications);
            _erros.AddRange(notifications);
        }

        public void AddNotification(ValidationResult validation)
        {
            ValidateNotification(validation);
            _erros.AddRange(validation.Errors.Select(x => x.ErrorMessage));
        }

        public void ClearErrors()
        {
            _erros.Clear();
        }

        private void ValidateNotification(object notification)
        {
            if (notification is null)
                throw new ArgumentNullException(nameof(notification));
        }
    }
}
