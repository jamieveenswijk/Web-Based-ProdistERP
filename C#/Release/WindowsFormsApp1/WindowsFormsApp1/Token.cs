using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsFormsApp1
{    public class Token
    {
        public string access_token { get; set; }
        public string token_type { get; set; }
        public string expires_in { get; set; }
        public string UserName { get; set; }
    }

    //     "access_token": "<TOKEN>,   
    //    "token_type": "bearer", 
    //"expires_in": 1209599,     
    //"userName": "<USERNAME>",
    //".issued": "Fri, 19 Oct 2018 08:39:08 GMT",     
    //".expires": "Fri, 02 Nov 2018 08:39:08 GMT" 

}
