using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuperPost.Entity
{
    public class Project
    {
        public int ID { get; set; }

        public int ParentID { get; set; }
        public string Name { get; set; }
        public List<HttpObject> Contents { get; set; }
        public List<Project> Projects { get; set; }

        public Project()
        {
            Contents = new List<HttpObject>();
            Contents.Add(new HttpObject("上传图片", EnumClass.HttpMethod.POST));
            Contents.Add(new HttpObject("下载图片", EnumClass.HttpMethod.GET));
            Contents.Add(new HttpObject("更新图片", EnumClass.HttpMethod.PUT));
            Contents.Add(new HttpObject("删除图片", EnumClass.HttpMethod.DELETE));
        }

        public Project(string name) : this()
        {
            this.Name = name;
        }

    }
}
