using StudentWpfApp.Commands;
using StudentWpfApp.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace StudentWpfApp.ViewModels
{
    internal class StudentViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler? PropertyChanged;
        private void OnPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }
        StudentService ObjStudentService;
        public StudentViewModel()
        {
            ObjStudentService = new StudentService();
            LoadData();
            CurrentStudents = new StudentDto();
            saveCommand = new RelayCommand(Save);
            updateCommand = new RelayCommand(Update);
           
        }
        #region DisplayOperation
        private ObservableCollection<StudentDto> StudentList;
        public ObservableCollection<StudentDto> StudentLists
        {
            get { return StudentList; }
            set { StudentList = value; OnPropertyChanged("StudentLists"); }
        }
        private void LoadData()
        {
            StudentLists = new ObservableCollection<StudentDto>(ObjStudentService.GetAll());
        }
        #endregion
        #region SaveOperation
        private StudentDto CurrentStudent;
        public StudentDto CurrentStudents
        {
            get { return CurrentStudent; }
            set { CurrentStudent = value; OnPropertyChanged("CurrentStudents"); }
        }

        private RelayCommand saveCommand;
        public RelayCommand SaveCommand
        {
            get { return saveCommand; }
        }
        private string message;
        public string Message
        {
            get { return message; }
            set { message = value; OnPropertyChanged("Message"); }
        }
        public void Save()
        {
            try
            {
                var IsSaved = ObjStudentService.Add(CurrentStudents);
                LoadData();
                if (IsSaved)
                    Message = "Employee saved";
                else
                    Message = "Save operation failed";
            }
            catch (Exception ex)
            {
               // Message = ex.Message;
            }
        }
        #endregion
        #region UpdateOperation
        private RelayCommand updateCommand;
        public RelayCommand UpdateCommand
        {
            get { return updateCommand; }
        }

        public void Update()
        {
            try
            {
                var IsUpdated = ObjStudentService.Update(CurrentStudents);
                if (IsUpdated)
                {
                    Message = "Employee updated";
                    LoadData();
                }
                else
                {
                    Message = "Update operation failed";
                }
            }
            catch (Exception ex)
            {
                Message = ex.Message;
            }
        }
        #endregion
    }
}
