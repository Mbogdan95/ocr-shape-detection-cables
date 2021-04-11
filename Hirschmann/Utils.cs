using System.Linq;
using System.Windows.Forms;

namespace Hirschmann
{
    public class Utils
    {
        public static bool CheckIfFormIsOpen(string formName)
        {
            bool formOpen = Application.OpenForms.Cast<Form>().Any(x => x.Name == formName);

            return formOpen;
        }

        public static Form GetFormReference(string formName)
        {
            Form form = Application.OpenForms.Cast<Form>().Where(x => x.Name == formName).FirstOrDefault();

            return form;
        }
    }
}
