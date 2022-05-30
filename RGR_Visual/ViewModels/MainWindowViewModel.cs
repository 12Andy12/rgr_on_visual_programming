using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Sqlite;
using RGR_Visual.Models;
using ReactiveUI;

namespace RGR_Visual.ViewModels
{
    public class MainWindowViewModel : ViewModelBase
    {
        public RGRContext Db { get; }
        public MainWindowViewModel()
        {
            Db = new RGRContext();
            Db.Horses.Load<Horse>();
            Horses = Db.Horses.Local.ToObservableCollection();
            Db.Trainers.Load<Trainer>();
            Trainers = Db.Trainers.Local.ToObservableCollection();
            Db.Jockeys.Load<Jockey>();
            Jockeys = Db.Jockeys.Local.ToObservableCollection();
            Db.Races.Load<Race>();
            Races = Db.Races.Local.ToObservableCollection();
            Db.Racecourses.Load<Racecourse>();
            Racecourses = Db.Racecourses.Local.ToObservableCollection();
            Db.Results.Load<Result>();
            Results = Db.Results.Local.ToObservableCollection();
            Db.Owners.Load<Owner>();
            Owners = Db.Owners.Local.ToObservableCollection();
            Error = "";
            Tabs = new ObservableCollection<Tab>();
        }
        public ObservableCollection<Horse> Horses { get; }
        public ObservableCollection<Trainer> Trainers { get; }
        public ObservableCollection<Jockey> Jockeys { get; }
        public ObservableCollection<Race> Races { get; }
        public ObservableCollection<Racecourse> Racecourses { get; }
        public ObservableCollection<Result> Results { get; }
        public ObservableCollection<Owner> Owners { get; }
        public ObservableCollection<Tab> Tabs { get; }
        public List<List<object>> QueryList { get; set; }
        string error;
        public string Error
        {
            get => error;
            set
            {
                this.RaiseAndSetIfChanged(ref error, value);
            }
        }
        public void Save()
        {
            try
            {
                Db.SaveChanges();
                Error = "";
            }
            catch (Exception ex)
            {
                Error += "Не удалось сохранить изменения: " + ex.Message + "\n";
            }
        }
        public void AddRow(int index)
        {
            try
            {
                switch (index)
                {
                    case 0:
                        Db.Horses.Add(new Horse());
                        break;
                    case 1:
                        Db.Trainers.Add(new Trainer());
                        break;
                    case 2:
                        Db.Jockeys.Add(new Jockey());
                        break;
                    case 3:
                        Db.Owners.Add(new Owner());
                        break;
                    case 4:
                        Db.Races.Add(new Race());
                        break;
                    case 5:
                        Db.Results.Add(new Result());
                        break;
                    case 6:
                        Db.Racecourses.Add(new Racecourse());
                        break;
                }
                Error = "";
            }
            catch (Exception ex)
            {
                Error += "Не удалось добавить строку: " + ex.Message + "\n";
            }
        }
        public void DeleteRow(int index)
        {
            
        }
    }
}
