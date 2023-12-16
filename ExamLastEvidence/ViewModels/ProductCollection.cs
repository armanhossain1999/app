using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExamLastEvidence.ViewModels
{
    public class ProductCollection : ObservableCollection<ProductViewModel>
    {
        public void ClearList()
        {
            this.Clear();
        }
    }
}
