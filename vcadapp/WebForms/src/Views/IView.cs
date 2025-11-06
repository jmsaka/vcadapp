using System.Collections.Generic;
using VCadApp.Models;

namespace VCadApp.Views
{
    public interface IView
    {
        string Name { get; }
        string BirthDateText { get; }
        string Email { get; }
        string MaritalStatus { get; }

        void SetPeopleList(List<Person> people);

        void ClearForm();
        void RebindData();
        void SetMessage(string message, string colorHex);
    }
}