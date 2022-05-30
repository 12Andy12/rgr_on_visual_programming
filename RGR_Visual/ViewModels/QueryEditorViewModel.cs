using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;
using ReactiveUI;
using RGR_Visual.Models;

namespace RGR_Visual.ViewModels
{
    public class QueryEditorViewModel : ViewModelBase
    {
        public ObservableCollection<string> Tables { get; }
        public ObservableCollection<string> ChosenTables { get; }
        public ObservableCollection<string> Rows { get; }
        public ObservableCollection<string> ChosenRows { get; }
        MainWindowViewModel main;
        List<Dictionary<string,object>> KeyValues { get; set; }
        public ObservableCollection<string> Operators { get; }
        public string Condition {
            get => condition;
            set
            {
                this.RaiseAndSetIfChanged(ref condition, value);
            }
        }
        string condition;
        string selectedTable;
        string selectedChosen;
        string selectedRow;
        string selectedRowChosen;
        string selectedOperator;
        string selectedRowForCondition;
        public string SelectedRowForCondition
        {
            get => selectedRowForCondition;
            set
            {
                this.RaiseAndSetIfChanged(ref selectedRowForCondition, value);
            }
        }
        public string SelectedOperator
        {
            get => selectedOperator;
            set
            {
                this.RaiseAndSetIfChanged(ref selectedOperator, value);
            }
        }
        public string SelectedTable
        {
            get => selectedTable;
            set
            {
                this.RaiseAndSetIfChanged(ref selectedTable, value);
            }
        }
        public string SelectedChosen
        {
            get => selectedChosen;
            set
            {
                this.RaiseAndSetIfChanged(ref selectedChosen, value);
            }
        }
        public string SelectedRow
        {
            get => selectedRow;
            set
            {
                this.RaiseAndSetIfChanged(ref selectedRow, value);
            }
        }
        public string SelectedRowChosen
        {
            get => selectedRowChosen;
            set
            {
                this.RaiseAndSetIfChanged(ref selectedRowChosen, value);
            }
        }
        public QueryEditorViewModel(MainWindowViewModel main)
        {
            Tables = new ObservableCollection<string>(new List<string> { "Horse", "Trainer", "Owner", "Jockey", "Race", "Result", "Racecourse" });
            Rows = new ObservableCollection<string>();
            ChosenTables = new ObservableCollection<string>();
            ChosenRows = new ObservableCollection<string>();
            this.main = main;
            Operators = new ObservableCollection<string> { "", "<", "<=", ">", ">=", "=", "AVG", "COUNT", "MAX", "MIN" };
        }
        public void Choose()
        {
            string item = SelectedTable;
            Tables.Remove(SelectedTable);
            ChosenTables.Add(item);
            switch (item)
            {
                case "Horse":
                    string[] attrs = Horse.GetAttr();
                    foreach(string attr in attrs)
                    {
                        Rows.Add(attr);
                    }
                    break;
                case "Trainer":
                    Rows.Add(Trainer.GetAttr());
                    break;
                case "Owner":
                    Rows.Add(Owner.GetAttr());
                    break;
                case "Jockey":
                    Rows.Add(Jockey.GetAttr());
                    break;
                case "Race":
                    attrs = Race.GetAttr();
                    foreach (string attr in attrs)
                    {
                        Rows.Add(attr);
                    }
                    break;
                case "Result":
                    attrs = Result.GetAttr();
                    foreach (string attr in attrs)
                    {
                        Rows.Add(attr);
                    }
                    break;
                case "Racecourse":
                    Rows.Add(Racecourse.GetAttr());
                    break;
            }
        }
        public void Delete()
        {
            string item = SelectedChosen;
            ChosenTables.Remove(SelectedChosen);
            Tables.Add(item);
            switch (item)
            {
                case "Horse":
                    string[] attrs = Horse.GetAttr();
                    foreach (string attr in attrs)
                    {
                        if (Rows.Contains(attr)) Rows.Remove(attr);
                        else if (ChosenRows.Contains(attr)) ChosenRows.Remove(attr);
                    }
                    break;
                case "Trainer":
                    if (Rows.Contains(Trainer.GetAttr())) Rows.Remove(Trainer.GetAttr());
                    else if (ChosenRows.Contains(Trainer.GetAttr())) ChosenRows.Remove(Trainer.GetAttr());
                    break;
                case "Owner":
                    if (Rows.Contains(Owner.GetAttr())) Rows.Remove(Owner.GetAttr());
                    else if (ChosenRows.Contains(Owner.GetAttr())) ChosenRows.Remove(Owner.GetAttr());
                    break;
                case "Jockey":
                    if (Rows.Contains(Jockey.GetAttr())) Rows.Remove(Jockey.GetAttr());
                    else if (ChosenRows.Contains(Jockey.GetAttr())) ChosenRows.Remove(Jockey.GetAttr());
                    Rows.Add(Jockey.GetAttr());
                    break;
                case "Race":
                    attrs = Race.GetAttr();
                    foreach (string attr in attrs)
                    {
                        if (Rows.Contains(attr)) Rows.Remove(attr);
                        else if (ChosenRows.Contains(attr)) ChosenRows.Remove(attr);
                    }
                    break;
                case "Result":
                    attrs = Result.GetAttr();
                    foreach (string attr in attrs)
                    {
                        if (Rows.Contains(attr)) Rows.Remove(attr);
                        else if (ChosenRows.Contains(attr)) ChosenRows.Remove(attr);
                    }
                    break;
                case "Racecourse":
                    if (Rows.Contains(Racecourse.GetAttr())) Rows.Remove(Racecourse.GetAttr());
                    else if (ChosenRows.Contains(Racecourse.GetAttr())) ChosenRows.Remove(Racecourse.GetAttr());
                    Rows.Add(Racecourse.GetAttr());
                    break;
            }
        }
        public void ChooseRow()
        {
            string item = SelectedRow;
            Rows.Remove(SelectedRow);
            ChosenRows.Add(item);
        }
        public void DeleteRow()
        {
            string item = SelectedRowChosen;
            ChosenRows.Remove(SelectedRowChosen);
            Rows.Add(item);
        }
        public void ExecuteQuery()
        {
            Select();
        }
        public string ExtractColumn(string str)
        {
            string[] str1 = str.Split(' ');
            string column = str1[1];
            return column;
        }
        public void Select()
        {
            List<List<object>> queryList = new();
            foreach (string str in ChosenRows)
            {
                string[] str1 = str.Split(' ');
                string table = str1[0];
                string column = str1[1];
                table = table.Remove(table.Length - 1);
                List<object> values = new List<object>();
                switch (table)
                {
                    case "Horse":
                        if (str == SelectedRowForCondition)
                        {
                            switch (SelectedOperator)
                            {
                                case "<":
                                    foreach (var horse in main.Horses.Where(item => double.Parse(item[ExtractColumn(SelectedRowForCondition)].ToString()) < double.Parse(Condition)))
                                    {
                                        values.Add(horse[column]);
                                    }
                                    break;
                                case ">":
                                    foreach (var horse in main.Horses.Where(item => double.Parse(item[ExtractColumn(SelectedRowForCondition)].ToString()) > double.Parse(Condition)))
                                    {
                                        values.Add(horse[column]);
                                    }
                                    break;
                                case "<=":
                                    foreach (var horse in main.Horses.Where(item => double.Parse(item[ExtractColumn(SelectedRowForCondition)].ToString()) <= double.Parse(Condition)))
                                    {
                                        values.Add(horse[column]);
                                    }
                                    break;
                                case ">=":
                                    foreach (var horse in main.Horses.Where(item => double.Parse(item[ExtractColumn(SelectedRowForCondition)].ToString()) >= double.Parse(Condition)))
                                    {
                                        values.Add(horse[column]);
                                    }
                                    break;
                                case "=":
                                    foreach (var horse in main.Horses.Where(item => item[ExtractColumn(SelectedRowForCondition)].Equals(Condition)))
                                    {
                                        values.Add(horse[column]);
                                    }
                                    break;
                                default:
                                    foreach (var horse in main.Horses)
                                    {
                                        values.Add(horse[column]);
                                    }
                                    break;
                            }
                        }
                        else
                        {
                                foreach (var horse in main.Horses)
                                {
                                    values.Add(horse[column]);
                                }
                        }
                        break;
                    case "Trainer":
                        if (str == SelectedRowForCondition)
                        {
                            switch (SelectedOperator)
                            {
                                case "":
                                    foreach (var trainer in main.Trainers)
                                    {
                                        values.Add(trainer[column]);
                                    }
                                    break;
                                case "=":
                                    foreach (var trainer in main.Trainers.Where(item => item[ExtractColumn(SelectedRowForCondition)].Equals(Condition)))
                                    {
                                        values.Add(trainer[column]);
                                    }
                                    break;
                            }
                        }
                        else
                        {
                            foreach (var trainer in main.Trainers)
                            {
                                values.Add(trainer[column]);
                            }
                        }
                        break;
                    case "Owner":
                        if (str == SelectedRowForCondition)
                        {
                            switch (SelectedOperator)
                            {
                                case "":
                                    foreach (var owner in main.Owners)
                                    {
                                        values.Add(owner[column]);
                                    }
                                    break;
                                case "=":
                                    foreach (var owner in main.Owners.Where(item => item[ExtractColumn(SelectedRowForCondition)].Equals(Condition)))
                                    {
                                        values.Add(owner[column]);
                                    }
                                    break;
                            }
                        }
                        else
                        {
                            foreach (var owner in main.Owners)
                            {
                                values.Add(owner[column]);
                            }
                        }
                        break;
                    case "Jockey":
                        if (str == SelectedRowForCondition)
                        {
                            switch (SelectedOperator)
                            {
                                case "":
                                    foreach (var jockey in main.Jockeys)
                                    {
                                        values.Add(jockey[column]);
                                    }
                                    break;
                                case "=":
                                    foreach (var jockey in main.Jockeys.Where(item => item[ExtractColumn(SelectedRowForCondition)].Equals(Condition)))
                                    {
                                        values.Add(jockey[column]);
                                    }
                                    break;
                            }
                        }
                        else
                        {
                            foreach (var jockey in main.Jockeys)
                            {
                                values.Add(jockey[column]);
                            }
                        }
                        break;
                    case "Race":
                        if (str == SelectedRowForCondition)
                        {
                            switch (SelectedOperator)
                            {
                                case "":
                                    foreach (var race in main.Races)
                                    {
                                        values.Add(race[column]);
                                    }
                                    break;
                                case "<":
                                    foreach (var race in main.Races.Where(item => double.Parse(item[ExtractColumn(SelectedRowForCondition)].ToString()) < double.Parse(Condition)))
                                    {
                                        values.Add(race[column]);
                                    }
                                    break;
                                case ">":
                                    foreach (var race in main.Races.Where(item => double.Parse(item[ExtractColumn(SelectedRowForCondition)].ToString()) > double.Parse(Condition)))
                                    {
                                        values.Add(race[column]);
                                    }
                                    break;
                                case "<=":
                                    foreach (var race in main.Races.Where(item => double.Parse(item[ExtractColumn(SelectedRowForCondition)].ToString()) <= double.Parse(Condition)))
                                    {
                                        values.Add(race[column]);
                                    }
                                    break;
                                case ">=":
                                    foreach (var race in main.Races.Where(item => double.Parse(item[ExtractColumn(SelectedRowForCondition)].ToString()) >= double.Parse(Condition)))
                                    {
                                        values.Add(race[column]);
                                    }
                                    break;
                                case "=":
                                    foreach (var race in main.Races.Where(item => item[ExtractColumn(SelectedRowForCondition)].Equals(Condition)))
                                    {
                                        values.Add(race[column]);
                                    }
                                    break;
                            }
                        }
                        else
                        {
                            foreach (var race in main.Races)
                            {
                                values.Add(race[column]);
                            }
                        }
                        break;
                    case "Result":
                        if (str == SelectedRowForCondition)
                        {
                            switch (SelectedOperator)
                            {
                                case "":
                                    foreach (var result in main.Results)
                                    {
                                        values.Add(result[column]);
                                    }
                                    break;
                                case "<":
                                    foreach (var result in main.Results.Where(item => double.Parse(item[ExtractColumn(SelectedRowForCondition)].ToString()) < double.Parse(Condition)))
                                    {
                                        values.Add(result[column]);
                                    }
                                    break;
                                case ">":
                                    foreach (var result in main.Results.Where(item => double.Parse(item[ExtractColumn(SelectedRowForCondition)].ToString()) > double.Parse(Condition)))
                                    {
                                        values.Add(result[column]);
                                    }
                                    break;
                                case "<=":
                                    foreach (var result in main.Results.Where(item => double.Parse(item[ExtractColumn(SelectedRowForCondition)].ToString()) <= double.Parse(Condition)))
                                    {
                                        values.Add(result[column]);
                                    }
                                    break;
                                case ">=":
                                    foreach (var result in main.Results.Where(item => double.Parse(item[ExtractColumn(SelectedRowForCondition)].ToString()) >= double.Parse(Condition)))
                                    {
                                        values.Add(result[column]);
                                    }
                                    break;
                                case "=":
                                    foreach (var result in main.Results.Where(item => item[ExtractColumn(SelectedRowForCondition)].Equals(Condition)))
                                    {
                                        values.Add(result[column]);
                                    }
                                    break;
                            }
                        }
                        else
                        {
                            foreach (var result in main.Results)
                            {
                                values.Add(result[column]);
                            }
                        }
                        break;
                    case "Racecourse":
                        if (str == SelectedRowForCondition)
                        {
                            switch (SelectedOperator)
                            {
                                case "":
                                    foreach (var rcs in main.Racecourses)
                                    {
                                        values.Add(rcs[column]);
                                    }
                                    break;
                                case "=":
                                    foreach (var rcs in main.Racecourses.Where(item => item[ExtractColumn(SelectedRowForCondition)].Equals(Condition)))
                                    {
                                        values.Add(rcs[column]);
                                    }
                                    break;
                            }
                        }
                        else
                        {
                            foreach (var rcs in main.Racecourses)
                            {
                                values.Add(rcs[column]);
                            }
                        }
                        break;
                }
                queryList.Add(values);
            }
            main.Tabs.Add(new Tab { Header = "test", Content = queryList });
        }
        public void GroupBy()
        {
            //0-значение, 1-count, 2-max, 3-min, 4-sum
            List<List<object>> resultGroup = new();

        }
    }
}
