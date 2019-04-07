using Newtonsoft.Json;
using System.Web.Script.Serialization;

namespace CTEP.Models
{

    public class MsItem 
    {
        public int StatusNum { get; set; }

        public string title { get; set; }

        public object body { get; set; }

        public int lenth { get; set; }

        public MsItem()
        {
        }
        /// <summary>
        /// 创建MsItem("jsonString",body)
        /// </summary>
        /// <param name="val">MsItem Body</param>
        public MsItem(object val) {
            StatusNum = 0;
            title = "";
            lenth = 1;
            body = JsonConvert.SerializeObject(val);
        }
        /// <summary>
        /// 创建MsItem (title,body)
        /// </summary>
        /// <param name="title">MsItem title</param>
        /// <param name="val">MsItem Body</param>
        public MsItem(string title,object val)
        {
            StatusNum = 0;
           
            lenth = 1;
            this.title = title;
            body = JsonConvert.SerializeObject(val);
        }
        /// <summary>
        /// 创建MsItem (StatusNum，title,body)
        /// </summary>
        /// <param name="status">MsItem StatusNum</param>
        /// <param name="title">MsItem title </param>
        /// <param name="val">MsItem body</param>
        public MsItem(int status,string title, object val)
        {
            this.StatusNum = status;
            this.title = title;
            lenth = 1;
            this.body = JsonConvert.SerializeObject(val);
        }
        /// <summary>
        /// 创建MsItem (StatusNum，title,body，lenth)
        /// </summary>
        /// <param name="status">MsItem StatusNum</param>
        /// <param name="title">MsItem title</param>
        /// <param name="val">MsItem body</param>
        /// <param name="l">MsItem lenth</param>
        public MsItem(int status, string title, object val,int l)
        {
            this.StatusNum = status;
            this.title = title;
            this.lenth = l;
            this.body = JsonConvert.SerializeObject(val);
        }
        public object Json(object data)
        {
            JavaScriptSerializer javaScriptSerializer = null;
            if (data != null)
            {
                javaScriptSerializer = new JavaScriptSerializer();
                return javaScriptSerializer.Serialize(data);
            }
            else
            {
                return string.Empty;
            }
        }

        public object GetJson() {
            return JsonConvert.SerializeObject(this);
        }

        public void SetS(int num) {
            this.StatusNum = num;
        }
        public void SetT(string t)
        {
            this.title = t;
        }
        public void SetB(object b)
        {
            this.body = b;
        }
        public void SetLenth(int l)
        {
            this.lenth = l;
        }

    }

    public class MsNonJson : MsItem
    {
       
        public MsNonJson(object body) {
            this.StatusNum = 0;
            this.title = "";
            this.body = body;
        }
        public MsNonJson(string t,object body)
        {
            this.StatusNum = 0;
            this.title = t;
            this.body = body;
        }
        public MsNonJson(int s,string t, object body)
        {
            this.StatusNum = s;
            this.title = t;
            this.body = body;
        }
    }
    public class MsJson : MsItem
    {
        public MsJson(object body)
        {
            this.StatusNum = 0;
            this.title = "";
            this.body = Json(body);
        }
        public MsJson(string t, object body)
        {
            this.StatusNum = 0;
            this.title = t;
            this.body = Json(body); 
        }
        public MsJson(int s, string t, object body)
        {
            this.StatusNum = s;
            this.title = t;
            this.body = Json(body);
        }

       
    }
}