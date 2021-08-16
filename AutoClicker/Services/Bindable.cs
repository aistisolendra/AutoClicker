using System.Windows.Forms;

namespace AutoClicker.Services
{
    public class Bindable
    {
        public Binding CreateTextBind(object dataSource, string dataMember)
        {
            return new("Text", dataSource, dataMember, false, DataSourceUpdateMode.OnPropertyChanged);
        }

        public Binding CreateSelectedBind(object dataSource, string dataMember)
        {
            return new("SelectedValue", dataSource, dataMember, true, DataSourceUpdateMode.OnPropertyChanged);
        }

        public Binding CreateCheckedBind(object dataSource, string dataMember)
        {
            return new("Checked", dataSource, dataMember, true, DataSourceUpdateMode.OnPropertyChanged);
        }

        public Binding CreateEnabledBindFromChecked(object dataSource)
        {
            return new("Enabled", dataSource, "Checked", true, DataSourceUpdateMode.OnPropertyChanged);
        }
    }
}