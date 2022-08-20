using ChaoControls.Utils.Sqlite;
using GalaSoft.MvvmLight;
using SuperPost.Entity;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static SuperPost.Entity.EnumClass;

namespace SuperPost.ViewModel
{
    public class MainViewModel : ViewModelBase
    {


        public MainViewModel()
        {
            initDataBase();

            foreach (HttpMethod item in (HttpMethod[])Enum.GetValues(typeof(HttpMethod)))
            {
                HttpMethods.Add(item.ToString());
            }

            for (int i = 0; i < 10; i++)
            {
                Project project = new Project("项目 " + i);
                project.Projects = new List<Project>();
                project.Projects.Add(new Project(i.ToString()));
                Projects.Add(project);
            }

        }


        // 从数据库中读取
        private static void initDataBase()
        {
            if (!File.Exists(DataBase.DATA_PATH))
            {
                Sqlite db = new Sqlite(DataBase.DATA_PATH);
                db.ExecuteSql(DataBase.TABLE_SIDE_HTTP);
                db.ExecuteSql(DataBase.TABLE_SIDE_PROJECT);
                db.Close();
            }


            //插入数据

        }

        private static void insertTestData()
        {

        }






        private ObservableCollection<Project> _Projects = new ObservableCollection<Project>();

        public ObservableCollection<Project> Projects
        {
            get { return _Projects; }
            set
            {
                _Projects = value;
                RaisePropertyChanged();
            }


        }

        private ObservableCollection<string> _HttpMethods = new ObservableCollection<string>();

        public ObservableCollection<string> HttpMethods
        {
            get { return _HttpMethods; }
            set
            {
                _HttpMethods = value;
                RaisePropertyChanged();
            }
        }





    }
}
