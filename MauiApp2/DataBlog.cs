using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.Xml.Linq;

namespace MauiApp2
{
    public class DataBlog: INotifyPropertyChanged
    {
        // Private
        private int _Likes;
        private int _Dislikes;
        // Public
        public int Id { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public DateTime CreatedAt { get; set; }
        public int Likes 
        {
            get => _Likes;
            set 
            {
                if (_Likes != value)
                {
                    _Likes = value;
                    OnPropertyChanged(nameof(Likes));
                }
            }
        }
        public int Dislikes 
        {
            get => _Dislikes;
            set
            {
                if (_Dislikes != value)
                {
                    _Dislikes = value;
                    OnPropertyChanged(nameof(Dislikes));
                }
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }

}
