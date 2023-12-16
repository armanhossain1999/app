using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExamLastEvidence.ViewModels
{
    public class ProductViewModel : INotifyPropertyChanged
    {
        public int _id;
        public int Id { get=>_id; set { _id = value; OnPropertyChanged(nameof(Id)); } }
        string _name=string.Empty;
        public string Name { get => _name; set { _name = value; OnPropertyChanged(nameof(Name)); } }
        string _description=string.Empty;
        public string Description { get => _description; set { _description = value; OnPropertyChanged(nameof(Description)); } }
        decimal _price;
        public decimal Price { get => _price; set { _price = value; OnPropertyChanged(nameof(Price)); } }
        string _picture=string.Empty;
        public string Picture { get => _picture; set { _picture = value; OnPropertyChanged(nameof(Picture)); } }
        string _picUrl =string.Empty;
        public string RemotePictureUrl { get=>_picUrl;
            set 
            { 
                _picUrl = value;
                OnPropertyChanged(nameof(RemotePictureUrl));
            }
        } 
        
        public event PropertyChangedEventHandler? PropertyChanged;
        private void OnPropertyChanged(string propertyName)
        {
            this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
