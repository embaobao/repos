using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.AspNet.SignalR;
using CTEP.Models;

namespace CTEP
{
    public class ChatHub : Hub
    {
        public void Send(string name, string message)
        {
            // Call the addNewMessageToPage method to update clients.
            Clients.All.addNewMessageToPage(name, message);
        }

        public void AddToRoom(string groupId, string userName)
        {
            try
            {
                //将分组Id放到上下文中
                Groups.Add(Context.ConnectionId, groupId);
                //群发人员进入信息提示
                Clients.Group(groupId, new string[0]).addUserIn(groupId, userName);
            }
            catch (Exception)
            {

               
            }
           
        }
        public void Send(string groupId, string detail, string userName,string uType)
        {
            //Clients.All.addSomeMessage(detail);//群发给所有
            //发给某一个组
            try
            {
                Clients.Group(groupId, new string[0]).addSomeMessage(groupId, detail, userName);
                CTEP.Controllers.BaseController bs = new CTEP.Controllers.BaseController();
                bs.AddData<Community>(new Community { I_From_ID = 0, S_From_Name = userName, S_Body = detail, I_Type = Int32.Parse(uType), I_To_ID = Int32.Parse(groupId), S_To_Name = "", C_STA = 1, TIME_CREATE = DateTime.Now });
            }
            catch (Exception)
            {

                
            }
           
        }

    }
}