using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using UnitLabrary.Customers.CustomerType;

namespace WebFormUnit.Form.CustomerForm
{
    public partial class FormCustomer : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if(!IsPostBack)
            {
                LoadCustomerType();
            }
        }
        private void LoadCustomerType()
        {
            ICustomerTypeComponent component = new CustomerTypeConcreteComponent();
            ICustomerTypeComponent decoratedComponent = new CustomerTypeConcreteDecorator(component);
            var load = decoratedComponent.CustomerTypeSelect("");
            if(load != null)
            {
                gvCustomerType.DataSource = load;
                gvCustomerType.DataBind();
            }
        }
    }
}