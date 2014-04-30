using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Script.Serialization;
using System.Windows.Input;
using CVGuru.Domain;
using Telerik.Windows.Controls;

namespace CVGuru.ViewModel
{
    public class MainViewModel : ViewModelBase
    {
        private string fullName;
        private string searchText;
        private TimeLineViewModel timeLineViewModel;
        private List<User> employees;
        private User selectedEmployee;

        public TimeLineViewModel TimeLineViewModel
        {
            get { return timeLineViewModel; }
            set
            {
                if (Equals(value, timeLineViewModel)) return;
                timeLineViewModel = value;
                OnPropertyChanged();
            }
        }

        public string FullName
        {
            get { return fullName; }
            set
            {
                if (value == fullName) return;
                fullName = value;
                OnPropertyChanged();
            }
        }

        public List<User> Employees
        {
            get { return employees; }
            set
            {
                if (Equals(value, employees)) return;
                employees = value;
                OnPropertyChanged();
            }
        }

        public User SelectedEmployee
        {
            get { return selectedEmployee; }
            set
            {
                if (Equals(value, selectedEmployee)) return;
                selectedEmployee = value;
                OnPropertyChanged();
            }
        }

        public MainViewModel()
        {
            Employees = GetUsers().OrderBy(u => u.Name).ToList();
            PropertyChanged += MainViewModel_PropertyChanged;
        }

        async void MainViewModel_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            if (e.PropertyName == "SelectedEmployee" && SelectedEmployee != null)
            {
                IsBusy = true;
                var cvStream = await Task.Run(() => CVService.GetCV(SelectedEmployee));
                LoadCV(cvStream);
                IsBusy = false;
            }
        }

        private void LoadCV(string cvStream)
        {
            try
            {
                var obj = GetProjects(cvStream);
                var projects = obj["project_experiences"] as dynamic[];

                var segments = new List<TimeLineItem>();

                foreach (var project in projects)
                {
                    var startYear = StringToInt(project["year_from"]);
                    var startMonth = StringToInt(project["month_from"]);
                    var endYear = StringToInt(project["year_to"]);
                    var endMonth = StringToInt(project["month_to"]);
                    var label = project["customer"] + " - " + project["description"];
                    var url = "http://artsyeditor.com/wp-content/uploads/2011/03/hello-world.png"; //todo : google search


                    DateTime endTime = endYear > 1 ? new DateTime(endYear, endMonth, 28) : DateTime.Now;
                    DateTime startTime = startYear > 1 ? new DateTime(startYear, startMonth, 1) : DateTime.Now;
                    if (endTime < startTime)
                        endTime = DateTime.Now;
                    segments.Add(new TimeLineItem(startTime, endTime, label, url, new List<ExtendedInfoItem>()));
                }


                TimeLineViewModel = new TimeLineViewModel()
                {
                    Segments = segments
                };
            }
            catch
            {
                
            }
        }

        private int StringToInt(string s)
        {
            return string.IsNullOrEmpty(s) ? 01 : int.Parse(s);
        }

        private Dictionary<string, object> GetProjects(string CVstring)
        {
            var jsonSerializer = new JavaScriptSerializer();
            var objectList = (Dictionary<string, object>)jsonSerializer.DeserializeObject(CVstring);
            return objectList;
        }

        private async void LoadData()
        {
            //var json = IOService.Load("C:\\input.txt");
            //await Task.Delay(1000);
            var json = await Task.Run(() => CVService.GetDataFile("https://raw.githubusercontent.com/mrsteffenjo/cvpartner-timeline-backend/master/cvpartner-timeline-backend/src/test/resources/timeline.json"));
            LoadJSONTimeLineData(json);
        }


        private void LoadJSONTimeLineData(string streamData)
        {
            var jsonSerializer = new JavaScriptSerializer();
            var objectList = (Dictionary<string,object>)jsonSerializer.DeserializeObject(streamData);
            FullName = objectList["userId"] as string;
            var objects = (objectList["timeline"] as dynamic[]).ToList();

            var segments = new List<TimeLineItem>();
            foreach (var o in objects)
            {
                var dynamicO = o as Dictionary<string,object>;
                var properties = dynamicO["data"] as Dictionary<string,object>;
                var propertyList = new List<ExtendedInfoItem>();
                foreach (var property in properties)
                {
                    propertyList.Add(new ExtendedInfoItem
                        {
                            Label = property.Key,
                            Value = property.Value as string
                        });
                }
                DateTime startTime;
                DateTime.TryParse(o["start"], out startTime);
                DateTime endTime = DateTime.Now;
                DateTime.TryParse(o["end"], out endTime);
                if (endTime < startTime)
                    endTime = DateTime.Now;
                segments.Add(new TimeLineItem(startTime, endTime, o["label"], o["image"], propertyList));
            }
            
            TimeLineViewModel = new TimeLineViewModel()
                {
                    Segments = segments
                };
        }

        private object[] GetJSONObjects(string s)
        {
            var jsonSerializer = new JavaScriptSerializer();
            var objectList = (object[])jsonSerializer.DeserializeObject(s);
            return objectList;
        }


        public ICommand SearchCommand
        {
            get { return new DelegateCommand(delegate { Search(); }); }
        }

        public string SearchText
        {
            get { return searchText; }
            set
            {
                if (value == searchText) return;
                searchText = value;
                OnPropertyChanged();
            }
        }

        public async void Search()
        {
            IsBusy = true;
            LoadData();
            IsBusy = false;
        }



           //var users = GetUsers();
            //foreach (var user in users)
            //{
            //    var cv = GetJSONObjects(CVService.GetCVs(user));
            //    user.ApplyCV(cv);
            //}
        //}

        //private void ButtonBase_OnClick(object sender, RoutedEventArgs e)
        //{
        //    var users = CVService.GetUsersStream();
        //    IOService.Save(users, "users.txt");
        //}



        private List<User> GetUsers()
        {
            //var users = CVService.GetUsersStream();
            //IOService.Save(users, "users.txt");
            var users = IOService.Load("users.txt");

            var userObjects = GetJSONObjects(users);
            var usersProcessed = userObjects.Select(o => new User(o as dynamic)).ToList();
            return usersProcessed;
        }

        public void LoadDummyData()
        {
            var properties = new List<ExtendedInfoItem>
                {
                    new ExtendedInfoItem() {Label = "Kjønn", Value = "Ukjent"},
                    new ExtendedInfoItem() {Label = "te", Value = "123"},
                    new ExtendedInfoItem() {Label = "lang ta ss", Value = "sss sdsnt"}
                };
            TimeLineViewModel = new TimeLineViewModel
                {
                    Segments = new List<TimeLineItem>
                            {
                                new TimeLineItem(DateTime.Now.AddDays(-7), DateTime.Now.AddDays(-2), "Ragnar",
                                    "http://img5.custompublish.com/getfile.php/1301709.1495.ywuadqrdcy/Ragnar_Stoelsmark_640pix.jpg", properties),
                                new TimeLineItem(DateTime.Now.AddMonths(-1), DateTime.Now.AddMonths(0), "Liv",
                                    "http://www.webstep.no/wp-content/uploads/2012/03/Liv_Thorsen_Webstep_Stavanger-110x150.jpg", properties),
                                new TimeLineItem(DateTime.Now.AddMonths(-1).AddDays(-15), DateTime.Now.AddDays(2), "Trond",
                                    "http://www.webstep.no/wp-content/uploads/2010/02/Trond_Iden_Webstep_i_Stavanger-110x150.jpg", properties),
                            }
                };
        }
    }
}
