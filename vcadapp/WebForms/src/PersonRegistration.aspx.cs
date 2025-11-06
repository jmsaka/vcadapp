using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Collections.Generic;
using VCadApp.Models;
using VCadApp.Presenters;
using VCadApp.Repositories;
using VCadApp.Views;
using System.Globalization;

namespace VCadApp
{
    public partial class PersonRegistration : Page, IView
    {
        private readonly PersonPresenter _presenter;

        public PersonRegistration()
        {
            IPersonRepository repository = new SqlPersonRepository();
            _presenter = new PersonPresenter(this, repository);
        }

        public string Name => txtName.Text.Trim();
        public string BirthDateText => txtBirthDate.Text;
        public string Email => txtEmail.Text.Trim();
        public string MaritalStatus => ddlMarital.SelectedValue;

        public void SetPeopleList(List<Person> people)
        {
            gvPersons.DataSource = people;
        }

        public void ClearForm()
        {
            txtName.Text = string.Empty;
            txtBirthDate.Text = string.Empty;
            txtEmail.Text = string.Empty;
            ddlMarital.SelectedIndex = 0;
        }

        public void RebindData()
        {
            gvPersons.DataBind();
        }

        public void SetMessage(string message, string colorHex)
        {
            lblMessage.Text = message;
            lblMessage.Style["color"] = colorHex;
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                _presenter.LoadPeopleList();
                RebindData();
            }
        }

        protected void BtnSubmit_Click(object sender, EventArgs e)
        {
            if (!Page.IsValid) return;

            _presenter.SaveNewPerson();
        }

        protected void CvBirthDate_ServerValidate(object source, ServerValidateEventArgs args)
        {
            CultureInfo culture = CultureInfo.GetCultureInfo("pt-BR");

            if (!DateTime.TryParse(txtBirthDate.Text, culture, DateTimeStyles.None, out DateTime dt))
            {
                args.IsValid = false;
                return;
            }

            args.IsValid = dt <= DateTime.Today;
        }
    }
}