using Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Server.Controller
{
    class BaseController
    {
        protected RequestCode requestCode = RequestCode.None;
        public RequestCode Requestcode { get { return requestCode; } }
    }
}
