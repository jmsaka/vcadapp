using System;
using System.Globalization;
using VCadApp.Repositories;
using VCadApp.Views;

namespace VCadApp.Presenters
{
    public class PersonPresenter
    {
        private readonly IView _view;
        private readonly IPersonRepository _repository;

        public PersonPresenter(IView view, IPersonRepository repository)
        {
            _view = view ?? throw new ArgumentNullException(nameof(view));
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
        }

        public void LoadPeopleList()
        {
            try
            {
                var people = _repository.GetAll();
                _view.SetPeopleList(people);
            }
            catch (Exception ex)
            {
                _view.SetMessage($"Erro ao carregar lista. Tente novamente. Detalhes: {ex.Message}", "#b71c1c");
            }
        }

        public void SaveNewPerson()
        {
            CultureInfo culture = CultureInfo.GetCultureInfo("pt-BR");

            if (!DateTime.TryParse(_view.BirthDateText, culture, DateTimeStyles.None, out DateTime birthDate))
            {
                _view.SetMessage("Erro de validação: Data de Nascimento inválida.", "#b71c1c");
                return;
            }

            var person = new Models.Person
            {
                Name = _view.Name,
                BirthDate = birthDate,
                Email = _view.Email,
                MaritalStatus = _view.MaritalStatus
            };

            try
            {
                int newId = _repository.Insert(person);

                _view.SetMessage($"Registro salvo com sucesso! (ID: {newId})", "#1b5e20");
                _view.ClearForm();

                LoadPeopleList();
                _view.RebindData();
            }
            catch (Exception ex)
            {
                _view.SetMessage("Erro ao salvar: Ocorreu um problema no sistema. " + ex.Message, "#b71c1c");
            }
        }
    }
}