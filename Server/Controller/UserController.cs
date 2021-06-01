using Common;
using Server.DAO;
using Server.Model;
using Server.Servers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Server.Controller
{
    class UserController : BaseController 
    {
        UserDAO userDao = new UserDAO();
        ResultDAO resultDAO = new ResultDAO();

        public string Login(string data, Client client, NetworkServer server)
        {
            string[] strs = data.Split(',');
            User user = userDao.VerifyUser(client.MysqlConn,strs[0], strs[1]);
            if(user == null)
            {
                return ((int)ReturnCode.Fail).ToString();
            }
            else
            {
                Result res = resultDAO.GetResultByUserId(client.MysqlConn,user.Id);
                client.SetUserData(user,res);
                return string.Format("{0},{1},{2},{3}", ((int)ReturnCode.Success).ToString(), user.Username, res.TotalCount, res.WinCount);
            }
        }
    }
}
