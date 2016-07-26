using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;

namespace Explorer_wpf
{
    public class DrivesViewModel : INotifyPropertyChanged
    {
        private ObservableCollection<string> _drives;
        private ObservableCollection<string> _directories;
        private ObservableCollection<string> _files;
        private string _selectedDrive;
        private string _selectedDirectory;
        
        public string SelectedDrive
        {
            get
            {
                return _selectedDrive; 
            }
            set
            {
                _selectedDrive = value;
                
                if (PropertyChanged != null)
                {
                    PropertyChanged(this, new PropertyChangedEventArgs("SelectedDrive"));
                    
                }
                CreateDirectityList();
            }
        }

        public string SelectedDirectory
        {
            get
            {
                return _selectedDirectory;
            }
            set
            {
                _selectedDirectory = value;
                
                if (PropertyChanged != null)
                {
                    PropertyChanged(this, new PropertyChangedEventArgs("SelectedDirectory"));

                }

                CreateSubDirectityList();

            }
        }

        public DrivesViewModel()
        {
            _drives = new ObservableCollection<string>();
            
            foreach (var d in DriveInfo.GetDrives())
            {
                _drives.Add(d.Name);
            }
            _selectedDrive = _drives.First();

            _directories = new ObservableCollection<string>();

            CreateDirectityList();

            _files = new ObservableCollection<string>();

            

        }

        private void CreateSubDirectityList()
        {
            if (_selectedDirectory != null)
            {
                FileAttributes attr = File.GetAttributes(_selectedDirectory);
                if ((attr & FileAttributes.Directory) == FileAttributes.Directory)
                {
                    _directories.Clear();
                    foreach (var d in Directory.GetDirectories(_selectedDirectory))
                    {
                        _directories.Add(d);
                    }
                    foreach (var f in Directory.GetFiles(_selectedDirectory))
                    {
                        _directories.Add(f);
                    }
                }
                else _selectedDirectory = null;
            }
        }

        private void CreateDirectityList()
        {
            if (_selectedDrive != null)
            {
                
                _directories.Clear();
                var d = new DriveInfo(_selectedDrive);
                if (d.IsReady)
                {
                    foreach (var dir in Directory.GetDirectories(_selectedDrive))
                    {
                        _directories.Add(dir);
                    }
                    foreach (var f in Directory.GetFiles(_selectedDrive))
                    {
                        _directories.Add(f);
                    }
                }
               
            }
        }


        public ObservableCollection<string> Drives
        {
            get
            {
                return _drives;
            }
            private set
            {
                _drives = value;
                if (PropertyChanged != null)
                    PropertyChanged(this, new PropertyChangedEventArgs("Drives"));
            }
        }

        public ObservableCollection<string> Directories
        {
            get
            {
                return _directories;
            }
            set
            {
                _directories = value;
                if (PropertyChanged != null)
                    PropertyChanged(this, new PropertyChangedEventArgs("Directories"));
            }
        }


        public ObservableCollection<string> Files { get; set; }


        public event PropertyChangedEventHandler PropertyChanged;
        
    }

}
